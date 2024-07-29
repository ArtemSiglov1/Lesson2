﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson2.Data.Models
{
    public class UserInfo
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int Age { get; set; }
        public DateTime DateCreate { get; set; }
    }
}
