using PolishVehicleRecords.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PolishVehicleRecords.Services
{
    interface IDictionariesValues
    {
        public Task<List<Voivodeship>> GetVoivodeships();
        public Task<List<CarType>> GetCarTypes();
    }
}
