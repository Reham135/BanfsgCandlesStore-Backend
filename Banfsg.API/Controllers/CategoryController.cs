using Banfsg.BL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Banfsg.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryManager _categoryManager;

        public CategoryController(ICategoryManager categoryManager)
        {
            _categoryManager = categoryManager;
        }

        
        #region Get All Categories

        [HttpGet]
        public ActionResult<IEnumerable<CategoryDto>> GetAllCategories()
        {
            IEnumerable<CategoryDto> categories = _categoryManager.GetAllCategoriesDto();
            return Ok(categories);
        }
        #endregion

        #region Get Category by id

        [HttpGet]
        [Route("{id}")]
        public ActionResult<CategoryDto> GetCategoryById(int id)
        {
            CategoryDto? category = _categoryManager.GetCategoryById(id);
            if (category is null) { return NotFound(); }

            return Ok(category);
        }
        #endregion

        #region Get Category By id With Products

        [HttpGet]
        [Route("{id}/Products")]
        public ActionResult CategoryDetails(int id)
        {
            IEnumerable<ProductChildDto>? categoryDetailDto = _categoryManager.GetCategoryWithProducts(id);
            if (categoryDetailDto == null) { return NotFound(); }
            return Ok(categoryDetailDto);
        }
        #endregion
    }
}
