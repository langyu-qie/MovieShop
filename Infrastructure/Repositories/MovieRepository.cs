using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
using ApplicationCore.Helpers;
using ApplicationCore.Models;
using ApplicationCore.RepositoryInterfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class MovieRepository : EfRepository<Movie>, IMovieRepository
    {
        private object movieShopDbContext;

        public MovieRepository(MovieShopDbContext dbContext): base(dbContext)
        {
            
        }
        public async Task<IEnumerable<Movie>> Get30HighestGrossingMovies()
        {
            // async/await go as pair
            // EF , Dapper...they have both async methods and sync method
            var movies = await _dbContext.Movies.OrderByDescending(m => m.Revenue).Take(30).ToListAsync();
            return movies;
        }

        public async Task<PagedResultSet<Movie>> GetMoviesByGenre(int genreId, int pageSize = 30, int pageIndex = 1)
        {
            var totalMoviesCountByGenre =
                await _dbContext.MovieGenres.Where(g => g.GenreId == genreId).CountAsync();

            if (totalMoviesCountByGenre == 0) throw new NotFoundException("NO Movies found for this genre");
            var movies = await _dbContext.MovieGenres.Where(g => g.GenreId == genreId).Include(g => g.Movie).OrderByDescending(m => m.Movie.Revenue)
                .Select(m => new Movie
                {
                    Id = m.MovieId,
                    PosterUrl = m.Movie.PosterUrl,
                    Title = m.Movie.Title,
                    ReleaseDate = m.Movie.ReleaseDate
                })
                .Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PagedResultSet<Movie>(movies, pageIndex, pageSize, totalMoviesCountByGenre);
        }


        public async Task<IEnumerable<Review>> GetMovieReviews(int id, int pageSize = 25, int page = 1)
        {
            var reviews = await _dbContext.Reviews.Where(r => r.MovieId == id).Include(r => r.User)
                .Select(r => new Review
                {
                    UserId = r.UserId,
                    Rating = r.Rating,
                    MovieId = r.MovieId,
                    ReviewText = r.ReviewText,
                    Movie = new Movie
                    {
                        Id = r.Movie.Id,
                        Title = r.Movie.Title
                    },
                    User = new User
                    {
                        Id = r.UserId,
                        FirstName = r.User.FirstName,
                        LastName = r.User.LastName
                    }
                }).Skip(pageSize * (page - 1)).Take(pageSize).ToListAsync();
            return reviews;
        }





        public override async Task<Movie> GetByIdAsync(int Id)
        {
            var moviedetails = await _dbContext.Movies.Include(m => m.Genres).ThenInclude(m => m.Genre)
                .Include(m => m.Trailers).Include(m =>m.Casts).ThenInclude(m=>m.Cast).FirstOrDefaultAsync(m => m.Id == Id);
            if(moviedetails == null)
            {
                throw new Exception($"No Movie Found for this {Id}");
            }

            var rating = await _dbContext.Reviews.Where(r => r.MovieId == Id).DefaultIfEmpty().AverageAsync(r =>r==null?0: r.Rating);
            moviedetails.Rating = rating;


            return moviedetails;

        }

        public async Task<IEnumerable<Movie>> GetTopRatedMovies()
        {
            var topRatedMovies = await _dbContext.Reviews.Include(m => m.Movie)
                .GroupBy(r => new
                {
                    Id = r.MovieId,
                    r.Movie.PosterUrl,
                    r.Movie.Title,
                    r.Movie.ReleaseDate
                })
                .OrderByDescending(g => g.Average(m => m.Rating))
                .Select(m => new Movie
                {
                    Id = m.Key.Id,
                    PosterUrl = m.Key.PosterUrl,
                    Title = m.Key.Title,
                    ReleaseDate = m.Key.ReleaseDate,
                    Rating = m.Average(x => x.Rating)
                })
                .Take(50)
                .ToListAsync();

            return topRatedMovies;
        }


    }
}
