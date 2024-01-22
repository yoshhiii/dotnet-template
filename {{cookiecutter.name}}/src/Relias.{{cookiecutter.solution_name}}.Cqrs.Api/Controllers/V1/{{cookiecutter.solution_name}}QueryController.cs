using Microsoft.AspNetCore.Mvc;
using Relias.UserProfile.Cqrs.App.Dto;
using Relias.{{ cookiecutter.solution_name }}.Cqrs.App.Queries;

namespace Relias.{{ cookiecutter.solution_name }}.Cqrs.Api.Controllers.V1
{
    /// <summary>
    /// User Profile query controller (CQRS pattern)
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class {{ cookiecutter.solution_name }}QueryController : ApiControllerBase
    {
        /// <summary>
        /// Returns <see cref="{{ cookiecutter.solution_name }}Dto"></see> for a given user id
        /// </summary>
        [HttpGet("{userId}", Name = "Get{{ cookiecutter.solution_name }}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof({{ cookiecutter.solution_name }}Dto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> Get([FromRoute] string userId)
        {
            var query = new Get{{ cookiecutter.solution_name }}Query.Query { UserId = userId };
            var result = await Mediator.Send(query);
            return new OkObjectResult(result);
        }
    }
}
