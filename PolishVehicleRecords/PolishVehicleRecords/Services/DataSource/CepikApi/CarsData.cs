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

        public Task<List<Car>> GetCars(CarsSearch search)
        {
            StringBuilder url = new StringBuilder(MAIN_URL);
            url.Append("/pojazdy?");
            url.Append($"data-od={search.StartDate.ToString("yyyyMMdd")}");
            url.Append($"&data-do={search.EndDate.ToString("yyyyMMdd")}");
            url.Append($"&limit={search.Limit}");
            url.Append($"&page={search.Page}");
            foreach (string voivodeship in search.Voivodeships)
            {
                url.Append($"&wojewodztwo={voivodeship}");
            }

            List<Car> cars = new List<Car>();

            var t = httpClient.GetAsync(url.ToString()).ContinueWith(response =>
            {
                var result = response.Result;
                var t = result.Content.ReadAsStringAsync().ContinueWith(jsonTask =>
                {
                    var jsonSettings = new JsonSerializerSettings();
                    jsonSettings.ContractResolver = new CustomCarResolver();
                    var result = jsonTask.Result;
                    var data = JsonConvert.DeserializeObject<JsonCarsResponse>(result, jsonSettings).data;
                    return data.Select(s => s.attributes).ToList();
                });

                return t.Result;
            });

            return t;
        }
    }
}
