using Lesson2.Data.Models;
using Lesson2.Services;

namespace Lesson2.Tests
{
    public class UserServiceTest
    {

        private readonly Configuration _configuration = new Configuration();
        private UserService _userService => _configuration.GetRequiredService<UserService>();


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

        [Fact(DisplayName = "Добавление пользователя (успешный кейс)")]
        public async Task SuccessAdd()
        {
            var user = await AddUser();

            var searchedUsers = await _userService.SearchUsers(user.Name);

            Assert.Single(searchedUsers);
            Assert.Equal(user.Name, searchedUsers[0].Name);
            Assert.Equal(user.Id, searchedUsers[0].Id);


        }
        [Fact(DisplayName = "Изменение имени пользователя(успешный кейс)")]
        public async Task SuccessEditName()
        {
            var user = await AddUser();


            string name = Guid.NewGuid().ToString();
            await _userService.EditName(user.Id, name);
            var searchedUsers1 = await _userService.SearchUsers(name);


            Assert.Single(searchedUsers1);
            Assert.Equal(name, searchedUsers1[0].Name);

        }
        [Fact(DisplayName = "Изменение возраста пользователя(успешный кейс)")]
        public async Task SuccessEditAge()
        {
            var user = await AddUser();

            int age = 11;
            await _userService.EditAge(user.Id, age);
            var searchedUsers1 = await _userService.GetUserInfo(user.Id);

            Assert.Equal(age,searchedUsers1.Age);

        }
        [Fact(DisplayName = "Изменение зарплаты пользователя(успешный кейс)")]
        public async Task SuccessEditSalary()
        {
            var user = await AddUser();

            int salary = 1000;
            await _userService.EditSalary(user.Id, salary);
            var searchedUsers1 = await _userService.SearchUsers(user.Name);
            Assert.Single(searchedUsers1);

            Assert.Equal(salary, searchedUsers1[0].Salary);
        }
        [Fact(DisplayName = "Изменение даты рождения пользователя(успешный кейс)")]
        public async Task SuccessEditDateofDirth()
        {
            var user = await AddUser();


            var dateOfBirth = new DateTime(2007, 03, 23);
            await _userService.EditDateOfBirth(user.Id, dateOfBirth);
            var searchedUsers1 = await _userService.SearchUsers(user.Name);
            Assert.Single(searchedUsers1);

            Assert.Equal(dateOfBirth, searchedUsers1[0].DateOfBirth);
        }
        [Fact(DisplayName = "Удаление пользователя(успешный кейс)")]
        public async Task SuccessDeleteUser()
        {
            var user = await AddUser();


            await _userService.Delete(user.Id);
            var users = await _userService.SearchUsers(user.Id);
            Assert.NotEqual(user.Id, users.Id);
        }
        [Fact(DisplayName = "Вывод на экран пользователя(успешный кейс)")]

        public async Task SeccessGetList()
        {

            int skip = 5;
            int take = 4;
           var user= await _userService.GetList(skip,take);
            Assert.Equal(skip+1, user[0].Id);
            Assert.Equal(take, user.Count);

        }
        [Fact(DisplayName = "Вывод на экран пользователя не полная(успешный кейс)")]

        public async Task SeccessAllShortUsers()
        {
            int skip = 5;
            int take = 4;
            var user = await _userService.AllShortUsers(skip, take);
            Assert.Equal(skip + 1, user[0].Id);
            Assert.Equal(take, user.Count);

        }
        [Fact(DisplayName = "Вывод на экран пользователей чей возраст больше указанного(успешный кейс)")]
        public async Task SeccessGetUsersMoreAge()
        {
            int age = 10;
            var user = await _userService.GetUsersMoreAge(age);
            foreach(var item in user)
            {
                Assert.True(item.Info.Age>age);
            }
        }

        [Fact(DisplayName = "Добавление профессии (успешный кейс)")]
        public async Task SuccessAddProfession()
        {
            string name = "Tester";
            await _userService.AddProfession(name);

            var searchedUsers = await _userService.SearchProf(name);

            Assert.Single(searchedUsers);
            Assert.Equal(name, searchedUsers[0].Name);
          


        }
        [Fact(DisplayName = "Изменение профессии пользователя(успешный кейс)")]
        public async Task SuccessEditProfessionUser()
        {
            var user = await AddUser();

            int profId = 1;
            await _userService.EditProfessionUser(user.Id,profId);
            var users = await _userService.SearchProfession(profId);
            Assert.Equal(profId, users.Id);
        }
        [Fact(DisplayName = "Изменение профессии пользователя(успешный кейс)")]
        public async Task SuccessAllProfessionUser()
        {

            var user = await _userService.AllProfessionUser();
            Assert.NotEmpty(user);
        }
        [Fact(DisplayName = "Удаление профессии(успешный кейс)")]
        public async Task SuccessDeleteProfession()
        {
            int id = 3;
            await _userService.RemoveProfession(id);
            var user = await _userService.SearchProfession(id);
            Assert.NotEqual(id, user.Id);
        }



    }
}
