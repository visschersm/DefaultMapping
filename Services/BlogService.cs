using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;
using MTech.DefaultMapping.Entities;
using MTech.DefaultMapping.ViewModel;
using MTech.DefaultMapping.DataModel;
using Microsoft.EntityFrameworkCore;

namespace MTech.DefaultMapping.Services
{
    public class BlogService
    {
        private readonly IBlogContext _context;
        private readonly DbSet<Blog> _repository;

        public BlogService(IBlogContext context)
        {
            _context = context;
            _repository = _context.Set<Blog>();
        }

        // For this we have a dependency on Entities
        public IEnumerable<Blog> GetEntities()
        {
            return _repository.AsNoTracking()
                .ToArray();
        }

        // Oldschool explicit View
        public IEnumerable<BlogTitleView> GetViews()
        {
            return _repository.AsNoTracking()
                .Select(x => new BlogTitleView
                {
                    Title = x.Title
                }).ToArray();
        }

        // Explicit typed projection
        public IEnumerable<BlogTitleView> Get()
        {
            return GetViews();
        }

        // Generic entrypoint maps properties with matching names/types
        public IEnumerable<TView> Get<TView>()
        {
            var sourceProperties = typeof(Blog).GetProperties()
                .Where(x => x.CanRead)
                .ToDictionary(x => x.Name, x => x);

            var destinationProperties = typeof(TView).GetProperties()
                .Where(x => x.CanWrite)
                .ToArray();

            return _repository.AsNoTracking()
                .AsEnumerable()
                .Select(x =>
                {
                    var view = Activator.CreateInstance<TView>();

                    foreach (var destinationProperty in destinationProperties)
                    {
                        if (!sourceProperties.TryGetValue(destinationProperty.Name, out var sourceProperty))
                        {
                            continue;
                        }

                        if (destinationProperty.PropertyType != sourceProperty.PropertyType)
                        {
                            continue;
                        }

                        destinationProperty.SetValue(view, sourceProperty.GetValue(x));
                    }

                    return view;
                })
                .ToArray();
        }

        // No dependency on ViewModel, only on Entities
        public IEnumerable<TView> Get<TView>(Expression<Func<Blog, TView>> selectExpression)
        {
            return _repository.AsNoTracking()
                .Select(selectExpression)
                .ToArray();
        }
    }
}
