using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson2.Data.Models
{
    public enum EnumTypeRoles
    {
        User=1,//пользователь
        Guest=2,//гость
        Admin=3,//администратор
    }
    public class Roles
    {
        public EnumTypeRoles RoleType { get; set; }
        public string Name { get; set; }
        public List<RoleUsers> Users { get; set; }

    }
}
