using Lesson2.Data;
using Lesson2.Data.Models;
using Lesson2.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using System.Data;

var configuration = new ConfigurationBuilder().Build();

var services = new ServiceCollection();

services.AddDbContext<DataContext>(x =>
    x.UseSqlServer(configuration.GetConnectionString("DataContext")));
//"Host=localhost;Port=5432;Database=UsersLunguage;Username=postgres;Password=111111";
//"Server=PS-3052023\TESTMSSQL;Port=1433;Database=TestLesson;Username=test;Password=test";

services.AddTransient<UserService>();

services.AddTransient<RoleService>();
var sp = services.BuildServiceProvider();


var userService = sp.GetRequiredService<UserService>();

var roleService = sp.GetRequiredService<RoleService>();

//await userService.Add(new User() { Name = "Artem", Age = 17 }); 
//await userService.Add(new User() { Name = "Roma", Age = 17 }); 
//await userService.Add(new User() { Name = "Petr", Age = 17 }); 
//await roleService.UserAddRole(1, EnumTypeRoles.User);
//await Output(EnumTypeRoles.User);

try
{
    //await roleService.UserChangeRole(1,new List<EnumTypeRoles> { EnumTypeRoles.Admin,EnumTypeRoles.Guest});
    await Output(EnumTypeRoles.Admin);
    await Output(EnumTypeRoles.User);

  //  await roleService.UserRemoveRole(1, EnumTypeRoles.Admin);
    // await Output(EnumTypeRoles.Admin);
    await OutputRoles(1);

    Console.ReadLine();

    ////await Output(0, 100);
    ////await userService.EditName(1, "Artem");
    ////Console.WriteLine("\n\n");
    async Task Output(EnumTypeRoles role)
    {
        try
        {
            foreach (var item in await roleService.GetUsers(role))
            {
                Console.Write($"{item.User.Id} {item.User.Name} {item.User.DateOfBirth}");
                foreach (var i in item.Roles)
                {
                    Console.WriteLine($"{i.Name} {i.Roles}");
                }
            }
        }
        catch (Exception ex) { Console.WriteLine(ex.Message); }
    }
    async Task OutputRoles(int id)
    {
        foreach (var item in await roleService.GetRolesUser(id))
        {
            Console.Write($"{item.RoleType} {item.Name} ");
        }
    }
}
catch (Exception ex) { Console.WriteLine(ex.Message); }
////await Output(0, 100);
////await userService.EditAge(1, 112);
////Console.WriteLine("\n\n");
////await Output(0,100);
////Console.WriteLine("\n\n");
////await OutputAge(20);
////Console.WriteLine("\n\n");
////await OutputSearch("A");
////Console.ReadLine();
////async Task Output(int skip,int take)
////{
////    if (userService == null)
////    {
////        return;
////    }
////    foreach (var elem in await userService.GetList(skip,take))
////    {
////        Console.WriteLine($"ID-{elem.Id},Name-{elem.Name},Age-{elem.Age}\n");
////    }
////}
////async Task OutputAge(int age)
////{
////    if (userService == null)
////    {
////        return;
////    }
////    foreach (var elem in await userService.GetUsersMoreAge(age))
////    {
////        Console.WriteLine($"ID-{elem.Id},Name-{elem.Name},Age-{elem.Age}\n");
////    }
////}
////async Task OutputSearch(string name)
////{
////    if (userService == null)
////    {
////        return;
////    }
////    foreach (var elem in await userService.SearchUsers(name))
////    {
////        Console.WriteLine($"ID-{elem.Id},Name-{elem.Name},Age-{elem.Age}\n");
////    }
