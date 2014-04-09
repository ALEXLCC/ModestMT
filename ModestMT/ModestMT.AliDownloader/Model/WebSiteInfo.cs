using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ModestMT.AliDownloader.Model
{
	public class WebSiteInfo : BaseEntity
	{
		private double pictureCount = 0;

		private double downloadIndex = 0;

		private string url;

		private string statusString;

		private string title;

		private StatusCode status;

		public WebSiteInfo()
		{
			title = "";
			RefrushStatus();
		}

		public string Url
		{
			get
			{
				return this.url;
			}
			set
			{
				if (url != value)
				{
					this.url = value;
					NotifyPropretyChanged("Url");
				}
			}
		}

		public double PictureCount
		{
			get
			{
				return this.pictureCount;
			}
			set
			{
				if (pictureCount != value)
				{
					this.pictureCount = value;
					NotifyPropretyChanged("PictureCount");
				}
			}
		}

		public double DownloadIndex
		{
			get
			{
				return this.downloadIndex;
			}
			set
			{
				if (downloadIndex != value)
				{
					this.downloadIndex = value;
					RefrushStatus();
					NotifyPropretyChanged("DownloadIndex");
				}
			}
		}


		public StatusCode Status
		{
			get
			{
				return this.status;
			}
			set
			{
				if (this.status != value)
				{
					this.status = value;
					RefrushStatus();
					NotifyPropretyChanged("Status");
				}
			}
		}

		public string StatusString
		{
			get
			{
				return this.statusString;
			}
			set
			{
				if (this.statusString != value)
				{
					this.statusString = value;
					NotifyPropretyChanged("StatusString");
				}
			}
		}

		public string Title
		{
			get
			{
				return this.title;
			}
			set
			{
				if (this.title != value)
				{
					this.title = value;
					NotifyPropretyChanged("Title");
				}
			}
		}

		public override int GetHashCode()
		{
			return this.Url.GetHashCode();
		}

		private void RefrushStatus()
		{
			switch (this.status)
			{
				case StatusCode.Process:
					StatusString = "下载中..." + (pictureCount == 0 ? 0 : downloadIndex / pictureCount).ToString("p");
					break;
				case StatusCode.Finish:
					StatusString = "完成 100%";
					break;
				case StatusCode.Wait:
					StatusString = "等待";
					break;
				case StatusCode.Failed:
					StatusString = "下载失败";
					break;
			}
		}
	}
}
