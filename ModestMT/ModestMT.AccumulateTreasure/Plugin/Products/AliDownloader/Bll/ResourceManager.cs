using ModestMT.Core.Collection;
using ModestMT.WinForm.ImagePicker.Plugin.Products.AliDownloader.Utility;
using ModestMT.WinForm.ImagePicker.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading;
using System.Xml;

namespace ModestMT.WinForm.ImagePicker.Plugin.Products.AliDownloader.Bll
{
	internal class ResourceManager : BaseEntity, IResourceManager
	{
		private static ObservableList<WebSiteInfo> WEB_SITE_INFO;

		internal ResourceManager()
		{
			if (WEB_SITE_INFO == null)
			{
				WEB_SITE_INFO = new ObservableList<WebSiteInfo>(ConfigManager.GetWebSiteInfo());
			}
		}

		internal static bool Contains(string url)
		{
			bool isExisted = false;
			foreach (WebSiteInfo webSiteInfo in WEB_SITE_INFO)
			{
				if (webSiteInfo.Url == url)
				{
					isExisted = true;
				}
			}
			return isExisted;
		}

		public ObservableList<WebSiteInfo> WebSiteInfoList
		{
			get
			{
				return WEB_SITE_INFO;
			}
			set
			{
				if (WEB_SITE_INFO != value)
				{
					WEB_SITE_INFO = new ObservableList<WebSiteInfo>();
					NotifyPropretyChanged("WebSiteInfoList");
				}
			}
		}

		internal void RegistePropertyChangedEvert(PropertyChangedEventHandler handler)
		{
			this.PropertyChanged += handler;
			WebSiteInfoList.PropertyChanged += handler;
			foreach (WebSiteInfo webSiteInfo in WebSiteInfoList)
			{
				webSiteInfo.PropertyChanged += handler;
			}
		}
	}
}
