using BlogApp.Areas.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace BlogApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class DashBoardController : Controller
    {
        private readonly IServiceManager _manager;

        public DashBoardController(IServiceManager manager)
        {
            _manager = manager;
        }
        public IActionResult Index()
        {
            var dashboard = new Dashboard();
            dashboard.MembersCount =  _manager.AuthService.UsersWithRoles().Count();
            dashboard.PostCount = _manager.PostServices.GetAllPost(false).Count();
            dashboard.CommentCount = _manager.CommentServices.CommentCount();
            dashboard.RoleCount = _manager.AuthService.Roles.Count();
            dashboard.PostCountsByMonth= _manager.PostServices.GetPostCountsByMonth();
            return View(dashboard);
        }
    }
}