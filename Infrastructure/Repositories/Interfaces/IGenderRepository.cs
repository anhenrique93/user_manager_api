using TaskCircle.UserManagerApi.Models;

namespace TaskCircle.UserManagerApi.Infrastructure.Repositories.Interfaces;

public interface IGenderRepository
{
    Task<Gender> getById(int id);
}
