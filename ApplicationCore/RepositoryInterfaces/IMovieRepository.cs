using ApplicationCore.Entities;
using ApplicationCore.Helpers;
using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.RepositoryInterfaces
{
    public interface IMovieRepository : IAsyncRepository<Movie>
    {
        Task<IEnumerable<Movie>> Get30HighestGrossingMovies();

        //IEnumerable<Movie> GetMoviesByGenre(int genreId);

        Task<PagedResultSet<Movie>> GetMoviesByGenre(int genreId, int pageSize = 30, int page = 1);


    }
}
