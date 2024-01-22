using Microsoft.AspNetCore.Mvc;
using Relias.UserProfile.Cqrs.App.Commands;
using Relias.UserProfile.Cqrs.App.Dto;

namespace Relias.{{ cookiecutter.solution_name }}.Cqrs.Api.Controllers.V1
{
    /// <summary>
    /// User Profile command controller (CQRS pattern)
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class {{ cookiecutter.solution_name }}CommandController : ApiControllerBase
    {
        /// <summary>
        /// Creates user profile and returns <see cref="{{ cookiecutter.solution_name }}Dto"></see> for a given user id
        /// </summary>
        [HttpPost(Name = "Create{{ cookiecutter.solution_name }}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof({{ cookiecutter.solution_name }}Dto))]
        public async Task<IActionResult> Create([FromBody] {{ cookiecutter.solution_name }}Dto userProfileDto)
        {
            var command = new Create{{ cookiecutter.solution_name }}Command.Command { UserProfile = userProfileDto };
            var result = await Mediator.Send(command);
            return new OkObjectResult(result);
        }

        /// <summary>
        /// Updates user profile and returns updated <see cref="{{ cookiecutter.solution_name }}Dto"></see> for a given user id
        /// </summary>
        [HttpPut(Name = "Update{{ cookiecutter.solution_name }}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof({{ cookiecutter.solution_name }}Dto))]
        public async Task<IActionResult> Update([FromBody] {{ cookiecutter.solution_name }}Dto userProfileDto)
        {
            var command = new Update{{ cookiecutter.solution_name }}Command.Command { UserProfile = userProfileDto };
            var result = await Mediator.Send(command);
            return new OkObjectResult(result);
        }

        /// <summary>
        /// Deletes user profile for a given user id
        /// </summary>
        [HttpDelete("{userId}", Name = "Delete{{ cookiecutter.solution_name }}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete([FromRoute] string userId)
        {
            var command = new Delete{{ cookiecutter.solution_name }}Command.Command { UserId = userId};
            await Mediator.Send(command);
            return Ok();
        }
    }
}
