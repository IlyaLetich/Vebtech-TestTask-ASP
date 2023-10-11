using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel;
using System.Globalization;
using System.Text.RegularExpressions;
using TestTask.Data.Dtos;
using TestTask.Data.Entitys;
using TestTask.Data.Enums;
using TestTask.Exceptions;
using TestTask.Services.Interfaces;

namespace TestTask.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("get-users")]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Get users", Description = "Returns a list of all users.")]
        [SwaggerResponse(200, Type = typeof(IEnumerable<UserDto>), Description = "200 OK: Returns a list of all users.")]
        [SwaggerResponse(404, Description = "404 Not Found: No users were found in the database.")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        #region get-sorted-users

        [HttpGet]
        [Route("get-users-sorted-by-name")]
        [AllowAnonymous]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [SwaggerOperation(Summary = "Get users sotrted by name", Description = "Returns a list of all users sotrted by name.")]
        [SwaggerResponse(200, Type = typeof(IEnumerable<UserDto>), Description = "200 OK: Returns a list of all users sotrted by name.")]
        [SwaggerResponse(404, Description = "404 Not Found: No users were found in the database.")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsersSortedByName([FromQuery] bool directOrder = true)
        {
            return Ok(EditUsers.SortUsersByName(await _userService.GetAllUsersAsync(), directOrder));
        }

        [HttpGet]
        [Route("get-users-sorted-by-age")]
        [AllowAnonymous]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [SwaggerOperation(Summary = "Get users sotrted by age", Description = "Returns a list of all users sotrted by age.")]
        [SwaggerResponse(200, Type = typeof(IEnumerable<UserDto>), Description = "200 OK: Returns a list of all users sotrted by age.")]
        [SwaggerResponse(404, Description = "404 Not Found: No users were found in the database.")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsersSortedByAge([FromQuery] bool directOrder = true)
        {
            return Ok(EditUsers.SortUsersByAge(await _userService.GetAllUsersAsync(), directOrder));
        }

        [HttpGet]
        [Route("get-users-sorted-by-email")]
        [AllowAnonymous]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [SwaggerOperation(Summary = "Get users sotrted by email", Description = "Returns a list of users sotrted by email")]
        [SwaggerResponse(200, Type = typeof(IEnumerable<UserDto>), Description = "200 OK: Returns a list of all users sotrted by email.")]
        [SwaggerResponse(404, Description = "404 Not Found: No users were found in the database.")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsersSortedByEmail([FromQuery] bool directOrder = true)
        {
            return Ok(EditUsers.SortUsersByEmail(await _userService.GetAllUsersAsync(), directOrder));
        }

        [HttpGet]
        [Route("get-users-sorted-max-role")]
        [AllowAnonymous]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [SwaggerOperation(Summary = "Get users sotrted by max role", Description = "Returns a list of users sotrted by max role")]
        [SwaggerResponse(200, Type = typeof(IEnumerable<UserDto>), Description = "200 OK: Returns a list of all users sotrted by max role.")]
        [SwaggerResponse(404, Description = "404 Not Found: No users were found in the database.")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsersSortedByMaxRole([FromQuery] bool directOrder = true)
        {
            return Ok(EditUsers.SortUsersByMaxRole(await _userService.GetAllUsersAsync(), directOrder));
        }

        #endregion

        #region get-filter-users

        [HttpGet]
        [Route("get-filter-by-name")]
        [AllowAnonymous]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [SwaggerOperation(Summary = "Get users filter by name", Description = "Returns a list of all users filter by name.")]
        [SwaggerResponse(200, Type = typeof(IEnumerable<UserDto>), Description = "200 OK: Returns a list of all users filter by name.")]
        [SwaggerResponse(404, Description = "404 Not Found: No users were found in the database.")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsersFilteredByName([FromQuery] string name = null)
        {
            return Ok(EditUsers.FilterUsersByName(await _userService.GetAllUsersAsync(), name));
        }

        [HttpGet]
        [Route("get-filter-by-email")]
        [AllowAnonymous]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [SwaggerOperation(Summary = "Get users filter by email", Description = "Returns a list of all users filter by email.")]
        [SwaggerResponse(200, Type = typeof(IEnumerable<UserDto>), Description = "200 OK: Returns a list of all users filter by email.")]
        [SwaggerResponse(404, Description = "404 Not Found: No users were found in the database.")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsersFilteredByEmail([FromQuery] string name = null)
        {
            return Ok(EditUsers.FilterUsersByEmail(await _userService.GetAllUsersAsync(), name));
        }

        [HttpGet]
        [Route("get-filter-by-age")]
        [AllowAnonymous]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [SwaggerOperation(Summary = "Get users filter by age", Description = "Returns a list of all users filter by age.")]
        [SwaggerResponse(200, Type = typeof(IEnumerable<UserDto>), Description = "200 OK: Returns a list of all users filter by age.")]
        [SwaggerResponse(404, Description = "404 Not Found: No users were found in the database.")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsersFilteredByAge([FromQuery] int fromAge = 0, [FromQuery] int toAge = 1000)
        {
            return Ok(EditUsers.FilterUsersByAge(await _userService.GetAllUsersAsync(), fromAge, toAge));
        }
        [HttpGet]
        [Route("get-filter-by-role")]
        [AllowAnonymous]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [SwaggerOperation(Summary = "Get users filter by role", Description = "Returns a list of all users filter by role.")]
        [SwaggerResponse(200, Type = typeof(IEnumerable<UserDto>), Description = "200 OK: Returns a list of all users filter by role.")]
        [SwaggerResponse(404, Description = "404 Not Found: No users were found in the database.")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsersFilteredByRole([FromQuery] int fromRole = 0, [FromQuery] int toRole = 1000)
        {
            return Ok(EditUsers.FilterUsersByRole(await _userService.GetAllUsersAsync(), fromRole, toRole));
        }

        #endregion

        [HttpGet]
        [Route("filtered-and-sorted")]
        [AllowAnonymous]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [SwaggerOperation(Summary = "Get users filtered and sorted.", Description = "Returns a list of all users filtered and sorted.")]
        [SwaggerResponse(200, Type = typeof(IEnumerable<UserDto>), Description = "200 OK: Returns a list of all users filtered and sorted.")]
        [SwaggerResponse(400, Description = "400 Bad Request : The request Body is not valid")]
        [SwaggerResponse(404, Description = "404 Not Found: No users or role were found in the database.")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetFilteredAndSortedUsers(
            [FromQuery] string? nameFilter = null,
            [FromQuery] string? emailFilter = null,
            [FromQuery] int? fromAge = null,
            [FromQuery] int? toAge = null,
            [FromQuery] int? fromRole = null,
            [FromQuery] int? toRole = null,
            [FromQuery] string? sortBy = null,
            [FromQuery] bool? directOrder = null)
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(EditUsers.SortAndFilterUsers(users, nameFilter, emailFilter, fromAge, toAge, fromRole, toRole, sortBy, directOrder));
        }

        [HttpGet]
        [Route("filtered-sorted-and-paginated")]
        [AllowAnonymous]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [SwaggerOperation(Summary = "Get users filtered, sorted and paginated", Description = "Returns a list of all users filtered, sorted and paginated.")]
        [SwaggerResponse(200, Type = typeof(IEnumerable<UserDto>), Description = "200 OK: Returns a list of all users filtered, sorted and paginated.")]
        [SwaggerResponse(400, Description = "400 Bad Request : The request Body is not valid")]
        [SwaggerResponse(404, Description = "404 Not Found: No users or role were found in the database.")]
        public async Task<ActionResult<IEnumerable<PageUsers>>> GetFilteredSortedAndPaginatedUsers(
            [FromQuery] string? nameFilter = null,
            [FromQuery] string? emailFilter = null,
            [FromQuery] int? fromAge = null,
            [FromQuery] int? toAge = null,
            [FromQuery] int? fromRole = null,
            [FromQuery] int? toRole = null,
            [FromQuery] string? sortBy = null,
            [FromQuery] bool? directOrder = null,
            [FromQuery] int? pageSize = null)
        {
            var filteredAndSortedUsers = EditUsers.SortAndFilterUsers(
            await _userService.GetAllUsersAsync(),
            nameFilter,
            emailFilter,
            fromAge, toAge,
            fromRole,
            toRole,
            sortBy,
            directOrder);

            int countPage = pageSize ?? 10;

            var paginatedUserGroups = EditUsers.PaginateUsers(filteredAndSortedUsers, countPage);

            return Ok(paginatedUserGroups);
        }

        [HttpGet]
        [Route("users-paginated")]
        [AllowAnonymous]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [SwaggerOperation(Summary = "Get users paginated", Description = "Returns a list of all users paginated.")]
        [SwaggerResponse(200, Type = typeof(IEnumerable<UserDto>), Description = "200 OK: Returns a list of all users paginated.")]
        [SwaggerResponse(400, Description = "400 Bad Request : The request Body is not valid")]
        [SwaggerResponse(404, Description = "404 Not Found: No users or role were found in the database.")]
        public async Task<ActionResult<IEnumerable<PageUsers>>> GetPaginatedUsers(
            [FromQuery] int pageSize = 10)
        {
            return Ok(EditUsers.PaginateUsers(await _userService.GetAllUsersAsync(), pageSize));
        }

        [HttpPost]
        [Route("create-user")]
        [AllowAnonymous]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [SwaggerOperation(Summary = "Create user", Description = "Returns a new created user.")]
        [SwaggerResponse(201, Type = typeof(UserDto), Description = "201 OK: Returns a new created user.")]
        [SwaggerResponse(400, Description = "400 Bad Request : The request Body is not valid")]
        [SwaggerResponse(404, Description = "404 Not Found: No user or role were found in the database.")]
        public async Task<ActionResult<UserDto>> CreateUser(UserDto userDto)
        {
            var user = await _userService.CreateUserAsync(userDto);
            return CreatedAtAction(nameof(CreateUser), user);
        }

        [HttpGet]
        [Route("get-user/{id}")]
        [AllowAnonymous]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [SwaggerOperation(Summary = "Get user", Description = "Return user.")]
        [SwaggerResponse(200, Type = typeof(UserDto), Description = "200 OK: Return user.")]
        [SwaggerResponse(400, Description = "400 Bad Request : The request Body is not valid")]
        [SwaggerResponse(404, Description = "404 Not Found: No user or role were found in the database.")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            return Ok(user);
        }

        [HttpPut]
        [Route("update-user/{id}")]
        [AllowAnonymous]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [SwaggerOperation(Summary = "Update user", Description = "Returns a updated user.")]
        [SwaggerResponse(200, Type = typeof(UserDto), Description = "200 OK: Returns a new updated user.")]
        [SwaggerResponse(400, Description = "400 Bad Request : The request Body is not valid")]
        [SwaggerResponse(404, Description = "404 Not Found: No user or role were found in the database.")]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UserDto updateUserDto)
        {
            var updatedUser = await _userService.UpdateUserByIdAsync(id, updateUserDto);
            return Ok(updatedUser);
        }

        [HttpDelete]
        [Route("delete-user/{id}")]
        [AllowAnonymous]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [SwaggerOperation(Summary = "Delete user", Description = "Returns a deleted user.")]
        [SwaggerResponse(200, Type = typeof(UserDto), Description = "200 OK: Returns a deleted user..")]
        [SwaggerResponse(404, Description = "404 Not Found: No user or role were found in the database.")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var deletedUser = await _userService.DeleteUserAsync(id);
            return Ok(deletedUser);
        }

        [HttpPost]
        [Route("roles/{id}")]
        [AllowAnonymous]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [SwaggerOperation(Summary = "Assigns a role to the user.", Description = "Returns a updated user with new role.")]
        [SwaggerResponse(200, Type = typeof(UserDto), Description = "200 OK: Returns a updated user.")]
        [SwaggerResponse(400, Description = "400 Bad Request : The request Body is not valid")]
        [SwaggerResponse(404, Description = "404 Not Found: No user or role were found in the database.")]
        public async Task<IActionResult> AddRoleToUser(Guid id, [FromBody] RoleDto role)
        {
            var userWithRole = await _userService.AddRoleUserAsync(id, role);
            return Ok(userWithRole);
        }
    }
}
