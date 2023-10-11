namespace TestTask.Data.Dtos
{
    public class UpdatedUserDto
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public ICollection<RoleDto> Roles { get; set; }
        public UpdatedUserDto() { }
    }
}
