using MTech.DefaultMapping.Entities;

namespace MTech.DefaultMapping.ViewModel
{
    public class BlogTitleView : IMappingOf<Blog>, IReverseMappingOf<Blog>
    {
        public string Title { get; set; }
    }
}