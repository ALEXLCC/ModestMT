using ModestMT.AliDownloader.Model;
using ModestMT.Core.Exception;
using ModestMT.Core.Logger;
using ModestMT.Core.System;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace ModestMT.AliDownloader.Utility
{
	public class ConfigManager
	{
		private static string CONFIG_FILE_NAME = Path.Combine(DirUtils.WorkingDirectory, "WebSite.xml");

		internal static string DOWNLOAD_FOLDER;

		private static readonly Logger logger = LogManager.GetLogger(typeof(ConfigManager));

		static ConfigManager()
		{
			XmlDocument document = new XmlDocument();

			document.Load(CONFIG_FILE_NAME);

			DOWNLOAD_FOLDER = document.SelectSingleNode("/AliDownload/DownloadFolder").InnerText;
		}

		internal static IList<WebSiteInfo> GetProductWebSiteInfo()
		{
			try
			{
				List<WebSiteInfo> webSiteInfos = new List<WebSiteInfo>();

				XmlDocument document = new XmlDocument();

				document.Load(CONFIG_FILE_NAME);

				XmlNodeList nodes = document.SelectNodes("/AliDownload/WebSites/WebSiteInfo");
				foreach (XmlNode node in nodes)
				{
					if (node.Attributes["Url"] == null || string.IsNullOrEmpty(node.Attributes["Url"].Value))
					{
						logger.Error("发现被损坏的链接节点.该节点将会被程序自动删除。");
						continue;
					}
					string url = node.Attributes["Url"].Value;

					StatusCode status = StatusCode.Wait;
					if (node.Attributes["Status"] != null)
					{
						int statusCode;
						status = int.TryParse(node.Attributes["Status"].Value, out statusCode) ? (StatusCode)Enum.ToObject(typeof(StatusCode), statusCode) : status;
					}

					string title = "";
					if (node.Attributes["Title"] != null)
					{
						title = node.Attributes["Title"].Value;
					}

					webSiteInfos.Add(new WebSiteInfo() { Url = url, Status = status, Title = title });
				}

				return webSiteInfos;
			}
			catch (Exception ex)
			{
				logger.Error("配置文件被损坏，请修复软件.");
				throw new BasicException("配置文件被损坏，请修复软件.", ex);
			}
		}

		//internal static IList<WebSiteInfo> GetStoreWebSiteInfo()
		//{
		//	try
		//	{
		//		List<WebSiteInfo> webSiteInfos = new List<WebSiteInfo>();

		//		XmlDocument document = new XmlDocument();

		//		document.Load(CONFIG_FILE_NAME);

		//		XmlNodeList nodes = document.SelectNodes("/AliDownload/WetStores/WebSiteInfo");
		//		foreach (XmlNode node in nodes)
		//		{
		//			StatusCode status = (StatusCode)Enum.ToObject(typeof(StatusCode), int.Parse(node.Attributes["Status"].Value));
		//			webSiteInfos.Add(new WebSiteInfo() { Url = node.Attributes["Url"].Value, Status = status });
		//		}

		//		return webSiteInfos;
		//	}
		//	catch (Exception ex)
		//	{
		//		throw new BasicException("配置文件被损坏，请修复软件.", ex);
		//	}
		//}

		internal static void SaveProductWebSiteInfo(IList<WebSiteInfo> webSiteInfos)
		{
			try
			{
				XmlDocument document = new XmlDocument();

				document.Load(CONFIG_FILE_NAME);

				XmlNode rootNode = document.SelectSingleNode("/AliDownload/WebSites");
				rootNode.RemoveAll();

				foreach (WebSiteInfo webSiteInfo in webSiteInfos)
				{
					XmlNode node = document.CreateElement("WebSiteInfo");

					XmlAttribute urlAttr = document.CreateAttribute("Url");
					urlAttr.Value = webSiteInfo.Url;
					node.Attributes.Append(urlAttr);

					XmlAttribute statusAttr = document.CreateAttribute("Status");
					statusAttr.Value = ((int)webSiteInfo.Status).ToString();
					node.Attributes.Append(statusAttr);

					XmlAttribute titleAttr = document.CreateAttribute("Title");
					titleAttr.Value = webSiteInfo.Title;
					node.Attributes.Append(titleAttr);

					rootNode.AppendChild(node);
				}

				document.SelectSingleNode("/AliDownload/DownloadFolder").InnerText = DOWNLOAD_FOLDER;

				document.Save(CONFIG_FILE_NAME);
			}
			catch
			{
				logger.Error("保存配置文件失败.");
			}
		}

		//internal static void SaveStoreWebSiteInfo(IList<WebSiteInfo> webSiteInfos)
		//{
		//	XmlDocument document = new XmlDocument();

		//	document.Load(CONFIG_FILE_NAME);

		//	XmlNode rootNode = document.SelectSingleNode("/AliDownload/WetStores");
		//	rootNode.RemoveAll();

		//	foreach (WebSiteInfo webSiteInfo in webSiteInfos)
		//	{
		//		XmlNode node = document.CreateElement("WebSiteInfo");

		//		XmlAttribute urlAttr = document.CreateAttribute("Url");
		//		urlAttr.Value = webSiteInfo.Url;
		//		node.Attributes.Append(urlAttr);

		//		XmlAttribute statusAttr = document.CreateAttribute("Status");
		//		statusAttr.Value = ((int)webSiteInfo.Status).ToString();
		//		node.Attributes.Append(statusAttr);

		//		rootNode.AppendChild(node);
		//	}
		//	document.Save(CONFIG_FILE_NAME);
		//}
	}
}
