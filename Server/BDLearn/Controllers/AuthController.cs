using BDLearn.Hash;
using BDLearn.Models;
using LibraryBLL;
using LibraryDAL.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BDLearn.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly AppDbContext context;
        public AuthController(AppDbContext _context)
        {
            context = _context;
        }


        [HttpPost("registration")]
        public async Task<IActionResult> CreateUser(UserAuth _user)
        {
            if (_user.Email == null && _user.Password != null) { return NotFound(new { message = "Email && Password == Null" }); }
            try
            {
                var user = context.User.FirstOrDefault(u => u.Email == _user.Email);
                if (user == null)
                {
                    var newUser = new UserModel
                    {
                        Email = _user.Email,
                        Password = new SHA().ComputeSha256Hash(_user.Password),
                        UserName = "User",
                        Role = "User"
                    };
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
            if (_user.Email == null && _user.Password != null) { return NotFound(new { message = "Email && Password == Null" }); }
            try
            {
                var user = context.User.FirstOrDefault(u => u.Email == _user.Email);
                if (user != null)
                {
                    if (user.Password == new SHA().ComputeSha256Hash(_user.Password))
                    {
                        return Ok(user.RefreshToken);
                    }
                    return Unauthorized(new { message = "Invalid email or password" });
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
