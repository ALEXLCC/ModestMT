using ModestMT.Core.Collection;
using ModestMT.Core.Logger;
using ModestMT.Core.System;
using ModestMT.WinForm.ImagePicker.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;

namespace ModestMT.WinForm.ImagePicker.Plugin.Products.AliDownloader.Bll
{
	internal delegate void UpdateStatus();

	internal abstract class AbstractDownLoader
	{
		private static readonly Logger logger = LogManager.GetLogger(typeof(AbstractDownLoader));

		internal static int TIMEOUT;

		internal static string PRODUCTS_FOLDER = Path.Combine(DirUtils.WorkingDirectory, "Products");

		internal static string TEMP_HTML_FOLDER = Path.Combine(DirUtils.WorkingDirectory, "Html");

		internal static string PHANTOMJS_PATH = Path.Combine(DirUtils.WorkingDirectory, "WebKit\\phantomjs.exe");

		internal static string SCRIPT_PATH = Path.Combine(DirUtils.WorkingDirectory, "WebKit\\imagePicker.js");

		private static Dictionary<WebSiteInfo, DownLoaderProcessInfo> processPool = new Dictionary<WebSiteInfo, DownLoaderProcessInfo>();

		private static System.Threading.Timer TIMER;

		private ObservableList<WebSiteInfo> downloadList;

		protected abstract void DownLoad(string html, WebSiteInfo webSiteInfo);

		static AbstractDownLoader()
		{
			TimerCallback callback = new TimerCallback(ManageProcess);
			TIMER = new System.Threading.Timer(callback, null, 5000, 5000);

			try
			{
				TIMEOUT = int.Parse(ConfigurationManager.AppSettings["timeout"].ToString());
			}
			catch (Exception ex)
			{
				throw new AccumulateTreasureException("读取配置timeout失败，请重新安装软件或联系客服。", ex);
			}

			if (File.Exists(PHANTOMJS_PATH) == false)
			{
				throw new AccumulateTreasureException("Webkit未找到，请重新安装软件或联系客服。");
			}
		}

		internal AbstractDownLoader(ObservableList<WebSiteInfo> downloadList)
		{
			this.downloadList = downloadList;
		}

		internal void DownLoad()
		{
			foreach (WebSiteInfo webSiteInfo in downloadList)
			{
				webSiteInfo.Status = StatusCode.Process;

				Process process = new Process();

				process.StartInfo = new ProcessStartInfo(PHANTOMJS_PATH);
				process.StartInfo.Arguments = string.Format(" {0} {1} {2} {3}", SCRIPT_PATH, webSiteInfo.Url, Path.Combine(TEMP_HTML_FOLDER, webSiteInfo.GetHashCode().ToString().Replace('-', '_')), TIMEOUT);

				process.EnableRaisingEvents = true;
				process.StartInfo.RedirectStandardOutput = true;
				process.StartInfo.CreateNoWindow = true;
				process.StartInfo.UseShellExecute = false;
				process.StartInfo.RedirectStandardOutput = true;
				process.StartInfo.RedirectStandardError = true;

				process.ErrorDataReceived += process_ErrorDataReceived;
				process.OutputDataReceived += process_OutputDataReceived;
				process.Exited += process_Exited;

				process.Start();

				logger.Info(string.Format("Download html {0} start...", webSiteInfo.Url));

				processPool.Add(webSiteInfo, new DownLoaderProcessInfo(webSiteInfo, process, DateTime.Now));
			}
		}

		private void process_OutputDataReceived(object sender, DataReceivedEventArgs e)
		{
			if (e.Data != null)
			{
				logger.Info(e.Data);
			}
		}

		private void process_ErrorDataReceived(object sender, DataReceivedEventArgs e)
		{
			if (e.Data != null)
			{
				logger.Error(e.Data);
			}
		}

