using api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers.Store
{
    [ApiController]
    [Route("api/[controller]")]
    public class StoreController : ControllerBase
    {
        private readonly AppDbContext _context;

        public StoreController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("store")]
        public async Task<IActionResult> GetStore(int page, int pageSize = 3)
        {
            var products = await _context.PortoflioMedia
                                 .Skip((page - 1) * pageSize)
                                 .Take(pageSize)
                                 .Where(p => p.ForSale == true)
                                 .ToListAsync();

            return Ok(products);
        }


    }
}
