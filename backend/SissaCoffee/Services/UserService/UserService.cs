using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using SissaCoffee.Models;
using SissaCoffee.Models.DTOs.User;
using SissaCoffee.Repositories.RoleRepository;
using SissaCoffee.Repositories.UserRepository;

namespace SissaCoffee.Services.UserService;

public class UserService: IUserService
{
    private readonly IConfiguration _configuration;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IMapper _mapper;

    public UserService(IConfiguration configuration, UserManager<ApplicationUser> userManager, IRoleRepository roleRepository, IUserRepository userRepository, IMapper mapper)
    {
        _configuration = configuration;
        _userManager = userManager;
        _roleRepository = roleRepository;
        _userRepository = userRepository;
        _mapper = mapper;
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

        IdentityOptions _options = new IdentityOptions();
        var roles = await _userManager.GetRolesAsync(user);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new []
            {
                new Claim("UserId", user.Id.ToString())
            }),
            Expires = DateTime.UtcNow.AddDays(10),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"])),
                SecurityAlgorithms.HmacSha256Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var securityToken = tokenHandler.CreateToken(tokenDescriptor);
        
        return tokenHandler.WriteToken(securityToken);
    }
}