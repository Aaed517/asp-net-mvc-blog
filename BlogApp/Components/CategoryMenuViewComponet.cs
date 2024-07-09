using Entities.Dtos;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace StoreApp.Components
{
    public class CategoryMenuViewComponent : ViewComponent
    {
        private readonly IServiceManager _manager;

        public CategoryMenuViewComponent(IServiceManager manager)
        {
            _manager = manager;
        }


        public  IViewComponentResult Invoke()
        {
  
            var categoriesWithPostCount =  _manager.CategoryServices.GetCategoriesWithPostCount(false);
            return View(categoriesWithPostCount);
        }
        

    }
}