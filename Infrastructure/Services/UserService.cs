using AutoMapper;
using TaskCircle.UserManagerApi.DTOs;
using TaskCircle.UserManagerApi.Infrastructure.Repositories.Interfaces;
using TaskCircle.UserManagerApi.Infrastructure.Services.Interfaces;
using TaskCircle.UserManagerApi.Models;

namespace TaskCircle.UserManagerApi.Infrastructure.Services;

public class UserService : IUserService
{
    private readonly IUserRepository? _userRepository;
    private readonly IMapper? _mapper;

    public UserService(IUserRepository? userRepository, IMapper? mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<UserDTO>?> GetUsers()
    {
        var usersEntity = await _userRepository.GetAll();
        return _mapper?.Map<IEnumerable<UserDTO>>(usersEntity);
    }

    public async Task<UserDTO>? GetUserById(int id)
    {
        var userEntity = await _userRepository.GetById(id);
        return _mapper?.Map<UserDTO>(userEntity);
    }

    public async Task AddUser(AddUserDTO addUserDto)
    {
        var userEntity = _mapper?.Map<User>(addUserDto);
        
        if (userEntity?.IdGender != null) 
        {
            userEntity.Gender = new Gender { IdGender = (int)userEntity.IdGender };
        }

        await _userRepository?.Create(userEntity);
    }

    public async Task UpdateUser(UpdateUserDTO updateUserDto)
    {
        var userEntity = _mapper?.Map<User>(updateUserDto);

        if (userEntity?.IdGender != null)
        {
            userEntity.Gender = new Gender { IdGender = (int)userEntity.IdGender };
        }
        await _userRepository?.Update(userEntity);
    }

    public async Task DeleteUser(int id)
    {
        User user = await _userRepository?.GetById(id);

        // Tentar deletar user
        await _userRepository?.Delete(user.IdUser);
    }
}