using TestTask.Data.Enums;

namespace TestTask.Data.Dtos
{
    public class RoleDto
    {
        public UserRole RoleId { get; set; }

        public RoleDto(UserRole roleId)
        {
            this.RoleId = roleId;
        }
    }
}
