using ModestMT.AliDownloader.Model;
using ModestMT.AliDownloader.Utility;
using ModestMT.Core.Collection;
using ModestMT.Core.Exception;
using ModestMT.Core.Logger;
using ModestMT.Core.System;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;

namespace ModestMT.AliDownloader.Bll
{
	internal abstract class AbstractDownLoader
	{
		private static readonly Logger logger = LogManager.GetLogger(typeof(AbstractDownLoader));

		private static Dictionary<WebSiteInfo, Process> downloadProcess = new Dictionary<WebSiteInfo, Process>();

		private static ProcessPool<WebSiteInfo> processPool;

		protected abstract void DownLoad(string html, WebSiteInfo webSiteInfo);

		protected abstract string GetArguments(WebSiteInfo webSiteInfo);

		protected abstract string SCRIPT_PATH { get; }

		static AbstractDownLoader()
		{
			processPool = new ProcessPool<WebSiteInfo>(Arguments.TIMEOUT + 500);
		}

		internal void DownLoad(IList<WebSiteInfo> downloadList)
		{
			foreach (WebSiteInfo webSiteInfo in downloadList)
			{
				webSiteInfo.PictureCount = 0;
				webSiteInfo.DownloadIndex = 0;
				webSiteInfo.Status = StatusCode.Process;

				Process process = new Process();

				process.StartInfo = new ProcessStartInfo(Path.Combine(Arguments.TEMP_FOLDER, Arguments.PHANTOMJS_NAME));
				process.StartInfo.WorkingDirectory = Arguments.TEMP_FOLDER;
				process.StartInfo.Arguments = GetArguments(webSiteInfo);

				process.EnableRaisingEvents = true;
				process.StartInfo.CreateNoWindow = true;
				process.StartInfo.UseShellExecute = false;

				process.Exited += process_Exited;

				process.Start();

				logger.Info(string.Format("开始下载： {0}", webSiteInfo.Url));

				downloadProcess.Add(webSiteInfo, process);
				processPool.AddProcess(process, webSiteInfo);
			}
		}

		internal static void ClearDownLoader()
		{
			WebSiteInfo[] downloadList = new WebSiteInfo[downloadProcess.Keys.Count];
			downloadProcess.Keys.CopyTo(downloadList, 0);
			foreach (WebSiteInfo webSiteInfo in downloadList)
			{
				StopTask(webSiteInfo);
			}
			downloadProcess.Clear();
		}

		internal static void StopTask(WebSiteInfo webSiteInfo)
		{
			if (downloadProcess.ContainsKey(webSiteInfo))
			{
				downloadProcess[webSiteInfo].EnableRaisingEvents = false;
				processPool.KillProcess(downloadProcess[webSiteInfo]);
				downloadProcess.Remove(webSiteInfo);
			}
			webSiteInfo.Status = StatusCode.Wait;
		}

		/// <summary>
		/// 取得HTML中所有图片的 URL。
		/// </summary>
		/// <param name="html">HTML代码</param>
		/// <returns>图片的URL列表</returns>
		protected List<string> GetHtmlImageUrlList(string html)
		{
			// 定义正则表达式用来匹配 img 标签
			Regex regImg = new Regex(@"<img\b[^<>]*?\bsrc[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<imgUrl>[^\s\t\r\n""'<>]*)[^<>]*?/?[\s\t\r\n]*>", RegexOptions.IgnoreCase);

			// 搜索匹配的字符串
			MatchCollection matches = regImg.Matches(html);

			List<string> urlList = new List<string>();

			// 取得匹配项列表
			foreach (Match match in matches)
			{
				string file = match.Groups["imgUrl"].Value;
				if (file.EndsWith(".jpg") || file.EndsWith(".JPG"))
				{
					urlList.Add(match.Groups["imgUrl"].Value);
				}
			}

			return urlList;
		}

		protected void DownLoadPicture(string url, string fileName)
		{
			WebClient wc = new WebClient();
			try
			{
				wc.Headers.Add("User-Agent", "Chrome");

				wc.DownloadFile(url, fileName);
			}
			catch
			{
				wc.DownloadFile(url, fileName);

				if (wc != null)
				{
					wc.Dispose();
				}
			}
			finally
			{
				if (wc != null)
				{
					wc.Dispose();
				}
			}
		}

		private void process_Exited(object sender, EventArgs e)
		{
			string html = string.Empty;
			string hashCode = string.Empty;
			string htmlPath = string.Empty;
			string realHashCode = string.Empty;
			WebSiteInfo webSiteInfo = null;

			try
			{
				// Download main pictures
				Process process = (Process)sender;
				htmlPath = process.StartInfo.Arguments.Split(' ')[3] + ".html";

				hashCode = Path.GetFileNameWithoutExtension(htmlPath);

				realHashCode = hashCode.Replace('_', '-');

				webSiteInfo = GetWebSiteInfoByHashCode(realHashCode);

				processPool.KillProcess(downloadProcess[webSiteInfo]);
				downloadProcess.Remove(webSiteInfo);

				using (StreamReader sw = new StreamReader(File.OpenRead(Path.Combine(Arguments.TEMP_FOLDER, htmlPath))))
				{
					html = sw.ReadToEnd();
				}
			}
			catch
			{
				SetWebSiteInfoStatus(webSiteInfo, StatusCode.Failed);
				return;
			}

			if (string.IsNullOrEmpty(html) == false)
			{
				try
				{
					logger.Info(string.Format("读取HTML结束 {0}.", webSiteInfo.Url));

					DownLoad(html, webSiteInfo);

					webSiteInfo.Status = StatusCode.Finish;
				}
				catch
				{
					SetWebSiteInfoStatus(webSiteInfo, StatusCode.Failed);
				}

				File.Delete(htmlPath);
			}
			else
			{
				logger.Info(string.Format("读取HTML结束失败."));
				SetWebSiteInfoStatus(webSiteInfo, StatusCode.Failed);
			}
		}

		private WebSiteInfo SetWebSiteInfoStatus(WebSiteInfo webSiteInfo, StatusCode status)
		{
			if (webSiteInfo != null)
			{
				webSiteInfo.Status = status;
			}
			return webSiteInfo;
		}

		private WebSiteInfo GetWebSiteInfoByHashCode(string hashCode)
		{
			WebSiteInfo result = null;
			foreach (WebSiteInfo webSiteInfo in downloadProcess.Keys)
			{
				if (webSiteInfo.GetHashCode().ToString() == hashCode)
				{
					result = webSiteInfo;
					break;
				}
			}
			return result;
		}
	}
}
