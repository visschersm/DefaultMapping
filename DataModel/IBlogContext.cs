using Microsoft.EntityFrameworkCore;
using MTech.DefaultMapping.Entities;

namespace MTech.DefaultMapping.DataModel
{
    public interface IBlogContext
    {
        DbSet<Blog> Blogs { get; set; }
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
    }
}