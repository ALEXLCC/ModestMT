using ModestMT.AliDownloader.Model;
using ModestMT.Core.Collection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ModestMT.AliDownloader.Bll
{
	public interface IProductResourceManager
	{
		BindingList<WebSiteInfo> ProductWebSiteInfoList { get; }
	}

	//public interface IStoreResourceManager
	//{
	//	BindingList<WebSiteInfo> StoreWebSiteInfoList { get; }
	//}
}
