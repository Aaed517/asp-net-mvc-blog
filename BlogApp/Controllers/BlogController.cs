using Bogus.DataSets;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Repositories.Contracts;
using Services.Contracts;

namespace BlogApp.Controllers
{
    public class BlogController : Controller
    {
        private readonly IServiceManager _manager;

        private readonly UserManager<IdentityUser> _userManager;
        public BlogController(IServiceManager manager, UserManager<IdentityUser> userManager)
        {
            _manager = manager;
            _userManager = userManager;
        }
        [Authorize]
        public IActionResult Create()
        {
            ViewBag.categories = _manager.CategoryServices.GetAllCategory(false).ToList();
            return View();
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] Post model, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                string path = Path.Combine(Directory.GetCurrentDirectory()
                , "wwwroot", "images", file.FileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                var user = await _userManager.GetUserAsync(User);
                var post = new Post
                {
                    AuthorId = user.Id,
                    Title = model.Title,
                    Content = model.Content,
                    PublishDate = DateTime.Now,
                    ImageURL = String.Concat("/images/", file.FileName),
                    CategoryId = model.CategoryId
                };
                _manager.PostServices.CreatePost(post);
                return RedirectToAction("Index", "Home");

            }
            return View(model);
        }
        public IActionResult GetOnePost([FromRoute(Name = "id")] int id)
        {
            Post post = _manager.PostServices.GetOnePost(id, false);
            ViewData["IsContentLimited"] = false;
            ViewData["ContentLength"] = 400;
            return View(post);
        }

        public IActionResult Index(int pageNumber = 1, int pageSize = 4)
        {
            ViewBag.categories = _manager.CategoryServices.GetAllCategory(false).ToList();
            //var model = _manager.PostServices.GetAllPost(trackChanges: false).OrderByDescending(p => p.PublishDate).AsQueryable();
            var posts = _manager.PostServices.GetAllPost(trackChanges: false).OrderByDescending(p => p.PublishDate).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            var totalPosts = _manager.PostServices.GetAllPost(trackChanges: false).Count();
            var model = new PaginatedPosts
            {
                Posts = posts,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPosts = totalPosts
            };
            ViewData["ContentLength"] = 400;
            ViewData["IsContentLimited"] = true;
            return View(model);
        }

        public IActionResult GetPostsByCategory([FromQuery(Name = "categoryname")] String categoryname)
        {
            ViewData["IsContentLimited"] = true;
            ViewData["ContentLength"] = 400;
            ViewData["CategoryName"] = categoryname;
            var model = _manager.PostServices.GetPostsByCategory(categoryname, false).ToList();
            return View(model);
        }
        [Authorize]
        public async Task<IActionResult> ListUserPosts()
        {
            ViewData["IsContentLimited"] = true;
            ViewData["ContentLength"] = 400;
            var user = await _userManager.GetUserAsync(User);
            var anypost = _manager.PostServices.GetAllPost(false).Where(p => p.AuthorId == user.Id);
            if(anypost.Count() == 0)
            {
                return Redirect("Create");
            }
            var model = _manager.PostServices.ListUserPosts(user.Id, false).OrderByDescending(p => p.PublishDate).AsQueryable();
            return View(model);
        }

    }
}