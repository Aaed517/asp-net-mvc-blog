using Microsoft.AspNetCore.Identity;

namespace Entities.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string? Content { get; set; }
        public DateTime PublishDate { get; set; }
        
        public int PostId { get; set; }
        public Post? Post { get; set; }
        
        public string? AuthorId { get; set; }
        public IdentityUser? Author { get; set; }
    }
}