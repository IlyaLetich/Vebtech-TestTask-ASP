using TestTask.Data.Entitys;
using TestTask.Data.Enums;

namespace TestTask.Data.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUserByIdAsync(Guid userId);
        Task<List<User>> GetAllUsersAsync();
        Task AddUserAsync(User user);
        Task<User> UpdateUserAsync(User user);
        Task DeleteUserAsync(Guid userId);
        Task AddUserRoleAsync(Guid userId, UserRole role);
        Task<bool> IsEmailInUseAsync(string email);
    }
}
