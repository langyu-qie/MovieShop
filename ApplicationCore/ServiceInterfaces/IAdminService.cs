using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Helpers;
using ApplicationCore.Models;
using ApplicationCore.Models.Reports;

namespace ApplicationCore.ServiceInterfaces
{
    public interface IAdminService
    {
        Task<MovieDetailsModel> CreateMovie(MovieCreateRequest movieCreateRequest);
        Task<MovieDetailsModel> UpdateMovie(MovieCreateRequest movieCreateRequest);
        //Task<PagedResultSet<MovieCardResponseModel>> GetAllPurchasesByMovieId(int movieId);

        Task<PagedResultSet<MoviesReportModel>> GetTopPurchasedMovies(DateTime? fromDate = null,
            DateTime? toDate = null, int pageSize = 30, int page = 1);
        //Task<PagedResultSet<MovieCardResponseModel>> GetAllMoviePurchasesByPagination(int pageSize = 20, int page = 1);


    }
}