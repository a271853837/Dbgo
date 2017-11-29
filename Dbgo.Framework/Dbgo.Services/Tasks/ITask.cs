using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dbgo.Services.Tasks
{
    public partial interface ITask
    {
        /// <summary>
        /// Executes a task
        /// </summary>
        void Execute();
    }
}
