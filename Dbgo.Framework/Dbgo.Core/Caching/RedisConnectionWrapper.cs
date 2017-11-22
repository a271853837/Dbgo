using RedLock;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Dbgo.Core.Caching
{
    public class RedisConnectionWrapper:IRedisConnectionWrapper
    {

        #region Fields
        private readonly NopConfig _config;
        private readonly Lazy<string> _connectionString;

        private volatile ConnectionMultiplexer _connection;
        private volatile RedisLockFactory _redisLockFactory;
        private readonly object _lock = new object(); 
        #endregion


        public RedisConnectionWrapper(NopConfig config)
        {
            this._config = config;
            this._connectionString = new Lazy<string>(GetConnectionString);
            this._redisLockFactory = CreateRedisLockFactory();
        }

        protected string GetConnectionString()
        {
            return _config.RedisCachingConnectionString;
        }


        protected ConnectionMultiplexer GetConnection()
        {
            if (_connection != null && _connection.IsConnected) return _connection;

            lock (_lock)
            {
                if (_connection != null && _connection.IsConnected) return _connection;

                if (_connection != null)
                {
                    //Connection disconnected. Disposing connection...
                    _connection.Dispose();
                }

                //Creating new instance of Redis Connection
                _connection = ConnectionMultiplexer.Connect(_connectionString.Value);
            }

            return _connection;
        }

        protected RedisLockFactory CreateRedisLockFactory()
        {
            //get password and value whether to use ssl from connection string
            var password = string.Empty;
            var useSsl = false;
            foreach (var option in GetConnectionString().Split(',').Where(option => option.Contains('=')))
            {
                switch (option.Substring(0, option.IndexOf('=')).Trim().ToLowerInvariant())
                {
                    case "password":
                        password = option.Substring(option.IndexOf('=') + 1).Trim();
                        break;
                    case "ssl":
                        bool.TryParse(option.Substring(option.IndexOf('=') + 1).Trim(), out useSsl);
                        break;
                }
            }

            //create RedisLockFactory for using Redlock distributed lock algorithm
            return new RedisLockFactory(GetEndPoints().Select(endPoint => new RedisLockEndPoint
            {
                EndPoint = endPoint,
                Password = password,
                Ssl = useSsl
            }));
        }

        public EndPoint[] GetEndPoints()
        {
            return GetConnection().GetEndPoints();
        }

        public IDatabase GetDatabase(int? db = default(int?))
        {
            return GetConnection().GetDatabase(db ?? -1); //_settings.DefaultDb);
        }

        public IServer GetServer(EndPoint endPoint)
        {
            return GetConnection().GetServer(endPoint);
        }

        /// <summary>
        /// 删除所有的数据库
        /// </summary>
        /// <param name="db"></param>
        public void FlushDatabase(int? db = default(int?))
        {
            var endPoints = GetEndPoints();

            foreach (var endPoint in endPoints)
            {
                GetServer(endPoint).FlushDatabase(db ?? -1); //_settings.DefaultDb);
            }
        }

        public bool PerformActionWithLock(string resource, TimeSpan expirationTime, Action action)
        {
            using (var redisLock = _redisLockFactory.Create(resource, expirationTime))
            {
                //ensure that lock is acquired
                if (!redisLock.IsAcquired)
                    return false;

                //perform action
                action();
                return true;
            }
        }

        public void Dispose()
        {
            if (_connection != null)
                _connection.Dispose();

            //dispose RedisLockFactory
            if (_redisLockFactory != null)
                _redisLockFactory.Dispose();
        }
    }
}
