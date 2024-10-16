using Avalonia.Data.Converters;
using Avalonia.Media;
using System;
using System.Globalization;

namespace ToDoApp.Utilities.Converter
{
    public class ColorToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var colorString = parameter as string;
            if (value is IBrush brush && colorString != null)
            {
                return brush.ToString() == Brush.Parse(colorString).ToString();
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isChecked && isChecked)
            {
                var colorString = parameter as string;
                if (!string.IsNullOrEmpty(colorString))
                {
                    return Brush.Parse(colorString);
                }
            }
            return Brushes.Transparent;
        }
    }
}
