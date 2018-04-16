using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Data;
using System.Globalization;
using DevExpress.Data.Filtering.Helpers;
using System.ComponentModel;

namespace GridWithExpressions {
    public class ExpressionConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            ExpressionEvaluator eval = new ExpressionEvaluator(TypeDescriptor.GetProperties((Task)value), parameter.ToString());
            bool result = eval.Fit((Task)value);
            return result;
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture) {
            return value;
        }
    }

    public class InternalStateConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value != null) {
                if (value.GetType() == typeof(StyleOption))
                    return true;
                else if (value.GetType() == typeof(int)) {
                    int count = System.Convert.ToInt32(value);
                    return count == 0 ? false : true;
                }
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture) {
            return value;
        }
    }

    //public class FontSizeConverter : IValueConverter {
    //    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
    //        if (value is double)
    //            return value;
    //        if (value is string)
    //            return double.Parse(value as string);
    //        return DependencyProperty.UnsetValue;
    //    }

    //    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
    //        if (value == null)
    //            return value;
    //        return value.ToString();
    //    }
    //}
}
