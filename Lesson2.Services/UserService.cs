
using Azure.Core;
using Lesson2.Data;
using Lesson2.Data.Models;
using Lesson2.Data.Models.BisinesLog;
using Lesson2.Services.Models.Users;
using Microsoft.EntityFrameworkCore;
using System.Xml.Serialization;

namespace Lesson2.Services
{
    public class UserService
    {
        private DbContextOptions<DataContext> _dbContextOptions;
        public UserService(DbContextOptions<DataContext> dbContextOptions)
        {
            _dbContextOptions = dbContextOptions;
        }
       //for me
        public async Task<User> SearchUsers(int id)
        {
            await using var db = new DataContext(_dbContextOptions);
            var user = await db.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
            {
                return new User() {Id=0 };
            }
            return user;
        }
        public async Task<Profession> SearchProfession(int id)
        {
            await using var db = new DataContext(_dbContextOptions);
            var user = await db.Professions.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
            {
                return new Profession() { Id = 0 };
            }
            return user;
        }
        //
        public async Task Add(User user)
        {
            await using var db = new DataContext(_dbContextOptions);
            if (string.IsNullOrWhiteSpace(user.Name))
            {
                await Console.Out.WriteLineAsync("Вы не указали имя"); return;
            }
            if (user.Info.Age < 0 && user.Info.Age > 100) {
                await Console.Out.WriteLineAsync("Вы указали неверный возраст"); return;
            }
            await db.Users.AddAsync(user);
            await db.SaveChangesAsync();
        }
        public async Task EditName(int userId,string name)
        {
            await using var db = new DataContext(_dbContextOptions);
            var user = await db.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (user == null)
            {
                await Console.Out.WriteLineAsync("Пользователя с данным айди не существует"); return;
            }
            if (string.IsNullOrWhiteSpace(name))
            {
                await Console.Out.WriteLineAsync("Вы не указали имя"); return;
            }
            user.Name = name;

            await db.SaveChangesAsync();
        }
        public async Task EditAge(int id, int age)
        {
            await using var db = new DataContext(_dbContextOptions);
            var user = await db.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
            {
                await Console.Out.WriteLineAsync("Пользователя с данным айди не существует"); return;
            }

            if (age < 0 || age > 100)
            {
                await Console.Out.WriteLineAsync("Вы указали не корректнный возраст"); return;
            }
            user.Info.Age = age;

            await db.SaveChangesAsync();
        }
        public async Task EditSalary(int id,int value)
        {
            await using var db=new DataContext(_dbContextOptions);
            var user = await db.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
            {
                await Console.Out.WriteLineAsync("Пользователя с данным айди не существует"); return;
            }
            user.Salary = value;
            await db.SaveChangesAsync();
        }
        public async Task EditDateOfBirth(int id,DateTime? date)
        {
            await using var db = new DataContext(_dbContextOptions);
            var user = await db.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
            {
                await Console.Out.WriteLineAsync("Пользователя с данным айди не существует"); return;
            }
            if (date > DateTime.Now || date.Value.Year < DateTime.Now.Year - 100)
            {
                await Console.Out.WriteLineAsync("Дата рождения указана не верно");return;
            }
            user.DateOfBirth = date;
            await db.SaveChangesAsync();
        }
        public async Task Delete(int userId)
        {
            await using var  db = new DataContext();
            await db.Users.Where(u => u.Id == userId).ExecuteDeleteAsync();
        }
        public async Task<List<User>> GetList(int skip,int take)
        {
            await using var db = new DataContext(_dbContextOptions);
            var query = db.Users.AsQueryable();
            var allUsers=await query.Skip(skip).Take(take).ToListAsync();
            return allUsers;
        }
        public async Task<List<User>> GetUsersMoreAge(int age)
        {
            await using var db = new DataContext(_dbContextOptions);
            var query = db.Users.AsQueryable();
            var userAge=await query.Where(u => u.Info.Age > age).ToListAsync();
            return userAge;
        }
        public async Task<List<User>> SearchUsers(string name)
        {
            await using var db = new DataContext(_dbContextOptions);
            var query = db.Users.AsQueryable();
           name=name.ToLower();
            if (string.IsNullOrWhiteSpace(name))
            {
                await Console.Out.WriteLineAsync("Вы не указали имя");return new List<User>();
            }
            var userName =await query.Where(x => x.Name.ToLower().Contains(name)).ToListAsync();
            return userName;
        }
        public async Task<List<ShortUser>> AllShortUsers(int skip,int take)
        {
            await using var db = new DataContext(_dbContextOptions);
            var query = db.Users.AsQueryable();
            var allUsers = await query.Skip(skip).Take(take).Select(x=>new ShortUser()
            {
                Id = x.Id,
                Name = x.Name,
                DateOfBirth = x.DateOfBirth
            }).ToListAsync();

            return allUsers;
        }
        public async Task AddProfession(string name)
        {
            await using var db = new DataContext(_dbContextOptions);
            if(string.IsNullOrWhiteSpace(name))
            {//todo поставить проверку на совпадение профессии
                await Console.Out.WriteLineAsync("Вы не указали профессию");return;
            }
            var profession = new Profession()
            {
                Name = name
            };
            await db.Professions.AddAsync(profession);
           await db.SaveChangesAsync();

        }
        //for me
        public async Task<List<Profession>> SearchProf(string name)
        {
            await using var db = new DataContext(_dbContextOptions);
            var query = db.Professions.AsQueryable();
            name = name.ToLower();
            if (string.IsNullOrWhiteSpace(name))
            {
                await Console.Out.WriteLineAsync("Вы не указали имя"); return new List<Profession>();
            }
            var userName = await query.Where(x => x.Name.ToLower().Contains(name)).ToListAsync();
            return userName;
        }
        public async Task EditProfessionUser(int id,int professionId)
        {
            await using var db = new DataContext(_dbContextOptions);
            var user = await db.Users.FirstOrDefaultAsync(x => x.Id == id);
            var profession=await db.Professions.FirstOrDefaultAsync(x=>x.Id == professionId);
            if (user == null)
            {
                await Console.Out.WriteLineAsync("Пользователя с данным айди не существует"); return;
            }
            if (profession==null)
            {
                await Console.Out.WriteLineAsync("Профессии с данным айди не существует"); return;
            }
            user.Profession=profession;
            db.SaveChanges();
        }
        public async Task<List<ModelProfessionUser>> AllProfessionUser()
        {
            await using var db = new DataContext(_dbContextOptions);
            var professions = await db.Users.Select(x => new ModelProfessionUser()
            {
                UserName = x.Name,
                ProfessionName=x.Profession.Name,
            })
           .ToListAsync();
            return professions;
        }
        public async Task RemoveProfession(int professionId)
        {
            await using var db = new DataContext();

            await db.Professions.Where(u => u.Id == professionId).ExecuteDeleteAsync();

        }
        public async Task<List<ModelProfessionStat>> AllProfessionStat()
        {
            await using var db = new DataContext(_dbContextOptions);
            var professions= await db.Professions.Select(x => new ModelProfessionStat() { 
            Count=x.Users.Count,
            Name=x.Name,
            })
           .ToListAsync();
            return professions;
        }
    }
}