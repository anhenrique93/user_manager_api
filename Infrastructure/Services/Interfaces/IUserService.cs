using TaskCircle.UserManagerApi.DTOs;

namespace TaskCircle.UserManagerApi.Infrastructure.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDTO>> GetUsers();
        Task<UserDTO> GetUserById(int id);
        Task AddUser(AddUserDTO UserDto);
        Task UpdateUser(UpdateUserDTO UserDto);
        Task DeleteUser(int id);
    }
}
