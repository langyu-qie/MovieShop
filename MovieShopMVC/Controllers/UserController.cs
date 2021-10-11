using ApplicationCore.Models;
using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShopMVC.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMovieService _movieService;

        public UserController(IUserService userService, ICurrentUserService currentUserService, IMovieService movieService)
        {
            _userService = userService;
            _currentUserService = currentUserService;
            _movieService = movieService;
        }

        // showing list of movies user purchased
        // Filters
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Purchases()
        {
            var userId = _currentUserService.UserId;
            var purchases =await _userService.GetPurchaseByUserId(userId);
            // call the User Service to get movies purchased by user, and send the data to the view, and use the existing MovieCard partial View
            return View(purchases);
        }

        [HttpGet]
        public async Task<IActionResult> BuyMovie(int id)
        {
            var movie = await _movieService.MovieDetailsById(id);
            return View(movie);
        }

        [HttpPost]
        public async Task<IActionResult> BuyMovie(PurchaseRequestModel purchase)
        {
            await _userService.PurchaseMovie(purchase, _currentUserService.UserId);
            return Ok();
        }








        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Favorites()
        {
            var userId = _currentUserService.UserId;
            var favorites = await _userService.GetFavoriteByUserId(userId);
            // call the User Service to get movies Favorited by user, and send the data to the view, and use the existing MovieCard partial View

            return View(favorites);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Details()
        {
            // call the User Service to get User Details and display in a View
            // get user id from httpcontext and send it to user services
            // 
            var userId = _currentUserService.UserId;
            var userDetails = await _userService.UserDetailById(userId);

            return View(userDetails);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            var userId = _currentUserService.UserId;

            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(UserEditRequestModel model)
        {

            

            return RedirectToAction("Login");
        }

    }
}
