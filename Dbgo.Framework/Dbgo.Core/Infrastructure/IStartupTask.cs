using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dbgo.Core.Infrastructure
{
    public interface IStartupTask
    {
        void Excute();

        int Order { get; }
    }
}
