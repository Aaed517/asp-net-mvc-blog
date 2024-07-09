using Entities.Models;

namespace Repositories.Contracts
{
    public interface IPostRepository : IRepositoryBase<Post>
    {
        IQueryable<Post> GetAllPost(bool trackChanges);
        IQueryable<Post> GetOnePost(int id,bool trackChanges);
        IQueryable<Post> GetPostsByCategory(string categorname,bool trackChanges);
        void CreatePost(Post post);
        void DeletePost(Post post);
        void UpdateOnePost(Post post, int id);
        IQueryable<Post> ListUserPosts(string AuthorId,bool trackChanges);


    }
}