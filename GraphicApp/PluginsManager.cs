using GraphicApp.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GraphicApp
{
    public class PluginsManager
    {
        private Dictionary<string, IGraphicPlugin> plugins = new Dictionary<string, IGraphicPlugin>();
        public void LoadPlugins(string directory)
        {
          
            var pluginFiles = Directory.GetFiles(directory, "*.dll");
            foreach (var filePath in pluginFiles)
            {
                Assembly currentAssembly = Assembly.LoadFrom(filePath);
                var exportedTypes = currentAssembly.GetExportedTypes();
                var currentPluginType = exportedTypes.Where(exportedType => exportedType
                                                    .IsAssignableTo(typeof(IGraphicPlugin)))
                             .SingleOrDefault();
                
                if (currentPluginType != null)
                {
                    var currentPlugin = (IGraphicPlugin)Activator.CreateInstance(currentPluginType);
                    plugins.Add(currentPlugin.Identifier.ToString(), currentPlugin);
                }

            }
            
        }
        public IReadOnlyCollection<IGraphicPlugin> GetPlugins()
        {
            return plugins.Values
                          .ToList()
                          .AsReadOnly();
        }
        public IGraphicPlugin GetPluginByIdentifier(string identifier) 
        {
            return plugins[identifier];
        }
    }
}
