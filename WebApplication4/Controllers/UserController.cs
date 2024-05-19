using Microsoft.AspNetCore.Mvc;
using WebApplication4.Models;
using WebApplication4.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace WebApplication4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<User>>> GetAll()
        {
            var username = User.Identity.Name;
            var data = await _context.Users.Include(u => u.DocumentHeaders).ThenInclude(dh => dh.DocumentLines).ToListAsync();
            return Ok(new { data, Username = username });
        }
    }
}
