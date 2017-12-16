using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace Api
{
    class Configuration : Common.IConfiguration
    {
        public Configuration(IConfigurationRoot root)
        {
            Root = root;
        }

        IConfigurationRoot Root { get; }

        public IEnumerable<(string Name, string Value)> Section(string sectionName)
        {
            var data = new Dictionary<string, string>();
            var section = Root.GetSection(sectionName);

            ConvertToDictionary(section, data, section as IConfigurationSection);
            return data.Select(x => (x.Key, x.Value));
        }

        void ConvertToDictionary(IConfiguration config, Dictionary<string, string> data = null, IConfigurationSection top = null)
        {
            if (data == null)
                data = new Dictionary<string, string>();

            var children = config.GetChildren();
            foreach (var child in children)
            {
                if (child.Value == null)
                {
                    ConvertToDictionary(config.GetSection(child.Key), data);
                    continue;
                }

                var key = top != null ? child.Path.Substring(top.Path.Length + 1) : child.Path;
                data[key] = child.Value;
            }
        }
    }
}