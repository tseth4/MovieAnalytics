using CsvHelper.Configuration;
using CsvHelper;
using CsvHelper.TypeConversion;
using System.Globalization;

namespace MovieAnalytics.Helpers
{
    public class ScientificDecimalConverter : DefaultTypeConverter
    {
        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return null; 
            }

            if (decimal.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out decimal result))
            {
                return result; 
            }

            throw new FormatException($"Invalid decimal value: {text}");
        }

    }
}
