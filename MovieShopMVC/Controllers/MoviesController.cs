using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Services;
using ApplicationCore.ServiceInterfaces;

namespace MovieShopMVC.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMovieService _movieService;
        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }
        public async Task<IActionResult> GetTopRevenueMovies()
        {
            var movies =await _movieService.Get30HighestGrossingMovies();
            return View(movies);

        }


        public async Task<IActionResult> Genre(int id, int pageSize = 30, int pageNumber = 1)
        {
            var movies = await _movieService.GetMoviesByGenre(id, pageSize, pageNumber);
            return View("PagedIndex", movies);
        }


        public async Task<IActionResult> Details(int Id)
        {

            var moviedetails =await _movieService.MovieDetailsById(Id);

            return View(moviedetails);

        }



    }
}
