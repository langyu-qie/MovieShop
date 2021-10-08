using ApplicationCore.Models;
using ApplicationCore.RepositoryInterfaces;
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
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IJwtService _jwtService;
        public AccountController(IUserService userService, IJwtService jwtService)
        {
            _userService = userService;
            _jwtService = jwtService;

        }

        [HttpGet]
        [Route("{id:int}", Name = "GetUser")]
        public async Task<ActionResult> GetUserByIdAsync(int id)
        {
            var user = await _userService.UserDetailById(id);
            return Ok(user);
        }

        [Route("register")]

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> RegisterUserAsync([FromBody] UserRegisterRequestModel user)
        {
            var createdUser = await _userService.RegisterUser(user);
            return CreatedAtRoute("GetUser", new { id = createdUser.Id }, createdUser);
        }

        [HttpGet]
        [Route("checkemail")]
        public async Task<ActionResult> EmailExists([FromQuery] string email)
        {
            var user = await _userService.GetUser(email);
            return Ok(user == null ? new { emailExists = false } : new { emailExists = true });
        }

        [HttpPost("login")]
        public async Task<ActionResult> LoginAsync([FromBody] UserLoginRequestModel loginRequest)
        {
            //   "email": "ab@example.com",
            //   "password": "Abhi1234!!"
            //    Roles Admin, SuperAdmin

            /* Abc123!!
             * 	"email": "abhilash@abhilash.com",
	            "password" : "Abc1234!!!",
            {
                "email": "Bill6@gmail.com",
                "password": "abc1234!"
            }
           */

            var user = await _userService.ValidateUser(loginRequest.Email, loginRequest.Password);
            if (user == null) return Unauthorized();

            return Ok(new { token = _jwtService.GenerateToken(user) });
        }









    }
}
