using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Dbgo.Core.Configuration
{
    public partial class DbgoConfig: IConfigurationSectionHandler
    {
        public object Create(object parent, object configContext, XmlNode section)
        {
            var config = new DbgoConfig();

            var redisCachingNode = section.SelectSingleNode("RedisCaching");
            config.RedisCachingEnabled = GetBool(redisCachingNode, "Enabled");
            config.RedisCachingConnectionString = GetString(redisCachingNode, "ConnectionString");

            return config;
        }


        private bool GetBool(XmlNode node, string attrName)
        {
            return SetByXElement<bool>(node, attrName, Convert.ToBoolean);
        }

        private string GetString(XmlNode node, string attrName)
        {
            return SetByXElement<string>(node, attrName, Convert.ToString);
        }

        private T SetByXElement<T>(XmlNode node, string attrName, Func<string, T> converter)
        {
            if (node == null || node.Attributes == null) return default(T);
            var attr = node.Attributes[attrName];
            if (attr == null) return default(T);
            var attrVal = attr.Value;
            return converter(attrVal);
        }

        public bool RedisCachingEnabled { get; private set; }
        /// <summary>
        /// Redis connection string. Used when Redis caching is enabled
        /// </summary>
        public string RedisCachingConnectionString { get; private set; }
    }
}
