using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Data;
using TestTask.Data.Dtos;
using TestTask.Data.Entitys;
using TestTask.Data.Enums;
using TestTask.Data.Interfaces;
using TestTask.Data.Repositories;
using TestTask.Exceptions;
using TestTask.Services.Interfaces;

namespace TestTask.Services.Classes
{
    public class UserService : IUserService
    {
        #region fields

        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        #endregion

        #region methods

        public UserService(IUserRepository userRepository, IRoleRepository roleRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllUsersAsync();

            if (!users.Any())
                throw new UsersNotFoundException("Users not found");

            return _mapper.Map<IEnumerable<UserDto>>(users); 
        }
        public async Task<UserDto> GetUserByIdAsync(Guid id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);

            if (user == null)
                throw new UserNotFoundException("Users not found");

            return _mapper.Map<UserDto>(user);
        }
        public async Task<UserDto> CreateUserAsync(UserDto userDto)
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
        public async Task<UserDto> UpdateUserByIdAsync(Guid id, UserDto updateUserDto)
        {
            var user = await _userRepository.GetUserByIdAsync(id);

            if (user == null)
                throw new UserNotFoundException("Users not found");

            user.Name = updateUserDto.Name;
            user.Age = updateUserDto.Age;
            user.Email = updateUserDto.Email;
            user.Roles = new HashSet<Role>();

            foreach (var roleDto in updateUserDto.Roles)
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

            await _userRepository.UpdateUserAsync(user);

            return _mapper.Map<UserDto>(await _userRepository.GetUserByIdAsync(id));
        }
        public async Task<UserDto> DeleteUserAsync(Guid id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);

            if (user == null)
                throw new UserNotFoundException("Users not found");

            await _userRepository.DeleteUserAsync(id);

            return _mapper.Map<UserDto>(user);
        }
        public async Task<UserDto> AddRoleUserAsync(Guid id, RoleDto role)
        {
            var userO = await _userRepository.GetUserByIdAsync(id);

            if (userO == null)
                throw new UserNotFoundException("Users not found");

            userO.Roles.Add(await _roleRepository.GetRoleByIdAsync(role.RoleId));

            return _mapper.Map<UserDto>(await _userRepository.UpdateUserAsync(userO));
        }

        #endregion
    }
}
