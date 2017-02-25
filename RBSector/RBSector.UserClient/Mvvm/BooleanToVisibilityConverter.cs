using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace RBSector.UserClient.Mvvm
{
    internal sealed class BooleanToVisibilityConverter : IValueConverter
    {
        public bool IsReversed { get; set; }

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var val = System.Convert.ToBoolean(value);
            if (IsReversed)
            {
                val = !val;
            }

            return val
                ? Visibility.Visible
                : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            //this is not needed here
            throw new NotImplementedException();
        }
    }
}
