using BDLearn.Hash;
using BDLearn.Models;
using LibraryBLL;
using LibraryDAL.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RedisDAL;

namespace BDLearn.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {

        private readonly AppDbContext context;
        private readonly RedisConfigure redis;
        HASH _HASH = new HASH();
        public AuthController(AppDbContext _context, RedisConfigure _redis) { context = _context; redis = _redis; }



        [HttpPost("registration")]
        public async Task<IActionResult> CreateUser(UserAuth _user)
        {
            if (string.IsNullOrWhiteSpace(_user.Email) || string.IsNullOrWhiteSpace(_user.Password)) { return BadRequest(new { message = "Email and Password cannot be null or empty" }); }
            try
            {
                var user = context.User.FirstOrDefault(u => u.Email == _user.Email);
                if (user == null)
                {
                    int nextUserNumber = await context.User.CountAsync() + 1;
                    var newUser = new UserModel
                    {
                        Email = _user.Email,
                        Password = _HASH.Encrypt(_user.Password),
                        Nick = $"User{nextUserNumber}",
                        Role = "User"
                    };
                    Console.WriteLine(_HASH.Encrypt(_user.Password));
                    context.User.Add(newUser);
                    await context.SaveChangesAsync();
                    var userId = newUser.Id;
                    var record = await context.User.FindAsync(userId);
                    if (record != null)
                    {
                        var RefreshToken = new JWT().GenerateJwtToken(userId);
                        record.RefreshToken = RefreshToken;
                        await context.SaveChangesAsync();
                        return Ok(RefreshToken);
                    }
                }
                return Unauthorized(new { message = "This user is in the database" });

            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            } 
        }


        [HttpPost("login")]
        public async Task<IActionResult> LoginUser(UserAuth _user)
        {
            var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
            if (redis.AuthRedisUser(ipAddress))
            {
                if (string.IsNullOrWhiteSpace(_user.Email) || string.IsNullOrWhiteSpace(_user.Password)) { return BadRequest(new { message = "Email and Password cannot be null or empty" }); }
                try
                {
                    var user = context.User.FirstOrDefault(u => u.Email == _user.Email);
                    if (user == null) { return NotFound(); }
                    if (_HASH.Encrypt(_user.Password) != user.Password) { return Unauthorized(new { message = "Invalid email or password" }); }

                    return Ok(user.RefreshToken);
                }
                catch (Exception ex)
                {
                    return NotFound(ex.Message);
                }
            }
            return StatusCode(StatusCodes.Status429TooManyRequests, new { message = "Too many requests from this IP address" });
        }
    }
}
