using ModestMT.AliDownloader.Model;
using ModestMT.Core.Exception;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace ModestMT.AliDownloader.Utility
{
	public class ProcessPool<T>
	{
		private Dictionary<Process, T> processPool = new Dictionary<Process, T>();
		private Dictionary<Process, DateTime> processTimeInfo = new Dictionary<Process, DateTime>();

		private int TIMEOUT;
		private Timer TIMER;

		public ProcessPool(int timeout)
		{
			TIMEOUT = timeout;

			TimerCallback callback = new TimerCallback(ManageProcess);
			TIMER = new System.Threading.Timer(callback, null, 5000, 5000);
		}

		public void AddProcess(Process process, T obj)
		{
			if (processPool.ContainsKey(process) == false)
			{
				processPool.Add(process, obj);
				processTimeInfo.Add(process, DateTime.Now);
			}
		}

		public void KillProcess(Process process)
		{
			if (process != null && process.HasExited == false)
			{
				process.Kill();
			}
			if (processPool.ContainsKey(process))
			{
				processPool.Remove(process);
				processTimeInfo.Remove(process);
			}
		}

		private void ManageProcess(object obj)
		{
			DateTime now = DateTime.Now;
			List<Process> deleteList = new List<Process>();
			foreach (KeyValuePair<Process, T> pair in processPool)
			{
				TimeSpan timeSpan = now - processTimeInfo[pair.Key];
				if (timeSpan.TotalMilliseconds > TIMEOUT)
				{
					deleteList.Add(pair.Key);
				}
			}

			foreach (Process process in deleteList)
			{
				if (process != null && process.HasExited == false)
				{
					process.Kill();
				}

				processPool.Remove(process);
				processTimeInfo.Remove(process);
			}
		}
	}
}
