using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CommonTest
{
    class Program
    {
        static void Main(string[] args)
        {
            SyncHelper sync = new SyncHelper();
            

            Thread thread = new Thread(delegate()
            {
                SyncHelper.LockType();
            });
            thread.Start();

            #region 1
            Timer time = new Timer(delegate
                {
                    SyncHelper.Excute();
                }, null, 0, 1000);
            #endregion

            Console.Read();
        }
    }
}
