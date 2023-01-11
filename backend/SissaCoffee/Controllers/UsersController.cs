using Microsoft.AspNetCore.Mvc;
using SissaCoffee.Models;
using SissaCoffee.Models.DTOs.User;
using SissaCoffee.Repositories.RoleRepository;
using SissaCoffee.Repositories.UserRepository;

namespace SissaCoffee.Controllers
{
    [Route("api/user/")]
    [ApiController]
    //[Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _usersRepository;
        private readonly IRoleRepository _rolesRepository;

        public UsersController(IUserRepository usersRepository, IRoleRepository rolesRepository)
        {
            _usersRepository = usersRepository;
            _rolesRepository = rolesRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers()
        {
            var users = (await _usersRepository.GetAllAsync()).Select(user => new UserDTO()
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Roles = user.Roles.Select(x => x.Name).ToList(),
            });
            
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUser(Guid id)
        {
            var user = await _usersRepository.FindByIdAsync(id);
            if (user is null)
            {
                return NotFound();
            }

            return new UserDTO
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Roles = user.Roles.Select(x => x.Name).ToList(),
            };
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(Guid id, UserDTO user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            var realUser = await _usersRepository.FindByIdAsync(id);
            if (realUser is null)
            {
                return NotFound();
            }
            
            realUser.FirstName = user.FirstName;
            realUser.LastName = user.LastName;
            realUser.Email = user.Email;
            realUser.Roles.Clear();

            foreach (var roleName in user.Roles)
            {
                var role = await _rolesRepository.GetByNameAsync(roleName);
                if (role is null)
                {
                    return NotFound();
                }
                realUser.Roles.Add(role);
            }

            await _usersRepository.UpdateAsync(realUser);

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<ApplicationUser>> PostUser([FromBody] UserDTO dto)
        {
            var roles = (await _rolesRepository.GetAllAsync())
                .Where(role => dto.Roles.Contains(role.Name))
                .ToList();

            var user = new ApplicationUser
            {
                Email = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Roles = roles
            };
            await _usersRepository.CreateAsync(user);

            return CreatedAtAction("GetUser", user);
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var user = await _usersRepository.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            await _usersRepository.DeleteAsync(user);

            return NoContent();
        }
    }
}