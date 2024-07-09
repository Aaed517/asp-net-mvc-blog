using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;

namespace Services
{
    public class CommentManager : ICommentServices
    {
        private readonly IRepositoryManager _manager;

        public CommentManager(IRepositoryManager manager)
        {
            _manager = manager;
        }

        public int CommentCount()
        {
            return _manager.Comment.GetAll(false).Count();
        }

        public  bool CreateComment(Comment comment)
        {
            try
            {
                _manager.Comment.Add(comment);
                return true;
            }
            catch (Exception){return false;}        
        }


        public void DeleteOneComment(Comment comment)
        {
            _manager.Comment.Delete(comment);
        }

        public IQueryable<Comment> GetAllPostComments(int postId, bool trackChanges)
        {
            return _manager.Comment.GetAllPostComments(postId, trackChanges);
        }


    }
}