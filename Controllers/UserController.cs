using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizApp.Dtos;
using QuizApp.Interfaces;

namespace QuizApp.Controllers;

[Route("[controller]")]
[ApiController]

public class UserController : CustomBaseController
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public async Task<IActionResult> AddUser([FromBody] CreateUserDto createUserDto)
    {
        var response = await _userService.AddUserAsync(createUserDto);
        return Ok(response);
    }

    [Authorize]
    [HttpPost("teacher")]
    public async Task<IActionResult> AddTeacher([FromBody] CreateUserDto createUserDto)
    {
        var response = await _userService.AddTeacherAsync(createUserDto, IsAdminUser());
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        var response = await _userService.GetAllUserAsync();
        return Ok(response);
    }
}