using BDLearn.Hash;
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
        HASH _HASH = new HASH();
        private readonly JWT _jwt = new JWT();
        public AccountSettings(AppDbContext _context) { context = _context; }

        [HttpPost("ConfirmationAccount")]
        public async Task<IActionResult> CheckingPassword(TokenModel Account)
        {
            try
            {
                if (Account.Jwt != null && _jwt.ValidateToken(Account.Jwt))
                {
                    var id = _jwt.GetUserIdFromToken(Account.Jwt);
                    var user = context.User.FirstOrDefault(u => u.Id == id);
                    if (user != null)
                    {
                        user.EmailConfirmed = true;
                        await context.SaveChangesAsync();
                    }
                    var accets = _jwt.GenerateJwtToken(id, user.ConcurrencyStamp, 1);
                    return Ok(new { token = accets });
                }
                return NotFound(new { message = "Invalid Token" });
            }
            catch (Exception ex)
            {
                throw new Exception("", ex);
            }
        }

        [HttpPost("ChangePassword")]
        public async Task<IActionResult> CheckingPassword(AccountModel Account)
        {
            try
            {
                if (Account.Password != null  && _jwt.ValidateToken(Account.Jwt))
                {
                    var id = _jwt.GetUserIdFromToken(Account.Jwt);
                    var user = await context.Users.FindAsync(id);
                    if (user != null)
                    {
                        string HashNewPassword = _HASH.Encrypt(Account.NewPassword, user.ConcurrencyStamp);
                        string HashPassword = _HASH.Encrypt(Account.Password, user.ConcurrencyStamp);
                        if (HashPassword == user.PasswordHash)
                        {
                            user.PasswordHash = HashNewPassword;
                            await context.SaveChangesAsync();
                            return Ok();
                        }
                        return Unauthorized("Invalid credentials");
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
