using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml.Data;

namespace Record2
{
    public class StringDoubleConverter : IValueConverter
    {
        
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            string str = (string)value;
           double dou = double.Parse(str);
           return dou;
            
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            double dou = (double)value;
            return dou.ToString();
        }
    }
}
