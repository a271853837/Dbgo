using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dbgo.Core.Domain
{
    public partial class User : BaseEntity
    {
        public User()
        {
            this.UserGuid = Guid.NewGuid();
        }
        public Guid UserGuid { get; set; }
        public string Name { get; set; }

        public string Pwd { get; set; }

    }
}
