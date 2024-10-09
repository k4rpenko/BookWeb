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
        private readonly JWT _jwt = new JWT();
        public Verification(AppDbContext _context) { context = _context; }

        [HttpPut("TokenUpdate")]
        public async Task<IActionResult> AccessToken(TokenModel _tokenM)
        {
            try
            {
                var id = _jwt.GetUserIdFromToken(_tokenM.Jwt);
                var user = context.User.FirstOrDefault(u => u.Id == id);
                var refreshToke = context.UserTokens.FirstOrDefault(t => t.UserId == id);
                if (_jwt.ValidateToken(refreshToke.Value) == false)
                {
                    refreshToke.Value = null;
                    await context.SaveChangesAsync();
                    return Unauthorized();
                }
                var accessToken = _jwt.GenerateJwtToken(id, user.ConcurrencyStamp, 1);
                return Ok(new { token = accessToken});
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
