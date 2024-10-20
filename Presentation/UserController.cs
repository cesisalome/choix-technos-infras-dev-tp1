using Choix_des_technos_et_infras_de_développement___TP1.Application;
using Choix_des_technos_et_infras_de_développement___TP1.Application.User.Commands;
using Choix_des_technos_et_infras_de_développement___TP1.Application.User.Queries;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Choix_des_technos_et_infras_de_développement___TP1.Presentation
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly GetUserQuery _getUserQuery;
        private readonly AddUserCommand _addUserCommand;
        private readonly UpdateUserCommand _updateUserCommand;
        private readonly DeleteUserCommand _deleteUserCommand;

        public UserController(GetUserQuery getUserQuery, AddUserCommand addUserCommand, UpdateUserCommand updateUserCommand, DeleteUserCommand deleteUserCommand)
        {
            _getUserQuery = getUserQuery;
            _addUserCommand = addUserCommand;
            _updateUserCommand = updateUserCommand;
            _deleteUserCommand = deleteUserCommand;
        }

        [HttpGet("/{id}")]
        public async Task<IActionResult> GetUserById([FromRoute, Required] int id, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _getUserQuery.GetUserByIdAsync(id, cancellationToken);

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
                await _addUserCommand.AddUserAsync(user, cancellationToken);
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
                await _updateUserCommand.UpdateUserAsync(id, user, cancellationToken);
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
                await _deleteUserCommand.DeleteUserByIdAsync(id, cancellationToken);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
