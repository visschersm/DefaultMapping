using System;

namespace MTech.DefaultMapping.TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var blogService = new BlogService();

            var entityResult = blogService.Get();
            
            var defaultMappingResult = blogService.Get<BlogTitleView>();

            var selectExpressionResult = blogService.Get(x => new 
            {
                Title = x.Title
            });
        }
    }
}
