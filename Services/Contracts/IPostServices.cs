using Entities.Models;
namespace Services.Contracts
{
    public interface IPostServices
    {
        IEnumerable<Post> GetAllPost(bool trackChanges);
        IEnumerable<Post> GetPostsByCategory(string categorname,bool trackChanges);
        Post GetOnePost(int id, bool trackChanges);
        void CreatePost(Post post);
        IEnumerable<Post>  ListUserPosts(string id,bool trackChanges);
        Task  DeletePost(int id);
        Post GetMostCommentPost(bool trackChanges);
        Post GetMostCommentedPostOfDay(bool trackChanges);
         IEnumerable<object> GetPostCountsByMonth();
    }
}