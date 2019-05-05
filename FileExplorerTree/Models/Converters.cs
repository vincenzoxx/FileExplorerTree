using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace FileExplorerTree.Models
{
    public class ByteToKBConverter : IValueConverter
    {
        public enum FileSizeLabel
        {
            Byte,
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
            int powCounter = 1;
            while (true)
            {
                if (Math.Round((num / Math.Pow(1024f, powCounter))) == 0)
                {
                    powCounter--;
                    break;
                }
                powCounter++;
            }
            if(powCounter < 2)
                return num == 0 ? "" : Math.Round((num / Math.Pow(1024f, powCounter))).ToString("N0") + " " + (FileSizeLabel)(powCounter);
            else
                return (num / Math.Pow(1024f, powCounter)).ToString("N2") + " " + (FileSizeLabel)(powCounter);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class TypeOfNodeToVisibilityConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            TypeOfNode t = TypeOfNode.Unknown;
            try
            {
                t = (TypeOfNode)value;
            }
            catch
            {

            }
            switch (t)
            {
                case TypeOfNode.Directory:
                    return false;
                 default:
                    return true;
            }
            
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
