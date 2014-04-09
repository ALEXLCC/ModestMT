using ModestMT.AliDownloader.Model;
using ModestMT.Core.Collection;
using ModestMT.Core.Exception;
using ModestMT.Core.Logger;
using ModestMT.Core.System;
using ModestMT.Core.Text;
using ModestMT.Core.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace ModestMT.AliDownloader.Bll
{
	internal class AliProductDownLoader : AbstractDownLoader
	{
		private static readonly Logger logger = LogManager.GetLogger(typeof(AliProductDownLoader));

		private static object locker = new object();

		private static Regex mainPictureRegex = new Regex("data-imgs=\".*\"");

		internal AliProductDownLoader()
			: base()
		{
		}

		protected override string SCRIPT_PATH
		{
			get
			{
				return Arguments.SCRIPT_NAME;
			}
		}

		protected override string GetArguments(WebSiteInfo webSiteInfo)
		{
			return string.Format(" {0} {1} {2} {3}", SCRIPT_PATH, webSiteInfo.Url, webSiteInfo.GetHashCode().ToString().Replace('-', '_'), Arguments.TIMEOUT);
		}

		protected override void DownLoad(string html, WebSiteInfo webSiteInfo)
		{
			string titile = TextUtility.RemoveSpecialChar(GetTitle(html));

			string folderTitile = titile.Length < 18 ? titile : titile.Substring(0, 18);

			webSiteInfo.Title = folderTitile;

			logger.Info(string.Format("创建文件夹: {0}", webSiteInfo.Url));

			DirectoryInfo directory;
			lock (locker)
			{
				string folderName = CalculateFolderName(folderTitile, webSiteInfo);
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

			FileStream fs = File.Create(Path.Combine(directory.FullName, titile + ".txt"));
			fs.Close();

			logger.Info(string.Format("开始下载主要图片: {0}", webSiteInfo.Url));

			MatchCollection collection = mainPictureRegex.Matches(html);
			int mainPictureCount = collection.Count;

			List<string> images = GetDetailPictures(html);
			int detailPictureCount = images.Count;

			webSiteInfo.PictureCount = mainPictureCount + detailPictureCount;

			// Download main pictures
			DownLoadMainPictures(html, mainDirectory, webSiteInfo);

			logger.Info(string.Format("开始下载详细图片: {0}", webSiteInfo.Url));

			DownLoadDetailPicture(images, ditailsDirectory, webSiteInfo);

			logger.Info(string.Format("下载完成: {0}", webSiteInfo.Url));
		}

		public static string ExistedFolder(string hashCode, string[] folders)
		{
			foreach (string folder in folders)
			{
				if (folder.EndsWith(hashCode))
				{
					return folder;
				}
			}
			return null;
		}

		private string CalculateFolderName(string title, WebSiteInfo webSiteInfo)
		{
			string hashCode = string.Format("[{0}]", webSiteInfo.Url.GetHashCode().ToString().Replace('-', '_'));

			string[] folders = Directory.GetDirectories(Arguments.PRODUCTS_FOLDER);

			// whether it's exsited.
			string folderName = ExistedFolder(hashCode, folders);
			if (folderName == null)
			{
				string preFolderName = GetPreFolderName(folders);
				return Path.Combine(Arguments.PRODUCTS_FOLDER, string.Format("{0}{1}{2}", preFolderName, title, hashCode));
			}
			else
			{
				return folderName;
			}
		}

		private string GetPreFolderName(string[] folders)
		{
			string lastFolderName = string.Empty;
			try
			{
				if (folders.Length == 0)
				{
					return "[1]";
				}
				List<string> folderNames = new List<string>();
				foreach (string folder in folders)
				{
					folderNames.Add(Path.GetFileName(folder));
				}
				folderNames.Sort();
				lastFolderName = folderNames[folderNames.Count - 1];
				int index = lastFolderName.IndexOf(']');
				if (index == -1)
				{
					return "[1]";
				}
				string number = lastFolderName.Substring(1, lastFolderName.IndexOf(']') - 1);
				return string.Format("[{0}]", (int.Parse(number) + 1).ToString());
			}
			catch
			{
				throw new BasicException(string.Format("取得文件夹序列出错: {0}。", lastFolderName));
			}
		}

		private void DownLoadMainPictures(string html, DirectoryInfo directoryInfo, WebSiteInfo webSiteInfo)
		{
			MatchCollection collection = mainPictureRegex.Matches(html);
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
				webSiteInfo.DownloadIndex++;

				index++;
			}
		}

		private List<string> GetDetailPictures(string html)
		{
			int index = html.IndexOf("<div id=\"desc-lazyload-container\"");

			string details = html.Substring(index, html.Length - index);

			List<string> images = GetHtmlImageUrlList(details);

			return images;
		}

		private void DownLoadDetailPicture(List<string> images, DirectoryInfo directoryInfo, WebSiteInfo webSiteInfo)
		{
			foreach (string image in images)
			{
				string count = (images.IndexOf(image) + 1).ToString();
				DownLoadPicture(image, Path.Combine(directoryInfo.FullName, count + Path.GetExtension(image)));
				webSiteInfo.DownloadIndex++;
			}
		}

		private string GetTitle(string html)
		{
			string IDENTIFY = "<div class=\"mod-detail-hd\" id=\"mod-detail-hd\">";
			int index = html.IndexOf(IDENTIFY);
			int index2 = html.IndexOf("</div>", index);

			string titleBody = html.Substring(index, index2 - index).Replace(IDENTIFY, ""); ;

			return HtmlUtitly.ExecRepaceHTML(titleBody);
		}
	}
}
