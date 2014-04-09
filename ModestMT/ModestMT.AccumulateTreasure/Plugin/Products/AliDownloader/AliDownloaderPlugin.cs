using ModestMT.AccumulateTreasure.Plugin.Interface;
using ModestMT.AccumulateTreasure.Plugin.MenuComponent;
using ModestMT.WinForm.ImagePicker.Plugin.Products.AliDownloader.Bll;
using ModestMT.WinForm.ImagePicker.Plugin.Products.AliDownloader.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace ModestMT.WinForm.ImagePicker.Plugin.Products.AliDownloader
{
	[MenuItemAttribute("产品下载", "1688下载", "_1", "_1688")]
	public class AliProductDownloaderPlugin : IFrameworkPlugin
	{
		IFramework framework;

		private AliDownloader aliDownloader = new AliDownloader();

		static string name = "AliDownloaderPlugin";

		public void OnConnection(object application)
		{
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
			ConfigManager.SaveWebSiteInfo(aliDownloader.WebSiteInfoList);
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

		public void QueryStatus(string cmdName, ref ModestMT.Plugin.CommandStatus StatusOption)
		{
		}
	}
}
