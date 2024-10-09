using BDLearn.Models;
using BDLearn.Sending;
using LibraryBLL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RedisDAL;
using StackExchange.Redis;


namespace BDLearn.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountSettings : Controller
    {
        private readonly EmailSeding _emailSend = new EmailSeding();
        private readonly AppDbContext context;
        public AccountSettings(AppDbContext _context) { context = _context; }

        [HttpPost("CheckingPassword")]
        public async Task<IActionResult> CheckingPassword(TokenModel _PasswordM)
        {
            try
            {
                if (_PasswordM.Jwt != null && new JWT().ValidateToken(_PasswordM.Jwt))
                {
                    var id = new JWT().GetUserIdFromToken(_PasswordM.Jwt);
                    var user = await context.Users.FindAsync(id);
                    if (user != null)
                    {
                        user.EmailConfirmed = true;
                        await context.SaveChangesAsync();
                    }
                    return Ok();
                }
                return NotFound(new { message = "Invalid Token" });
            }
            catch (Exception ex)
            {
                throw new Exception("", ex);
            }
        }

        [HttpPost("ConfirmationEmail")]
        public async Task<IActionResult> ConfirmationEmail(string email)
        {
            try
            {
                if (email != null)
                {
                    var user = await context.Users.FindAsync(email);
                    if (user != null)
                    {
                        await _emailSend.PasswordCheckEmailAsync(user.Email, new JWT().GenerateJwtToken(user.Id, user.ConcurrencyStamp, 1), Request.Scheme, Request.Host.ToString());
                        return Ok();
                    }
                }
                return NotFound(new { message = "Invalid Token" });
            }
            catch (Exception ex)
            {
                throw new Exception("", ex);
            }
        }
    }
}
