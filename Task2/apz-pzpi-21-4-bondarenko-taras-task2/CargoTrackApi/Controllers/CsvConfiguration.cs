using System.Globalization;
using System.Text;

namespace CargoTrackApi.Api.Controllers
{
    internal class CsvConfiguration
    {
        private CultureInfo invariantCulture;

        public CsvConfiguration(CultureInfo invariantCulture)
        {
            this.invariantCulture = invariantCulture;
        }

        public string Delimiter { get; set; }
        public Encoding Encoding { get; set; }
    }
}