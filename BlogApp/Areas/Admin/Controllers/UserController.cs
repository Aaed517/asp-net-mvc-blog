using Entities.Dtos;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Services.Contracts;

namespace BlogApp.Areas.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly IServiceManager _manager;

        public UserController(IServiceManager manager)
        {
            _manager = manager;
        }
        public IActionResult Index()
        {
            var users = _manager.AuthService.UsersWithRoles();
            return View(users);
        }
        public IActionResult Update([FromRoute(Name = "id")] string id)
        {
            var roles = _manager.AuthService.Roles;
            ViewBag.Roles = roles;
            var user = _manager.AuthService.GetOneUserWithRole(id);
            return View(user);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update([FromForm] UserDtoForUpdate user)
        {
            if (ModelState.IsValid)
            {
                await _manager.AuthService.Update(user);
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Create()
        {
            return View(new UserDtoForCreation()
            {
                Roles = new HashSet<string>(_manager
                        .AuthService
                        .Roles
                        .Select(r => r.Name)
                        .ToList())
            });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] UserDtoForCreation userDto)
        {
            var result = await _manager.AuthService.CreateUser(userDto);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return RedirectToAction("Create");
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteOneUser([FromForm] UserDto userDto)
        {
            if (string.IsNullOrEmpty(userDto.UserName))
            {
                ModelState.AddModelError("", "User name cannot be null or empty.");
                return View(userDto);
            }

            var result = await _manager
                .AuthService
                .DeleteOneUser(userDto.UserName);

            return result.Succeeded
                ? RedirectToAction("Index")
                : View(userDto);

        }

    }
}