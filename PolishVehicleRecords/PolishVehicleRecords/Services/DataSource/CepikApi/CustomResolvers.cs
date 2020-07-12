using Newtonsoft.Json.Serialization;
using System.Collections.Generic;

namespace PolishVehicleRecords.Services.DataSource.CepikApi
{
    public class CustomBaseResolver : DefaultContractResolver
    {
        public Dictionary<string, string> PropertyMappings { get; set; }

        protected override string ResolvePropertyName(string propertyName)
        {
            string resolvedName = null;
            var resolved = this.PropertyMappings.TryGetValue(propertyName, out resolvedName);
            return (resolved) ? resolvedName : base.ResolvePropertyName(propertyName);
        }
    }

    public class CustomCarResolver : CustomBaseResolver
    {
        public CustomCarResolver()
        {
            this.PropertyMappings = new Dictionary<string, string>
            {
                {"Brand", "marka" },
                {"Model", "model" },
                {"Type", "rodzaj-pojazdu" },
                {"EngineCapacity", "pojemnosc-skokowa-silnika" },
                {"Weight", "masa-wlasna" },
                {"Fuel", "rodzaj-paliwa" },
            };
        }
    }

    public class CustomVoivedoshipResolver : CustomBaseResolver
    {
        public CustomVoivedoshipResolver()
        {
            this.PropertyMappings = new Dictionary<string, string>
            {
                {"Code", "klucz-slownika"},
                {"Name", "wartosc-slownika"}
            };
        }
    }

    public class CustomCarTypeResolver : CustomBaseResolver
    {
        public CustomCarTypeResolver()
        {
            this.PropertyMappings = new Dictionary<string, string>
            {
                {"Name", "klucz-slownika"}
            };
        }
    }
}
