using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace ModestMT.WinForm.ImagePicker.Plugin.Products.AliDownloader.Bll
{
	internal class DownLoaderProcessInfo
	{
		private WebSiteInfo webSiteInfo;
		private Process process;
		private DateTime time;

		public DownLoaderProcessInfo(WebSiteInfo webSiteInfo, Process process, DateTime time)
		{
			this.webSiteInfo = webSiteInfo;
			this.process = process;
			this.time = time;
		}

		public Process Process { get { return this.process; } }

		public WebSiteInfo WebSiteInfo { get { return this.webSiteInfo; } }

		public DateTime Time { get { return this.time; } }
	}
}
