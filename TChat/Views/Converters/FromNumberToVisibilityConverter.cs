using System.Globalization;
using System.Windows.Data;
using System.Windows;
using System;

namespace TChat.Views.Converters;

public class FromNumberToVisibilityConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        string fromNumber = (string)value;
        string targetNumber = (string)parameter;
        return fromNumber == targetNumber ? Visibility.Visible : Visibility.Collapsed;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}