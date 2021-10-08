using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Models
{
    public class UserDetailResponseModel
    {

        public UserDetailResponseModel()
        {
            Reviews = new List<ReviewResponseModel>();

            Purchases = new List<PurchaseResponseModel>();

            Favorites = new List<FavoriteResponseModel>();

        }
        public int Id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public List<ReviewResponseModel> Reviews { get; set; }

        public List<PurchaseResponseModel> Purchases { get; set; }

        public List<FavoriteResponseModel> Favorites { get; set; }
    }
}
