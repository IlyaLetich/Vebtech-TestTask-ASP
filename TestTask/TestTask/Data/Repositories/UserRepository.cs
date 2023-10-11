using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Data;
using TestTask.Data.DataContexts;
using TestTask.Data.Entitys;
using TestTask.Data.Enums;
using TestTask.Data.Interfaces;
using TestTask.Exceptions;

namespace TestTask.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        #region fields

        private readonly DataContext _context;

        #endregion

        #region methods

        public UserRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<User> GetUserByIdAsync(Guid userId)
        {
            var users = await _context.Users
                .Include(u => u.Roles)
                .ToListAsync();

            return users.Find(u => userId == u.UserId);
        }
        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _context.Users
                .Include(u => u.Roles)
                .ToListAsync();
        }
        public async Task AddUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }
        public async Task<User> UpdateUserAsync(User user)
        {
            try
            {
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
                return user;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
            
        }
        public async Task AddUserRoleAsync(Guid userId, UserRole role)
        {
            var user = await _context.Users.Include(u => u.Roles).FirstOrDefaultAsync(u => u.UserId == userId);
            if (user != null)
            {
                var existingRole = await _context.Roles.FirstOrDefaultAsync(r => r.RoleId == role);
                if (existingRole != null)
                {
                    user.Roles.Add(existingRole);
                    await _context.SaveChangesAsync();
                }
            }
        }
        public async Task DeleteUserAsync(Guid userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<bool> IsEmailInUseAsync(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }

        #endregion
    }
}
