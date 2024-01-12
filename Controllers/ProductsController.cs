using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Model;

 namespace UserManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ApiResource api;
        private readonly CartResponse cartResponse; 

        public ProductsController(ApiResource api, CartResponse cartResponse)
        {
            this.api = api;
            this.cartResponse = cartResponse;   
        }

        [HttpGet("product")]
        public async Task<IActionResult> getproduct(int id)
        {
            return Ok(await api.ProductAsync(id));
        }

        [HttpGet("all-product")]
        public async Task<IActionResult> getAllproduct()
        {
            return Ok(await api.AllProductAsync());
        }


        [HttpGet]
        [Route("getcartnyid")]
        public async Task<IActionResult> GetCartbyId(int id)
        {
            return Ok(await cartResponse.CartsAsync(id));

        }

        [HttpGet("all-Carts")]
        public async Task<IActionResult> GetAllCartAsync()
        {
            return Ok(await cartResponse.AllCartsAsync());
        }

    }
}
    

