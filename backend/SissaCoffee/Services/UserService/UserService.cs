using AutoMapper;
using Microsoft.AspNetCore.Identity;
using SissaCoffee.Models;
using SissaCoffee.Models.DTOs.User;
using SissaCoffee.Repositories.RoleRepository;
using SissaCoffee.Repositories.UserRepository;
using SissaCoffee.Helpers.JwtUtils;

namespace SissaCoffee.Services.UserService;

public class UserService: IUserService
{
    private readonly IConfiguration _configuration;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IUserRepository _userRepository;
    private readonly IJwtUtils _jwtUtils;
    private readonly IRoleRepository _roleRepository;
    private readonly IMapper _mapper;

    public UserService(IConfiguration configuration, UserManager<ApplicationUser> userManager, IRoleRepository roleRepository, IUserRepository userRepository, IMapper mapper, IJwtUtils jwtUtils)
    {
        _configuration = configuration;
        _userManager = userManager;
        _roleRepository = roleRepository;
        _userRepository = userRepository;
        _mapper = mapper;
        _jwtUtils = jwtUtils;
    }

    public async Task<IdentityResult> RegisterUserAsync(RegisterUserDTO dto)
    {
        var customerRole = await _roleRepository.GetRoleByNameAsync("Customer");
        if (customerRole is null)
        {
            throw new Exception("Customer role not found.");
        }
        
        var user = new ApplicationUser
        {
            Email = dto.Email,
            UserName = dto.Email,
            FirstName = dto.FirstName,
            LastName = dto.LastName
        };

        var res = await _userManager.CreateAsync(user, dto.Password);

        if (res.Succeeded)
        {
            var secondRes = await _userManager.AddToRoleAsync(user, customerRole.Name);
            if (!secondRes.Succeeded)
            {
                throw new Exception("Failed to add user to role.");
            }
        }

        return res;
    }

    public async Task<string?> LoginUserAsync(LoginUserDTO dto)
    {
        var user = await _userManager.FindByEmailAsync(dto.Email);

        if (user is null) return null;

        if (!await _userManager.CheckPasswordAsync(user, dto.Password)) return null;

        return _jwtUtils.GenerateJwtToken(user);
    }
    
    public async Task<UserDTO?> GetUserDtoByIdAsync(Guid id)
    {
        var user =  await _userManager.FindByIdAsync(id.ToString());
        var roles = await _userManager.GetRolesAsync(user);
        return new UserDTO
        {
            Id = user.Id,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Roles = roles,
        };
    }
    
    public async Task<ApplicationUser?> GetUserByEmailAsync(string email)
    {
        return await _userManager.FindByEmailAsync(email);
    }
}