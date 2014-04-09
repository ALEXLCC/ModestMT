using System;
using System.Collections.Generic;
using System.Text;

namespace ModestMT.WinForm.ImagePicker.Plugin.Products.AliDownloader.Bll
{
	[Flags]
	public enum StatusCode
	{
		Process = 0,
		Wait = 1,
		Finish = 2,
		Failed = 4
	}
}
