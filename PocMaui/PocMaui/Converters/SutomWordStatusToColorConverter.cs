using System.Globalization;
using Microsoft.Maui.Graphics;

namespace PocMaui.Converters
{
    public class SutomWordStatusToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                switch ((int)value)
                {
                    case 0:
                        return Color.FromArgb("FFFFFF");
                    case 1:
                        return "#FFFF00";
                    case 2:
                        return "#FF0000";
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }

            return "FFFFFF";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
