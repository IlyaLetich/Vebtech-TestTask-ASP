using System.Collections.Generic;
using TestTask.Data.Dtos;
using TestTask.Data.Enums;

namespace TestTask.Data.Entitys
{
    public class User
    {
        #region propertys

        public Guid UserId { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public ICollection<Role> Roles { get; set; }

        #endregion

        #region methods

        public User() { }
        public User(string name, int age, string email, string password)
        {
            this.Name = name;
            this.Age = age;
            this.Email = email;
            this.Roles = new HashSet<Role>();
            this.Password = password;
        }
        public User(string name, int age, string email, string password, ICollection<Role> roles)
        {
            this.Name = name;
            this.Age = age;
            this.Email = email;
            this.Roles = roles;
            this.Password = password;
        }

        #endregion
    }
}
