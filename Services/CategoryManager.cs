using Entities.Dtos;
using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;

namespace Services
{
    public class CategoryManager : ICategoryServices
    {
        private readonly IRepositoryManager _manager;

        public CategoryManager(IRepositoryManager manager)
        {
            _manager = manager;
        }

        public void AddCategory(Category category)
        {
            Random random = new Random();
            int red = random.Next(256);
            int green = random.Next(256);
            int blue = random.Next(256);
            category.Color = $"#{red:X2}{green:X2}{blue:X2}";
            _manager.Category.Add(category);
        }

        public async Task DeleteCategory(int id)
        {
            var category = await _manager.Category.GetOneCategory(id, false).FirstOrDefaultAsync();
            if(category is not null)
                _manager.Category.Delete(category);
            else
                throw new Exception("Category not found.");
            
        }

        public IQueryable<Category> GetAllCategory(bool trackChanges)
        {
            return _manager.Category.GetAll(trackChanges);
        }

        public List<CategoryPostCountDto> GetCategoriesWithPostCount(bool trackChanges)
        {

            return trackChanges ?
                _manager.Category.GetAll(trackChanges)
                    .Select(c => new CategoryPostCountDto
                    {
                        CategoryId = c.CategoryId,
                        Name = c.Name,
                        PostCount = c.Posts.Count()
                    })
                    .ToList() :
                _manager.Category.GetAll(trackChanges)
                    .AsNoTracking()
                    .Select(c => new CategoryPostCountDto
                    {
                        CategoryId = c.CategoryId,
                        Name = c.Name,
                        PostCount = c.Posts.Count()
                    })
                    .ToList();
        }

        public Category GetCategory(int categoryId, bool trackChanges)
        {
            var category = _manager.Category.FindByCondition(c => c.CategoryId == categoryId, trackChanges).FirstOrDefault();
            if (category is not null)
            {
                return category;
            }
            else
            {
                throw new Exception("Category Not Found");
            }
        }

        public void UpdateCategory(Category category)
        {
            _manager.Category.Update(category, category.CategoryId);
        }

    }
}