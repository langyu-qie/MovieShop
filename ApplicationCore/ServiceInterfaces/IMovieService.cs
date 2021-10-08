using ApplicationCore.Helpers;
using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.ServiceInterfaces
{
    //Services return Models
    public interface IMovieService
    {
        Task<IEnumerable<MovieCardResponseModel>> Get30HighestGrossingMovies();

        //IEnumerable<MovieCardResponseModel> GetMovieByGenre(int genreId);

        Task<MovieDetailsModel> MovieDetailsById(int Id);

        //Task<PagedResultSet<MovieCardResponseModel>> GetMoviesByPagination(int pageSize = 30, int page = 1, string title = "");

        Task<PagedResultSet<MovieCardResponseModel>> GetMoviesByGenre(int genreId, int pageSize = 30, int pageIndex = 1);


    }
}
