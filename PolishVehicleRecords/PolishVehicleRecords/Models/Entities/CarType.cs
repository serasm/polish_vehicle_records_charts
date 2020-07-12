using System;
using System.Collections.Generic;
using System.Text;

namespace PolishVehicleRecords.Models.Entities
{
    public class CarType
    {
        public string Name { get; set; }
        public bool IsSelected { get; set; } = false;
    }
}
