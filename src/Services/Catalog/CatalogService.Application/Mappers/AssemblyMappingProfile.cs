using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.Mappers
{
    public class AssemblyMappingProfile : Profile
    {
        public AssemblyMappingProfile(Assembly assembly)
            => ApplyMappingsFromAssembly(assembly);

       /* public AssemblyMappingProfile()
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                try
                {
                    ApplyMappingsFromAssembly(assembly);
                }
                catch 
                { }
            }
        }*/

        private void ApplyMappingsFromAssembly(Assembly assembly)
        {
            var types = assembly.GetExportedTypes()
                .Where(type => type.GetInterfaces()
                .Any(i => i.IsGenericType &&
                i.GetGenericTypeDefinition() == typeof(IMapWith<>)))
                .ToList();

            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);
                var methodInfo = type.GetMethod("Mapping")
                    ?? type.GetInterface(typeof(IMapWith<>).Name)!.GetMethod("Mapping");
                methodInfo?.Invoke(instance, new object[] { this });
            }
        }

    }
}
