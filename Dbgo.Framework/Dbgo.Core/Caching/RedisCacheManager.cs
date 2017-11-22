using Dbgo.Core.Configuration;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dbgo.Core.Caching
{
    public partial class RedisCacheManager : ICacheManager
    {
        #region Fields
        private readonly IRedisConnectionWrapper _connectionWrapper;
        private readonly IDatabase _db;
        private readonly ICacheManager _perRequestCacheManager;
        #endregion


        public RedisCacheManager(DbgoConfig config, IRedisConnectionWrapper connectionWrapper)
        {
            if (String.IsNullOrEmpty(config.RedisCachingConnectionString))
                throw new Exception("Redis connection string is empty");
            // ConnectionMultiplexer.Connect should only be called once and shared between callers
            this._connectionWrapper = connectionWrapper;
            this._db = _connectionWrapper.GetDatabase();
            //this._perRequestCacheManager = EngineContext.Current.Resolve<ICacheManager>();
        }


        #region Utilities

        protected virtual byte[] Serialize(object item)
        {
            var jsonString = JsonConvert.SerializeObject(item);
            return Encoding.UTF8.GetBytes(jsonString);
        }
        protected virtual T Deserialize<T>(byte[] serializedObject)
        {
            if (serializedObject == null)
                return default(T);

            var jsonString = Encoding.UTF8.GetString(serializedObject);
            return JsonConvert.DeserializeObject<T>(jsonString);
        }

        #endregion

        public virtual T Get<T>(string key)
        {
            if (_perRequestCacheManager.IsSet(key))
                return _perRequestCacheManager.Get<T>(key);

            var rValue = _db.StringGet(key);
            if (!rValue.HasValue)
                return default(T);
            var result = Deserialize<T>(rValue);
            _perRequestCacheManager.Set(key, result, 0);
            return result;

        }

        public virtual void Set(string key, object data, int cacheTime)
        {
            if (data == null)
                return;
            var entryBytes = Serialize(data);
            var expiresIn = TimeSpan.FromMinutes(cacheTime);
            _db.StringSet(key, entryBytes, expiresIn);
        }

        public virtual bool IsSet(string key)
        {
            if (_perRequestCacheManager.IsSet(key))
                return true;
            return _db.KeyExists(key);
        }

        public virtual void Remove(string key)
        {
            _db.KeyDelete(key);
            _perRequestCacheManager.Remove(key);
        }

        public virtual void RemoveByPattern(string pattern)
        {
            foreach (var ep in _connectionWrapper.GetEndPoints())
            {
                var server = _connectionWrapper.GetServer(ep);
                var keys = server.Keys(database: _db.Database, pattern: "*" + pattern + "*");
                foreach (var key in keys)
                    Remove(key);
            }
        }

        public virtual void Clear()
        {
            foreach (var ep in _connectionWrapper.GetEndPoints())
            {
                var server = _connectionWrapper.GetServer(ep);
                var keys = server.Keys(database: _db.Database);
                foreach (var key in keys)
                    Remove(key);
            }
        }

        public virtual void Dispose()
        {
            //if (_connectionWrapper != null)
            //    _connectionWrapper.Dispose();
        }
    }
}
