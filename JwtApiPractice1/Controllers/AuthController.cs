using JwtApiPractice1.Data;
using JwtApiPractice1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NuGet.Common;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JwtApiPractice1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly ApplicationDbContext _context;

        public AuthController(IConfiguration config, ApplicationDbContext context)
        {
            _config = config;
            _context = context;
        }

        //// Sample in-memory user list
        //private List<usertbl> users = new List<usertbl>
        //{
        //    new usertbl { id = 1, username = "testuser", passwd = "password", email = "test@example.com", firstname = "John" }
        //};

        [HttpPost("login")]
        public IActionResult Login([FromBody] usertbl login)
        {
            var user = AuthenticateUser(login);

            if (user != null)
            {
                var tokenString = GenerateJwtToken(user);
                Console.WriteLine("Generated Token: " + tokenString);
                return Ok(new { token = tokenString });
            }

            return Unauthorized();
        }

        // Authenticate the user credentials
        private usertbl AuthenticateUser(usertbl login)
        {
            // In a real application, you would query the database for user credentials
            var user = _context.usertbl.FirstOrDefault(u => u.username == login.username && u.passwd == login.passwd);
            if (user == null)
            {
                Console.WriteLine("User not found."); // Debug output
                //return Unauthorized(); // Invalid credentials
            }
            return user;
        }

        // Generate JWT Token
        private string GenerateJwtToken(usertbl user)
        {
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, user.username),
            new Claim(JwtRegisteredClaimNames.Email, user.email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds);

            //var tokenDescriptor = new SecurityTokenDescriptor
            //{
            //    Subject = new ClaimsIdentity(new Claim[]
            //{
            //    new Claim(ClaimTypes.Name, user.UserName)
            //}),
            //    Expires = DateTime.UtcNow.AddHours(1),
            //    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            //};

            var tokenHandler = new JwtSecurityTokenHandler();
            //var token = tokenHandler.CreateToken(tokenDescriptor);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        [HttpPost("login22")]
        public IActionResult Login22([FromBody] usrtbl2 login)
        {
            var user = AuthenticateUser22(login);

            if (user != null)
            {
                var tokenString = GenerateJwtToken22(user);
                Console.WriteLine("Generated Token: " + tokenString);
                return Ok(new { token = tokenString });
            }
            return Unauthorized();
        }


        private usrtbl2 AuthenticateUser22(usrtbl2 login)
        {
            // In a real application, you would query the database for user credentials
            var user = _context.usrtbl2.FirstOrDefault(u => u.mobile == login.mobile && u.password == login.password);
            if (user == null)
            {
                Console.WriteLine("User not found."); // Debug output
                //return Unauthorized(); // Invalid credentials
            }
            return user;
        }


        private string GenerateJwtToken22(usrtbl2 user)
        {
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, user.mobile),
           // new Claim(JwtRegisteredClaimNames.Email, user.email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds);
            var tokenHandler = new JwtSecurityTokenHandler();
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [Authorize]
        [HttpGet("ustbl")]
        public List<usertbl> ustbl()
        {
            var d = new List<usertbl>();
            d = _context.usertbl.ToList();
            return d;
        }

        [HttpGet("test")]
        public ActionResult<string> Test()
        {
            return "API is working!";
        }
    }
}
