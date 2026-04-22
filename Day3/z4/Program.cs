using System;

partial class Tour
{
    public string Destination { get; set; }
    public int Duration { get; set; }
    public decimal Price { get; set; }
    public string Hotel { get; set; }

    public Tour(string destination, int duration, decimal price, string hotel)
    {
        Destination = destination;
        Duration = duration;
        Price = price;
        Hotel = hotel;
    }
}

partial class Tour
{
    public int GetDuration()
    {
        return Duration;
    }

    public string GetDestination()
    {
        return Destination;
    }
}

class TravelAgency
{
    public Tour[] Tours { get; set; }

    public TravelAgency(Tour[] tours)
    {
        Tours = tours;
    }

    public Tour GetLongestTour()
    {
        Tour longest = Tours[0];

        for (int i = 1; i < Tours.Length; i++)
        {
            if (Tours[i].Duration > longest.Duration)
            {
                longest = Tours[i];
            }
        }

        return longest;
    }

    public Tour[] GetToursByDestination(string destination)
    {
        int count = 0;

        for (int i = 0; i < Tours.Length; i++)
        {
            if (Tours[i].Destination == destination)
            {
                count++;
            }
        }

        Tour[] result = new Tour[count];
        int index = 0;

        for (int i = 0; i < Tours.Length; i++)
        {
            if (Tours[i].Destination == destination)
            {
                result[index] = Tours[i];
                index++;
            }
        }

        return result;
    }
}

class Program
{
    static void Main()
    {
        Tour[] tours = new Tour[]
        {
            new Tour("Турция", 7, 50000, "5 звезд"),
            new Tour("Египет", 10, 45000, "4 звезды"),
            new Tour("Турция", 14, 85000, "5 звезд"),
            new Tour("Италия", 5, 70000, "3 звезды"),
            new Tour("Египет", 7, 55000, "5 звезд")
        };

        TravelAgency agency = new TravelAgency(tours);

        Tour longest = agency.GetLongestTour();
        Console.WriteLine("Самый длительный тур: " + longest.Destination + " (" + longest.Duration + " дней)");

        Console.WriteLine("\nТуры в Турцию:");
        Tour[] turkeyTours = agency.GetToursByDestination("Турция");
        for (int i = 0; i < turkeyTours.Length; i++)
        {
            Console.WriteLine(turkeyTours[i].Destination + " - " + turkeyTours[i].Duration + " дней, отель: " + turkeyTours[i].Hotel);
        }

        Console.WriteLine("\nТуры в Египет:");
        Tour[] egyptTours = agency.GetToursByDestination("Египет");
        for (int i = 0; i < egyptTours.Length; i++)
        {
            Console.WriteLine(egyptTours[i].Destination + " - " + egyptTours[i].Duration + " дней, отель: " + egyptTours[i].Hotel);
        }
    }
}