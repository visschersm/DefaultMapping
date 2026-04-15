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

        // Generic entrypoint requires explicit projection
        public IEnumerable<TView> Get<TView>()
        {
            throw new NotSupportedException($"Call {nameof(Get)} with a select expression for {typeof(TView).Name}.");
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
