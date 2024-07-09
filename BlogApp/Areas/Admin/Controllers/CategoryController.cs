using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace BlogApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        private readonly IServiceManager _manager;

        public CategoryController(IServiceManager manager)
        {
            _manager = manager;
        }
        public IActionResult Index()
        {
            var model = _manager.CategoryServices.GetAllCategory(false);
            return View(model);
        }
        public IActionResult Update([FromRoute(Name = "id")] int id)
        {
            var model = _manager.CategoryServices.GetCategory(id, false);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update([FromForm] Category category)
        {
            if (ModelState.IsValid)
            {
                var entity = _manager.CategoryServices.GetCategory(category.CategoryId, false);
                entity.Name = category.Name;
                _manager.CategoryServices.UpdateCategory(entity);
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Create()
        {
            return View(new Category());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([FromForm] Category category)
        {
            if (ModelState.IsValid)
            {
                _manager.CategoryServices.AddCategory(category);
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute(Name = "id")] int id)
        {
            try
            {
                await _manager.CategoryServices.DeleteCategory(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}