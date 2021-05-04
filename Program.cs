using System;
using System.Collections.Generic;
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

            var responseAsStream = await client.GetStreamAsync("https://ghibliapi.herokuapp.com/films");

            // Supply that *stream of data* to a Deserialize that will interpret it as a List of GhibliFilm objects.
            var ghibliFilms = await JsonSerializer.DeserializeAsync<List<GhibliFilm>>(responseAsStream);

            var table = new ConsoleTable("Title", "Release Year", "Original Title");

            // For each film in ghibliFilms
            foreach (var film in ghibliFilms)
            {
                // Output some details on that film
                table.AddRow(film.Title, film.ReleaseDate, film.OriginalTitle);

            }
            table.Write();


        }
    }
}
