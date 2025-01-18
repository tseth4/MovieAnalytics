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
            // Return null for empty or whitespace values
            if (string.IsNullOrWhiteSpace(text))
            {
                return null;
            }

            // Remove symbols like '$' or ',' for cleaner parsing
            text = text.Replace("$", "").Replace(",", "").Trim();

            // Try parsing as a decimal using invariant culture
            if (decimal.TryParse(text, NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out decimal result))
            {
                return result;
            }

            // Log invalid value and return null instead of throwing an exception
            Console.WriteLine($"Invalid decimal value: {text}");
            return null;
        }

    }
}
