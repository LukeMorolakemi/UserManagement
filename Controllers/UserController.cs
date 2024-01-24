using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserManagement.Data;
using UserManagement.Model;

namespace UserManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public UserController(DataContext dataContext) 
        {
        
        _dataContext = dataContext;   
         }


        [HttpPost]
        [Route("add_user")]

        public async Task<IActionResult> Adduser(User1 user)
        {

           _dataContext.User1s.Add(user);
          await  _dataContext.SaveChangesAsync();

            return Ok(user);
               
        }

        [HttpGet]
        [Route("get_alluser")]
        public async Task<IActionResult> Getuser()
        {
            return Ok(_dataContext.User1s.ToList());
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetResume(int id)
        {
            var user1 = await _dataContext.User1s.FindAsync(id);
            if (user1 == null)
            {
                return NotFound("user not found");
            }

            return Ok(user1);
        }

        [HttpPut("id")]
        public async Task<IActionResult>  EditUser(int id, [FromBody] User1 updatedUser)
        {


            var existingUser = await _dataContext.User1s.FindAsync(id);

            if (existingUser == null)
            {
                return NotFound("User not found");
            }

            // Update the properties of the existing user with the values from the updatedUser
            existingUser.Name = updatedUser.Name;
            existingUser.Email = updatedUser.Email;
            existingUser.Phone = updatedUser.Phone;
            existingUser.Password = updatedUser.Password;
            // ... update other properties as needed

            _dataContext.User1s.Update(existingUser);
            await _dataContext.SaveChangesAsync();

            return Ok(existingUser);

        }
        

        [HttpDelete("{id}")]
        public async Task<IActionResult> Deleteuser(int id) 
        {
            var deleteuser = await _dataContext.User1s.FindAsync(id);

            if(deleteuser == null)
            {
                return BadRequest("user does not exist");
            }
            _dataContext.User1s.Remove(deleteuser);  
           await _dataContext.SaveChangesAsync();

            return Ok("User Sucessfuly Deleted");

        }

       





    }


}
