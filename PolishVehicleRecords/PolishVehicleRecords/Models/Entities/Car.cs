namespace PolishVehicleRecords.Models.Entities
{
    public class Car
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Type { get; set; }
        public double? EngineCapacity { get; set; }
        public int? Weight { get; set; }
        public string Fuel { get; set; }
    }
}
