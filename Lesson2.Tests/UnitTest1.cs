using Lesson2.Data.Models;
using Lesson2.Services;

namespace Lesson2.Tests
{
    public class UserServiceTest
    {

        private readonly Configuration _configuration = new Configuration();
        private UserService _userService => _configuration.GetRequiredService<UserService>();


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

        [Fact(DisplayName = "Добавление пользователя (успешный кейс)")]
        public async Task SuccessAdd()
        {
            var user = new User()
            {

                Age = 1,
                Name = Guid.NewGuid().ToString(),
                SecondName = Guid.NewGuid().ToString(),

            };
            await _userService.Add(user);

            var searchedUsers = await _userService.SearchUsers(user.Name);

            Assert.Single(searchedUsers);
            Assert.Equal(user.Name, searchedUsers[0].Name);
            Assert.Equal(user.Age, searchedUsers[0].Age);
            Assert.Equal(user.Id, searchedUsers[0].Id);


        }
        [Fact(DisplayName = "Изменение имени пользователя(успешный кейс)")]
        public async Task SuccessEditName()
        {
            string name = Guid.NewGuid().ToString();
            await _userService.EditName(3, name);
            var searchedUsers = await _userService.SearchUsers(name);
            Assert.Single(searchedUsers);
            Assert.Equal(name, searchedUsers[0].Name);

        }
        [Fact(DisplayName = "Изменение возраста пользователя(успешный кейс)")]
        public async Task SuccessEditAge()
        {
            int age = 1;
            await _userService.EditAge(1, age);
            var searchedUsers = await _userService.SearchUsers("Artem");
            Assert.Single(searchedUsers);

            Assert.Equal(age, searchedUsers[0].Age);

        }
        [Fact(DisplayName = "Изменение зарплаты пользователя(успешный кейс)")]
        public async Task SuccessEditSalary()
        {
            int salary = 1000;
            await _userService.EditSalary(1, salary);
            var searchedUsers = await _userService.SearchUsers("Artem");
            Assert.Single(searchedUsers);

            Assert.Equal(salary, searchedUsers[0].Salary);
        }
        [Fact(DisplayName = "Изменение даты рождения пользователя(успешный кейс)")]
        public async Task SuccessEditDateofDirth()
        {
            var dateOfBirth = new DateTime(2007, 03, 23);
            await _userService.EditDateOfBirth(1, dateOfBirth);
            var searchedUsers = await _userService.SearchUsers("Artem");
            Assert.Single(searchedUsers);

            Assert.Equal(dateOfBirth, searchedUsers[0].DateOfBirth);
        }
        [Fact(DisplayName = "Удаление пользователя(успешный кейс)")]
        public async Task SuccessDeleteUser()
        {
            int id = 8;
            await _userService.Delete(id);
            var user = await _userService.SearchUsers(id);
            Assert.NotEqual(id, user.Id);
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
                Assert.True(item.Age>age);
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
            int id = 3;
            int profId = 1;
            await _userService.EditProfessionUser(id,profId);
            var user = await _userService.SearchProfession(profId);
            Assert.Equal(profId, user.Id);
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
