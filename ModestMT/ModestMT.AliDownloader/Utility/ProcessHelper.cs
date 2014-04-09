using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace ModestMT.AliDownloader.Utility
{
	public class ProcessHelper
	{
		public static bool KillProcess(string name)
		{
			bool flag = true;
			Process[] processes = Process.GetProcessesByName(name);
			foreach (Process p in processes)
			{
				try
				{
					if (p.HasExited == false)
					{
						p.Kill();
					}
				}
				catch
				{
					flag = false;
					continue;
				}
			}
			return flag;
		}
	}
}
