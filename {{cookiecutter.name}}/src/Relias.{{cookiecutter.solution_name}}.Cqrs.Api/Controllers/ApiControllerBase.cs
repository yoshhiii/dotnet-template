using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Relias.{{cookiecutter.solution_name}}.Cqrs.Api.Controllers
{
    /// <summary>
    /// API controller base class which holds an instance of <see cref="ISender"/> Mediatr service
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public abstract class ApiControllerBase : ControllerBase
    {
        private ISender _mediator = null!;

        protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
    }
}
