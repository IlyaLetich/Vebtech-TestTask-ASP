using System.Numerics;
using TestTask.Data.Dtos;

namespace TestTask.Data.Entitys
{
    public class PageUsers
    {
        public ICollection<UserDto> Users { get; set; }
        public PageInformation PageInfo { get; set; }
        public PageUsers(ICollection<UserDto> users, PageInformation pageInfo)
        { 
            this.Users = users;
            this.PageInfo = pageInfo;
        }
    }
}
