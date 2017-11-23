using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dbgo.Core.Infrastructure
{
    public class EngineContext
    {

        public static IEngine Initialize()
        {
            return new DbgoEngine();
        }


        public static IEngine Current
        {
            get
            {
                return new DbgoEngine();
            }
        }
    }
}
