using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.RepositoryInterfaces;

namespace Infrastructure.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        public IEnumerable<Movie> Get30HighestGrossingMovies()
        {
            // go to datasbe and execute SP, SQL
            var movies = new List<Movie>
            {
                new Movie { Id =1, Title="Inception", PosterUrl="https://image.tmdb.org/t/p/w342//9gk7adHYeDvHkCSEqAvQNLV5Uge.jpg", Revenue=825532764},
                new Movie { Id =2, Title="Interstellar", PosterUrl="https://image.tmdb.org/t/p/w342//gEU2QniE6E77NI6lCU6MxlNBvIx.jpg", Revenue=825532764},
                new Movie { Id =3, Title="The Dark Knight", PosterUrl="https://image.tmdb.org/t/p/w342//qJ2tW6WMUDux911r6m7haRef0WH.jpg", Revenue=825532764},
                new Movie { Id =4, Title="Deadpool", PosterUrl="https://image.tmdb.org/t/p/w342//yGSxMiF0cYuAiyuve5DA6bnWEOI.jpg", Revenue=825532764},
                new Movie { Id =5, Title="The Avengers", PosterUrl="https://image.tmdb.org/t/p/w342//RYMX2wcKCBAr24UyPD7xwmjaTn.jpg", Revenue=825532764},
            };

            return movies;
        }

    }
}
