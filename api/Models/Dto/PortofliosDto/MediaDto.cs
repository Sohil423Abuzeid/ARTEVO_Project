namespace api.Models.Dto.PortofliosDto
{
    public class MediaDto
    {

        public required IFormFile FormFile { get; set; }

        public string Description { get; set; } = string.Empty;

        public string LongDescription { get; set; } = string.Empty;

        public bool ForSale { get; set; }

        public int Price { get; set; }
    }
}
