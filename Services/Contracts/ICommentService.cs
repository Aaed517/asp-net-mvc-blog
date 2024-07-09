using Entities.Dtos;
using Entities.Models;

namespace Services.Contracts
{
    public interface ICommentServices
    {
        IQueryable<Comment> GetAllPostComments (int postId,bool trackChanges); 
        void DeleteOneComment(Comment comment);
        bool CreateComment(Comment comment);
        int CommentCount();
    }
}