using ModestMT.Core.Collection;
using ModestMT.Core.Logger;
using ModestMT.Core.Text;
using ModestMT.WinForm.ImagePicker.Utility;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace ModestMT.WinForm.ImagePicker.Plugin.Products.AliDownloader.Bll
{
	internal class AliDownLoader : AbstractDownLoader
	{
		private static readonly Logger logger = LogManager.GetLogger(typeof(AliDownLoader));

		private static object locker = new object();

		private static string PRE_FOLDER_NAME = "f";

		internal AliDownLoader(ObservableList<WebSiteInfo> downloadList)
			: base(downloadList)
		{
		}

		protected override void DownLoad(string html, WebSiteInfo webSiteInfo)
		{
			logger.Info(string.Format("Create folders for website: {0}", webSiteInfo.Url));
			DirectoryInfo directory;
			lock (locker)
			{
				string folderName = CalculateFolderName(webSiteInfo);
				directory = new DirectoryInfo(folderName);
				if (directory.Exists == true)
				{
					directory.Delete(true);
				}
				directory.Create();
			}

			DirectoryInfo mainDirectory = new DirectoryInfo(Path.Combine(directory.FullName, "main"));
			mainDirectory.Create();

			DirectoryInfo ditailsDirectory = new DirectoryInfo(Path.Combine(directory.FullName, "details"));
			ditailsDirectory.Create();

			string titile = TextUtility.RemoveSpecialChar(GetTitle(html));

			File.Create(Path.Combine(directory.FullName, titile + ".txt"));

			logger.Info(string.Format("Start download main pictures for website: {0}", webSiteInfo.Url));

			// Download main pictures
			DownLoadMainPictures(html, mainDirectory);

			logger.Info(string.Format("Start download detail pictures for website: {0}", webSiteInfo.Url));

			DownLoadDetailPicture(html, ditailsDirectory);

			logger.Info(string.Format("Download pictures finished for website: {0}", webSiteInfo.Url));
		}

		private string CalculateFolderName(WebSiteInfo webSiteInfo)
		{
			string basicName = webSiteInfo.Url.GetHashCode().ToString().Replace('-', '_');

			string[] folders = Directory.GetDirectories(PRODUCTS_FOLDER);

			// whether it's exsited.
			string folderName = ExistedFolder(basicName, folders);
			if (folderName == null)
			{
				string preFolderName = GetPreFolderName(folders);
				return Path.Combine(PRODUCTS_FOLDER, preFolderName + basicName);
			}
			else
			{
				return folderName;
			}
		}

		private string ExistedFolder(string name, string[] folders)
		{
			foreach (string folder in folders)
			{
				if (folder.EndsWith(name))
				{
					return folder;
				}
			}
			return null;
		}

		private string GetPreFolderName(string[] folders)
		{
			string lastFolderName = string.Empty;
			try
			{
				if (folders.Length == 0)
				{
					return "0" + PRE_FOLDER_NAME;
				}
				List<string> folderNames = new List<string>();
				foreach (string folder in folders)
				{
					folderNames.Add(Path.GetFileName(folder));
				}
				folderNames.Sort();
				lastFolderName = folderNames[folderNames.Count - 1];
				string index = lastFolderName.Substring(0, lastFolderName.IndexOf(PRE_FOLDER_NAME));
				return (int.Parse(index) + 1).ToString() + PRE_FOLDER_NAME;
			}
			catch
			{
				throw new AccumulateTreasureException(string.Format("取得文件夹序列出错: {0}。", lastFolderName));
			}
		}

		private void DownLoadMainPictures(string html, DirectoryInfo directoryInfo)
		{
			Regex regex = new Regex("data-imgs=\".*\"");
			MatchCollection collection = regex.Matches(html);
			int index = 1;
			foreach (Match m in collection)
			{
				string value = HttpUtility.HtmlDecode(m.Value);
				string pictureInfo = value.Substring(11, value.Length - 12);

				JObject jsonArray = (JObject)JsonConvert.DeserializeObject(pictureInfo);
				// Preview imapge will be created by ALI or TAOBAO, there is no need to download them.
				//string previewPictureUrl = jsonArray["preview"].ToString();
				string originalPictureUrl = jsonArray["original"].ToString();

				//string previewFileName = Path.Combine(directoryInfo.FullName, index+"_small");
				string suffix = Path.GetExtension(originalPictureUrl);
				string originalFileName = Path.Combine(directoryInfo.FullName, index.ToString() + suffix);

				//DownLoadPicture(previewPictureUrl, previewFileName);
				DownLoadPicture(originalPictureUrl, originalFileName);

				index++;
			}
		}

		private void DownLoadDetailPicture(string html, DirectoryInfo directoryInfo)
		{
			int index = html.IndexOf("<div id=\"desc-lazyload-container\"");
			int index2 = html.IndexOf("</div>", index);

			string details = html.Substring(index, index2 - index);

			List<string> images = GetHtmlImageUrlList(details);

			foreach (string image in images)
			{
				string count = (images.IndexOf(image) + 1).ToString();
				DownLoadPicture(image, Path.Combine(directoryInfo.FullName, count + Path.GetExtension(image)));
			}
		}

		private string GetTitle(string html)
		{
			string IDENTIFY = "<div class=\"mod-detail-hd\" id=\"mod-detail-hd\">";
			int index = html.IndexOf(IDENTIFY);
			int index2 = html.IndexOf("</div>", index);

			string titleBody = html.Substring(index, index2 - index).Replace(IDENTIFY, ""); ;

			index = titleBody.IndexOf(">");
			index2 = titleBody.IndexOf("<", index);

			return titleBody.Substring(index + 1, index2 - index - 1);
		}
	}
}
