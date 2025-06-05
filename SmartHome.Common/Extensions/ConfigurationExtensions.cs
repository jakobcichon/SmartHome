using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace SmartHome.Common.Extensions
{
    public static class ConfigurationExtensions
    {
        public static IConfigurationBuilder AddCommonConfiguration(this IConfigurationBuilder builder)
        {
            var a = Assembly.GetCallingAssembly();
            using var stream = a.GetManifestResourceStream("SmartHome.MobileApp.common.settings.json");
            builder.AddJsonStream(stream);
            return builder;
        }
    }
}
