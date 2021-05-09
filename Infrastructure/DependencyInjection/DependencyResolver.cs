using System;

namespace Infrastructure.DependencyInjection
{
    public class DependencyResolver : IDependencyResolver
    {
        private readonly IServiceProvider _serviceProvider;

        public DependencyResolver(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public T Resolve<T>()
        {
            return (T) _serviceProvider.GetService(typeof(T));
        }
    }
}