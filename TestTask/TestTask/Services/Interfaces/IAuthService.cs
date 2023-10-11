using Microsoft.AspNetCore.Mvc;
using TestTask.Data.Dtos;

namespace TestTask.Services.Interfaces
{
    public interface IAuthService
    {
        public Task<string> Login(UserLogin userLogin);

        public Task<UserDto> Register(UserDto userDto);
    }
}
