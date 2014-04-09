using ModestMT.Core.Collection;
using ModestMT.WinForm.ImagePicker.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModestMT.WinForm.ImagePicker.Plugin.Products.AliDownloader.Bll
{
	public interface IResourceManager
	{
		ObservableList<WebSiteInfo> WebSiteInfoList { get; }
	}
}
