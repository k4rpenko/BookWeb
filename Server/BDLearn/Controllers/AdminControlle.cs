using BDLearn.Models;
using LibraryBLL;
using LibraryDAL.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BDLearn.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminControlle : Controller
    {
        private readonly AppDbContext context;

        public AdminControlle(AppDbContext _context)
        {
            context = _context;
        }

        private bool Review(AdminModel _AdminModel, IdentityRole Admin)
        {
            if (Admin == null) { return false; }
            if (Admin.Name != "Admin") { return false; }
            return true;
        }

        [HttpDelete("deleteuser")]
        public async Task<IActionResult> DeleteUser([FromBody] AdminModel _AdminModel)
        {
            if (_AdminModel.IdAdmin == null) { return NotFound(new { message = "IdAdmin == Null" }); }
            var Admin = await context.Roles.FindAsync(_AdminModel.IdAdmin);
            if (!Review(_AdminModel, Admin)) { return NotFound(); }

            var User = await context.User.FindAsync(_AdminModel.IdUser);
            if (User == null) { return NotFound(new { message = "User == null" }); }

            context.User.Remove(User);
            await context.SaveChangesAsync();
            return Ok();
            
        }

        [HttpPost("blockuser")]
        public async Task<IActionResult> BlockUser([FromBody] AdminModel _AdminModel)
        {
            if (_AdminModel.IdAdmin == null) { return NotFound(new { message = "IdAdmin == Null" }); }
            var Admin = await context.Roles.FindAsync(_AdminModel.IdAdmin);
            if (!Review(_AdminModel, Admin)) { return NotFound(); }

            var User = await context.User.FindAsync(_AdminModel.IdUser);
            if (User == null) { return NotFound(new { message = "User == null" }); }

            User.LockoutEnd = _AdminModel.Blocked;
            await context.SaveChangesAsync();
            return Ok();
            
        }

        [HttpPost("showusernick")]
        public async Task<IActionResult> ShowUserToNick([FromBody] AdminModel _AdminModel)
        {
            if (_AdminModel.IdAdmin == null) { return NotFound(new { message = "IdAdmin == Null" }); }
            var Admin = await context.Roles.FindAsync(_AdminModel.IdAdmin);
            if (!Review(_AdminModel, Admin)) { return NotFound(); }

            var user = await context.User.FindAsync(_AdminModel.IdUser);
            if (user == null) { return NotFound(new { message = "User == null" }); }


            return Ok(new { user });
        }
    }
}
