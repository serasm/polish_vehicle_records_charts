using PolishVehicleRecords.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PolishVehicleRecords.Services
{
    interface ICarsData
    {
        public Task<List<Car>> GetCars(CarsSearch search);
    }
}
