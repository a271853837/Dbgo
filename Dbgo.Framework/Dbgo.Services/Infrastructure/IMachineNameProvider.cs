using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dbgo.Services.Infrastructure
{
    public interface IMachineNameProvider
    {
        string GetMachineName();
    }
}
