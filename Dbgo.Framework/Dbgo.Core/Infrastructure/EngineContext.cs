using Dbgo.Core.Configuration;
using System.Configuration;
using System.Runtime.CompilerServices;

namespace Dbgo.Core.Infrastructure
{
    public class EngineContext
    {
        private static IEngine _engine;

        [MethodImpl(MethodImplOptions.Synchronized)]

        public static IEngine Initialize()
        {
            if (_engine == null)
                _engine = new DbgoEngine();
            var config = ConfigurationManager.GetSection("DbgoConfig") as DbgoConfig;
            _engine.Initialize(config);
            return _engine;
        }


        public static IEngine Current
        {
            get
            {
                return _engine;
            }
        }
    }
}
