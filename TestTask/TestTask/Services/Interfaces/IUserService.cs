using Microsoft.Extensions.Logging;
using TestTask.Data.Dtos;
using TestTask.Data.Entitys;

namespace TestTask.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
        Task<UserDto> CreateUserAsync(UserDto userDto);
        Task<UserDto> GetUserByIdAsync(Guid id);
        Task<UserDto> UpdateUserByIdAsync(Guid id, UserDto updateUserDto);
        Task<UserDto> DeleteUserAsync(Guid id);
        Task<UserDto> AddRoleUserAsync(Guid id, RoleDto role);
    }
}
