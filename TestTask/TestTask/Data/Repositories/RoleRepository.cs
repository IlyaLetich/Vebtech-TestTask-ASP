using Microsoft.EntityFrameworkCore;
using TestTask.Data.DataContexts;
using TestTask.Data.Entitys;
using TestTask.Data.Enums;
using TestTask.Data.Interfaces;

namespace TestTask.Data.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        #region fields

        private readonly DataContext _context;

        #endregion

        #region methods

        public RoleRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<Role> GetRoleByIdAsync(UserRole roleId)
        {
            return await _context.Roles.FirstOrDefaultAsync(r => r.RoleId == roleId);
        }
        public async Task<List<Role>> GetAllRolesAsync()
        {
            return await _context.Roles.ToListAsync();
        }
        public async Task<List<User>> GetUsersByRoleAsync(UserRole role)
        {
            return await _context.Users
                .Include(u => u.Roles)
                .Where(u => u.Roles.Any(r => r.RoleId == role))
                .ToListAsync();
        }
        public async Task AddRoleAsync(Role role)
        {
            _context.Roles.Add(role);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateRoleAsync(Role role)
        {
            _context.Entry(role).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        public async Task DeleteRoleAsync(UserRole roleId)
        {
            var role = await _context.Roles.FindAsync(roleId);
            if (role != null)
            {
                _context.Roles.Remove(role);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<IEnumerable<Role>> GetRolesByIdsAsync(IEnumerable<UserRole> roleIds)
        {
            var roles = await _context.Roles
                .Where(r => roleIds.Contains(r.RoleId))
                .ToListAsync();

            return roles;
        }

        #endregion
    }
}
