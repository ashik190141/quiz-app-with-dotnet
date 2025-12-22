using System.Text.Json;
using QuizApp.Dtos;
using QuizApp.Helper;
using QuizApp.Interfaces;
using QuizApp.Models;

namespace QuizApp.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<StandardApiResponse<CreateUserResponse>> AddUserAsync(CreateUserDto createUserDto)
    {
        var createdUser = await CreateUserMethodAsync(createUserDto, (int)Roles.Student);
        if(createdUser != null)
        {
            return new StandardApiResponse<CreateUserResponse>(true, "User added successfully", createdUser);
        }
        return new StandardApiResponse<CreateUserResponse>(false, "Failed to add user", null);
    }

    public async Task<StandardApiResponse<CreateUserResponse>> AddTeacherAsync(CreateUserDto createUserDto, bool isAdminUser)
    {
        if (!isAdminUser)
        {
            return new StandardApiResponse<CreateUserResponse>(false, "Only admin can create teacher", null);
        }
        var createdTeacher = await CreateUserMethodAsync(createUserDto, (int)Roles.Teacher);
        if(createdTeacher != null)
        {
            return new StandardApiResponse<CreateUserResponse>(true, "Teacher added successfully", createdTeacher);
        }
        return new StandardApiResponse<CreateUserResponse>(false, "Failed to add user", null);
    }

    public async Task<CreateUserResponse?> CreateUserMethodAsync(CreateUserDto createUserDto, int roleId)
    {
        createUserDto.Password = BCrypt.Net.BCrypt.HashPassword(createUserDto.Password);
        var newUser = new User
        {
            Name = createUserDto.Name,
            Email = createUserDto.Email,
            Password = createUserDto.Password,
            RoleId = roleId
        };
        User user = await _userRepository.AddUserAsync(newUser);
        if (user != null)
        {
            var serializeUser = JsonSerializer.Serialize(user);
            CreateUserResponse? createUserResponse = JsonSerializer.Deserialize<CreateUserResponse>(serializeUser);
            return createUserResponse;
        }
        return null;
    }

    public async Task<StandardApiResponse<IEnumerable<GetAllUserResponse>>> GetAllUserAsync()
    {
        var users = await _userRepository.GetAllUserAsync();
        if(users == null)
        {
            return new StandardApiResponse<IEnumerable<GetAllUserResponse>>(false, "User not found", []);
        }
        var serializeUsers = JsonSerializer.Serialize(users);
        IEnumerable<GetAllUserResponse>? allUsers = JsonSerializer.Deserialize<IEnumerable<GetAllUserResponse>>(serializeUsers);
        return new StandardApiResponse<IEnumerable<GetAllUserResponse>>(true, "Users Fetch Successfully", allUsers);
    }
}