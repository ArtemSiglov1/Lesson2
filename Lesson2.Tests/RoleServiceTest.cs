using Lesson2.Data.Models;
using Lesson2.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson2.Tests
{
    public class RoleServiceTest
    {

        private readonly Configuration _configuration = new Configuration();
        private RoleService _roleService => _configuration.GetRequiredService<RoleService>();
        private readonly Configuration configuration = new Configuration();
        private UserService _userService => configuration.GetRequiredService<UserService>();
        private async Task<string> AddUser()
        {
            var user = new User()
            {
                Name = Guid.NewGuid().ToString(),
                SecondName = Guid.NewGuid().ToString(),
            };
            await _userService.Add(user);
            return user.Name;
        }

        [Fact(DisplayName = "Изменение роли пользователя(успешный кейс)")]

        public async Task SuccessChangeRoles()
        {
            var searchedUsers = await _userService.SearchUsers(await AddUser());

            var typeRoles =new List<EnumTypeRoles>() { EnumTypeRoles.Guest, EnumTypeRoles.Admin };
            await _roleService.UserChangeRole(searchedUsers[0].Id,typeRoles);
            var roles=await _roleService.TypeRolesUser(searchedUsers[0].Id);
            Assert.Equal(roles, typeRoles);
        }
        [Fact(DisplayName = "Добавление роли пользователя(успешный кейс)")]

        public async Task SuccessUserAddRole()
        {
            var searchedUsers = await _userService.SearchUsers(await AddUser());
            EnumTypeRoles typeRoles = EnumTypeRoles.Guest;
            await _roleService.UserAddRole(searchedUsers[0].Id, typeRoles);
            var roles = await _roleService.TypeRolesUser(searchedUsers[0].Id);
            Assert.Equal(roles.First(), typeRoles);
        }
        [Fact(DisplayName = "Удаление роли пользователя(успешный кейс)")]
        public async Task SuccessDeleteUserRole()
        {
            var searchedUsers = await _userService.SearchUsers(await AddUser());
            EnumTypeRoles enumRoles = EnumTypeRoles.Guest;
            await _roleService.UserRemoveRole(61, enumRoles);
            var roles = await _roleService.TypeRolesUser(61);
            var i = searchedUsers[0].RoleUsers.Any(x => x.RoleId != enumRoles);
            Assert.True(i);
        }
    }
}
