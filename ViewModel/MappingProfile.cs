using AutoMapper;
using System;
using System.Linq;
using System.Reflection;

namespace MTech.DefaultMapping.ViewModel
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            ApplyMappingsFromAssembly(AppDomain.CurrentDomain.GetAssemblies().Where(x => !x.IsDynamic).ToArray());
        }

        private void ApplyMappingsFromAssembly(Assembly[] assemblies)
        {
            foreach (var assembly in assemblies)
            {
                var types = assembly.GetExportedTypes()
                    .Where(t => t.GetInterfaces().Any(i => i.IsGenericType
                    && (i.GetGenericTypeDefinition() == typeof(IMappingOf<>)
                        || i.GetGenericTypeDefinition() == typeof(IReverseMappingOf<>))))
                    .ToList();

                foreach (var type in types)
                {
                    var instance = Activator.CreateInstance(type);

                    var methodInfo = type.GetInterface(typeof(IMappingOf<>).Name)?.GetMethod("Mapping");
                    methodInfo?.Invoke(instance, new object[] { this });

                    var reverseMapping = type.GetInterface(typeof(IReverseMappingOf<>).Name)?.GetMethod("Mapping");
                    reverseMapping?.Invoke(instance, new object[] { this });
                }
            }
        }
    }
}
