using Newtonsoft.Json;
using PolishVehicleRecords.Models.Entities;
using PolishVehicleRecords.Services.DataSource.CepikApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PolishVehicleRecords.Services.DataSource.CepikApi
{
    public class DictionariesValues : IDictionariesValues
    {
        private const string MAIN_URL = "https://api.cepik.gov.pl/slowniki";
        private HttpClient httpClient;

        public DictionariesValues()
        {
            httpClient = new HttpClient();
        }

        public Task<List<CarType>> GetCarTypes()
        {
            StringBuilder url = new StringBuilder(MAIN_URL);
            url.Append("/rodzaj-pojazdu");

            var carTypes = new List<CarType>();

            var t = httpClient.GetAsync(url.ToString()).ContinueWith(response =>
            {
                var result = response.Result;
                var t = result.Content.ReadAsStringAsync().ContinueWith(jsonTask =>
                {
                    var jsonSettings = new JsonSerializerSettings();
                    jsonSettings.ContractResolver = new CustomCarTypeResolver();
                    var result = jsonTask.Result;
                    var data = JsonConvert.DeserializeObject<JsonResponse<Data<Records<List<CarType>>>>>(result, jsonSettings).data;
                    return data.attributes.records;
                });
                return t.Result;
            });

            return t;
        }

        public Task<List<Voivodeship>> GetVoivodeships()
        {
            StringBuilder url = new StringBuilder(MAIN_URL);
            url.Append("/wojewodztwa");

            var t = httpClient.GetAsync(url.ToString()).ContinueWith(response =>
            {
                var result = response.Result;
                var t = result.Content.ReadAsStringAsync().ContinueWith(jsonTask =>
                {
                    var jsonSettings = new JsonSerializerSettings();
                    jsonSettings.ContractResolver = new CustomVoivedoshipResolver();
                    var result = jsonTask.Result;
                    var data = JsonConvert.DeserializeObject<JsonResponse<Data<Records<List<Voivodeship>>>>>(result, jsonSettings).data;
                    return data.attributes.records;
                });
                return t.Result;
            });

            return t;
        }
    }
}
