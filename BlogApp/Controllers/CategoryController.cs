using Microsoft.AspNetCore.Mvc;
using Repositories.Contracts;
using Services.Contracts;

namespace BlogApp.Controllers
{
    public class CategoryController : Controller
    {        
        private readonly IServiceManager _manager;

        public CategoryController(IServiceManager manager)
        {
            _manager = manager;
        }

        public ActionResult Index()
        {
            var categories =  _manager.CategoryServices.GetAllCategory(false);

            return PartialView("_CategoryListPartial", categories);
        }
    }
}