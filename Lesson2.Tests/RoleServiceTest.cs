using Lesson2.Data.Models;
using Lesson2.Services;

namespace Lesson2.Tests
{
    public class RoleServiceTest
    {

        private readonly Configuration _configuration = new Configuration();
        private RoleService _roleService => _configuration.GetRequiredService<RoleService>();
        private readonly Configuration configuration = new Configuration();
        private UserService _userService => configuration.GetRequiredService<UserService>();
        private async Task<User> AddUser()
        {
            var user = new User()
            {
                Name = Guid.NewGuid().ToString(),
                SecondName = Guid.NewGuid().ToString(),
                Info = new UserInfo() { Age = 1 }
            };
            await _userService.Add(user);
            return user;
        }
        [Fact(DisplayName = "Изменение роли пользователя(успешный кейс)")]

        public async Task SuccessChangeRoles()
        {
            var user = await AddUser();
            EnumTypeRoles typeRole = EnumTypeRoles.Guest;
            await _roleService.UserAddRole(user.Id, typeRole);
            var typeRoles = new List<EnumTypeRoles>() { EnumTypeRoles.Guest, EnumTypeRoles.Admin };
            await _roleService.UserChangeRole(user.Id, typeRoles);
            var roles = await _roleService.TypeRolesUser(user.Id);
            Assert.Equal(roles, typeRoles);
        }
        //public async Task SuccessGetUsers()
        //{
        //    EnumTypeRoles typeRoles = EnumTypeRoles.Guest;
        //    var shortUserRoles =await _roleService.GetUsers(typeRoles);
        //    var itemUserRoles = shortUserRoles.Select(x => x.Roles.);


        //}
        [Fact(DisplayName = "Добавление роли пользователя(успешный кейс)")]

        public async Task SuccessUserAddRole()
        {
            var user = await AddUser();
            EnumTypeRoles typeRoles = EnumTypeRoles.Guest;
            await _roleService.UserAddRole(user.Id, typeRoles);
            var roles = await _roleService.GetRolesUser(user.Id);
            Assert.Equal(roles.First().RoleType, typeRoles);
        }
        [Fact(DisplayName = "Удаление роли пользователя(успешный кейс)")]
        public async Task SuccessDeleteUserRole()
        {
            var user = await AddUser();
            EnumTypeRoles enumRoles = EnumTypeRoles.Guest;
            await _roleService.UserAddRole(user.Id, enumRoles);

            await _roleService.UserRemoveRole(user.Id, enumRoles);
            var roles = await _roleService.GetRolesUser(user.Id);
            Assert.Empty(roles);
        }
        [Fact(DisplayName = "Вывод информации о пользователях(успешный кейс)")]

        public async Task SuccessGetAllUsers()
        {
            var users = await _roleService.GetAllUsers();
            Assert.NotEmpty(users);
        }
        [Fact(DisplayName = "Вывод информации о пользователях(успешный кейс)")]

        public async Task SuccessGetRolesUser()
        {
            int id = 1;
            var users = await _roleService.GetRolesUser(id);
            Assert.NotEmpty(users);
        }
    }
}
