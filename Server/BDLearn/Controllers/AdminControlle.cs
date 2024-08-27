using BDLearn.Models;
using LibraryBLL;
using LibraryDAL.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace BDLearn.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminControlle : Controller
    {
        readonly AppDbContext context;
        public AdminControlle(AppDbContext _context) { context = _context;}

        public bool Review(AdminModel _AdminModel, UserModel Admin)
        {
            if (Admin == null) { return false; }
            if (Admin.Role != "Admin") { return false; }
            return true;
        }

        [HttpDelete("deleteuser")]
        public async Task<IActionResult> DeleteUser(AdminModel _AdminModel)
        {
            if (_AdminModel.IdAdmin == null) { return NotFound(new { message = "Id == Null" }); }
            var Admin = await context.User.FindAsync(Guid.Parse(_AdminModel.IdAdmin));
            if(!Review(_AdminModel, Admin)) { return NotFound(); }

            var User = await context.User.FindAsync(Guid.Parse(_AdminModel.IdUser));
            if (User != null) {
                context.User.Remove(User);
                await context.SaveChangesAsync();
                return Ok();
            }
            return NotFound(new { message = "User == null" });
        }

        [HttpPost("blockuser")]
        public async Task<IActionResult> BlockUser(AdminModel _AdminModel)
        {
            if (_AdminModel.IdAdmin == null) { return NotFound(new { message = "Id == Null" }); }
            var Admin = await context.User.FindAsync(Guid.Parse(_AdminModel.IdAdmin));
            if (!Review(_AdminModel, Admin)) { return NotFound(); }

            var User = await context.User.FindAsync(Guid.Parse(_AdminModel.IdUser));
            if (User != null)
            {
                if(_AdminModel.Blocked == null) { User.Blocked = null; }
                else { User.Blocked = _AdminModel.Blocked; }
                await context.SaveChangesAsync();
                return Ok();
            }
            return NotFound(new { message = "User == null" });
        }

        [HttpPost("showuser")]
        public async Task<IActionResult> ShowUser(AdminModel _AdminModel)
        {
            if (_AdminModel.IdAdmin == null) { return NotFound(new { message = "Id == Null" }); }
            var Admin = await context.User.FindAsync(Guid.Parse(_AdminModel.IdAdmin));
            if (!Review(_AdminModel, Admin)) { return NotFound(); }


            return NotFound(new { message = "User == null" });
        }
    }
}
