using TestTask.Data.Entitys;

namespace TestTask.Data.Dtos
{
    public class UserDto
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public ICollection<RoleDto> Roles { get; set; }

        public UserDto() { }
        public UserDto(string name, int age, string email, ICollection<Role> roles, string password) 
        {
            this.Password = password;
            this.Name = name;
            this.Age = age;
            this.Email = email;
            this.Roles = new HashSet<RoleDto>();

            roles.ToList().ForEach((role) => Roles.Add(new RoleDto(role.RoleId)));
        }
    }
}
