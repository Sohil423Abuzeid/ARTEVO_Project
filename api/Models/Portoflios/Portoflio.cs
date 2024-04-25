using api.Models.Users;

namespace api.Models.Portoflios
{
    public class Portoflio
    {
        public int Id { get; set; }
       
        public required List<PortoflioMedia> Files { get; set; }

        public int ArtistId { get; set; }

        public required Artist Artist { get; set; }
    }
}
