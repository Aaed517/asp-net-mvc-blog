using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using Services.Contracts;

namespace Services
{
    public class PostManager : IPostServices
    {
        private readonly IRepositoryManager _manager;

        public PostManager(IRepositoryManager manager)
        {
            _manager = manager;
        }

        public void CreatePost(Post post)
        {
            _manager.Post.Add(post);
        }

        public async Task DeletePost(int id)
        {
            var post = _manager.Post.GetOnePost(id, false).First();
            _manager.Post.Delete(post);
        }


        public IEnumerable<Post> GetAllPost(bool trackChanges)
        {
            return _manager.Post.GetAllPost(trackChanges).Include(p => p.Category).Include(p => p.Author).Include(p => p.Comments).ThenInclude(c => c.Author);
        }

        public Post GetMostCommentPost(bool trackChanges)
        {
            var post = _manager.Post.GetAllPost(trackChanges)
                            .Include(p => p.Comments)
                            .Include(p => p.Author)
                            .OrderByDescending(p => p.Comments.Count)
                            .FirstOrDefault();
            if (post is not null)
                return post;
            throw new Exception("No post found.");
        }

        public Post? GetOnePost(int id, bool trackChanges)
        {
            var post = _manager.Post.GetOnePost(id, trackChanges).Include(p => p.Category).Include(p => p.Author).FirstOrDefault();
            if (post is null)
            {
                throw new Exception("post not found!");
            }
            return post;
        }

        public IEnumerable<Post> GetPostsByCategory(string categorname, bool trackChanges)
        {
            var post = _manager.Post.GetPostsByCategory(categorname, trackChanges).Include(p => p.Category).Include(p => p.Author).ToList();
            return post;
        }

        public Post GetMostCommentedPostOfDay(bool trackChanges)
        {
            var post = _manager.Post.GetAllPost(trackChanges)
                .Where(p => p.PublishDate >= DateTime.Today)
                .Include(p => p.Comments)
                .Include(p => p.Author)
                .OrderByDescending(p => p.Comments.Count)
                .FirstOrDefault();

            if (post != null)
                return post;

            return new Post();
        }

        public IEnumerable<Post> ListUserPosts(string AuthorId, bool trackChanges)
        {
            var post = _manager.Post.ListUserPosts(AuthorId, trackChanges).Include(p => p.Category).Include(p => p.Author).Include(p => p.Comments).ThenInclude(p => p.Author).ToList();
            return post;
        }
        public IEnumerable<object> GetPostCountsByMonth()
        {
            var postCounts = _manager.Post.GetAll(false)
                .Where(p => p.PublishDate.HasValue)
                .GroupBy(p => new { p.PublishDate.Value.Year, p.PublishDate.Value.Month })
                .Select(g => new
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    PostCount = g.Count()
                })
                .OrderBy(g => g.Year)
                .ThenBy(g => g.Month)
                .ToList();
            return postCounts;

        }
    }


 

}