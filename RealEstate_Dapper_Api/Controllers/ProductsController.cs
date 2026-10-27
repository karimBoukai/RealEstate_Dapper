using Microsoft.AspNetCore.Mvc;
using RealEstate_Dapper_Api.Dtos.ProductDtos;
using RealEstate_Dapper_Api.Repositories.ProductRepository;

namespace RealEstate_Dapper_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<IActionResult> ProductList()
        {
            var values = await _productRepository.GetAllProductAsync();
            return Ok(values);
        }

        [HttpGet("GetProductWithCategory")]
        public async Task<IActionResult> GetProductWithCategory()
        {
            var values = await _productRepository.GetAllProductWithCategoriesAsync();
            return Ok(values);
        }
        [HttpGet("ProductDealOfTheDayeStatusChangeToTrue/{id}")]
        public async Task<IActionResult> ProductDealOfTheDayeStatusChangeToTrue(int id)
        {
            await _productRepository.ProductDealOfTheDayeStatusChangeToTrue(id);
            return Ok("The ad has been Activated");
        }

        [HttpGet("ProductDealOfTheDayeStatusChangeToFalse/{id}")]
        public async Task<IActionResult> ProductDealOfTheDayeStatusChangeToFalse(int id)
        {
            await _productRepository.ProductDealOfTheDayeStatusChangeToFalse(id);
            return Ok("The ad has been Deactivated");
        }
        [HttpGet("Last5ProductList")]
        public async Task<IActionResult> Last5ProductList()
        {
            var values = await _productRepository.GetLast5ProductsAsync();
            return Ok(values);

        }

        [HttpGet("GetProductAdvertsListByEmployeeAsyncByTrue")]

        public async Task<IActionResult> ProductAvertsListByEmployeeByTrue(int id)
        {
            var values = await _productRepository.GetProductAdvertsListByEmployeeAsyncByTrue(id);
            return Ok(values);
        }
        [HttpGet("GetProductAdvertsListByEmployeeAsyncByFalse")]

        public async Task<IActionResult> ProductAvertsListByEmployeeByFalse(int id)
        {
            var values = await _productRepository.GetProductAdvertsListByEmployeeAsyncByFalse(id);
            return Ok(values);
        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductDtos createProductDtos)
        {
            await _productRepository.CreateProduct(createProductDtos);
            return Ok("The ad has been created");
        }
        [HttpGet("GetProductByProductID")]
        public async Task<IActionResult> GetProductByProductID(int id)
        {
            var values = await _productRepository.GetProductByProductID(id);
            return Ok(values);
        }
        [HttpGet("ResultProductWithSearchList")]
        public async Task<IActionResult> ResultProductWithSearchList(string searchKeyValue, int propertyCategoryID, string city)
        {
            var values = await _productRepository.ResultProductWithSearchList(searchKeyValue, propertyCategoryID, city);
            return Ok(values);
        }

        [HttpGet("GetProductByDealOfTheDayTrueWithCategory")]
        public async Task<IActionResult> GetProductByDealOfTheDayTrueWithCategory()
        {
            var values = await _productRepository.GetProductByDealOfTheDayTrueWithCategoryAsync();
            return Ok(values);


        }
        [HttpGet("GetLast3Product")]
        public async Task<IActionResult> GetLast3Product()
        {
            var values = await _productRepository.GetLast3ProductsAsync();
            return Ok(values);


        }

        [HttpGet("GetActiveProductForAdsPage")]
        public async Task<IActionResult> GetActiveAdsOnly()
        {
            var values = await _productRepository.GetActiveAdsOnly();
            return Ok(values);


        }

        [HttpGet("ProductAdsPageStatusChangeToTrue/{id}")]
        public async Task<IActionResult> ProductAdsPageStatusChangeToTrue(int id)
        {
            await _productRepository.ProductAdsPageStatusChangeToTrue(id);
            return Ok("The ad has been Activated");
        }

        [HttpGet("ProductAdsPageStatusChangeToFalse/{id}")]
        public async Task<IActionResult> ProductAdsPageStatusChangeToFalse(int id)
        {
            await _productRepository.ProductAdsPageStatusChangeToFalse(id);
            return Ok("The ad has been Activated");
        }
    }
}
