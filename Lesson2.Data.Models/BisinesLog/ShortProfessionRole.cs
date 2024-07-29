using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson2.Data.Models.BisinesLog
{
    public class ShortProfessionRole
    {
        public ShortUser User { get; set; }
        public ShortRole Role { get; set; }
        public string ProfessionName { get; set; }

    }
}
