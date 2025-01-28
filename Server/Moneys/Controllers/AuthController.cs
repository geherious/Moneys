using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Mvc;
using Moneys.Domain.Entities;
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

            var command = request.ToCommand();

            await handler.Handle(command);

            return Ok();
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login(LoginUserRequest request)
        {
            var handler = serviceProvider.GetRequiredService<ILoginUserHandler>();

            var command = request.ToCommand();
            
            var result = await handler.Handle(command);

            if (result is null)
                return Unauthorized();
            
            AppendRefreshCookie(Response.Cookies, result.RefreshToken);
                    
            return Ok(new
            {
                AccessToken = result.AccessToken
            });
        }

        [HttpPost("refresh")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Refresh()
        {
            var handler = serviceProvider.GetRequiredService<IRefreshUserTokenHandler>();

            if (!Request.Cookies.Any(x => x.Key == "X-Refresh-Token"))
            {
                return BadRequest("Refresh cookie is not set");
            }

            var cookie = Request.Cookies.First(x => x.Key == "X-Refresh-Token").Value;

            var result = await handler.Handle(
                new()
                {
                    Hash = cookie
                });
            
            if (result is null)
                return Unauthorized();
            
            AppendRefreshCookie(Response.Cookies, result.RefreshToken);
                    
            return Ok(new
            {
                AccessToken = result.AccessToken
            });
        }
        private void AppendRefreshCookie(IResponseCookies cookies, RefreshToken token)
        {
            cookies.Append(
                "X-Refresh-Token",
                token.Hash,
                new CookieOptions()
                {
                    Expires = token.ExpiresAt,
                    Secure = true,
                    HttpOnly = true
                });
        }
    }
}
