using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace ModestMT.AliDownloader.Utility
{
	internal class ExtractHelper
	{
		internal static void Extract(string path, params string[] resources)
		{
			foreach (string resource in resources)
			{
				string resourePath = Path.Combine(path, resource);
				FileInfo file = new FileInfo(resourePath);
				if (file.Exists == true)
				{
					file.Delete();
				}

				FileStream fs = File.Create(resourePath);
				Stream stream = typeof(ExtractHelper).Assembly.GetManifestResourceStream(resource);
				byte[] content = new byte[stream.Length];
				stream.Read(content, 0, (int)stream.Length);
				fs.Write(content, 0, content.Length);
				fs.Close();
				stream.Close();
			}
		}
	}
}
