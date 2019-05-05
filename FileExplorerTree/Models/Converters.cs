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
        public enum FileSizeLabel
        {
            Byte = 2,
            KB,
            MB,
            GB,
            TB
        }
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
            int powCounter = 2;
            while (true)
            {
                if (Math.Round((num / Math.Pow(1024f, powCounter))) == 0)
                {
                    break;
                }
                powCounter++;
            }
            if(powCounter < 3)
                return Math.Round((num / Math.Pow(1024f, powCounter-1))).ToString("N0") + " " + (FileSizeLabel)(powCounter);
            else
                return (num / Math.Pow(1024f, powCounter-1)).ToString("N2") + " " + (FileSizeLabel)(powCounter+1);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
