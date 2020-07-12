using System;
using System.Collections.Generic;
using System.Text;

namespace PolishVehicleRecords.Models.Entities
{
    public class Voivodeship
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public bool IsSelected { get; set; } = false;
    }
}
