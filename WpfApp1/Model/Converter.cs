using System.Collections;
using System.Globalization;
using System.Windows.Data;

namespace WpfApp1.Model;
public class EnumerableJoinConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is string[] items)
        {
            return string.Join(", ", items);
        }
        return string.Empty;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is string items)
        {
            return items.Split(", ");
        }
        return null;
    }
}
