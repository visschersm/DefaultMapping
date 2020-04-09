using AutoMapper;

namespace MTech.DefaultMapping.ViewModel
{
    public interface IMappingOf<TEntity> where TEntity : class
    {
        void Mapping(Profile profile)
        {
            profile.CreateMap(typeof(TEntity), GetType());
        }
    }
}