using Lesson2.Data;
using Lesson2.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Lesson2.Tests
{
    public class Configuration
    {
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// 
        /// </summary>
        public Configuration() {
            //var configuration = new ConfigurationBuilder().Build();
            var services = new ServiceCollection();

            services.AddDbContext<DataContext>(x =>
                x.UseSqlServer());//configuration.GetConnectionString("DataContext"))
            //"Host=localhost;Port=5432;Database=UsersLunguage;Username=postgres;Password=111111";
            //"Server=PS-3052023\TESTMSSQL;Port=1433;Database=TestLesson;Username=test;Password=test";

            services.AddTransient<UserService>();
            services.AddTransient<RoleService>();

            _serviceProvider = services.BuildServiceProvider();


        }

        public T GetRequiredService<T>() where T : notnull
        {
            return _serviceProvider.GetRequiredService<T>();
        }
    }
}
