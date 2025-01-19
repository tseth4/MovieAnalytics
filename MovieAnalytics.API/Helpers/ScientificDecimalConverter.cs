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

            text = text.Replace("$", "").Replace(",", "").Trim();

            if (decimal.TryParse(text, NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out decimal result))
            {
                return result;
            }

            Console.WriteLine($"Invalid decimal value: {text}");
            return null;
        }

    }
}
