using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; } //128

        public string LastName { get; set; }//128

        public DateTime? DateOfBirth { get; set; }

        public string Email { get; set; }//256

        public string HashedPassword { get; set; }//1024

        public string Salt { get; set; }//1024

        public string PhoneNumber { get; set; }//16

        public Boolean? TwoFactorEnabled { get; set; }

        public DateTime? LockoutEndDate { get; set; }

        public DateTime? LastLoginDateTime { get; set; }

        public Boolean? IsLocked { get; set; }


        public int? AccessFailedCount { get; set; }

        public ICollection<Review> Reviews { get; set; }

        public ICollection<Purchase> Purchases { get; set; }

        public ICollection<Favorite> Favorites { get; set; }

        public ICollection<Role> Roles { get; set; }


    }
}
