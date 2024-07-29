using Lesson2.Data;
using Lesson2.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson2.Services
{
    public class ServiceAccount
    {

        private DbContextOptions<DataContext> _dbContextOptions;

        public ServiceAccount(DbContextOptions<DataContext> dbContextOptions)
        {
            _dbContextOptions = dbContextOptions;
        }
            public async Task UpdateAccount(int id, string login, string password)
        {
            await using var db = new DataContext(_dbContextOptions);

            if (string.IsNullOrWhiteSpace(login))
            {
                await Console.Out.WriteLineAsync("Логин не может быть пустым"); return;
            }
            if (string.IsNullOrWhiteSpace(password))
            {
                await Console.Out.WriteLineAsync("Пароль не может быть пустым"); return;
            }
            var account = await db.Accounts.FirstOrDefaultAsync(x=>x.UserId==id);
            if (account == null)
            {
                await Console.Out.WriteLineAsync("Аккаунта с таким айди не существет");return;
            }
             account.User.Accounts.Login = login;
            account.User.Accounts.Password = password;
            await db.SaveChangesAsync();
        }
        public async Task RemoveAccounts(int id)
        {
            await using var db = new DataContext(_dbContextOptions);
            var accounts = await db.Accounts.Select(x => new Accounts
            {
                UserId = id,
                User = x.User,
                Login = x.Login,
                Password = x.Password
            }).FirstOrDefaultAsync();
            if (accounts == null) {
                await Console.Out.WriteLineAsync("Аккаунта с таким айди не сущестует");return;
            }
            db.Accounts.Remove(accounts);
            db.SaveChanges();
        }
            public async Task<Accounts> GetAccounts(int id)
        {
            await using var db = new DataContext(_dbContextOptions);
            var accounts = await db.Accounts.Select(x => new Accounts { UserId = id,
                User=x.User,
                Login=x.Login,
                Password=x.Password
            }).FirstOrDefaultAsync();
            
            if (accounts == null)
            {
                await Console.Out.WriteLineAsync("Аккаунт с таким айди не найден");return new Accounts();
            }
            return accounts;
        }
    }
}
