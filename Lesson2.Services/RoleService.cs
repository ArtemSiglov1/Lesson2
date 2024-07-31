using Lesson2.Data;
using Lesson2.Data.Models;
using Lesson2.Data.Models.BisinesLog;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Lesson2.Services
{
    public class RoleService
    {
        private DbContextOptions<DataContext> _dbContextOptions;

        public RoleService(DbContextOptions<DataContext> dbContextOptions)
        {
            _dbContextOptions = dbContextOptions;
        }

        public async Task UserChangeRole(int id, List<EnumTypeRoles> roles)
        {
            if (roles == null || roles.Count == 0)
            {
                await Console.Out.WriteLineAsync("Вы не указали роль которую надо дать человеку");
                return;
            }
            await using var db = new DataContext(_dbContextOptions);
            var user = await db.Users.AnyAsync(x => x.Id == id);
            if (user == false)
            {
                await Console.Out.WriteLineAsync("Пользователя с данным айди не существует");
                return;
            }
            var dbRoles = await db.RoleUsers
                               .Where(x => x.UserId == id)                              
                               .ToListAsync();
            if(dbRoles == null)
            {
                await Console.Out.WriteLineAsync("Пользователя с данным айди не существет");return;
            }
           

            var roleRemove = dbRoles
                .Where(x => x.UserId == id)
                .Where(x => !roles.Contains(x.RoleId))
                .ToList();

            db.RoleUsers.RemoveRange(roleRemove);

 
            var dbRolesIds = dbRoles
                .Select(x => x.RoleId)
                .ToList();
            var userRole = roles
                .Where(x => !dbRolesIds.Contains(x))
                .Select(x => new RoleUsers { UserId = id, RoleId = x })
                .ToList();
            await db.RoleUsers.AddRangeAsync(userRole);


            await db.SaveChangesAsync();
        }
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
        public async Task UserAddRole(int userId, EnumTypeRoles role)
        {
            await using var db = new DataContext(_dbContextOptions);

            var user = await db.Users.AnyAsync(x => x.Id == userId);
            if (user == false)
            {
                await Console.Out.WriteLineAsync("Пользователя с таким айди не существует");
                return;
            }
            var roles = await db.RoleUsers.AnyAsync(x => x.UserId == userId && x.RoleId == role);

            if (roles == true)
            {
                await Console.Out.WriteLineAsync("Данная роль уже существет у пользователя");
            }


            var roleEntity = await db.Roles.FirstOrDefaultAsync(r => r.RoleType == role);
            if (roleEntity == null)
            {
                await Console.Out.WriteLineAsync("Данной роли не существует"); return;
            }

            var userRole = new RoleUsers
            {
                RoleId = roleEntity.RoleType,
                UserId = userId
            };

            await db.RoleUsers.AddAsync(userRole);
            await db.SaveChangesAsync();
        }
        public async Task UserRemoveRole(int userId, EnumTypeRoles role)
        {
            await using var db = new DataContext(_dbContextOptions);
            //var user = await db.Users.Include(u => u.RoleUsers).ThenInclude(ru => ru.Role).FirstOrDefaultAsync(x => x.Id == userId);
            var user = await db.Users.AnyAsync(x => x.Id == userId);

            if (user == false)
            {
                await Console.Out.WriteLineAsync("Пользователя с таким айди не существует");
                return;
            }

            var rolesRemove = await db.RoleUsers.FirstOrDefaultAsync(x => x.RoleId == role && x.UserId == userId);

            if (rolesRemove == null)
            {
                await Console.Out.WriteLineAsync("Роль не найдена у пользователя"); return;
            }
            db.RoleUsers.Remove(rolesRemove);
            await db.SaveChangesAsync();


        }
        public async Task<List<Roles>> GetRolesUser(int userId)
        {
            await using var db = new DataContext(_dbContextOptions);

            var roleUsers = db.RoleUsers
                                    .Where(x => x.User.Id == userId)
                                    .Select(x => new Roles { RoleType = x.RoleId, Name = x.Role.Name }).ToList();
            return roleUsers;
        }
        //for me
        public async Task<List<EnumTypeRoles>> TypeRolesUser(int userId)
        {
            await using var db = new DataContext(_dbContextOptions);

            var roleUsers = db.RoleUsers
                                    .Where(x => x.User.Id == userId)
                                    .Select(x => new Roles { RoleType = x.RoleId }).ToList();
            var list = new List<EnumTypeRoles>();
            foreach (var role in roleUsers)
            {
                list.Add(role.RoleType);
            }
            return list;
        }

        //
        public async Task<List<ShortUserRoles>> GetAllUsers()
        {
            await using var db = new DataContext(_dbContextOptions);

            var roleUsers = await db.RoleUsers
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
                .ToListAsync();
            return roleUsers;
        }
    }
}
