﻿using BDLearn.Models;
using LibraryBLL;
using LibraryDAL.Model;
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

        private bool Review(AdminModel _AdminModel, UserModel Admin)
        {
            if (Admin == null) { return false; }
            if (Admin.Role != "Admin") { return false; }
            return true;
        }

        [HttpDelete("deleteuser")]
        public async Task<IActionResult> DeleteUser([FromBody] AdminModel _AdminModel)
        {
            if (_AdminModel.IdAdmin == null) { return NotFound(new { message = "IdAdmin == Null" }); }
            var Admin = await context.User.FindAsync(Guid.Parse(_AdminModel.IdAdmin));
            if (!Review(_AdminModel, Admin)) { return NotFound(); }

            var User = await context.User.FindAsync(Guid.Parse(_AdminModel.IdUser));
            if (User != null)
            {
                context.User.Remove(User);
                await context.SaveChangesAsync();
                return Ok();
            }
            return NotFound(new { message = "User == null" });
        }

        [HttpPost("blockuser")]
        public async Task<IActionResult> BlockUser([FromBody] AdminModel _AdminModel)
        {
            if (_AdminModel.IdAdmin == null) { return NotFound(new { message = "IdAdmin == Null" }); }
            var Admin = await context.User.FindAsync(Guid.Parse(_AdminModel.IdAdmin));
            if (!Review(_AdminModel, Admin)) { return NotFound(); }

            var User = await context.User.FindAsync(Guid.Parse(_AdminModel.IdUser));
            if (User != null)
            {
                User.Blocked = _AdminModel.Blocked;
                await context.SaveChangesAsync();
                return Ok();
            }
            return NotFound(new { message = "User == null" });
        }

        [HttpPost("showusernick")]
        public async Task<IActionResult> ShowUserToNick([FromBody] AdminModel _AdminModel)
        {
            if (_AdminModel.IdAdmin == null) { return NotFound(new { message = "IdAdmin == Null" }); }
            var Admin = await context.User.FindAsync(Guid.Parse(_AdminModel.IdAdmin));
            if (!Review(_AdminModel, Admin)) { return NotFound(); }

            // Example logic to show user nick
            var user = await context.User.FindAsync(Guid.Parse(_AdminModel.IdUser));
            if (user != null)
            {
                return Ok(new { user });
            }
            return NotFound(new { message = "User == null" });
        }
    }
}
