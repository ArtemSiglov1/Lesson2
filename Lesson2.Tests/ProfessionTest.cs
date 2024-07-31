using Lesson2.Data.Models;
using Lesson2.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson2.Tests
{
    public class ProfessionTest
    {
        private readonly Configuration _configuration = new Configuration();
        private ProfessionService _professionService => _configuration.GetRequiredService<ProfessionService>();
        private readonly Configuration _configurations = new Configuration();
        private UserService _userService => _configurations.GetRequiredService<UserService>();


        private async Task<User> AddUser()
        {
            var user = new User()
            {
                Name = Guid.NewGuid().ToString(),
                SecondName = Guid.NewGuid().ToString(),
                Info = new UserInfo() { Age = 1 },
                ProfissionId=5,
                Profession=new Profession() { Name="Malar"},
                RoleUsers=new List<RoleUsers> { new RoleUsers() {RoleId=EnumTypeRoles.User } }
            };
            await _userService.Add(user);
            return user;
        }
        [Fact(DisplayName = "Вывод информации о пользователях определенной роли и профессии(успешный кейс)")]

        public async Task SuccessGetRolesUser()
        {
            var user = await AddUser();
           

            var users = await _professionService.GetUserProfessionRole(user.Profession.Name,user.RoleUsers.First().RoleId);

            Assert.NotEmpty(users);

        }

    }
}
