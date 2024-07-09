using Services.Contracts;

namespace Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly IPostServices _postServices;
        private readonly ICategoryServices _categoryServices;
        private readonly IAuthService _authServices;
        private readonly ICommentServices _commentServices;

        public ServiceManager(IPostServices postServices, ICategoryServices categoryServices, IAuthService authServices, ICommentServices commentServices)
        {
            _postServices = postServices;
            _categoryServices = categoryServices;
            _authServices = authServices;
            _commentServices = commentServices;
        }

        public IPostServices PostServices => _postServices;

        public ICategoryServices CategoryServices => _categoryServices;

        public IAuthService AuthService => _authServices;
        public ICommentServices CommentServices => _commentServices;
    }
}