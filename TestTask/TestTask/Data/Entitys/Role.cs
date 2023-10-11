using TestTask.Data.Enums;

namespace TestTask.Data.Entitys
{
    public class Role
    {
        #region propertys

        public UserRole RoleId { get; set; }
        public string NameRole { get; set; }
        public ICollection<User> Users { get; set; }

        #endregion

        #region methods

        public Role() { }

        public Role (UserRole id, string role)
        {
            this.RoleId = id;
            this.NameRole = role;
            this.Users = new List<User>(); 
        }

        #endregion
    }
}
