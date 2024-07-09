using Entities.Models;
using Repositories.Contracts;

namespace Repositories
{
    public class CommentRepository : RepositoryBase<Comment>, ICommentRepository
    {
        public CommentRepository(RepositoryContext context) : base(context)
        {

        }
        public IQueryable<Comment> GetAllPostComments(int postId,bool trackChanges) => FindByCondition(p => p.PostId.Equals(postId), trackChanges);

        public void CreateComment(Comment comment) => Add(comment);
        public void DeleteOneComment(Comment comment) => Delete(comment);
    }
}