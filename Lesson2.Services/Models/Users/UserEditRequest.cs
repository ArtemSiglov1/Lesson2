using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson2.Services.Models.Users
{
    /// <summary>
    /// запрос на редактир пользов
    /// </summary>
    public class UserEditRequest
    {
        /// <summary>
        /// конструктор по умолчанию 
        /// </summary>
        public UserEditRequest() { }
        /// <summary>
        /// конструктор с параметрами
        /// </summary>
        /// <param name="id">идентиф</param>
        /// <param name="fullName">полное имя</param>
        public UserEditRequest(int id, string name)
        {
            Id = id;
            Name = name;
            
        }
        /// <summary>
        /// идентиф
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// полное имя
        /// </summary>
        public string Name { get; set; }

    }
}