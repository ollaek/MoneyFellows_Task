using MediatR;
using MF_Task.Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace MF_Task.Infrastructure
{
    public static class RepositoryServiceCollectionExtension
    {
        public static void AddRepositories(this IServiceCollection services, Assembly assembly)
        {
            // Get all the types in the provided assembly
            var repositoryTypes = assembly.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IBaseRepository<>)))
                .ToList();

            foreach (var repo in repositoryTypes)
            {
                // Find the interfaces that match IBaseRepository<T>
                var interfaces = repo.GetInterfaces()
                    .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IBaseRepository<>))
                    .ToList();

                foreach (var iface in interfaces)
                {
                    services.AddScoped(iface, repo);  // Register with scoped lifetime
                }
            }
        }
    }
}