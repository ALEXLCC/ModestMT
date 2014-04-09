using ModestMT.Core.System;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ModestMT.AliDownloader.Bll
{
	internal class AliStoreDownLoader : AbstractDownLoader
	{
		protected override void DownLoad(string html, Model.WebSiteInfo webSiteInfo)
		{
		}

		protected override string SCRIPT_PATH
		{
			get
			{
				return Path.Combine(DirUtils.WorkingDirectory, "alistoredownloader.js");
			}
		}

		protected override string GetArguments(Model.WebSiteInfo webSiteInfo)
		{
			return string.Format(" {0} {1} {2}", SCRIPT_PATH, webSiteInfo.Url, Path.Combine(TEMP_HTML_FOLDER, webSiteInfo.GetHashCode().ToString().Replace('-', '_')));
		}
	}
}
