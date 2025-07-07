using System.Windows.Markup;

namespace aCHADemia.Core.MarkupExtensions
{
    public class BoolArrayExtension : MarkupExtension
    {
        // Property to accept comma-separated values
        public string Values { get; set; }

        // Constructor that takes the string
        public BoolArrayExtension(string values)
        {
            Values = values;
        }

        // Parameterless constructor for XAML usage
        public BoolArrayExtension() { }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (string.IsNullOrWhiteSpace(Values))
                return Array.Empty<bool>();

            try
            {
                return Array.ConvertAll(
                    Values.Split(','),
                    x => bool.Parse(x.Trim())
                );
            }
            catch
            {
                throw new FormatException("Invalid boolean array format. Use 'true,false,true'");
            }
        }
    }
}
