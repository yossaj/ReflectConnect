using System;
using System.Globalization;
using SRConnect.Models;
using Xamarin.Forms;

namespace SRConnect.ValueConverters
{
    public class ConnectedValueConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Color color = (bool)value ? Color.Green : Color.Red;
            return color;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
