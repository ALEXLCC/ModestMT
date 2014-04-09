using System;
using System.Collections.Generic;
using System.Text;

namespace ModestMT.AliDownloader.Model
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
