using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.ServiceInterfaces;

namespace MovieShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;
        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }


        [Route("toprevenue")]
        [HttpGet]
        public async Task<IActionResult> GetTopRevenueMovies()
        {
           
            var movies = await _movieService.Get30HighestGrossingMovies();
            if (!movies.Any())
            {
                return NotFound("No Movies Found");
            }
            return Ok(movies);
            //Serialization => object to another type of object
            //C# to JSON
            //Deserialization => JSON to C#
            //.NET Core 3.1 or less JSON.NET => 3rd party library, included
            //System.Text.Json
            //along with data you also need to return HTTP status code
        }


        [Route("{id}")]
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {

            var moviedetails = await _movieService.MovieDetailsById(id);

            return Ok(moviedetails);

        }




















    }
}
