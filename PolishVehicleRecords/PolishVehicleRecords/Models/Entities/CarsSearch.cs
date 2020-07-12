using System;
using System.Collections.Generic;

namespace PolishVehicleRecords.Models.Entities
{
    public class CarsSearch
    {
        public DateTime StartDate, EndDate;
        public bool OnlyRegistered = true;
        public ushort Limit = 100;
        public List<string> Fields, Voivodeships, Types;
    }
}
