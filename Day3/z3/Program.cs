using System;

abstract class Movie
{
    public string Title { get; set; }
    public string Genre { get; set; }
    public int Duration { get; set; }

    public Movie(string title, string genre, int duration)
    {
        Title = title;
        Genre = genre;
        Duration = duration;
    }
}

sealed class ActionMovie : Movie
{
    public ActionMovie(string title, int duration) : base(title, "Action", duration)
    {
    }
}

sealed class ComedyMovie : Movie
{
    public ComedyMovie(string title, int duration) : base(title, "Comedy", duration)
    {
    }
}

class Cinema
{
    public Movie[] Movies { get; set; }

    public Cinema(Movie[] movies)
    {
        Movies = movies;
    }

    public Movie GetLongestMovie()
    {
        Movie longest = Movies[0];

        for (int i = 1; i < Movies.Length; i++)
        {
            if (Movies[i].Duration > longest.Duration)
            {
                longest = Movies[i];
            }
        }

        return longest;
    }

    public Movie[] GetMoviesByGenre(string genre)
    {
        int count = 0;

        for (int i = 0; i < Movies.Length; i++)
        {
            if (Movies[i].Genre == genre)
            {
                count++;
            }
        }

        Movie[] result = new Movie[count];
        int index = 0;

        for (int i = 0; i < Movies.Length; i++)
        {
            if (Movies[i].Genre == genre)
            {
                result[index] = Movies[i];
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
        Movie[] movies = new Movie[]
        {
            new ActionMovie("Крепкий орешек", 132),
            new ComedyMovie("Суперперцы", 113),
            new ActionMovie("Джон Уик", 101),
            new ComedyMovie("Мальчишник в Вегасе", 108),
            new ActionMovie("Безумный Макс", 120)
        };

        Cinema cinema = new Cinema(movies);

        Movie longest = cinema.GetLongestMovie();
        Console.WriteLine("Самый длинный фильм: " + longest.Title + " (" + longest.Duration + " мин)");

        Console.WriteLine("\nФильмы в жанре Action:");
        Movie[] actionMovies = cinema.GetMoviesByGenre("Action");
        for (int i = 0; i < actionMovies.Length; i++)
        {
            Console.WriteLine(actionMovies[i].Title);
        }

        Console.WriteLine("\nФильмы в жанре Comedy:");
        Movie[] comedyMovies = cinema.GetMoviesByGenre("Comedy");
        for (int i = 0; i < comedyMovies.Length; i++)
        {
            Console.WriteLine(comedyMovies[i].Title);
        }
    }
}