		internal static void ClearDownLoader()
		{
			WebSiteInfo[] list = new WebSiteInfo[processPool.Keys.Count];
			processPool.Keys.CopyTo(list, 0);
			foreach (WebSiteInfo webSiteInfo in list)
			{
				StopTask(webSiteInfo);
				webSiteInfo.Status = StatusCode.Wait;
			}
		}

		internal static void StopTask(WebSiteInfo webSiteInfo)
		{
			KillProcess(webSiteInfo, false);
		}

		private void process_Exited(object sender, EventArgs e)
		{
			string html = string.Empty;
			string hashCode = string.Empty;
			string htmlPath = string.Empty;
			string realHashCode = string.Empty;

			try
			{
				// Download main pictures
				Process process = (Process)sender;
				htmlPath = process.StartInfo.Arguments.Split(' ')[3] + ".html";

				hashCode = Path.GetFileNameWithoutExtension(htmlPath);

				realHashCode = hashCode.Replace('_', '-');

				using (StreamReader sw = new StreamReader(File.OpenRead(htmlPath)))
				{
					html = sw.ReadToEnd();
				}
			}
			catch
			{
				SetWebSiteInfoStatus(realHashCode, StatusCode.Failed);
				return;
			}

			if (string.IsNullOrEmpty(html) == false)
			{
				try
				{
					WebSiteInfo webSiteInfo = GetWebSiteInfoByHashCode(realHashCode);

					logger.Info(string.Format("Read html {0} finished", webSiteInfo.Url));

					DownLoad(html, webSiteInfo);

					SetWebSiteInfoStatus(realHashCode, StatusCode.Finish);

					//Kiss this process
					KillProcess(realHashCode);
				}
				catch
				{
					KillProcess(realHashCode);
					SetWebSiteInfoStatus(realHashCode, StatusCode.Failed);
					return;
				}

				File.Delete(htmlPath);
			}
			else
			{
				logger.Info(string.Format("Read html failed."));
				KillProcess(realHashCode);
				SetWebSiteInfoStatus(realHashCode, StatusCode.Failed);
				return;
			}

		}

		private static void KillProcess(WebSiteInfo webSiteInfo, bool enableRaisingEvernts)
		{
			if (processPool.ContainsKey(webSiteInfo))
			{
				Process process = processPool[webSiteInfo].Process;
				process.EnableRaisingEvents = enableRaisingEvernts;
				if (process != null && process.HasExited == false)
				{
					process.Kill();
				}
				processPool.Remove(webSiteInfo);
			}
		}

		private static void ManageProcess(object obj)
		{
			DateTime now = DateTime.Now;
			List<WebSiteInfo> deleteList = new List<WebSiteInfo>();
			foreach (KeyValuePair<WebSiteInfo, DownLoaderProcessInfo> pair in processPool)
			{
				TimeSpan timeSpan = now - pair.Value.Time;
				if (timeSpan.TotalMilliseconds > TIMEOUT)
				{
					deleteList.Add(pair.Key);
				}
			}

			foreach (WebSiteInfo webSiteInfo in deleteList)
			{
				StopTask(webSiteInfo);
				webSiteInfo.Status = StatusCode.Failed;
			}
		}

		private void KillProcess(string realHashCode)
		{
			WebSiteInfo webSiteInfo = GetWebSiteInfoByHashCode(realHashCode);
			if (webSiteInfo != null)
			{
				StopTask(webSiteInfo);
			}
		}

		private void SetWebSiteInfoStatus(string hashCode, StatusCode status)
		{
			WebSiteInfo webSiteInfo = GetWebSiteInfoByHashCode(hashCode);
			if (webSiteInfo != null)
			{
				webSiteInfo.Status = status;
			}
		}

		private WebSiteInfo GetWebSiteInfoByHashCode(string hashCode)
		{
			WebSiteInfo result = null;
			foreach (WebSiteInfo webSiteInfo in this.downloadList)
			{
				if (webSiteInfo.GetHashCode().ToString() == hashCode)
				{
					result = webSiteInfo;
					break;
				}
			}
			return result;
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
				urlList.Add(match.Groups["imgUrl"].Value);
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
	}
}
