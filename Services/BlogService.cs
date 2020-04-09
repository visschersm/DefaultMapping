using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;
using MTech.DefaultMapping.Entities;
using MTech.DefaultMapping.ViewModel;
using MTech.DefaultMapping.DataModel;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using AutoMapper.Extensions;

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

        public IEnumerable<BlogTitleView> GetViews()
        {
            return _repository.AsNoTracking()
                .Select(x => new BlogTitleView
                {
                    
                }).ToArray();
        }

        // With AutoMapper and ProjectTo
        public IEnumerable<BlogTitleView> Get()
        {
            return _repository.AsNoTracking()
                .ProjectTo<BlogTitleView>(_mapper.ConfigurationProvider)
                .ToArray();
        }

        public IEnumerable<TView> Get<TView>()
        {
            return _repository.AsNoTracking()
                .ProjectTo<TView>(_mapper.ConfigurationProvider);
        }

        // Dependency on Entities
        public IEnumerable<TView> Get<TView>(Expression<Func<Blog, TView>> selectExpression)
        {
            return _repository.AsNoTracking()
                .Select(selectExpression).ToArray();
        }
    }
}