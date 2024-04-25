
using api.Models.Portoflios;
using System.ComponentModel.DataAnnotations;

namespace api.Models.Users
{
    public class Artist : IUser
    {
        public int Id { get; set; }
        public required string Name { get; set; } 
        public Portoflio? Portoflio { get; set; }
        public string Bio { get;  set; } = "";
        public Uri? Facebook { get;  set; }
        public Uri? Twitter { get;  set; }

        [EmailAddress]
        public string Email { get;  set; } = "";
        [Phone]
        public string PhoneNumber { get;  set; } = "";
     
        public int NumberOfFollowers { get; set; }
        public int NumberOfFollowing { get; set; }

        public bool isVerified { get; set; }
    }
}
