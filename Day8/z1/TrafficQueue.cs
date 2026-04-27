using System.Collections;

namespace TrafficSimulation
{
    public class TrafficQueue
    {
        private Queue carQueue;

        public TrafficQueue()
        {
            carQueue = new Queue();
        }

        public void AddCar(Car car)
        {
            carQueue.Enqueue(car);
            Console.WriteLine($"Автомобиль {car.LicensePlate} добавлен в очередь");
        }

        public void RemoveCar()
        {
            if (carQueue.Count > 0)
            {
                Car removedCar = (Car)carQueue.Dequeue();
                Console.WriteLine($"Автомобиль {removedCar.LicensePlate} удален из очереди");
            }
            else
            {
                Console.WriteLine("Очередь пуста");
            }
        }

        public Car FindCarByPlate(string licensePlate)
        {
            foreach (Car car in carQueue)
            {
                if (car.LicensePlate == licensePlate)
                {
                    Console.WriteLine($"Автомобиль {car.LicensePlate} найден");
                    return car;
                }
            }
            Console.WriteLine($"Автомобиль с номером {licensePlate} не найден");
            return null;
        }

        public void SortByEntryTime()
        {
            if (carQueue.Count == 0)
            {
                Console.WriteLine("Очередь пуста");
                return;
            }

            Car[] cars = new Car[carQueue.Count];
            carQueue.CopyTo(cars, 0);

            for (int i = 0; i < cars.Length - 1; i++)
            {
                for (int j = i + 1; j < cars.Length; j++)
                {
                    if (cars[i].EntryTime > cars[j].EntryTime)
                    {
                        Car temp = cars[i];
                        cars[i] = cars[j];
                        cars[j] = temp;
                    }
                }
            }

            carQueue.Clear();
            foreach (Car car in cars)
            {
                carQueue.Enqueue(car);
            }
            Console.WriteLine("Очередь отсортирована по времени въезда");
        }

        public void FilterByEntryTime(DateTime minTime)
        {
            if (carQueue.Count == 0)
            {
                Console.WriteLine("Очередь пуста");
                return;
            }

            Car[] cars = new Car[carQueue.Count];
            carQueue.CopyTo(cars, 0);

            carQueue.Clear();
            foreach (Car car in cars)
            {
                if (car.EntryTime > minTime)
                {
                    carQueue.Enqueue(car);
                }
            }
            Console.WriteLine($"Отфильтровано: оставлены автомобили въехавшие после {minTime}");
        }

        public void DisplayAllCars()
        {
            if (carQueue.Count == 0)
            {
                Console.WriteLine("Очередь пуста");
                return;
            }

            Console.WriteLine($"\nВсего автомобилей: {carQueue.Count}");
            int index = 1;
            foreach (Car car in carQueue)
            {
                Console.WriteLine($"{index}. {car.LicensePlate} - въехал в {car.EntryTime:HH:mm:ss}");
                index++;
            }
            Console.WriteLine();
        }

        public int GetCarCount()
        {
            return carQueue.Count;
        }
    }
}