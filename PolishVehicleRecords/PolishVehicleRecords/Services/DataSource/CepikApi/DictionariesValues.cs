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

        public async Task<List<CarType>> GetCarTypes()
        {
            StringBuilder url = new StringBuilder(MAIN_URL);
            url.Append("/rodzaj-pojazdu");

            var carTypes = new List<CarType>();

            await httpClient.GetAsync(url.ToString()).ContinueWith(async response =>
            {
                var result = response.Result;
                await result.Content.ReadAsStringAsync().ContinueWith(jsonTask =>
                {
                    var jsonSettings = new JsonSerializerSettings();
                    jsonSettings.ContractResolver = new CustomCarTypeResolver();
                    var data = JsonConvert.DeserializeObject<JsonResponse<Records<CarType>>>(jsonTask.Result, jsonSettings).data;
                    carTypes = data.Select(s => s.attributes.records).ToList();
                });
            });

            return carTypes;
        }

        public async Task<List<Voivodeship>> GetVoivodeships()
        {
            StringBuilder url = new StringBuilder(MAIN_URL);
            url.Append("/rodzaj-pojazdu");

            var voivedoships = new List<Voivodeship>();

            await httpClient.GetAsync(url.ToString()).ContinueWith(async response =>
            {
                var result = response.Result;
                await result.Content.ReadAsStringAsync().ContinueWith(jsonTask =>
                {
                    var jsonSettings = new JsonSerializerSettings();
                    jsonSettings.ContractResolver = new CustomCarTypeResolver();
                    var data = JsonConvert.DeserializeObject<JsonResponse<Records<Voivodeship>>>(jsonTask.Result, jsonSettings).data;
                    voivedoships = data.Select(s => s.attributes.records).ToList();
                });
            });

            return voivedoships;
        }
    }
}
