using Entities.Models;

namespace  Repositories.Contracts
{
    public interface ICategoryRepository  : IRepositoryBase<Category>
    {
        IQueryable<Category> GetAllCategory(bool trackChanges);
        IQueryable<Category> GetOneCategory(int id,bool trackChanges);
        void CreateCategory(Category category);
        void DeleteCategory(Category category);
        void UpdateOneCategory(Category category, int id);
       
    }
}