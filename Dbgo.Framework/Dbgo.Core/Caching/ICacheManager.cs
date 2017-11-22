using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dbgo.Core.Caching
{
    public interface ICacheManager : IDisposable
    {
        T Get<T>(string key);

        void Set(string key, object data, int cacheTime);

        bool IsSet(string key);

        void Remove(string key);

        void RemoveByPattern(string pattern);

        void Clear();
    }
}
