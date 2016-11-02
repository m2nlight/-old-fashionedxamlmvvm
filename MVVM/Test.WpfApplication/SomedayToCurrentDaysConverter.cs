using System;
using System.Globalization;
using System.Windows.Data;

namespace Test.WpfApplication
{
    internal class SomedayToCurrentDaysConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var dt = (DateTime?)value;
            if (dt == null)
            {
                return double.NaN;
            }

            var ts = DateTime.Now - dt.Value;
            return ts.TotalDays;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
