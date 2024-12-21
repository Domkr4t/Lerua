using AutoMapper;
using Lerua.Application.Products.Queries.GetProductsList;
using Lerua.Domain;
using System.Reflection;

namespace Lerua.Application.Common.Mappings
{
    public class AssemblyMapping : Profile
    {
        public AssemblyMapping() : this(typeof(AssemblyMapping).Assembly)
        {
        }

        public AssemblyMapping(Assembly assembly)
        {
            ApplyMappingsFromAssembly(assembly);
        }

        private void ApplyMappingsFromAssembly(Assembly assembly)
        {
            var types = assembly.GetExportedTypes()
                .Where(t => t.GetInterfaces()
                    .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapWith<>)))
                .ToList();

            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);
                var methodInfo = type.GetMethod("Mapping");
                methodInfo?.Invoke(instance, new object[] { this });
            }

            CreateMap<Product, ProductLookupDto>();
        }
    }
}
