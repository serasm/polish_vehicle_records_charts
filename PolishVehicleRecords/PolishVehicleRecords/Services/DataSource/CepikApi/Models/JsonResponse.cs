using Newtonsoft.Json;
using PolishVehicleRecords.Models.Entities;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PolishVehicleRecords.Services.DataSource.CepikApi.Models
{
    public class JsonResponse<T>
    {
        public T data { get; set; }
    }
    public class Data<T>
    {
        public T attributes { get; set; }
    }
    public class Records<T>
    {
        [JsonProperty(PropertyName = "dostepne-rekordy-slownika")]
        public T records { get; set; }
    }

    public class JsonCarsResponse
    {
        public List<Datas> data { get; set; }
    }
    public class Datas
    {
        public Car attributes { get; set; }
    }
}
