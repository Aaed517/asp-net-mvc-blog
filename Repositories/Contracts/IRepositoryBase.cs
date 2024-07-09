using System.Linq.Expressions;
namespace Repositories.Contracts
{
public interface IRepositoryBase<T> 
where T : class, new()
{
    IQueryable<T>  FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges);
    IQueryable<T> GetAll(bool trackChanges);
    void Add(T? entity);
    void Update(T entity, int id);
    void Delete(T entity);
}
}