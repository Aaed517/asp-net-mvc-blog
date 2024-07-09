using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace BlogApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class PostController : Controller
    {
        private readonly IServiceManager _manager;

        private readonly UserManager<IdentityUser> _userManager;
        public PostController(IServiceManager manager, UserManager<IdentityUser> userManager)
        {
            _manager = manager;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            var model = _manager.PostServices.GetAllPost(trackChanges: false).OrderByDescending(p => p.PublishDate).AsQueryable();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteOnePost(int id)
        {
            var result = _manager.PostServices.DeletePost(id);
            return RedirectToAction("Index");
        }
    }

}