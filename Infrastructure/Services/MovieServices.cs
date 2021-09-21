using ApplicationCore.Models;
using ApplicationCore.ServiceInterfaces;
using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class MovieService : IMovieService
    {
        public IEnumerable<MovieCardResponseModel> Get30HighestGrossingMovies()
        {
            var repo = new MovieRepository();

            // list of movie entites
            var movies = repo.Get30HighestGrossingMovies();

            var moviesCardResponseModel = new List<MovieCardResponseModel>();

            // mapping entites to models data so that services always return models mot entites
            foreach (var movie in movies)
            {
                moviesCardResponseModel.Add(new MovieCardResponseModel { Id = movie.Id, PosterUrl = movie.PosterUrl });
            }

            // return list of movieresponse models
            return moviesCardResponseModel;
        }
    }


}
