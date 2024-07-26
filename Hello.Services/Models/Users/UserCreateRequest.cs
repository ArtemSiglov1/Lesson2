using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestUsers.Services.Models.Users
{
    /// <summary>
    /// создание пользователем запроса
    /// </summary>
    public class UserCreateRequest
    {
        /// <summary>
        /// конструктор по умолчанию 
        /// </summary>
        public UserCreateRequest() { }
        /// <summary>
        /// конструктор с параметрами
        /// </summary>
        /// <param name="name">полное имя</param>
        public UserCreateRequest(int id,string name,int age)
        {
            Id = id;
            Name = name;
            Age=age;
        }
        public int Id { get; set; }
        /// <summary>
        /// имейл
        /// </summary>
        public int Age { get; set; } //имейл
        /// <summary>
        /// полное имя
        /// </summary>
        public string Name { get; set; } // полное имя
    }
}