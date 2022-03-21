using HR.Common.Utilities;

using Microsoft.Extensions.DependencyInjection;

using System;

namespace HR.Common.Persistence.InMemory
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddUnitOfWork(this IServiceCollection services, Type customRepositoryFactoryType = null, ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
        {
            services.Add(new ServiceDescriptor(typeof(IRepositoryFactory), EnsureRepositoryFactoryType() ?? typeof(RepositoryFactory), serviceLifetime));
            services.Add(new ServiceDescriptor(typeof(IUnitOfWork), typeof(UnitOfWork), serviceLifetime));

            return services;

            Type EnsureRepositoryFactoryType()
            {
                if (customRepositoryFactoryType != null && !(typeof(IRepositoryFactory).IsAssignableFrom(customRepositoryFactoryType) && customRepositoryFactoryType.IsConcrete()))
                {
                    var message = $"Parameter '{nameof(customRepositoryFactoryType)}' must be a concrete type that implements the {nameof(IRepositoryFactory)} interface.";
                    throw new ArgumentException(message, paramName: nameof(customRepositoryFactoryType));
                }
                return customRepositoryFactoryType;
            }
        }
    }
}
