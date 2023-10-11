using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using TestTask.Data.Dtos;

namespace TestTask.Data.Entitys
{
    static public class EditUsers
    {
        public static IEnumerable<UserDto> SortUsersByName(IEnumerable<UserDto> userDtos, bool directOrder = true)
        {
            IEnumerable<UserDto> sortedUsers = directOrder ? userDtos.OrderBy(u => u.Name) : userDtos.OrderByDescending(u => u.Name);

            return sortedUsers;
        }
        public static IEnumerable<UserDto> SortUsersByAge(IEnumerable<UserDto> userDtos, bool directOrder = true)
        {
            IEnumerable<UserDto> sortedUsers = directOrder ? userDtos.OrderBy(u => u.Age) : userDtos.OrderByDescending(u => u.Age);

            return sortedUsers;

        }
        public static IEnumerable<UserDto> SortUsersByEmail(IEnumerable<UserDto> userDtos, bool directOrder = true)
        {
            IEnumerable<UserDto> sortedUsers = directOrder ? userDtos.OrderBy(u => u.Email) : userDtos.OrderByDescending(u => u.Email);

            return sortedUsers;

        }
        public static IEnumerable<UserDto> SortUsersByMaxRole(IEnumerable<UserDto> userDtos, bool directOrder = true)
        {
            IEnumerable<UserDto> sortedUsers = directOrder ? userDtos.OrderBy(u => u.Roles.Max(r => r.RoleId)) : userDtos.OrderByDescending(u => u.Roles.Max(r => r.RoleId));

            return sortedUsers;
        }
        public static IEnumerable<UserDto> FilterUsersByName(IEnumerable<UserDto> userDtos, string name = null)
        {

            if (name == null)
                return userDtos;
            else if (!string.IsNullOrEmpty(name))
            {
                var regex = new Regex(name, RegexOptions.IgnoreCase);
                return userDtos.Where(u => regex.IsMatch(u.Name)).ToList();
            }
            else
                return userDtos;
        }
        public static IEnumerable<UserDto> FilterUsersByEmail(IEnumerable<UserDto> userDtos, string email = null)
        {
            if (email == null)
                return userDtos;
            else if (!string.IsNullOrEmpty(email))
            {
                var regex = new Regex(email, RegexOptions.IgnoreCase);
                return userDtos.Where(u => regex.IsMatch(u.Email)).ToList();
            }
            else
                return userDtos; 
        }
        public static IEnumerable<UserDto> FilterUsersByAge(IEnumerable<UserDto> userDtos, int? fromAge = null, int? toAge = null)
        {
            var numberOne = fromAge ?? 0;
            var numberSecond = toAge ?? 0;
            if (fromAge == null || toAge == null) return userDtos;
            return userDtos.Where(u => u.Age <= Math.Max(numberOne, numberSecond) && u.Age >= Math.Min(numberOne, numberSecond));
        }
        public static IEnumerable<UserDto> FilterUsersByRole(IEnumerable<UserDto> userDtos, int? fromRole = null, int? toRole = null)
        {
            if (fromRole == null || toRole == null) return userDtos;
            return userDtos.Where(u => u.Roles.Max(r => (int)r.RoleId) >= fromRole && u.Roles.Max(r => (int)r.RoleId) <= toRole);
        }
        public static IEnumerable<PageUsers> PaginateUsers(IEnumerable<UserDto> users, int pageSize)
        {
            var totalItems = users.Count();
            var pageCount = (int)Math.Ceiling((decimal)totalItems / pageSize);

            var paginatedUserGroups = new List<PageUsers>();

            for (int i = 0; i < pageCount; i++)
            {
                var pageUsers = users.Skip(i * pageSize).Take(pageSize).ToList();
                var pageInformation = new PageInformation(i + 1, pageSize, totalItems);
                var pageUser = new PageUsers(pageUsers, pageInformation);
                paginatedUserGroups.Add(pageUser);
            }

            return paginatedUserGroups;
        }
        public static IEnumerable<UserDto> SortAndFilterUsers(
            IEnumerable<UserDto> userDtos, 
            string? nameFilter = null, 
            string? emailFilter = null, 
            int? fromAge = null, 
            int? toAge = null, 
            int? fromRole = null, 
            int? toRole = null, 
            string? sortBy = null, 
            bool? directOrder = null)
        {
            bool orderBy = directOrder ?? true;

            var filteredUsers = userDtos;

            filteredUsers = FilterUsersByName(filteredUsers, nameFilter);

            filteredUsers = FilterUsersByEmail(filteredUsers, emailFilter);

            filteredUsers = FilterUsersByAge(filteredUsers, fromAge, toAge);

            filteredUsers = FilterUsersByRole(filteredUsers, fromRole, toRole);

            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy.ToLower())
                {
                    case "name":
                        filteredUsers = SortUsersByName(filteredUsers, orderBy);
                        break;
                    case "age":
                        filteredUsers = SortUsersByAge(filteredUsers, orderBy);
                        break;
                    case "email":
                        filteredUsers = SortUsersByEmail(filteredUsers, orderBy);
                        break;
                    case "maxrole":
                        filteredUsers = SortUsersByMaxRole(filteredUsers, orderBy);
                        break;
                    default:
                        break;
                }
            }
            return filteredUsers;
        }
    }
}
