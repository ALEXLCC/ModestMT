using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;

namespace ModestMT.WinForm.ImagePicker.Plugin.Products.AliDownloader.Bll
{
	public class WebSiteInfo : BaseEntity
	{
		private string url;

		private string statusString;

		private StatusCode status;

		private bool canViewFolder;


		public WebSiteInfo()
		{
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

		public bool CanViewFolder
		{
			get
			{
				return this.canViewFolder;
			}
			set
			{
				if (this.canViewFolder != value)
				{
					this.canViewFolder = value;
					NotifyPropretyChanged("CanViewFolder");
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
					CanViewFolder = false;
					StatusString = "下载中...";
					break;
				case StatusCode.Finish:
					CanViewFolder = true;
					StatusString = "完成";
					break;
				case StatusCode.Wait:
					CanViewFolder = false;
					StatusString = "等待";
					break;
				case StatusCode.Failed:
					CanViewFolder = false;
					StatusString = "下载失败";
					break;
			}
		}
	}
}
