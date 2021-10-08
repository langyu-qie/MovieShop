using ApplicationCore.ServiceInterfaces;
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

        [Route("purchases")]
        [HttpGet]
        public async Task<IActionResult> Purchases()
        {
            var userId = _currentUserService.UserId;
            var purchases = await _userService.GetPurchaseByUserId(userId);
            // call the User Service to get movies purchased by user, and send the data to the view, and use the existing MovieCard partial View
            return Ok(purchases);
        }

        [Route("favorites")]
        [HttpGet]
        public async Task<IActionResult> Favorites()
        {
            var userId = _currentUserService.UserId;
            var favorites = await _userService.GetFavoriteByUserId(userId);
            // call the User Service to get movies Favorited by user, and send the data to the view, and use the existing MovieCard partial View

            return Ok(favorites);
        }













    }
}
