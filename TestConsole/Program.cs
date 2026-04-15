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
            var blogService = new BlogService(context);

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

            // old school mapping with generic projection
            var toBeMappedEntityResult = blogService.GetEntities();
            var oldSchoolMappedResult = toBeMappedEntityResult
                .Select(x => new BlogTitleView
                {
                    Title = x.Title
                }).ToArray();

            LogBlogs("Old school mapping with generic projection", oldSchoolMappedResult);

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
