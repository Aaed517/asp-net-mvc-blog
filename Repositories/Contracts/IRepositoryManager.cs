namespace Repositories.Contracts
{
    public interface IRepositoryManager
    {
        IPostRepository Post {get; }
        ICategoryRepository Category{get; }
        ICommentRepository Comment{get;}
        void Save();
    }
}