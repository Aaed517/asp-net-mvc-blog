using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Repositories;
using Repositories.Contracts;
using Services.Contracts;

namespace BlogApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IServiceManager _manager;

        public HomeController(IServiceManager manager)
        {
            _manager = manager;
        }

        public IActionResult HandleError(int statusCode)
        {
            if (statusCode == 404)
            {
                return RedirectToAction("Index");
            }
            return View("Error");
        }
        public IActionResult Index()
        {
            var model = _manager.PostServices.GetAllPost(false);
            var mostcommentpost = _manager.PostServices.GetMostCommentPost(false);
            var mostcommetpostofday = _manager.PostServices.GetMostCommentedPostOfDay(true);
            ViewBag.mostcommentpost = mostcommentpost;
            ViewBag.mostcommetpostofday = mostcommetpostofday;
            return View(model);
        }
    }
}