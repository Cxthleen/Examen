using Microsoft.UI.Xaml.Data;
using System;

namespace CrudApp
{
    /// <summary>
    /// Zet een double-prijs om naar een leesbare string met euroteken.
    /// Gebruik in XAML: {Binding Prijs, Converter={StaticResource PrijsConverter}}
    /// </summary>
    public class PrijsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is double prijs)
                return $"€ {prijs:F2}";
            return value?.ToString() ?? string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
            => throw new NotImplementedException();
    }
}
