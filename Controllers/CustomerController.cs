using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using UserManagement.Data;
using UserManagement.Model;

namespace UserManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]


    public class CustomerController : ControllerBase
    {
        private readonly DataContext _dataContext;



        public CustomerController(DataContext context)

        {
            _dataContext = context;
        }

      



      


        

        [HttpGet]
        [Route("get_user"), Authorize(Roles = "Admin")]
        public IEnumerable<string> Get()
        {
            return new string[] { "John Doe", "Jane Doe" };
        }
    }
}


