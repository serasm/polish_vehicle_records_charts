using System.Collections.Generic;

namespace PolishVehicleRecords.Services.DataSource.CepikApi.Models
{
    public class JsonResponse<T>
    {
        public List<Data<T>> data { get; set; }
    }
    public class Data<T>
    {
        public T attributes { get; set; }
    }
    public class Records<T>
    {
        public T records { get; set; }
    }
}
