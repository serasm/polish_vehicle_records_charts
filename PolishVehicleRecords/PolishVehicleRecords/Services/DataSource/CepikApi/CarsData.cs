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
    public class CarsData : ICarsData
    {
        private const string MAIN_URL = "https://api.cepik.gov.pl";
        private HttpClient httpClient;

        public CarsData()
        {
            httpClient = new HttpClient();
        }

        public async Task<List<Car>> GetCars(CarsSearch search)
        {
            StringBuilder url = new StringBuilder(MAIN_URL);
            url.Append("/pojazdy?");
            url.Append($"data-od={search.StartDate.ToString("yyyyMMdd")}");
            url.Append($"&data-do={search.EndDate.ToString("yyyyMMdd")}");
            url.Append($"&limit={search.Limit}");
            foreach (string voivodeship in search.Voivodeships)
            {
                url.Append($"&wojewodztwo={voivodeship}");
            }

            List<Car> cars = new List<Car>();

            await httpClient.GetAsync(url.ToString()).ContinueWith(async response =>
            {
                var result = response.Result;
                await result.Content.ReadAsStringAsync().ContinueWith(jsonTask =>
                {
                    var jsonSettings = new JsonSerializerSettings();
                    jsonSettings.ContractResolver = new CustomCarResolver();
                    var data = JsonConvert.DeserializeObject<JsonResponse<Car>>(jsonTask.Result, jsonSettings).data;
                    cars = data.Select(s => s.attributes).ToList();
                });
            });

            return cars;
        }
    }
}
