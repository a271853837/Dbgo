using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Dbgo.Core.Caching
{
    public interface IRedisConnectionWrapper : IDisposable
    {
        IDatabase GetDatabase(int? db = null);

        IServer GetServer(EndPoint endPoint);

        EndPoint[] GetEndPoints();

        void FlushDatabase(int? db = null);

        bool PerformActionWithLock(string resource, TimeSpan expirationTime, Action action);
    }
}
