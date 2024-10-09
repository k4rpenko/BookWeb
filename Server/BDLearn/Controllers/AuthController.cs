﻿using BDLearn.Hash;
using BDLearn.Models;
using BDLearn.Sending;
using LibraryBLL;
using LibraryDAL.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RedisDAL;

namespace BDLearn.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly EmailSeding _emailSend = new EmailSeding();
        private readonly AppDbContext context;
        private readonly RedisConfigure redis;
        private readonly JWT _jwt = new JWT();
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

                    var KeyG = BitConverter.ToString(_HASH.GenerateKey()).Replace("-", "").ToLower();
                    int nextUserNumber = await context.User.CountAsync() + 1;
                    var newUser = new UserModel
                    {
                        Email = _user.Email,
                        ConcurrencyStamp = KeyG,
                        PasswordHash = _HASH.Encrypt(_user.Password, KeyG),
                        UserName = $"User{nextUserNumber}",
                        FirstName = "User"
                    };

                    context.User.Add(newUser);
 

                    var newToken = new IdentityUserToken<string> 
                    {
                        UserId = newUser.Id,
                        LoginProvider = "Default",
                        Name = newUser.UserName,
                        Value = _jwt.GenerateJwtToken(newUser.Id, KeyG, 720)
                    };

                    context.UserTokens.Add(newToken); 
                    

                    var UserRoleID = context.Roles.FirstOrDefault(u => u.Name == "User");
                    var UserRole = new IdentityUserRole<string>
                    {
                        UserId = newUser.Id,
                        RoleId = UserRoleID.Id
                    };


                    context.UserRoles.Add(UserRole);
                    await context.SaveChangesAsync();
                   
                    var userId = newUser.Id;
                    var record = await context.User.FindAsync(userId);

                    if (record != null)
                    {
                        var RefreshToken = newToken.Value;
                        
                        await context.SaveChangesAsync();
                        await _emailSend.PasswordCheckEmailAsync(_user.Email, _jwt.GenerateJwtToken(userId, KeyG, 1), Request.Scheme, Request.Host.ToString());
                        return Ok();
                    }
                }
                if (user.EmailConfirmed == false)
                {
                    return BadRequest(new { message = "Ваша електронна адреса не підтверджена." });
                }
                return Unauthorized(new { message = "Цей користувач є в базі даних." });


            }
            catch (Exception ex)
            {
                throw new Exception("", ex);
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
                    if (_HASH.Encrypt(_user.Password, user.ConcurrencyStamp) != user.PasswordHash) { return Unauthorized(new { message = "Invalid email or password" }); }
                    if (user.EmailConfirmed == false)
                    {
                        return BadRequest(new { message = "Ваша електронна адреса не підтверджена." });
                    }
                    var accets = _jwt.GenerateJwtToken(user.Id, user.ConcurrencyStamp, 1);
                    return Ok(new { token = accets });
                }
                catch (Exception ex)
                {
                    throw new Exception("", ex);
                }
            }
            return StatusCode(StatusCodes.Status429TooManyRequests, new { message = "Too many requests from this IP address" });
        }
    }
}
