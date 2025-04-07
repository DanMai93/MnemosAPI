using MnemosAPI.DTO;

namespace MnemosAPI.Services
{
    public interface ICategoryService
    {
        Task<CategoryDto?> GetCategoryAsync(int categoryId);

        Task<IEnumerable<CategoryDto>> GetCategoriesAsync();
    }
}
