using ModestMT.AccumulateTreasure.Plugin.Interface;
using ModestMT.AccumulateTreasure.Plugin.MenuComponent;
using ModestMT.AliDownloader.Bll;
using ModestMT.AliDownloader.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ModestMT.AliDownloader
{
	[MenuItemAttribute("店辅下载", "1688下载", "_1", "_1688")]
	public class AliStoreDownloaderPlugin : IFrameworkPlugin
	{
		IFramework framework;

		private AliStoreDownloader aliDownloader = new AliStoreDownloader();

		static string name = "AliStoreDownloaderPlugin";

		public void OnConnection(object application)
		{
			//wp-all-offer-tab
			this.framework = (IFramework)application;

			DirectoryInfo productsDirectoryInfo = new DirectoryInfo(AbstractDownLoader.PRODUCTS_FOLDER);
			if (productsDirectoryInfo.Exists == false)
			{
				productsDirectoryInfo.Create();
			}

			DirectoryInfo htmlDirectoryInfo = new DirectoryInfo(AbstractDownLoader.TEMP_HTML_FOLDER);
			if (htmlDirectoryInfo.Exists == false)
			{
				htmlDirectoryInfo.Create();
			}
		}

		public void OnDisconnection()
		{
			AbstractDownLoader.ClearDownLoader();
			ConfigManager.SaveStoreWebSiteInfo(aliDownloader.StoreWebSiteInfoList);
		}

		public string Name
		{
			get { return name; }
		}

		public void Execute(string cmdName, ref bool handled)
		{
			this.framework.UpdateContent(aliDownloader);
			handled = true;
		}

		public void QueryStatus(string cmdName, ref Plugin.CommandStatus StatusOption)
		{
		}
	}
}
