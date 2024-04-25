using api.Models.Portoflios;
using System.ComponentModel.DataAnnotations;

namespace api.Models.Dto.Users
{
    public class ArtistDto
    {
        public required string Name { get; set; }
        public string Bio { get; set; } = "";

        public Uri? Facebook { get; set; }
        public Uri? Twitter { get; set; }

        [EmailAddress]
        public string Email { get; set; } = "";
        [Phone]
        public string PhoneNumber { get; set; } = "";
    }
}