using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
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

            

            return Ok("Login successful");
        }
    
    }
    }

