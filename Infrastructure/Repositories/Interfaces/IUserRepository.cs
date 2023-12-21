using TaskCircle.UserManagerApi.DTOs;
using TaskCircle.UserManagerApi.Infrastructure.Repository.Interfaces;
using TaskCircle.UserManagerApi.Models;

namespace TaskCircle.UserManagerApi.Infrastructure.Repositories.Interfaces;

public interface IUserRepository : IRepository<User>
{
    Task<User> GetByEmail() 
    {
        throw new NotImplementedException();
    }

}
