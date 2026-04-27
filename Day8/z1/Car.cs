namespace TrafficSimulation
{
    public class Car
    {
        public string LicensePlate { get; set; }
        public DateTime EntryTime { get; set; }

        public Car(string licensePlate)
        {
            LicensePlate = licensePlate;
            EntryTime = DateTime.Now;
        }
    }
}