using BDLearn.Models;
using LibraryBLL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RedisDAL;
using StackExchange.Redis;

namespace BDLearn.Controllers
{


    [ApiController]
    [Route("api/[controller]")]
    public class Verification : Controller
    {
        private readonly AppDbContext context;
        private readonly JWT _jwt;
        public Verification(AppDbContext _context) { context = _context; }

        [HttpGet("TokenUpdate")]
        public async Task<IActionResult> AccessToken(TokenModel _tokenM)
        {
            try
            {
                if (_jwt.ValidateToken(_tokenM.Jwt)){

                }
                else
                {
                    //var user = context.User.FirstOrDefault(u => u.Email == );
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
