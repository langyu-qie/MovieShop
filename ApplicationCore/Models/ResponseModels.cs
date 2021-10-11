using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Models
{

    public class MovieCardResponseModel
    {
        public int Id { get; set; }
        public string PosterUrl { get; set; }
        public string Title { get; set; }

        public decimal? Revenue { get; set; }


        public decimal? Rating { get; set; }

    }

    public class MovieDetailsModel
    {
        public MovieDetailsModel()
        {
            Casts = new List<CastModel>();
            Genres = new List<GenreModel>();
            Reviews = new List<UserReviewResponseModel>();

            Trailers = new List<TrailerModel>();
        }


        public int Id { get; set; }
        public string Title { get; set; }
        public string Overview { get; set; }
        public string Tagline { get; set; }

        public decimal? Budget { get; set; }
        public int Year { get; set; }
        public decimal? Revenue { get; set; }

        public string PosterUrl { get; set; }
        public string BackdropUrl { get; set; }
        public DateTime? ReleaseDate { get; set; }

        public int? RunTime { get; set; }

        public string ImdbUrl { get; set; }
        public string TmdbUrl { get; set; }
        public decimal? Rating { get; set; }

        public decimal? Price { get; set; }

        public List<GenreModel> Genres { get; set; }
        public List<CastModel> Casts { get; set; }

        public List<UserReviewResponseModel> Reviews { get; set; }
        public List<TrailerModel> Trailers { get; set; }

    }

    public class CastModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Gender { get; set; }
        public string TmdbUrl { get; set; }
        public string ProfilePath { get; set; }

        public string Character { get; set; }
    }

    public class GenreModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
  
    public class UserReviewResponseModel
    {
        public int UserId { get; set; }
        public List<MovieReviewResponseModel> MovieReviews { get; set; }
    }
    

    public class MovieReviewResponseModel
    {
        public int UserId { get; set; }
        public int MovieId { get; set; }

        public string ReviewText { get; set; }
        public decimal Rating { get; set; }
        public string Name { get; set; }
    }

    public class TrailerModel
    {
        public int Id { get; set; }
        public string TrailerUrl { get; set; }
        public string Name { get; set; }
        public int MovieId { get; set; }

    }

    public class FavoriteResponseModel
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public int UserId { get; set; }

    }

    public class PurchaseResponseModel
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public Guid PurchaseNumber { get; set; }

        public Decimal TotalPrice { get; set; }
        public DateTime PurchaseDateTime { get; set; }
        public int MovieId { get; set; }
    }

    public class RoleModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class UserDetailResponseModel
    {

        public UserDetailResponseModel()
        {
            Reviews = new List<MovieReviewResponseModel>();

            Purchases = new List<PurchaseResponseModel>();

            Favorites = new List<FavoriteResponseModel>();

        }
        public int Id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public List<MovieReviewResponseModel> Reviews { get; set; }

        public List<PurchaseResponseModel> Purchases { get; set; }

        public List<FavoriteResponseModel> Favorites { get; set; }
    }

    public class UserEditResponseModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
    }

    public class UserLoginResponseModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }

        public List<RoleModel> Roles { get; set; }


    }

    public class UserRegisterResponseModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }

    public class CastDetailsResponseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string TmdbUrl { get; set; }
        public string ProfilePath { get; set; }
        public IEnumerable<MovieCardResponseModel> Movies { get; set; }
    }















}
