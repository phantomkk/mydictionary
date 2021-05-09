using System;

namespace Infrastructure.Configuration
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ConfigPath: Attribute
    {
        public string Path { get; }

        public ConfigPath(string path)
        {
            Path = path;
        }
    }
}