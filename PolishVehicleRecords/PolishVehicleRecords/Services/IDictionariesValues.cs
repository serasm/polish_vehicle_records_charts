using PolishVehicleRecords.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PolishVehicleRecords.Services
{
    public interface IDictionariesValues
    {
        public Task<List<Voivodeship>> GetVoivodeships();
        public Task<List<CarType>> GetCarTypes();
    }
}
