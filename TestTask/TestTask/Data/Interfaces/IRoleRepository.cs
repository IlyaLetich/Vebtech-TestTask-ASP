using TestTask.Data.Entitys;
using TestTask.Data.Enums;

namespace TestTask.Data.Interfaces
{
    public interface IRoleRepository
    {
        Task<Role> GetRoleByIdAsync(UserRole roleId);
        Task<List<Role>> GetAllRolesAsync();
        Task AddRoleAsync(Role role);
        Task UpdateRoleAsync(Role role);
        Task DeleteRoleAsync(UserRole roleId);
        Task<List<User>> GetUsersByRoleAsync(UserRole role);
        Task<IEnumerable<Role>> GetRolesByIdsAsync(IEnumerable<UserRole> roleIds);
    }
}
