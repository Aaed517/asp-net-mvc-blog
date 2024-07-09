using Repositories.Contracts;

namespace Repositories
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _context;
        private readonly IPostRepository _postRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICommentRepository _commentRepository;

        public RepositoryManager(RepositoryContext context, IPostRepository postRepository, ICategoryRepository categoryRepository, ICommentRepository commentRepository)
        {
            _context = context;
            _postRepository = postRepository;
            _categoryRepository = categoryRepository;
            _commentRepository = commentRepository;
        }

        public IPostRepository Post => _postRepository;
        public ICategoryRepository Category => _categoryRepository;
        public ICommentRepository Comment => _commentRepository;

        public void Save()
        {
            _context.SaveChanges();
        }
    }

}