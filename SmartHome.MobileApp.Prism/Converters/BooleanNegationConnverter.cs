using System.Globalization;

namespace SmartHome.MobileApp.Prism.Converters;

public class BooleanNegationConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is bool boolean) return !boolean;
        return value;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is bool boolean) return !boolean;
        return value;
    }
}