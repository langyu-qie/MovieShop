using ApplicationCore.Models;
using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ICurrentUserService _currentUserService;


        public UserController(IUserService userService, ICurrentUserService currentUserService)
        {
            _userService = userService;
            _currentUserService = currentUserService;

        }

        [Authorize]
        [Route("purchases")]
        [HttpGet]

        public async Task<IActionResult> GetUserPurchasedMoviesAsync()
        {
            var userId = _currentUserService.UserId;
            var purchases = await _userService.GetPurchaseByUserId(userId);
            // call the User Service to get movies purchased by user, and send the data to the view, and use the existing MovieCard partial View
            return Ok(purchases);
        }

        [Authorize]
        [HttpPost("purchase")]
        public async Task<ActionResult> CreatePurchase([FromBody] PurchaseRequestModel purchaseRequest)
        {
            var purchasedStatus =
                await _userService.PurchaseMovie(purchaseRequest, _currentUserService.UserId);
            return Ok(new { purchased = purchasedStatus });
        }

        [Authorize]
        [HttpPost("favorite")]
        public async Task<ActionResult> CreateFavorite([FromBody] FavoriteRequestModel favoriteRequest)
        {
            await _userService.AddFavorite(favoriteRequest);
            return Ok();
        }

        [Authorize]
        [HttpPost("unfavorite")]
        public async Task<ActionResult> DeleteFavorite([FromBody] FavoriteRequestModel favoriteRequest)
        {
            await _userService.RemoveFavorite(favoriteRequest);
            return Ok();
        }

        [Authorize]
        [HttpGet("{id:int}/movie/{movieId}/favorite")]
        public async Task<ActionResult> IsFavoriteExists(int id, int movieId)
        {
            var favoriteExists = await _userService.FavoriteExists(id, movieId);
            return Ok(new { isFavorited = favoriteExists });
        }


        [Authorize]
        [Route("favorites")]
        [HttpGet]
        public async Task<IActionResult> GetUserFavoriteMoviesAsync()
        {
            var userId = _currentUserService.UserId;
            var favorites = await _userService.GetFavoriteByUserId(userId);
            // call the User Service to get movies Favorited by user, and send the data to the view, and use the existing MovieCard partial View

            return Ok(favorites);
        }

        [Authorize]
        [HttpPost("review")]
        public async Task<ActionResult> CreateReview([FromBody] ReviewRequestModel reviewRequest)
        {
            await _userService.AddMovieReview(reviewRequest);
            return Ok();
        }
        [Authorize]
        [HttpPut("review")]
        public async Task<ActionResult> UpdateReview([FromBody] ReviewRequestModel reviewRequest)
        {
            await _userService.UpdateMovieReview(reviewRequest);
            return Ok();
        }

        [Authorize]
        [HttpDelete("{userId:int}/movie/{movieId:int}")]
        public async Task<ActionResult> DeleteReview(int userId, int movieId)
        {
            await _userService.DeleteMovieReview(userId, movieId);
            return NoContent();
        }

        [Authorize]
        [HttpGet("{id:int}/reviews")]
        public async Task<ActionResult> GetUserReviewedMoviesAsync(int id)
        {
            var userMovies = await _userService.GetAllReviewsByUser(id);
            return Ok(userMovies);
        }








    }
}
