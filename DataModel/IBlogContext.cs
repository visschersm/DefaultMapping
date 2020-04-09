using Microsoft.EntityFrameworkCore;

namespace MTech.DefaultMapping.DataModel
{
    public interface IBlogContext
    {
         DbSet<TEntity> Set<TEntity>() where TEntity : class;
    }
}