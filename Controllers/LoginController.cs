using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Data;
using UserManagement.DTO;
using UserManagement.Model;

namespace UserManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly DataContext _dataContext;
        private readonly string _jwtSecretKey = "superSecretKey@345"; 
        private readonly int _jwtExpirationMinutes = 5;

        public LoginController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpPost]
        [Route("userlogin")]
        public async Task<IActionResult> Login([FromBody] LoginDto userLogin)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid input");
            }

            // Find the user by email
            var user = await _dataContext.Users.FirstOrDefaultAsync(u => u.Email == userLogin.Email);

            if (user == null)
            {
                return NotFound("User not found");
            }

            // Compare the provided password with the hashed password stored in the database
            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(userLogin.Password, user.Password);

            if (!isPasswordValid)
            {
                return Unauthorized("Invalid password");
            }

            var token = GenerateJwtToken(user);

            var response = new Response<CustomTokenResponse>
            {
                Data = new CustomTokenResponse
                {
                    Token = token,
                    UserId = user.Id,
                    Email = user.Email,
       
                },
                Code = "00",
                Message = "Login successful"
            };

            return Ok(response);
        }

        private string GenerateJwtToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Role, "Admin")
            
             
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "https://localhost:5001",
                audience: "https://localhost:5001",
                claims: claims,
                expires: DateTime.Now.AddMinutes(_jwtExpirationMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public class Response<T>
        {
            public string Message { get; set; }
            public string Code { get; set; }
            public T Data { get; set; }
        }

        public class CustomTokenResponse
        {
            public string Token { get; set; }
            public int UserId { get; set; }
            public string Email { get; set; }
           
        }
    }
}
