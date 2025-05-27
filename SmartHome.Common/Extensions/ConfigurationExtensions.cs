using Microsoft.Extensions.Configuration;

namespace SmartHome.Common.Extensions
{
    public static class ConfigurationExtensions
    {
        public static IConfigurationBuilder AddCommonConfiguration(this IConfigurationBuilder builder)
        {
            builder.AddJsonFile(
                Path.Join(AppDomain.CurrentDomain.BaseDirectory, "common.settings.json"),
                optional: false);

            return builder;
        }
    }
}
