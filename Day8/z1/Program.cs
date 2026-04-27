using System;

namespace TrafficSimulation
{

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Трафик на дороге");

            TrafficQueue trafficQueue = new TrafficQueue();

            Car car1 = new Car("A123BC");
            Car car2 = new Car("B456DE");
            Car car3 = new Car("C789FG");
            Car car4 = new Car("D012HI");

            trafficQueue.AddCar(car1);
            trafficQueue.AddCar(car2);
            trafficQueue.AddCar(car3);
            trafficQueue.AddCar(car4);

            trafficQueue.DisplayAllCars();

            trafficQueue.FindCarByPlate("B456DE");
            trafficQueue.FindCarByPlate("X999XX");

            trafficQueue.RemoveCar();
            trafficQueue.DisplayAllCars();

            trafficQueue.SortByEntryTime();
            trafficQueue.DisplayAllCars();

            System.Threading.Thread.Sleep(2000);
            trafficQueue.FilterByEntryTime(DateTime.Now.AddSeconds(-3));
            trafficQueue.DisplayAllCars();

            Console.WriteLine($"Осталось автомобилей: {trafficQueue.GetCarCount()}");
        }
    }
}