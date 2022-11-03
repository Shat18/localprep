using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalPrep.Entity
{
   public class UserEntity
    {
        public int? Step {get; set;}
        public string Id { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public int StateId { get; set; }
        public string Zip { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public DateTime? LockoutEndDateUtc { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Website { get; set; }
        public string CuisinesSelected { get; set; }
        public string DietsSelected { get; set; }
        public string CookingStyle { get; set; }
        public byte[] ProfilePic { get; set; }
    }
   public class APIResponsecs
    {
        public bool Error
        {
            get;
            set;
        }
        public string Message
        {
            get;
            set;
        }
        public string Id { get; set; }

        //public List<UserEntity> Data = new List<UserEntity>();

    }

    public class LoginModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }


    public class UserModel
    {
        public int? Step { get; set; }
        public string Id { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public int StateId { get; set; }
        public string Zip { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public DateTime? LockoutEndDateUtc { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Website { get; set; }
        public string CuisinesSelected { get; set; }
        public string DietsSelected { get; set; }
        public string CookingStyle { get; set; }
        public byte[] ProfilePic { get; set; }
    }
}
