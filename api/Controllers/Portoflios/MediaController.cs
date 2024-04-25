using api.Models;
using api.Models.Dto.PortofliosDto;
using api.Models.Portoflios;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers.Portoflios
{
    [Route("api/[controller]")]
    [ApiController]
    public class MediaController : ControllerBase
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly AppDbContext _context;

        public MediaController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            _context = context;
        }

        // Get Poject
        [HttpGet("GetFile")] 
        public async Task<IActionResult> GetFile(int fileId)
        {
            var file = await _context.PortoflioMedia.FirstOrDefaultAsync(f => f.Id == fileId);
            if (file is null)
            {
                return NotFound();
            }

            return Ok(file);
        }

        [HttpGet("GetFiles")]
        public async Task<IActionResult> GetFiles()
        {
            var files = await _context.PortoflioMedia.ToListAsync();
            return Ok(files);
        }

        // Post Poject 
        [HttpPost("PostFile")]
        public async Task<IActionResult> PostFile(int artistId, MediaDto portoflioMedia)
        {

            var artist = await _context.Artists
                    .Include(a => a.Portoflio)
                    //.ThenInclude(p => p.Files)
                    .FirstOrDefaultAsync(a => a.Id == artistId);

            if (artist is null)
            {
                return NotFound();
            }

            if (artist.Portoflio is null)
            {
                // Create a new Portoflio
                artist.Portoflio = new Portoflio {
                    Files = new List<PortoflioMedia>(), 
                    Artist = artist,
                    ArtistId = artistId
                };

                await _context.Portoflios.AddAsync(artist.Portoflio);
                await _context.SaveChangesAsync();
            }

            var filePath = await SaveFile(portoflioMedia.FormFile);
            var file = new PortoflioMedia
            {
                FormFile = portoflioMedia.FormFile,
                Path = filePath,
                Artist = artist,
                Portoflio = artist.Portoflio,
                Description = portoflioMedia.Description,
                LongDescription = portoflioMedia.LongDescription, 
                ForSale = portoflioMedia.ForSale,
                Price = portoflioMedia.Price, 
                
            };

            // Add the file to the Portoflio
            await _context.PortoflioMedia.AddAsync(file);
            _context.Portoflios.Update(artist.Portoflio);
            _context.Artists.Update(artist);

            await _context.SaveChangesAsync();

            return Ok(); 
        }

        // there is trick in the id here 
        [HttpPut("EditFile")]
        public async Task<IActionResult> EditFile(int artistId,int fileId, MediaDto portoflioMedia)
        {
            var file = await _context.PortoflioMedia
                .FirstOrDefaultAsync(f => f.Id == fileId);

            if (file is null)
            {
                return NotFound();
            }

            _context.PortoflioMedia.Remove(file);
            await _context.SaveChangesAsync();

            await PostFile(artistId, portoflioMedia);
            return Ok(file); 
        }

        [HttpDelete("DeleteFile")]
        public async Task<IActionResult> DeleteFile(int fileId)
        {
            var file = await _context.PortoflioMedia.FirstOrDefaultAsync(f => f.Id == fileId);
            if (file is null)
            {
                return NotFound();
            }
            _context.PortoflioMedia.Remove(file);
            await _context.SaveChangesAsync();
            return Accepted(); // 202 Accepted
        }
       
        private async Task<string> SaveFile(IFormFile file)
        {
            if (!(file.Length > 0))
                return "";

            //Create the "Images" folder if it doesn't exist
            var imagesFolderPath = Path.Combine(_webHostEnvironment.ContentRootPath, "Images");

            if (!Directory.Exists(imagesFolderPath))
            {
                Directory.CreateDirectory(imagesFolderPath);
            }

            //Combine paths with proper directory separator
            var filePath = Path.Combine(imagesFolderPath, Path.GetRandomFileName() + Path.GetExtension(file.FileName));

            // Use a safer approach for file creation (avoid overwriting)
            using (var fileStream = new FileStream(filePath, FileMode.CreateNew))
            {
                await file.CopyToAsync(fileStream);
            }

            return filePath;
        }
    }
}