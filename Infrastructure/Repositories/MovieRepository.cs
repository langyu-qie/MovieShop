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




    }
}
