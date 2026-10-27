using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstate_Dapper_Api.Dtos.CategoryDtos;
using RealEstate_Dapper_Api.Repositories.CategoryRepository;
namespace RealEstate_Dapper_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepsoistory;

        public CategoriesController(ICategoryRepository categoryRepsoistory)
        {
            _categoryRepsoistory = categoryRepsoistory;
        }

        [HttpGet]
        public async Task<IActionResult> CategoryList()
        {
            var values = await _categoryRepsoistory.GetAllCategory();
            return Ok(values);
        }
        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryDtos createCategoryDto)
        {
           await _categoryRepsoistory.CreateCategory(createCategoryDto);
            return Ok("The category has been successfully added.");
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            await _categoryRepsoistory.DeleteCategory(id);
            return Ok("The category has been successfully deleted.");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryDtos updateCategoryDtos)
        {
            await _categoryRepsoistory.UpdateCategory(updateCategoryDtos);
            return Ok("The category has been successfully updated.");
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(int id)
        {
            var values = await _categoryRepsoistory.GetCategory(id);
            return Ok(values);
        }
    }
}
