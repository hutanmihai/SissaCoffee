using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using SissaCoffee.Models;
using SissaCoffee.Models.DTOs.User;
using SissaCoffee.Repositories.RoleRepository;

namespace SissaCoffee.Services;

public class UserService: IUserService
{
    private readonly IConfiguration _configuration;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IRoleRepository _roleRepository;

    public UserService(IConfiguration configuration, UserManager<ApplicationUser> userManager, IRoleRepository roleRepository)
    {
        _configuration = configuration;
        _userManager = userManager;
        _roleRepository = roleRepository;
    }

    public async Task<IdentityResult> RegisterUserAsync(RegisterUserDTO dto)
    {
        var customerRole = await _roleRepository.GetByNameAsync("Customer");
        if (customerRole is null)
        {
            throw new Exception("Customer role not found.");
        }
        
        var user = new ApplicationUser
        {
            Email = dto.Email,
            UserName = dto.Email,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Roles = new List<ApplicationRole> { customerRole }
        };

        return await _userManager.CreateAsync(user, dto.Password);
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
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim("UserID", user.Id.ToString()),
                new Claim(_options.ClaimsIdentity.RoleClaimType, roles.FirstOrDefault()),   
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