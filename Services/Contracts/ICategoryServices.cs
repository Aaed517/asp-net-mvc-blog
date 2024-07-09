using Entities.Dtos;
using Entities.Models;

namespace Services.Contracts
{
    public interface ICategoryServices
    {
        IQueryable<Category> GetAllCategory(bool trackChanges);
        List<CategoryPostCountDto> GetCategoriesWithPostCount(bool trackChanges);
        Category GetCategory(int categoryId, bool trackChanges);
        void UpdateCategory(Category category);
        void AddCategory(Category category);
        Task DeleteCategory(int id);

    }
}