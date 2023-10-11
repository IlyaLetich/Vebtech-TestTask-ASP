using TestTask.Data.Entitys;
using TestTask.Data.Enums;

namespace TestTask.Data.Dtos
{
    public class AddUserRoleDto
    {
        public Guid UserId { get; set; }
        public UserRole Role { get; set; }
    }
}
