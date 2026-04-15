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

        // Explicit with AutoMapper and ProjectTo
        public IEnumerable<BlogTitleView> Get()
        {
            return GetViews();
        }

        // No dependency on ViewModel or entities
        public IEnumerable<TView> Get<TView>()
        {
            if (typeof(TView) == typeof(BlogTitleView))
            {
                return _repository.AsNoTracking()
                    .Select(x => (TView)(object)new BlogTitleView
                    {
                        Title = x.Title
                    }).ToArray();
            }

            throw new NotSupportedException($"Mapping for view type {typeof(TView).Name} is not supported.");
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
