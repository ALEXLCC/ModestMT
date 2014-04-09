using ModestMT.Core.System;
using ModestMT.WinForm.ImagePicker.Plugin.Products.AliDownloader.Bll;
using ModestMT.WinForm.ImagePicker.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace ModestMT.WinForm.ImagePicker.Plugin.Products.AliDownloader.Utility
{
	public static class ConfigManager
	{
		static string CONFIG_FILE_NAME = Path.Combine(DirUtils.WorkingDirectory, "WebSite.xml");

		internal static List<WebSiteInfo> GetWebSiteInfo()
		{
			try
			{
				List<WebSiteInfo> webSiteInfos = new List<WebSiteInfo>();

				XmlDocument document = new XmlDocument();

				document.Load(CONFIG_FILE_NAME);

				XmlNodeList nodes = document.SelectNodes("/WebSites/WebSiteInfo");
				foreach (XmlNode node in nodes)
				{
					StatusCode status = (StatusCode)Enum.ToObject(typeof(StatusCode), int.Parse(node.Attributes["Status"].Value));
					webSiteInfos.Add(new WebSiteInfo() { Url = node.Attributes["Url"].Value, Status = status });
				}

				return webSiteInfos;
			}
			catch (Exception ex)
			{
				throw new AccumulateTreasureException("配置文件被损坏，请修复软件.", ex);
			}
		}

		internal static void SaveWebSiteInfo(IList<WebSiteInfo> webSiteInfos)
		{
			XmlDocument document = new XmlDocument();

			document.Load(CONFIG_FILE_NAME);

			XmlNode rootNode = document.SelectSingleNode("/WebSites");
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

				rootNode.AppendChild(node);
			}
			document.Save(CONFIG_FILE_NAME);
		}
	}
}
