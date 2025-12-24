using QuizApp.Models;
using QuizApp.Helper;
using QuizApp.Dtos;

namespace QuizApp.Interfaces;

public interface IUserRepository
{
    Task<User> AddUserAsync(User user);
    Task<IEnumerable<User>> GetAllUserAsync();
    Task<User?> GetUserByIdAsync(int id);
    // Task<User> UpdateUserAsync(int id, UpdateUserDto updateUserDto, User existingUser);
    // Task<int> DeleteUserAsync(int id);
}

public interface IUserService
{
    Task<StandardApiResponse<CreateUserResponse>> AddUserAsync(CreateUserDto createUserDto);
    Task<StandardApiResponse<CreateUserResponse>> AddTeacherAsync(CreateUserDto createUserDto, bool isAdminUser);
    Task<CreateUserResponse?> CreateUserMethodAsync(CreateUserDto createUserDto, int roleId);
    Task<StandardApiResponse<IEnumerable<GetAllUserResponse>>> GetAllUserAsync();
    // Task<StandardApiResponse<User>> GetUserByIdAsync(int id);
    // Task<StandardApiResponse<User>> UpdateUserAsync(int id, UpdateUserDto updateUserDto);
    // Task<StandardApiResponse<User>> DeleteUserAsync(int id);
}
