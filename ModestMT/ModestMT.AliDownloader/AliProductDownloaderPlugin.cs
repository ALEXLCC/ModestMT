using ModestMT.AccumulateTreasure.Plugin.Interface;
using ModestMT.AccumulateTreasure.Plugin.MenuComponent;
using ModestMT.AliDownloader.Bll;
using ModestMT.AliDownloader.Utility;
using ModestMT.Core.Exception;
using ModestMT.Core.Logger;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace ModestMT.AliDownloader
{
	[MenuItemAttribute("产品下载", "阿里巴巴", "_1", "_1688")]
	public class AliProductDownloaderPlugin : IFrameworkPlugin
	{
		private static readonly Logger logger = LogManager.GetLogger(typeof(AbstractDownLoader));

		private IFramework framework;

		private AliProductDownloader aliDownloader;

		private Setting settingForm;

		private static string name = "产品下载";

		public AliProductDownloaderPlugin()
		{
			logger.Info("初始化AliProductDownloader界面...");
			aliDownloader = new AliProductDownloader();
			settingForm = new Setting();
			logger.Info("初始化AliProductDownloader成功.");
		}

		public void OnConnection(object application)
		{
			this.framework = (IFramework)application;

			logger.Info("解压核心程序...");
			try
			{
				bool flag = ProcessHelper.KillProcess(Arguments.PHANTOMJS_PROCESS_NAME);
				if (flag == true)
				{
					ExtractHelper.Extract(Arguments.TEMP_FOLDER, Arguments.PHANTOMJS_NAME, Arguments.SCRIPT_NAME);
				}
			}
			catch (Exception ex)
			{
				logger.Error("Extract core failed.", ex);
				throw new BasicException("Extract core failed.");
			}

			logger.Info("解压核心程序结束.");

			if (File.Exists(Path.Combine(Arguments.TEMP_FOLDER, Arguments.PHANTOMJS_NAME)) == false)
			{
				throw new BasicException("Webkit未找到，请重新安装软件或联系客服。");
			}

			VerifyDirectory();
		}

		public static void VerifyDirectory()
		{
			logger.Info("创建/检测产品文件夹...");
			DirectoryInfo productsDirectoryInfo = new DirectoryInfo(Arguments.PRODUCTS_FOLDER);
			if (productsDirectoryInfo.Exists == false)
			{
				productsDirectoryInfo.Create();
			}
			logger.Info("创建/检测产品文件夹结束");
		}

		public void OnDisconnection()
		{
			logger.Info("开始清空正在下载的进程...");
			AbstractDownLoader.ClearDownLoader();
			logger.Info("清空正在下载的进程结束.");
			logger.Info("开始保存配置文件...");
			ConfigManager.SaveProductWebSiteInfo(aliDownloader.ProductWebSiteInfoList);
			logger.Info("保存配置文件结束.");
			DeleteCoreFiles();
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
			if (cmdName == "Save")
			{
				settingForm.Save();
			}
		}

		public Control GetSetting()
		{
			return settingForm;
		}

		private void DeleteCoreFiles()
		{
			try
			{
				File.Delete(Path.Combine(Arguments.TEMP_FOLDER, Arguments.PHANTOMJS_NAME));
				File.Delete(Path.Combine(Arguments.TEMP_FOLDER, Arguments.SCRIPT_NAME));
			}
			catch (Exception ex)
			{
				logger.Error("Delete core files failed.", ex);
			}
		}
	}
}
