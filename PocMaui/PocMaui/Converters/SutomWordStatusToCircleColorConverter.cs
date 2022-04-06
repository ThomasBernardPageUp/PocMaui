using System.Globalization;

namespace PocMaui.Converters
{
    public class SutomWordStatusToCircleColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                var status = (int)value;

                switch (status)
                {
                    case 0:
                        return "#0077C7";
                    case 1:
                        return "#FFBD00";
                    case 2:
                        return "#E7002A";
                }
            }
            catch (Exception ex)
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
