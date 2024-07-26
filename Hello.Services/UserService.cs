using Hello.Data;
using Hello.Data.Models;
using Microsoft.EntityFrameworkCore;
using TestUsers.Services.Models.Users;

namespace Hello.Services
{
    public class UserService
    {
        private DbContextOptions<DataContext> _dbContextOptions;

        public UserService(DbContextOptions<DataContext> dbContextOptions)
        {
            _dbContextOptions = dbContextOptions;
        }

        public async Task Add(User user)
        {
            await using var db = new DataContext(_dbContextOptions);
            var query = db.Users.AsQueryable();
            if (string.IsNullOrWhiteSpace(user.Name))
                await Console.Out.WriteLineAsync("Вы не указали имя");
            if (user.Age < 0 && user.Age > 100)
                await Console.Out.WriteLineAsync("Вы указали неверный возраст");
            await db.Users.AddAsync(user);
            await db.SaveChangesAsync();
        }
        public async Task EditName(UserEditRequest request)
        {
            await using var db = new DataContext(_dbContextOptions);
            var query = db.Users.AsQueryable();
            var user = await db.Users.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (string.IsNullOrWhiteSpace(request.Name))
                await Console.Out.WriteLineAsync("Вы не указали имя");
            user.Name = request.Name;

            await db.SaveChangesAsync();
        }
        public async Task EditAge(int id, int age)
        {
            await using var db = new DataContext(_dbContextOptions);
            var query = db.Users.AsQueryable();
            var user = await db.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user.Age < 0 && user.Age > 100)
                await Console.Out.WriteLineAsync("Вы не указали имя");
            user.Age = age;

            await db.SaveChangesAsync();
        }
        public async Task Delete(int userId)
        {
            using (DataContext db = new DataContext())
            {
                // обновляем только объекты, у которых имя Bob
                await db.Users.Where(u => u.Id == userId).ExecuteDeleteAsync();
            }
        }
        public async Task GetList(List<User> users)
        {
            await using var db = new DataContext(_dbContextOptions);

            var query = db.Users.AsQueryable();

            foreach (var user in users)
            {
                await db.Users.AddAsync(user);
            }
            db.SaveChanges();


        }
        public async Task<List<User>> GetUsersMoreAge(int age)
        {
            await using var db = new DataContext(_dbContextOptions);
            var query = db.Users.AsQueryable();
            var userAge=query.Where(u => u.Age>age).ToList();
            return userAge;
        }
        public async Task<List<User>> SearchUsers(string name)
        {
            await using var db = new DataContext(_dbContextOptions);
            var query = db.Users.AsQueryable();
            var userName = query.Where(x => x.Name.Contains(name)).ToList();
            return userName;
        }
    }
}