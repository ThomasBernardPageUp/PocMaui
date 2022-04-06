using System.Globalization;
using Microsoft.Maui.Graphics;

namespace PocMaui.Converters
{
    public class SutomWordStatusToCaseColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                var status = (int)value;

                if (status == 2)
                    return "#E7002A";
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }

            return "#0077C7";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
