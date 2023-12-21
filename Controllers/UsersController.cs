using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskCircle.UserManagerApi.DTOs;
using TaskCircle.UserManagerApi.Infrastructure.Services.Interfaces;

namespace TaskCircle.UserManagerApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserService? _userService;

    public UsersController(IUserService? userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDTO>>> GetAll()
    {
        var usersDto = await _userService?.GetUsers();

        if(usersDto is null) return NotFound("Users not found");

        return Ok(usersDto);
    }

    [HttpGet("{id:int}", Name = "GetUser")]
    public async Task<ActionResult<UserDTO>> GetById(int id) 
    {
        var userDto = await _userService?.GetUserById(id);
        if (userDto is null) return NotFound("User not found");
        return Ok(userDto);
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] AddUserDTO addUserDto)
    {
        if (addUserDto is null) return BadRequest("Invalid Data");
        await _userService.AddUser(addUserDto);
        return Ok(addUserDto);
    }

    [HttpPut("{id:int}", Name = "UpdateUser")]
    public async Task<ActionResult<UserDTO>> Put(int id, [FromBody] UpdateUserDTO updateUserDto)
    {

        if (updateUserDto is null) return BadRequest();

        // TODO: Verificar se é o usuario logado

        updateUserDto.IdUser = id;

        await _userService.UpdateUser(updateUserDto);

        return Ok(updateUserDto);
    }

    [HttpDelete("{id:int}", Name = "DeleteUser")]
    public async Task<ActionResult<UserDTO>> Delete(int id)
    {
        var userDto = await _userService?.GetUserById(id);

        if (userDto is null) return NotFound("User not found!");

        await _userService?.DeleteUser(id);

        return Ok(userDto);
    }
}