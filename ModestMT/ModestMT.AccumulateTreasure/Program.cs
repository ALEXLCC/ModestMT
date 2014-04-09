using ModestMT.Core.Logger;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace ModestMT.AccumulateTreasure
{
	static class Program
	{
		private static readonly Logger logger = LogManager.GetLogger(typeof(Program));

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			if (IsAppRunning() == true)
			{
				logger.Warn("已存在该程序进程.");
				return;
			}
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new Main());
		}

		static bool IsAppRunning()
		{
			return Process.GetProcessesByName("ModestMT.AccumulateTreasure").Length > 1;
		}
	}
}
