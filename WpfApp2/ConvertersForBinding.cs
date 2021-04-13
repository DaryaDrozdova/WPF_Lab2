using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Windows.Data;
using DataLibrary;

namespace Converters
{
    [ValueConversion(typeof(DataItem), typeof(string))]
    public class DataCollection_Converter1 : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                DataItem item = (DataItem)value;
                return item.coord + "\n";
            }
            else
                return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }

    [ValueConversion(typeof(DataItem), typeof(string))]
    public class DataCollection_Converter2 : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                DataItem item = (DataItem)value;
                return "Value: " + item.val + " Abs: " + Complex.Abs(item.val) + "\n";
            }
            else
                return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }

    [ValueConversion(typeof(V4DataOnGrid), typeof(string))]
    public class MaxMin_Converter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                V4DataOnGrid item = (V4DataOnGrid)value;
                double min = Complex.Abs(item.values[0, 0]);
                double max = Complex.Abs(item.values[0, 0]);
                for (int i = 0; i < item.grid.num_Ox; i++)
                    for (int j = 0; j < item.grid.num_Oy; j++)
                    {
                        if (Complex.Abs(item.values[i, j]) < min)
                            min = Complex.Abs(item.values[i, j]);
                        if (Complex.Abs(item.values[i, j]) > max)
                            max = Complex.Abs(item.values[i, j]);
                    }
                return "Max Abs Value: " + max + " Min Abs Value: " + min;
            }
            else
                return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }

    [ValueConversion(typeof(double), typeof(string))]
    public class Max_Converter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                double item = (double)value;
                return "Max Abs Value: " + item;
            }
            else
                return "";
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }
}