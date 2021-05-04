using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using ConsoleTables;

namespace ApiClient
{
    class GhibliFilm
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("original_title")]
        public string OriginalTitle { get; set; }

        [JsonPropertyName("original_title_romanised")]
        public string OriginalTitleRomanized { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("director")]
        public string Director { get; set; }

        [JsonPropertyName("producer")]
        public string Producer { get; set; }

        [JsonPropertyName("release_date")]
        public string ReleaseDate { get; set; }

        [JsonPropertyName("running_time")]
        public string RunningTime { get; set; }

        [JsonPropertyName("rt_score")]
        public string RtScore { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

    }
    class Program
    {
        static async Task Main(string[] args)
        {

            var client = new HttpClient();

            var url = $"https://ghibliapi.herokuapp.com/films";
            var responseAsStream = await client.GetStreamAsync(url);


            // Supply that *stream of data* to a Deserialize that will interpret it as a List of GhibliFilm objects.
            var ghibliFilms = await JsonSerializer.DeserializeAsync<List<GhibliFilm>>(responseAsStream);

            //var table = new ConsoleTable("Title", "Release Year", "Original Title");

            Console.WriteLine();
            Console.WriteLine("List of Ghibli Movies:");

            // I don't like how this looks
            // var ghibliTitles = new List<string> { };
            // foreach (var movie in ghibliFilms)
            // {
            //     ghibliTitles.Add(movie.Title);
            // }

            // Console.WriteLine(String.Join(", ", ghibliTitles));

            foreach (var film in ghibliFilms)
            {
                Console.WriteLine(film.Title);
            }
            Console.WriteLine();
            Console.WriteLine("What movie would you like to see more information about?");

            var response = Console.ReadLine().ToLower();

            var chosenMovie = ghibliFilms.FirstOrDefault(movie => movie.Title.ToLower() == response);

            if (chosenMovie == null)
            {
                Console.WriteLine("Sorry, I don't understand.");
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine($"{chosenMovie.Title} was released in {chosenMovie.ReleaseDate}.");
                Console.WriteLine($"Its Japanese name is {chosenMovie.OriginalTitle} ({chosenMovie.OriginalTitleRomanized}).");
                Console.WriteLine($"It was directed by {chosenMovie.Director} and produced by {chosenMovie.Producer}");
                Console.WriteLine($"Runtime: {chosenMovie.RunningTime} minutes; Rotten Tomatoes Score: {chosenMovie.RtScore}");

                Console.WriteLine("Summary:");
                Console.WriteLine(chosenMovie.Description);

                Console.WriteLine();
                Console.WriteLine($"URL: {chosenMovie.Url}");
                Console.WriteLine();
            }

            // table.Write();


        }
    }
}
