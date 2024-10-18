using Choix_des_technos_et_infras_de_développement___TP1.Application;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Choix_des_technos_et_infras_de_développement___TP1.Presentation
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet("/{id}")]
        public async Task<IActionResult> GetUserById([FromRoute, Required] int id, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _userService.GetUserByIdAsync(id, cancellationToken);

                if (result == null)
                {
                    return NotFound();
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("/")]
        public async Task AddUserAsync([FromBody, Required] UserModel user, CancellationToken cancellationToken)
        {
            try
            {
                await _userService.AddUserAsync(user, cancellationToken);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        [HttpPost("/{id}/update")]
        public async Task UpdateUserAsync([FromRoute, Required] int id, [FromBody, Required] UserModel user, CancellationToken cancellationToken)
        {
            try
            {
                await _userService.UpdateUserAsync(id, user, cancellationToken);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        [HttpDelete("/{id}")]
        public async Task DeleteUser([FromRoute, Required] int id, CancellationToken cancellationToken)
        {
            try
            {
                await _userService.DeleteUserByIdAsync(id, cancellationToken);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
