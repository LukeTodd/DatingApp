using System.Threading.Tasks;
using datingapp.api.Data;
using datingapp.api.Models;
using Microsoft.AspNetCore.Mvc;

namespace datingapp.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        public AuthController(IAuthRepository repo)
        {
            _repo = repo;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(string username, string password)
        {
            // validate request

            username = username.ToLower();

            if (await _repo.UserExists(username))
                return BadRequest("Username already exists");

            var userToCreate = new User
            {
                Username = username
            };

            var createdUser = await _repo.Register(userToCreate, password);

            return StatusCode(201);
        }
    }
}