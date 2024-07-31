using Lesson2.Data;
using Lesson2.Data.Models;
using Lesson2.Data.Models.BisinesLog;
using Microsoft.EntityFrameworkCore;

namespace Lesson2.Services
{
    public class ProfessionService
    {
        private DbContextOptions<DataContext> _dbContextOptions;

        public ProfessionService(DbContextOptions<DataContext> dbContextOptions)
        {
            _dbContextOptions = dbContextOptions;
        }

        public async Task<List<ShortProfessionRole>> GetUserProfessionRole(string nameProfession, EnumTypeRoles role)
        {
              await using var db = new DataContext(_dbContextOptions);
            var roleUsers = await db.Users
           .Where(x => x.Profession.Name == nameProfession && x.RoleUsers.Any(x => x.RoleId == role))
           .Select(x => new ShortProfessionRole
           {
               ProfessionName = x.Profession.Name,
               User = new ShortUser
               {
                   Id = x.Id,
                   Name = x.Name,
                   DateOfBirth = x.DateOfBirth
               },
               Role = x.RoleUsers
                        .Select(ru => new ShortRole
                        {
                            Name = ru.Role.Name,

                            Roles = ru.Role.RoleType
                        }).First()
           })
           .ToListAsync();

            return roleUsers;
        }
    }
}
