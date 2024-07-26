using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson2.Data.Models.BisinesLog
{
    public class ShortUserRoles
    {
        public ShortUser User { get; set; }
        public List<ShortRole> Roles { get; set; }
    }
}
