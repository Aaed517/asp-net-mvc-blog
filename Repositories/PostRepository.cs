using Entities.Models;
using Repositories.Contracts;

namespace Repositories
{
    public class PostRepository : RepositoryBase<Post>, IPostRepository
    {
        public PostRepository(RepositoryContext context) : base(context)
        {

        }
        public void CreatePost(Post post) => Add(post);
        public IQueryable<Post> GetAllPost(bool trackChanges) => GetAll(trackChanges);

        public IQueryable<Post> GetOnePost(int id, bool trackChanges) => FindByCondition(p => p.PostId.Equals(id), trackChanges);
        public void UpdateOnePost(Post post,int id) => Update(post,id);
        public void DeletePost(Post post) => Delete(post);

        public IQueryable<Post> GetPostsByCategory(string categorname, bool trackChanges)=> FindByCondition(p => p.Category.Name.Equals(categorname), trackChanges);

        public IQueryable<Post> ListUserPosts(string AuthorId, bool trackChanges) => FindByCondition(p => p.AuthorId.Equals(AuthorId), trackChanges);


    }
}