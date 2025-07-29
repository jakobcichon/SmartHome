using SmartHome.Common.Models.Settings;
using System.Runtime.CompilerServices;

namespace SmartHome.MobileApp.Prism.Extensions.Maui
{
    internal static class ConfigurationExtensions
    {
        public static MauiAppBuilder BindConfigurationToModel(this MauiAppBuilder builder)
        {
            builder.Services.Configure<SmartHomeCommonSettingsModel>(builder.Configuration.GetSection("SmartHomeSettings"));

            return builder;
        }
    }
}
