
using Microsoft.AspNetCore.Identity;
namespace Entities.Models
{
    public class Post
    {
        public int? PostId { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public DateTime? PublishDate { get; set; }
        public string? ImageURL { get; set; }
        public int? CategoryId { get; set; }
        public Category? Category { get; set; }
        public string? AuthorId { get; set; }
        public IdentityUser? Author { get; set; }
        public ICollection<IdentityUser> Likes { get; set; } = new List<IdentityUser>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();

    }
}
