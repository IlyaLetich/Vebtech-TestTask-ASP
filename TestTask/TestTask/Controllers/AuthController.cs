using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TestTask.Data.Dtos;
using TestTask.Data.Entitys;
using TestTask.Exceptions;
using TestTask.Services.Classes;
using TestTask.Services.Interfaces;

namespace TestTask.Controllers
{
    [ApiController]
    [Route("api/auth")]
    [AllowAnonymous]
    [SwaggerTag("This controller is used to manage authentication")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [Route("login")]
        [SwaggerOperation(Summary = "Login to the system", Description = "Login to the system")]
        [SwaggerResponse(200, Type = typeof(string), Description = "200 OK : Returns the jwt-token of the user")]
        [SwaggerResponse(400, Description = "400 Bad Request : The request Body is not valid")]
        [SwaggerResponse(401, Description = "401 Unauthorized : The credentials are not valid")]
        public async Task<string> Login([FromBody] UserLogin userLogin)
        {
            return await _authService.Login(userLogin);
        }

        [HttpPost]
        [Route("register")]
        [SwaggerOperation(Summary = "Register to the system", Description = "Register to the system")]
        [SwaggerResponse(200, Type = typeof(UserDto), Description = "200 OK : Returns the registered user")]
        [SwaggerResponse(400, Description = "400 Bad Request : The request Body is not valid")]
        [SwaggerResponse(404, Description = "404 Not Found: Users or Role not found.")]
        public async Task<IActionResult> Register([FromBody] UserDto userDto)
        {
            return Ok(await _authService.Register(userDto));
        }
    }
}
