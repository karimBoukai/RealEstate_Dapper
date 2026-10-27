using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstate_Dapper_Api.Repositories.ProductRepository;

namespace RealEstate_Dapper_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductDetailsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductDetailsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        [HttpGet("GetProductDetailsByProductID")]
        public async Task<IActionResult> GetProductDetailsByProductID(int id)
        {
            var values = await _productRepository.GetProductDetailsByProductID(id);
            return Ok(values);
        }



    }
}
