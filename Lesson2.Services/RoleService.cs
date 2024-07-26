using Azure.Core;
using Lesson2.Data;
using Lesson2.Data.Models;
using Lesson2.Data.Models.BisinesLog;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Xml.Linq;

namespace Lesson2.Services
{
    public class RoleService
    {
        private DbContextOptions<DataContext> _dbContextOptions;

        public RoleService(DbContextOptions<DataContext> dbContextOptions)
        {
            _dbContextOptions = dbContextOptions;
        }

        //public async Task UserChangeRole(int id, List<EnumTypeRoles> roles)
        //{
        //    await using var db = new DataContext(_dbContextOptions);
        //    var user = await db.Users
        //                        .Where(x => x.Id == id)
        //                        .Select(x => x.RoleUsers)
        //                        .FirstOrDefaultAsync();


        //    //user = await db.Users.Include(u => roleUsers).FirstOrDefaultAsync(x => x.Id == id);

        //    if (user == null)
        //    {
        //        await Console.Out.WriteLineAsync("Пользователя с данным айди не существует");
        //        return;
        //    }

        //    if (roles == null || roles.Count == 0)
        //    {
        //        await Console.Out.WriteLineAsync("Вы не указали роль которую надо дать человеку");
        //        return;
        //    }
        //    //todo: убрать инклуд, переделать на добавление новых и удаление ненужных

        //    // db.RoleUsers.RemoveRange(user.RoleUsers);
        //    var contactRequest = user.Select(c => c.RoleId).ToList();
        //    var contactToRemove = db.Users.Where(x => !contactRequest.Containsroles.Any(x=>x))).ToList();
        //    db.UsersContact.RemoveRange(contactToRemove);
        //    foreach (var role in roles)
        //    {
        //        var userRole = new RoleUsers { UserId = id, RoleId = role };
        //        db.RoleUsers.Add(userRole);

        //    }

        //    await db.SaveChangesAsync();
        //}
        public async Task<List<ShortUserRoles>> GetUsers(EnumTypeRoles roleType)
        {
            await using var db = new DataContext(_dbContextOptions);
           
            var roleUsers = await db.RoleUsers
                .Where(x => x.Role.RoleType == roleType)
                .Select(x => new ShortUserRoles
                {
                    User = new ShortUser
                    {
                        Id = x.User.Id,
                        Name = x.User.Name,
                        DateOfBirth = x.User.DateOfBirth
                    },
                    Roles = new List<ShortRole>
                    {
                new ShortRole
                {
                    Name = x.Role.Name,
                    Roles = x.Role.RoleType
                }
                    }
                })
                .ToListAsync();
            return roleUsers;
        }
        //public async Task UserAddRole(int userId, EnumTypeRoles role)
        //{
        //    await using var db = new DataContext(_dbContextOptions);
        //    var user = await db.Users.Include(u => u.RoleUsers).FirstOrDefaultAsync(x => x.Id == userId);

        //    if (user == null)
        //    {
        //        await Console.Out.WriteLineAsync("Пользователя с таким айди не существует");
        //        return;
        //    }

        //    var roleEntity = await db.Roles.FirstOrDefaultAsync(r => r.RoleType == role);
        //    if (roleEntity == null)
        //    {
        //        await Console.Out.WriteLineAsync("Роли с таким айди не сущетсвует"); return;
        //    }
        //    var userRole = new RoleUsers
        //    {
        //        RoleId = roleEntity.RoleType,
        //        UserId = user.Id
        //    };

        //    user.RoleUsers.Add(userRole);
        //    //await db.SaveChangesAsync();
        //}
        //public async Task UserRemoveRole(int userId, EnumTypeRoles role)
        //{
        //    await using var db = new DataContext(_dbContextOptions);
        //    var user = await db.Users.Include(u => u.RoleUsers).ThenInclude(ru => ru.Role).FirstOrDefaultAsync(x => x.Id == userId);

        //    if (user == null)
        //    {
        //        await Console.Out.WriteLineAsync("Пользователя с таким айди не существует");
        //        return;
        //    }

        //    var userRole = user.RoleUsers.FirstOrDefault(x => x.Role.RoleType == role);

        //    if (userRole == null)
        //    {
        //        await Console.Out.WriteLineAsync("Роль не найдена у пользователя"); return;
        //    }
        //    user.RoleUsers.Remove(userRole);
        //    await db.SaveChangesAsync();


        //}
        public async Task<List<Roles>> GetRolesUser(int userId)
        {
            await using var db = new DataContext(_dbContextOptions);

            var roleUsers =  db.RoleUsers
                                    .Where(x => x.User.Id == userId)
                                    .Select(x => new Roles { RoleType = x.RoleId, Name = x.Role.Name }).ToList();
            return roleUsers;
        }
        public async Task<List<ShortUserRoles>> GetAllUsers()
        {
            await using var db = new DataContext(_dbContextOptions);

            var roleUsers = db.RoleUsers
                .Where(x => x.User.RoleUsers.Count >= 1)
                .Select(x => new ShortUserRoles
                {
                    User = new ShortUser
                    {
                        Id = x.User.Id,
                        Name = x.User.Name,
                        DateOfBirth = x.User.DateOfBirth
                    },
                    Roles = new List<ShortRole>
                    {
                new ShortRole
                        {
                        Name = x.Role.Name,
                        Roles = x.Role.RoleType
                        }
                    }
                })
                .ToList();
            return roleUsers;
        }
    }
}
