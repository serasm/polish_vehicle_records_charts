namespace PolishVehicleRecords.Models.Entities
{
    public class Car
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Type { get; set; }
        public ushort EngineCapacity { get; set; }
        public ushort Weight { get; set; }
        public string Fuel { get; set; }
    }
}
