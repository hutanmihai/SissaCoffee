using Microsoft.AspNetCore.Identity;
using SissaCoffee.Models;
using SissaCoffee.Models.DTOs.User;

namespace SissaCoffee.Services.UserService;

public interface IUserService
{
    public Task<IdentityResult> RegisterUserAsync(RegisterUserDTO dto);
    public Task<string?> LoginUserAsync(LoginUserDTO dto);
    public Task<UserDTO?> GetUserDtoByIdAsync(Guid id);
    public Task<ApplicationUser?> GetUserByEmailAsync(string email);
}