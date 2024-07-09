using Entities.Models;

namespace Repositories.Contracts
{
    public interface ICommentRepository : IRepositoryBase<Comment>
    {
        IQueryable<Comment> GetAllPostComments (int postId,bool trackChanges); 
        void DeleteOneComment(Comment comment);
        void CreateComment(Comment comment);
    }
}