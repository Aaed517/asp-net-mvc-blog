using System.Runtime.CompilerServices;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace BlogApp.Controllers
{
    public class CommentController : Controller
    {
        private readonly IServiceManager _manager;

        private readonly UserManager<IdentityUser> _userManager;
        public CommentController(IServiceManager manager, UserManager<IdentityUser> userManager)
        {
            _manager = manager;
            _userManager = userManager;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] Comment comment, int PostId)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                comment.AuthorId = user.Id;
                comment.PostId = PostId;
                comment.PublishDate  = DateTime.Now;
                var result = _manager.CommentServices.CreateComment(comment);
                if (result)
                {
                    ViewBag.SuccessMessage = "Comment successfully added!";
                }
                else
                {
                    ViewBag.ErrorMessage = "Failed to add comment. Please try again.";
                }
                return RedirectToAction("Index", "Blog"); 
            }
            return Redirect("/");
        }
    }
}