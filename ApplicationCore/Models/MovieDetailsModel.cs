using ApplicationCore.Entities;
using System;
using System.Collections.Generic;

namespace ApplicationCore.Models
{
    public class MovieDetailsModel
    {
        public MovieDetailsModel()
        {
            Casts = new List<CastModel>();
            Genres = new List<GenreModel>();
            Reviews = new List<ReviewResponseModel>();

            Trailers = new List<TrailerModel>();
        }


        public int Id { get; set; }
        public string Title { get; set; }
        public string Overview { get; set; }
        public string Tagline { get; set; }

        public decimal? Budget { get; set; }
        public  int Year { get; set; }
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

        public List<ReviewResponseModel> Reviews { get; set; }
        public List<TrailerModel> Trailers { get; set; }

    }
}
