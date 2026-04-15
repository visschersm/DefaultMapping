using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;
using MTech.DefaultMapping.Entities;
using MTech.DefaultMapping.ViewModel;
using MTech.DefaultMapping.DataModel;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;

namespace MTech.DefaultMapping.Services
{
    public class BlogService
    {
        private readonly IBlogContext _context;
        private readonly DbSet<Blog> _repository;
        private readonly IMapper _mapper;

        public BlogService(IBlogContext context, IMapper mapper)
        {
            _context = context;
            _repository = _context.Set<Blog>();
            _mapper = mapper;
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
            return _repository.AsNoTracking()
                .ProjectTo<BlogTitleView>(_mapper.ConfigurationProvider)
                .ToArray();
        }

        // No dependency on ViewModel or entities, Only on AutoMapper
        public IEnumerable<TView> Get<TView>()
        {
            return _repository.AsNoTracking()
                .ProjectTo<TView>(_mapper.ConfigurationProvider)
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