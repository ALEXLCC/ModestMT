using ModestMT.AliDownloader.Model;
using ModestMT.AliDownloader.Utility;
using ModestMT.Core.Collection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading;
using System.Xml;

namespace ModestMT.AliDownloader.Bll
{
	internal class ResourceManager : IProductResourceManager //, IStoreResourceManager
	{
		private static BindingList<WebSiteInfo> PRODUCT_WEB_SITE_INFO;

		private static BindingList<WebSiteInfo> STORE_WEB_STIE_INFO;

		internal ResourceManager()
		{
			if (PRODUCT_WEB_SITE_INFO == null)
			{
				PRODUCT_WEB_SITE_INFO = new BindingList<WebSiteInfo>(ConfigManager.GetProductWebSiteInfo());
			}
			//if (STORE_WEB_STIE_INFO == null)
			//{
			//	STORE_WEB_STIE_INFO = new BindingList<WebSiteInfo>(ConfigManager.GetStoreWebSiteInfo());
			//}
		}

		internal static bool ContainsProduct(string url)
		{
			bool isExisted = false;
			foreach (WebSiteInfo webSiteInfo in PRODUCT_WEB_SITE_INFO)
			{
				if (webSiteInfo.Url == url)
				{
					isExisted = true;
				}
			}
			return isExisted;
		}

		//internal static bool ContainsStore(string url)
		//{
		//	bool isExisted = false;
		//	foreach (WebSiteInfo webSiteInfo in STORE_WEB_STIE_INFO)
		//	{
		//		if (webSiteInfo.Url == url)
		//		{
		//			isExisted = true;
		//		}
		//	}
		//	return isExisted;
		//}

		public BindingList<WebSiteInfo> ProductWebSiteInfoList
		{
			get
			{
				return PRODUCT_WEB_SITE_INFO;
			}
			set
			{
				if (PRODUCT_WEB_SITE_INFO != value)
				{
					PRODUCT_WEB_SITE_INFO = new BindingList<WebSiteInfo>();
				}
			}
		}

		//public BindingList<WebSiteInfo> StoreWebSiteInfoList
		//{
		//	get
		//	{
		//		return STORE_WEB_STIE_INFO;
		//	}
		//	set
		//	{
		//		if (STORE_WEB_STIE_INFO != value)
		//		{
		//			STORE_WEB_STIE_INFO = new BindingList<WebSiteInfo>();
		//		}
		//	}
		//}
	}
}
