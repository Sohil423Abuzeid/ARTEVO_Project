using api.Models.Users;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata.Ecma335;

namespace api.Models.Portoflios
{
    public class Media
    {
        [Key]
        public int Id { get; set; }

        public string Path { get; set; }

        [NotMapped]
        public required IFormFile FormFile { get; set; } 
    }

    public class PortoflioMedia: Media
    {
        public Artist Artist { get; set; } 

        public Portoflio Portoflio { get; set; }

        public string Description { get; set; } = string.Empty;

        public string LongDescription { get; set; } = string.Empty;

        public bool ForSale { get; set; }

        public int Price { get; set; }
    }
}
