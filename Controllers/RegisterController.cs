using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Data;
using UserManagement.Model;
using BCrypt.Net;
using System;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;

namespace UserManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public RegisterController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpPost]
        [Route("RegisterUser")]

        public IActionResult Register([FromBody] User user)
        {
            if (!IsValidEmail(user.Email))
            {
                return BadRequest("Invalid email format");
            }

            // Hash the password using BCrypt
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);

            // Save the user with the hashed password to the database
            user.Password = hashedPassword;
            _dataContext.Users.Add(user);
            _dataContext.SaveChanges();

            return Ok("User registered successfully");
        }
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }

        }
    }
}
