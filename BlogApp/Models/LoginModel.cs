using System.ComponentModel.DataAnnotations;

namespace BlogApp.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage ="Name is required.")]
        public string? Name { get; set; }
        [Required(ErrorMessage ="Password is required.")]
        public string? Password { get; set; }
        private string? _returnUrl;

        public string ReturnUrl
        {
            get => _returnUrl ?? "/";
            set => _returnUrl = value;
        }
        /*
        public string ReturnUrl
        {
            get
            {
                if(_returnUrl is null)
                    return "/";
                else
                    return _returnUrl;
            }
            set
            {
                _returnUrl = value;
            }
        }
        */

    }
}