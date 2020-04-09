using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MTech.DefaultMapping.ViewModel
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
        }

        private void ApplyMappingsFromAssembly(Assembly assembly)
        {
            var types = assembly.GetExportedTypes()
                .Where(t => t.GetInterfaces().Any(i => i.IsGenericType
                && (i.GetGenericTypeDefinition() == typeof(IMappingOf<>)
                    || i.GetGenericTypeDefinition() == typeof(IReverseMappingOf<>))))
                .ToList();

            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);

                var methodInfo = type.GetInterface(typeof(IMappingOf<>).Name).GetMethod("Mapping");
                methodInfo?.Invoke(instance, new object[] { this });

                var reverseMapping = type.GetInterface(typeof(IReverseMappingOf<>).Name).GetMethod("Mapping");
                reverseMapping?.Invoke(instance, new object[] { this });
            }
        }
    }
}
