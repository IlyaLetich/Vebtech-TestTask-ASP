using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;
using TestTask.Data.Dtos;
using TestTask.Data.Entitys;
using TestTask.Data.Interfaces;
using TestTask.Exceptions;
using TestTask.Services.Interfaces;

namespace TestTask.Services.Classes
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;

        public AuthService(IUserRepository userRepository, IRoleRepository roleRepository, IMapper mapper, IConfiguration configuration)
        {
            _mapper = mapper;
            _configuration = configuration;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        public async Task<string> Login(UserLogin userLogin)
        {
            if(userLogin.Email == null && userLogin.Password == null)
            {
                throw new UserNotFoundException("User no found");
            }
            var user = (await _userRepository.GetAllUsersAsync()).Find(u => u.Email == userLogin.Email) ??
                throw new UserNotFoundException("User no found");

            if (!BCrypt.Net.BCrypt.Verify(userLogin.Password, user.Password))
                throw new InvalidCredentialException("Wrong password");

            var handler = new JsonWebTokenHandler();

            var key = Encoding.UTF8.GetBytes(_configuration.GetSection("ApplicationSettings").GetValue<string>("Secret") ??
                                             string.Empty);

            var token = handler.CreateToken(new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim("id", user.UserId.ToString()),
                new Claim("email", user.Email)
            }),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256),
                Expires = DateTime.Now.AddDays(1)
            });

            return token;
        }
        public async Task<UserDto> Register(UserDto userDto)
        {
            if (await _userRepository.IsEmailInUseAsync(userDto.Email))
            {
                throw new ArgumentException("This email already exists");
            }

            if (userDto.Name == null || userDto.Email == null || userDto.Roles == null || userDto.Password == null)
            {
                throw new ArgumentException("Fields are empty");
            }

            if (userDto.Age <= 0)
            {
                throw new ArgumentException("Incorrectly specified age");
            }

            var user = new User(userDto.Name, userDto.Age, userDto.Email, BCrypt.Net.BCrypt.HashPassword(userDto.Password));

            foreach (var roleDto in userDto.Roles)
            {
                var role = await _roleRepository.GetRoleByIdAsync(roleDto.RoleId);
                if (role != null)
                {
                    user.Roles.Add(role);
                }
                else
                {
                    throw new RoleNotFoundException($"Role with ID {roleDto.RoleId} not found");
                }
            }

            await _userRepository.AddUserAsync(user);

            return _mapper.Map<UserDto>(await _userRepository.GetUserByIdAsync(user.UserId));
        }
    }
}
