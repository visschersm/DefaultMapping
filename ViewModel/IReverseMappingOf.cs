using AutoMapper;

namespace MTech.DefaultMapping.ViewModel
{
    public interface IReverseMappingOf<TEntity> where TEntity : class
    {
        void Mapping(Profile profile)
        {
            profile.CreateMap(typeof(TEntity), GetType()).ReverseMap();
        }
    }
}