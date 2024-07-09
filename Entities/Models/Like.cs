using Microsoft.AspNetCore.Identity;

namespace Entities.Models
{
    public class Like
    {
        public int LikeId { get; set; }
        public string? UserId { get; set; } 
        public IdentityUser? User { get; set; }
        public int PostId { get; set; } 
        public Post? Post { get; set; }
    }
}