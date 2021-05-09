using System;
using System.Reflection;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Configuration
{
    public static class ConfigurationFactory
    {
        public static T GetConfiguration<T>(this IConfiguration configuration)
        { 
            var instance = Activator.CreateInstance<T>();
            var configPathAttr = GetConfigPathAttribute(instance.GetType());
            if (configPathAttr == null)
            {
                throw new Exception($"Path not found for instance {instance.GetType()}");
            }

            configuration.Bind(configPathAttr.Path, instance);

            return instance;
        }

        private static ConfigPath GetConfigPathAttribute(Type type)
        {
            var custAttrs = type.GetCustomAttributes();
            foreach (var attribute in custAttrs)
            {
                if (attribute is ConfigPath)
                {
                    return (ConfigPath) attribute;
                }
            }

            return null;
        }
    }
}