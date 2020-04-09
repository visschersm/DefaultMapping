using Microsoft.EntityFrameworkCore;
using MTech.DefaultMapping.DataModel;
using MTech.DefaultMapping.Entities;

namespace DataModel.Implementation
{
    public class BlogContext : DbContext, IBlogContext
    {
        public BlogContext()
            : base()
        {

        }

        public BlogContext(DbContextOptions options)
            : base(options)
        {

        }

        public DbSet<Blog> Blogs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("BlogContext");

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
