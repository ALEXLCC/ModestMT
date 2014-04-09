using ModestMT.AliDownloader.Utility;
using ModestMT.Core.Exception;
using ModestMT.Core.Logger;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;

namespace ModestMT.AliDownloader.Bll
{
	public class Arguments
	{
		private static readonly Logger logger = LogManager.GetLogger(typeof(Arguments));

		internal static string PRODUCTS_FOLDER;

		internal static string PRODUCT_FOLDER_NAME = "产品";

		internal static string TEMP_FOLDER = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Temp");

		internal static string PHANTOMJS_PROCESS_NAME = "ModestMT.AliDownloader.command";

		internal static string PHANTOMJS_NAME = PHANTOMJS_PROCESS_NAME + ".exe";

		internal static string SCRIPT_NAME = "ModestMT.AliDownloader.aliproductdownloader.js";

		internal static int TIMEOUT = 6000;

		static Arguments()
		{
			Arguments.PRODUCTS_FOLDER = Path.Combine(ConfigManager.DOWNLOAD_FOLDER, PRODUCT_FOLDER_NAME);

			try
			{
				TIMEOUT = int.Parse(ConfigurationManager.AppSettings["timeout"].ToString());
			}
			catch (Exception ex)
			{
				logger.Error("读取配置timeout失败.", ex);
				throw new BasicException("读取配置timeout失败，请重新安装软件或联系客服。", ex);
			}
		}

		internal static void RefreshArguments()
		{
			Arguments.PRODUCTS_FOLDER = Path.Combine(ConfigManager.DOWNLOAD_FOLDER, PRODUCT_FOLDER_NAME);
		}
	}
}
