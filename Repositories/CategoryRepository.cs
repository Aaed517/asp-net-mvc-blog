using System.Diagnostics;
using System.IO.Compression;
using Entities.Models;
using Repositories.Contracts;

namespace Repositories
{
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(RepositoryContext context) : base(context)
        {

        }
        public void CreateCategory(Category category) => Add(category);
        public IQueryable<Category> GetAllCategory(bool trackChanges) => GetAll(trackChanges);

        public IQueryable<Category> GetOneCategory(int id, bool trackChanges) => FindByCondition(p => p.CategoryId.Equals(id), trackChanges);
        public void UpdateOneCategory(Category category, int id) => Update(category , id);
        public void DeleteCategory(Category category) => Delete(category);

       
     
    }
}