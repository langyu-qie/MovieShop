using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.ServiceInterfaces;
using ApplicationCore.Models;

namespace Infrastructure.Services
{
    public class MovieNoSQLService : IMovieService
    {
        public IEnumerable<MovieCardResponseModel> Get30HighestGrossingMovies()
        {
            var movies = new List<MovieCardResponseModel>
            {
                new MovieCardResponseModel { Id =111, Title="Inception", PosterUrl="https://image.tmdb.org/t/p/w342//9gk7adHYeDvHkCSEqAvQNLV5Uge.jpg", Revenue=825532764},
                new MovieCardResponseModel { Id =222, Title="Interstellar", PosterUrl="https://image.tmdb.org/t/p/w342//gEU2QniE6E77NI6lCU6MxlNBvIx.jpg", Revenue=825532764},
                new MovieCardResponseModel { Id =333, Title="The Dark Knight", PosterUrl="https://image.tmdb.org/t/p/w342//qJ2tW6WMUDux911r6m7haRef0WH.jpg", Revenue=825532764},
                new MovieCardResponseModel { Id =444, Title="Deadpool", PosterUrl="https://image.tmdb.org/t/p/w342//yGSxMiF0cYuAiyuve5DA6bnWEOI.jpg", Revenue=825532764},
                new MovieCardResponseModel { Id =555, Title="The Avengers", PosterUrl="https://image.tmdb.org/t/p/w342//RYMX2wcKCBAr24UyPD7xwmjaTn.jpg", Revenue=825532764},
            };

            return movies;
        }

    }
}
