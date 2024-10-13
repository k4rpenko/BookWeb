using BDLearn.Models;
using LibraryBLL;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace BDLearn.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Account : Controller
    {
        private readonly AppDbContext context;
        public Account(AppDbContext _context) { context = _context; }

        [HttpGet("{id}")]
        public async Task<IActionResult> AccountInformatoin(string id)
        {
            var user = context.User.FirstOrDefault(u => u.Id == id);
            if (user != null) {
                var Account = new 
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Avatar = user.Avatar,
                    Books = user.Books,
                    UserName = user.UserName,
                    Email =  user.Email,
                    ConcurrencyStamp = user.ConcurrencyStamp,
                    PhoneNumber = user.PhoneNumber
                };
                return Ok(new { Account = Account });
            }
            return NotFound(new { message = "Invalid Id" });
        }
    }
}
