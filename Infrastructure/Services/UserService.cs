using ApplicationCore.Models;
using ApplicationCore.ServiceInterfaces;
using System;
using System.Collections.Generic;

using System.Security.Cryptography;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.Entities;

namespace Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly IFavoriteRepository _favoriteRepository;

        public UserService(IUserRepository userRepository, IPurchaseRepository purchaseRepository, IFavoriteRepository favoriteRepository)
        {
            _userRepository = userRepository;
            _purchaseRepository = purchaseRepository;
            _favoriteRepository = favoriteRepository;
        }

        //public async Task<UserEditRequestModel> UpdateUser(UserEditRequestModel userEditModel)
        //{
        //    var user = await _userRepository.GetUserByEmail(userEditModel.Email);


        //}
        public async Task<UserDetailResponseModel> UserDetailById(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            var userDetails = new UserDetailResponseModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                DateOfBirth = user.DateOfBirth.GetValueOrDefault(),
                Email = user.Email,
                PhoneNumber = user.PhoneNumber
            };

            foreach (var userFavorite in user.Favorites)
                userDetails.Favorites.Add(new FavoriteResponseModel
                {
                    Id = userFavorite.Id,
                    MovieId = userFavorite.MovieId,
                    UserId = userFavorite.UserId

                });
            foreach (var userPurchase in user.Purchases)
                userDetails.Purchases.Add(new PurchaseResponseModel
                {
                    Id = userPurchase.Id,
                    MovieId = userPurchase.MovieId,
                    PurchaseNumber = userPurchase.PurchaseNumber,
                    TotalPrice = userPurchase.TotalPrice,
                    PurchaseDateTime = userPurchase.PurchaseDateTime,
                    UserId = userPurchase.UserId

                });
            foreach (var userReview in user.Reviews)
                userDetails.Reviews.Add(new ReviewResponseModel
                {

                    Id = userReview.MovieId,
                    UserId = userReview.UserId,
                    Rating = userReview.Rating,
                    ReviewText = userReview.ReviewText
                });


            return userDetails;
        }


        public async Task<User> GetUser(string email)
        {
            return await _userRepository.GetUserByEmail(email);
        }
        public async Task<IEnumerable<MovieCardResponseModel>> GetFavoriteByUserId(int userId)
        {
            var favorites = await _favoriteRepository.ListAsync(f => f.UserId == userId);
            var movieCardResponseModel = new List<MovieCardResponseModel>();

            if (favorites == null)
            {
                throw new Exception($"No favorite movie for this user");
            }



            foreach (var favorite in favorites)
            {
                movieCardResponseModel.Add(new MovieCardResponseModel
                {
                    Id = favorite.Movie.Id,
                    PosterUrl = favorite.Movie.PosterUrl,
                    Revenue = favorite.Movie.Revenue.GetValueOrDefault(),
                    Title = favorite.Movie.Title

                });

            };

            return movieCardResponseModel;



        }




        public async Task<IEnumerable<MovieCardResponseModel>> GetPurchaseByUserId(int userId)
        {

            var purchases = await _purchaseRepository.ListAsync(p => p.UserId == userId);
            var movieCardResponseModel = new List<MovieCardResponseModel>();

            if (purchases == null)
            {
                throw new Exception($"No purchase has been made for this user");
            }



            foreach (var purchase in purchases)
            {
                movieCardResponseModel.Add(new MovieCardResponseModel
                {
                    Id = purchase.Movie.Id,
                    PosterUrl = purchase.Movie.PosterUrl,
                    Revenue = purchase.Movie.Revenue.GetValueOrDefault(),
                    Title = purchase.Movie.Title


                });

            };

            return movieCardResponseModel;

        }





        public async Task<UserRegisterResponseModel> RegisterUser(UserRegisterRequestModel requestModel)
        {
            // first check if the email user entered exists in the database
            // if yes, throw an throw exception or send a message saying email exists
            var user = await _userRepository.GetUserByEmail(requestModel.Email);

            if (user != null)
            {
                // email exits in the database
                throw new Exception($"Email {requestModel.Email} exists, please try to login");
            }
            // continue
            // create a random salt and hash the password with the salt

            var salt = GenerateSalt();
            var hashedPassword = GenerateHashedPassword(requestModel.Password, salt);

            // create user entity object and call user repo to save
            var newUser = new User
            {
                Email = requestModel.Email,
                FirstName = requestModel.FirstName,
                LastName = requestModel.LastName,
                DateOfBirth = requestModel.DateOfBirth,
                Salt = salt,
                HashedPassword = hashedPassword
            };

            var createdUser = await _userRepository.AddAsync(newUser);

            var userRegisterResponseModel = new UserRegisterResponseModel
            {
                Id = createdUser.Id,
                Email = createdUser.Email,
                FirstName = createdUser.FirstName,
                LastName = createdUser.LastName
            };

            return userRegisterResponseModel;

        }


        public async Task<UserLoginResponseModel> ValidateUser(string email, string password)
        {
            // get the user info from database by email
            var user = await _userRepository.GetUserByEmail(email);
            if (user == null)
            {
                // we dont have the email in the database
                return null;
            }

            // we need to hash the user entered password along with salt from database.
            var hashedPassword = GenerateHashedPassword(password, user.Salt);

            if (hashedPassword == user.HashedPassword)
            {
                // user entered correct password
                var userLoginResponseModel = new UserLoginResponseModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    DateOfBirth = user.DateOfBirth
                };
                return userLoginResponseModel;
            }

            return null;

        }


        private string GenerateSalt()
        {
            byte[] randomBytes = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
            }

            return Convert.ToBase64String(randomBytes);
        }

        private string GenerateHashedPassword(string password, string salt)
        {
            var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: Convert.FromBase64String(salt),
                prf: KeyDerivationPrf.HMACSHA512,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
            return hashed;
        }





       
    }
}
