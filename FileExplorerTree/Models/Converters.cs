using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace FileExplorerTree.Models
{
    public class ByteToKBConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            long num = 0;
            try
            {
                long.TryParse(value.ToString(), out num);
            }
            catch
            {
                num = -1;
            }
            return Math.Round((num / 1024f)).ToString("N0");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
