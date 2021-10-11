using ApplicationCore.Entities;
using ApplicationCore.Helpers;
using ApplicationCore.Models;
using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.ServiceInterfaces;
using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace Infrastructure.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        //private readonly IMapper _mapper;


        public MovieService(IMovieRepository movieRepository/*, IMapper mapper*/)
        {
            _movieRepository = movieRepository;
            ////_mapper = mapper;
        }

        public async Task<IEnumerable<MovieCardResponseModel>> Get30HighestGrossingMovies()
        {
            // list of movie entites 
            var movies =await _movieRepository.Get30HighestGrossingMovies();

            var moviesCardResponseModel = new List<MovieCardResponseModel>();

            // mapping entites to models data so that services always return models mot entites
            foreach (var movie in movies)
                moviesCardResponseModel.Add(new MovieCardResponseModel { Id = movie.Id, PosterUrl = movie.PosterUrl });

            // return list of movieresponse models
            return moviesCardResponseModel;
        }

        public async Task<PagedResultSet<MovieCardResponseModel>> GetMoviesByPagination(
             int pageSize = 20, int pageIndex = 0, string title = "")
        {
            Expression<Func<Movie, bool>> filterExpression = null;
            if (!string.IsNullOrEmpty(title)) filterExpression = movie => title != null && movie.Title.Contains(title);

            var pagedMovies = await _movieRepository.GetPagedData(pageIndex, pageSize, mov => mov.OrderBy(m => m.Title),
                filterExpression);
            var lstOfMovieCards = new List<MovieCardResponseModel>();
            foreach (var movie in pagedMovies)
                lstOfMovieCards.Add(new MovieCardResponseModel { Id = movie.Id, PosterUrl = movie.PosterUrl });
            var movies =
                new PagedResultSet<MovieCardResponseModel>(
                    lstOfMovieCards,
                    pagedMovies.PageIndex,
                    pageSize, pagedMovies.TotalCount);
            return movies;
        }


        public async Task<PagedResultSet<MovieCardResponseModel>> GetMoviesByGenre(int genreId, int pageSize = 30,
            int pageIndex = 1)
        {
            var pagedMovies = await _movieRepository.GetMoviesByGenre(genreId, pageSize, pageIndex);
            var movieCards = new List<MovieCardResponseModel>();
            movieCards.AddRange(pagedMovies.Data.Select(movie => new MovieCardResponseModel
            {
                Id = movie.Id,
                Title = movie.Title,
                PosterUrl = movie.PosterUrl,
                Revenue = movie.Revenue.GetValueOrDefault()
            }));

            return new PagedResultSet<MovieCardResponseModel>(movieCards, pageIndex, pageSize, pagedMovies.Count);
        }

        public async Task<MovieDetailsModel> MovieDetailsById(int Id)
        {


            var movie =await _movieRepository.GetByIdAsync(Id);
            if (movie == null) throw new Exception($"No Movie Found for this {Id}");

            var movieDetails = new MovieDetailsModel
            {
                Id = movie.Id,
                Budget = movie.Budget,
                Overview = movie.Overview,
                Price = movie.Price,
                PosterUrl = movie.PosterUrl,
                Revenue = movie.Revenue,
                ReleaseDate = movie.ReleaseDate.GetValueOrDefault(),
                Rating = movie.Rating,
                Tagline = movie.Tagline,
                Title = movie.Title,
                RunTime = movie.RunTime,
                BackdropUrl = movie.BackdropUrl,
                ImdbUrl = movie.ImdbUrl,
                TmdbUrl = movie.TmdbUrl
            };

            foreach (var movieGenre in movie.Genres)
                movieDetails.Genres.Add(new GenreModel
                {
                    Id = movieGenre.Genre.Id,
                    Name = movieGenre.Genre.Name
                }); 

            foreach (var movieCast in movie.Casts) 
                movieDetails.Casts.Add(new CastModel
                {
                    Id = movieCast.Cast.Id,
                     Name = movieCast.Cast.Name,
                    Character = movieCast.Character,
                    Gender = movieCast.Cast.Gender,
                    ProfilePath = movieCast.Cast.ProfilePath,
                    TmdbUrl = movieCast.Cast.TmdbUrl
                });

            foreach (var trailer in movie.Trailers)
                movieDetails.Trailers.Add(new TrailerModel
                {
                    Id = trailer.Id,
                    Name = trailer.Name,
                    TrailerUrl = trailer.TrailerUrl,
                    MovieId = trailer.MovieId
                });
            return movieDetails;


        }

        public async Task<IEnumerable<MovieCardResponseModel>> GetTopRatedMovies()
        {
            var topMovies = await _movieRepository.GetTopRatedMovies();
            var moviesCardResponseModel = new List<MovieCardResponseModel>();

            // mapping entites to models data so that services always return models mot entites
            foreach (var movie in topMovies)
                moviesCardResponseModel.Add(new MovieCardResponseModel { Id = movie.Id, PosterUrl = movie.PosterUrl, Rating = movie.Rating});

            // return list of movieresponse models

            return moviesCardResponseModel;
        }

        public async Task<IEnumerable<MovieReviewResponseModel>> GetReviewsForMovie(int id, int pageSize = 25,
    int page = 1)
        {
            var reviews = await _movieRepository.GetMovieReviews(id, pageSize, page);
            var reviewsMovieModel = reviews.Select(r => new MovieReviewResponseModel
            {
                MovieId = r.MovieId,
                Rating = r.Rating,
                ReviewText = r.ReviewText,
                UserId = r.UserId,
                Name = r.User.FirstName + " " + r.User.LastName
            });
            return reviewsMovieModel;
        }


    }

}
