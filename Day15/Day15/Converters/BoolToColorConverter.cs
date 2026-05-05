using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace EventManagementApp.Converters
{
    public class BoolToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
            {
                return boolValue ? new SolidColorBrush(Color.FromRgb(40, 167, 69)) : new SolidColorBrush(Color.FromRgb(220, 53, 69));
            }
            return new SolidColorBrush(Color.FromRgb(108, 117, 125));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}