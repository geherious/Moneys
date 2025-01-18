using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moneys.Domain.UseCases.Auth.RegisterUser;
using Moneys.Mappers;
using Moneys.Models;

namespace Moneys.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController(IServiceProvider serviceProvider) : ControllerBase
    {
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register(RegisterUserRequest request)
        {
            var handler = serviceProvider.GetRequiredService<IRegisterUserHandler>();

            var user = request.ToDomain();

            await handler.Handle(user, request.Password);

            return Ok();
        }
    }
}
