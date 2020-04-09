using AutoMapper;
using DataModel.Implementation;
using MTech.DefaultMapping.Entities;
using MTech.DefaultMapping.Services;
using MTech.DefaultMapping.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MTech.DefaultMapping.TestConsole
{
    class Program
    {
        static void Main()
        {
            using var context = new BlogContext();

            var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            var mapper = config.CreateMapper();

            var blogService = new BlogService(context, mapper);

            // Setup some test data
            context.Blogs.AddRange(new Blog[]
            {
                new Blog { Title = "Blog 1" },
                new Blog { Title = "DefaultMapping" },
                new Blog { Title = "Dotnet 5" },
                new Blog { Title = "Blazor" }
            });

            context.SaveChanges();

            // Old school mapping
            var entityResult = blogService.GetEntities().Select(x => new BlogTitleView
            {
                Title = x.Title
            });

            LogBlogs("Old school mapping", entityResult);

            // Old school mapping with view
            var entityViewResult = blogService.GetViews();

            LogBlogs("Old school mapping with view", entityViewResult);

            // old school mapping with automapper
            var toBeMappedEntityResult = blogService.GetEntities();
            var oldSchoolMappedResult = mapper.Map<BlogTitleView[]>(toBeMappedEntityResult);

            LogBlogs("Old school mapping with automapper", oldSchoolMappedResult);

            // New style Generic
            var genericMappedResult = blogService.Get<BlogTitleView>();
            LogBlogs("New style mapping, generic", genericMappedResult);

            // New style explicit
            var explicitMappedResult = blogService.Get();
            LogBlogs("New style mapping, explicit", explicitMappedResult);
            
            // With select expression
            var selectExpressionResult = blogService.Get(x => new BlogTitleView
            {
                Title = x.Title
            });
            LogBlogs("Mapping with select expression", selectExpressionResult);
        }

        private static void LogBlogs(string mappingExample, IEnumerable<BlogTitleView> entityResult)
        {
            Console.WriteLine(mappingExample);

            foreach (var entity in entityResult)
            {
                Console.WriteLine($"Blog found with title: {entity.Title}");
            }

            Console.WriteLine();
        }
    }
}
