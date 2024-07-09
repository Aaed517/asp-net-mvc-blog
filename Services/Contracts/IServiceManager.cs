namespace Services.Contracts
{
    public interface IServiceManager
    {
        IPostServices PostServices {get;}
        ICategoryServices CategoryServices {get;}
        IAuthService AuthService {get;}
        ICommentServices CommentServices {get;}
        
    }
}