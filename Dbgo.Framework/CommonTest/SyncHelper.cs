using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CommonTest
{
    public class SyncHelper
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void Excute()
        {
            Console.WriteLine("当前时间："+DateTime.Now);
            Thread.Sleep(3000);
        }


        public void LockMyself()
        {
            lock (this)
            {
                Console.WriteLine("lock at:"+DateTime.Now);
                Thread.Sleep(3000);
                Console.WriteLine("unlock at"+DateTime.Now);
            }
        }

        public static void LockType()
        {
            lock (typeof(SyncHelper))
            {
                Console.WriteLine("lock SyncHelper at "+DateTime.Now);
                Thread.Sleep(3000);
                Console.WriteLine("unlock SyncHelper at"+DateTime.Now);
            }
        }
    }
}
