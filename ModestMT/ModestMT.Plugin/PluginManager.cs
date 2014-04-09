using ModestMT.Core.Logger;
using ModestMT.Core.System;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace ModestMT.Plugin
{
	public class PluginManager
	{
		private static readonly Logger logger = LogManager.GetLogger(typeof(PluginManager));

		public List<IPlugin> Plugins { get; set; }

		private Assembly mainAssembly;

		private Type pluginType;

		private object application;

		private PluginManager(Assembly assembly, Type type, object application)
		{
			this.mainAssembly = assembly;
			this.pluginType = type;
			this.application = application;

			LoadPlugin();
		}

		private void LoadPlugin()
		{
			Plugins = new List<IPlugin>();
			string[] assemblys = Directory.GetFiles(DirUtils.WorkingDirectory, "*.dll", SearchOption.AllDirectories);


			// Load Plugin in self
			CheckPlugin(mainAssembly);

			foreach (string assemblyPath in assemblys)
			{
				Assembly assembly = Assembly.LoadFile(assemblyPath);
				CheckPlugin(assembly);
			}
		}

		private void CheckPlugin(Assembly assembly)
		{
			Type[] types = assembly.GetTypes();
			foreach (Type type in types)
			{
				if (this.pluginType.IsAssignableFrom(type) && type.IsClass)
				{
					try
					{
						IPlugin plugin = Activator.CreateInstance(type) as IPlugin;
						plugin.OnConnection(application);
						Plugins.Add(plugin);
					}
					catch
					{
						logger.Error(string.Format("加载插件 {0} 出错", type.Name));
					}
				}
			}
		}

		public static PluginManager Create(Assembly assembly, Type pluginType, object application)
		{
			return new PluginManager(assembly, pluginType, application);
		}
	}
}
