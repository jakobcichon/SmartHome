using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace SmartHome.Common.Extensions
{
    public static class ConfigurationExtensions
    {
        public static IConfigurationBuilder AddCommonConfiguration(this IConfigurationBuilder builder)
        {
            var a = Assembly.GetCallingAssembly();
            using var stream = a.GetManifestResourceStream($"{a.GetName().Name}.common.settings.json");
            builder.AddJsonStream(stream);
            return builder;
        }

    }
}
