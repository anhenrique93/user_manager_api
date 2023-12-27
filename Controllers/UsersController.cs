using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TaskCircle.UserManagerApi.DTOs;
using TaskCircle.UserManagerApi.Infrastructure.Services.Interfaces;

namespace TaskCircle.UserManagerApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[EnableCors("User")]
public class UsersController : ControllerBase
{
    private readonly IUserService? _userService;

    public UsersController(IUserService? userService)
    {
        _userService = userService;
    }

    /// <summary>
    /// Get All Users
    /// </summary>
    /// <response code="200">Ok</response>
    /// <response code="404">Users not foud</response>
    /// <returns>User</returns>
    [HttpGet, Authorize]
    public async Task<ActionResult<IEnumerable<UserDTO>>> GetAll()
    {
        var usersDto = await _userService?.GetUsers();

        if(usersDto is null) return NotFound("Users not found");

        return Ok(usersDto);
    }

    /// <summary>
    /// Get User by Id
    /// </summary>
    /// <param name="email" example="example@email.com">The User email</param>
    /// <param name="password" example="password123!">The User password</param>
    /// <response code="200">Ok</response>
    /// <response code="404">User not found</response>
    /// <returns>User</returns>
    [HttpGet("{id:int}"), Authorize]
    public async Task<ActionResult<UserDTO>> GetById(int id) 
    {
        var userDto = await _userService?.GetUserById(id);
        if (userDto is null) return NotFound("User not found");
        return Ok(userDto);
    }

    /// <summary>
    /// Register a User
    /// </summary>
    /// <param name="email" example="example@email.com">The User email</param>
    /// <param name="password" example="password123!">The User password</param>
    /// <response code="200">Registered User</response>
    /// <response code="400">Inalid Data</response>
    /// <response code="500">An account with this email already exists.</response>
    /// <returns>User</returns>
    [HttpPost]
    public async Task<ActionResult> Post([FromBody] AddUserDTO addUserDto)
    {
        if (addUserDto is null) return BadRequest("Invalid Data");

        await _userService.AddUser(addUserDto);
        return Ok(addUserDto);
    }

    /// <summary>
    /// Update a User
    /// </summary>
    /// <param name="First Name">The User First Name</param>
    /// <param name="Last Name">The User Last Name</param>
    /// <param name="Email" example="example@email.com">The User email</param>
    /// <param name="Phone" example="+351999999999">The Phone Number</param>
    /// <param name="Adress" example="City Name">The Adress</param>
    /// <param name="Gender Id" example="1 for masculine; 2 for feminine">The Adress</param>
    /// <response code="200">Updated User</response>
    /// <response code="400">The email does not meet the requirements.</response>
    /// <response code="500">An account with this email already exists.</response>
    /// <returns>User</returns>
    [HttpPut("{id:int}"), Authorize]
    public async Task<ActionResult<UserDTO>> Put(int id, [FromBody] UpdateUserDTO updateUserDto)
    {

        if (updateUserDto is null) return BadRequest();

        //Verificar se é o usuario logado
        var accessToken = HttpContext.Request.Headers["Authorization"].ToString().Split(' ')[1];

        // Decodificar o token de acesso
        var handler = new JwtSecurityTokenHandler();
        var tokenS = handler.ReadToken(accessToken) as JwtSecurityToken;
        var userId = tokenS.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value;

        if (id == int.Parse(userId))
        {
            updateUserDto.IdUser = id;
        }
        else
        {
            return BadRequest("Unnauthorized!");
        }

        await _userService.UpdateUser(updateUserDto);

        return Ok(updateUserDto);
    }

    /// <summary>
    /// Delete User
    /// </summary>
    /// <response code="200">User deleted</response>
    /// <response code="404">User not found!</response>
    /// /// <response code="401">Unnouthorized!</response>
    /// <returns>string</returns>
    [HttpDelete("{id:int}"), Authorize]
    public async Task<ActionResult<UserDTO>> Delete(int id)
    {
        var userDto = await _userService?.GetUserById(id);

        if (userDto is null) return NotFound("User not found!");

        //Verificar se é o usuario logado
        var accessToken = HttpContext.Request.Headers["Authorization"].ToString().Split(' ')[1];

        // Decodificar o token de acesso
        var handler = new JwtSecurityTokenHandler();
        var tokenS = handler.ReadToken(accessToken) as JwtSecurityToken;
        var userId = tokenS.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value;

        if (id != int.Parse(userId))
        {
            return Unauthorized("Unnauthorized!");
        }

        await _userService?.DeleteUser(id);

        return Ok("User deleted");
    }
}