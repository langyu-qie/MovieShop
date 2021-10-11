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
using AutoMapper;
using ApplicationCore.Exceptions;

namespace Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly IFavoriteRepository _favoriteRepository;
        private readonly IMovieService _movieService;
        private readonly IAsyncRepository<Review> _reviewRepository;
        public UserService(IUserRepository userRepository, IPurchaseRepository purchaseRepository, IFavoriteRepository favoriteRepository, IMovieService movieService,
            IAsyncRepository<Review> reviewRepository)
        {
            _userRepository = userRepository;
            _purchaseRepository = purchaseRepository;
            _favoriteRepository = favoriteRepository;
            _movieService = movieService;
            _reviewRepository = reviewRepository;
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
                userDetails.Reviews.Add(new MovieReviewResponseModel
                {

                    MovieId = userReview.MovieId,
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

        public async Task AddFavorite(FavoriteRequestModel favoriteRequest)
        {
            // See if Movie is already Favorite.
            if (await FavoriteExists(favoriteRequest.UserId, favoriteRequest.MovieId))
                throw new ConflictException("Movie already Favorited");

            var favorite = new Favorite
            {
                UserId = favoriteRequest.UserId,
                MovieId = favoriteRequest.MovieId
            };


            await _favoriteRepository.AddAsync(favorite);
        }

        public async Task RemoveFavorite(FavoriteRequestModel favoriteRequest)
        {
            var dbFavorite =
                await _favoriteRepository.ListAsync(r => r.UserId == favoriteRequest.UserId &&
                                                         r.MovieId == favoriteRequest.MovieId);
            await _favoriteRepository.DeleteAsync(dbFavorite.First());
        }

        public async Task<bool> FavoriteExists(int id, int movieId)
        {
            return await _favoriteRepository.GetExistsAsync(f => f.MovieId == movieId &&
                                                                 f.UserId == id);
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

        public async Task<bool> PurchaseMovie(PurchaseRequestModel purchaseRequest, int userId)
        {
            // See if Movie is already purchased.
            if (await IsMoviePurchased(purchaseRequest, userId))
                throw new ConflictException("Movie already Purchased");
            // Get Movie Price from Movie Table
            var movie = await _movieService.MovieDetailsById(purchaseRequest.MovieId);

            var purchase = new Purchase
            {
                MovieId = purchaseRequest.MovieId,
                PurchaseNumber = Guid.NewGuid(),
                PurchaseDateTime = DateTime.UtcNow,
                TotalPrice = movie.Price.GetValueOrDefault(),
                UserId = userId
            };
            //  var purchase = _mapper.Map<Purchase>(purchaseRequest);
            var createdPurchase = await _purchaseRepository.AddAsync(purchase);
            return createdPurchase.Id > 0;
        }

        public async Task<bool> IsMoviePurchased(PurchaseRequestModel purchaseRequest, int userId)
        {
            return await _purchaseRepository.GetExistsAsync(p =>
                p.UserId == userId && p.MovieId == purchaseRequest.MovieId);
        }

        public async Task AddMovieReview(ReviewRequestModel reviewRequest)
        {
            var review = new Review
            {
                UserId = reviewRequest.UserId,
                MovieId = reviewRequest.MovieId,
                ReviewText = reviewRequest.ReviewText,
                Rating = reviewRequest.Rating 

            };
            await _reviewRepository.AddAsync(review);
        }

        public async Task UpdateMovieReview(ReviewRequestModel reviewRequest)
        {
            var review = new Review
            {
                UserId = reviewRequest.UserId,
                MovieId = reviewRequest.MovieId,
                ReviewText = reviewRequest.ReviewText,
                Rating = reviewRequest.Rating
            };

            await _reviewRepository.UpdateAsync(review);
        }

        public async Task DeleteMovieReview(int userId, int movieId)
        {
            var review = await _reviewRepository.ListAsync(r => r.UserId == userId && r.MovieId == movieId);
            await _reviewRepository.DeleteAsync(review.First());
        }


        public async Task<UserReviewResponseModel> GetAllReviewsByUser(int id)
        {
            var userReviews = await _userRepository.GetReviewsByUser(id);
            var userReviewModel = new UserReviewResponseModel
            {
                MovieReviews = new List<MovieReviewResponseModel>(),
                UserId = id
            };

            userReviewModel.MovieReviews = userReviews.Select(ur => new MovieReviewResponseModel
            {
                UserId = id,
                MovieId = ur.MovieId,
                Rating = ur.Rating,
                ReviewText = ur.ReviewText
            }).ToList();

            return userReviewModel;
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
