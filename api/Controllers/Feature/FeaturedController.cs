using api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers.Feature
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeaturedController: ControllerBase
    {
        private readonly AppDbContext _context;

        public FeaturedController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("Artists")]
        public async Task<IActionResult> Artists()
        {
            var artists = await _context.Artists
                                        .Take(3)
                                        .ToListAsync();
            return Ok(artists);
        }

        public async Task<IActionResult> Pieces()
        {
            var pieces = await _context.PortoflioMedia.ToListAsync();
            return Ok(pieces);
        }
    }
}
