using Microsoft.AspNetCore.Mvc;
using Moneys.Domain.UseCases.Auth;
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
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Register(RegisterUserRequest request)
        {
            var handler = serviceProvider.GetRequiredService<IRegisterUserHandler>();

            var user = request.ToDomain();

            await handler.Handle(user, request.Password);

            return Ok();
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login(LoginUserRequest request)
        {
            var handler = serviceProvider.GetRequiredService<ILoginUserHandler>();
            
            var result = await handler.Handle(request.Email, request.Password);

            if (result is null)
                return Unauthorized();

            return Ok(result);
        }
    }
}
