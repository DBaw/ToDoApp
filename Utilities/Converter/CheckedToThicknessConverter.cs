using Avalonia;
using Avalonia.Data.Converters;
using System;
using System.Globalization;

namespace ToDoApp.Utilities.Converter
{
    public class CheckedToThicknessConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is bool isChecked && isChecked)
            {
                return new Thickness(3);
            }

            return new Thickness(0);
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is Thickness thickness)
            {
                if(thickness == new Thickness(0))
                {
                    return false;
                }

                return true;
            }

            return false;
        }
    }
}
