using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;

namespace TaskCircle.UserManagerApi.Infrastructure.Repository.Interfaces;

public interface IRepository<T>
{
    Task<IEnumerable<T>> GetAll();
    Task<T> GetById(int id);
    Task<T> Create(T Entity);
    Task<T> Update(T Entity);
    Task<T> Delete(int id);
}
