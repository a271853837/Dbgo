using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebTest.Models
{
    public class User:BaseEntity
    {
        public string UserName { get; set; }
        public string Pwd { get; set; }

        public DateTime? CreateTime { get; set; }

        public bool IsDel { get; set; }

    }
}