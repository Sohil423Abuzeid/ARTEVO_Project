using api.Models;
using api.Models.Dto.PortofliosDto;
using api.Models.Portoflios;
using api.Models.Users;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace api.Controllers.Portoflios
{
    [Route("api/[controller]")]
    [ApiController]
    public class PortofliosController : ControllerBase
    {
        // portofolio creation 
        /*
         * - Implement file upload functionality (backend API endpoint)
         * - Validate file type and size (e.g., JPEG, PNG, MP4)
         * - Store uploaded files in cloud storage (e.g., S3)
         * - Associate uploaded files with artist profile in database 
         */

        // CRUD operations   

        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PortofliosController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            _context = context;
        }

        // the most confusing function in linking with front end
        [HttpGet("portfolio")]
        public async Task<IActionResult> GetPortfolio(int artistId)
        {
            var artist = await _context.Artists
                    .Include(a => a.Portoflio)
                    .ThenInclude(p => p.Files)
                    .FirstOrDefaultAsync(a => a.Id == artistId);

            if (artist is null || artist.Portoflio is null)
            {
                return NotFound();
            }

            return Ok(artist.Portoflio.Files);
        }

        [HttpGet("forSale")]
        public async Task<IActionResult> GetForSale(int artistId)
        {
            var artist = await _context.Artists
                    .Include(a => a.Portoflio)
                    .ThenInclude(p => p.Files)
                    .FirstOrDefaultAsync(a => a.Id == artistId);

            if (artist is null || artist.Portoflio is null)
            {
                return NotFound();
            }

            var portoflio = artist?.Portoflio;
            var sellableFiles = portoflio!.Files.Where(f => f.ForSale == true);

            return Ok(sellableFiles);
        }
    }
}
