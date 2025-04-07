using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MnemosAPI.DTO;
using MnemosAPI.Services;

namespace MnemosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService categoryService;
        private readonly IMapper mapper;

        public CategoryController(ICategoryService categoryService, IMapper mapper)
        {
            this.categoryService = categoryService;
            this.mapper = mapper;
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<IEnumerable<CategoryDto>> GetCategoriesAsync()
        {
            return await categoryService.GetCategoriesAsync();
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public async Task<CategoryDto?> GetCategoryAsync(int id)
        {
            var category = await categoryService.GetCategoryAsync(id);
            
            if (category == null)
            {
                return null;
            }

            return category;
        }
    }
}
