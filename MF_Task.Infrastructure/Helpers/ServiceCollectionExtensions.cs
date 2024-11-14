using MediatR;
using MF_Task.Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

public static class ServiceCollectionExtensions
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

    public static void AddMediatRHandlers(this IServiceCollection services, Assembly assembly)
    {
        var handlerTypes = assembly.GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract && t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRequestHandler<,>)))
            .ToList();

        foreach (var handler in handlerTypes)
        {
            var interfaces = handler.GetInterfaces()
                .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRequestHandler<,>))
                .ToList();

            foreach (var iface in interfaces)
            {
                services.AddScoped(iface, handler);  // Register with scoped lifetime
            }
        }
    }
}
