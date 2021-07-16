using GraphicApp.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GraphicApp
{
    public class PluginsManager
    {
        /*
         * Use this to automatically load the plugins
         */
        [ImportMany(typeof(IGraphicPlugin))]
        private IEnumerable<IGraphicPlugin> pluginList;     
       
        /*
         * Keep the plugins indexed by identifier
         */
        private Dictionary<string, IGraphicPlugin> pluginsDictionary = new Dictionary<string, IGraphicPlugin>();
        public void LoadPlugins(string directory)
        {
            DirectoryCatalog catalog = new DirectoryCatalog(directory);
            CompositionContainer composition = new CompositionContainer(catalog);
            composition.ComposeParts(this);

            foreach (var plugin in pluginList)
            {
                pluginsDictionary[plugin.Identifier.ToString()] = plugin;
            }            
            
        }
        public IReadOnlyCollection<IGraphicPlugin> GetPlugins()
        {
            return pluginsDictionary.Values
                          .ToList()
                          .AsReadOnly();
        }
        public IGraphicPlugin GetPluginByIdentifier(string identifier) 
        {
            return pluginsDictionary[identifier];
        }
    }
}
