using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace StoreApp.Components
{
    public class PostCountViewComponent : ViewComponent
    {
        private readonly IServiceManager _manager;

        public PostCountViewComponent(IServiceManager manager)
        {
            _manager = manager;
        }


        public string Invoke()
        {
            return _manager.PostServices.GetAllPost(false).Count().ToString();
        }
        

    }
}