using Lesson2.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Lesson2.Data
{
    public class DataContext : DbContext
    {
        public DataContext()
        {
            //Database.EnsureCreated();
        }
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
       public DbSet<Roles> Roles { get; set; }
       public DbSet<Profession> Professions { get; set; }
        public DbSet<RoleUsers> RoleUsers { get; set; }
        public DbSet<UserInfo> UserInfos { get; set; }
        public DbSet<Accounts> Accounts { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=PS-3052023\\TESTMSSQL;Database=TestLesson;User Id=test;Password=test;TrustServerCertificate=True");
            //optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=Users;Username=postgres;Password=111111");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<User>().HasKey(x => x.Id);
            //modelBuilder.Entity<UserInfo>().Property(u => u.DateCreate).
            //   HasComputedColumnSql("GETDATE()");
            modelBuilder.ApplyConfiguration(new UserConfiguration());

            modelBuilder.Entity<User>().Property(u => u.FullName).HasComputedColumnSql("[SecondName]+' '+[Name]");

            modelBuilder.Entity<Roles>().ToTable("Roles");
            modelBuilder.Entity<Roles>().HasKey(p => p.RoleType);
            modelBuilder.Entity<Roles>().Property(p => p.RoleType)
                .ValueGeneratedNever();
            modelBuilder.Entity<Roles>().HasData(
                new Roles[]
                {
                    new Roles{RoleType=EnumTypeRoles.User,Name="Пользователь"},
                    new Roles{RoleType=EnumTypeRoles.Guest,Name="Гость"},
                    new Roles{RoleType=EnumTypeRoles.Admin,Name="Администратор"}
                }
            );

            modelBuilder.Entity<User>().HasOne(user => user.Profession)
                .WithMany(profession => profession.Users)
                .HasForeignKey(user => user.ProfissionId);
            modelBuilder.Entity<User>().HasOne(user => user.Profession)
                .WithMany(profession => profession.Users)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<RoleUsers>()
            .HasKey(ru => new { ru.RoleId, ru.UserId });
            modelBuilder.Entity<RoleUsers>()
            .HasKey(ru => new { ru.UserId, ru.RoleId });
            modelBuilder.Entity<RoleUsers>()
                .HasOne(ru => ru.User)
                .WithMany(u => u.RoleUsers)
                .HasForeignKey(ru => ru.UserId);
            
            modelBuilder
                .Entity<UserInfo>()
                .HasOne(u => u.User)
                .WithOne(p => p.Info)
                .HasForeignKey<UserInfo>(p => p.UserId);
            modelBuilder.Entity<Accounts>().HasKey(u => new { u.UserId });
            modelBuilder
                .Entity<User>()
                .HasOne(u => u.Accounts)
                .WithOne(p => p.User)
                .HasForeignKey<Accounts>(p => p.UserId)
                .HasPrincipalKey<User>(_ => _.Id);
            base.OnModelCreating(modelBuilder);
        }
    }
}
