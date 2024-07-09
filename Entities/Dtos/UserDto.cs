using System.ComponentModel.DataAnnotations;

namespace Entities.Dtos
{
    public record UserDto
    {
        public string? UserId {get;init;}
        [Required(ErrorMessage ="UserName is required!")]
        public String? UserName {get;init;}
        [Required(ErrorMessage ="Enail is required!")]
        public String? Email {get;init;}
        public HashSet<String>  Roles {get;set;} = new HashSet<string>();

    }
}