using ModestMT.Core.System;
using System;
using System.IO;

namespace ModestMT.Core.Logger
{
	public sealed class LogManager
	{
		private LogManager()
		{
		}

		public static Logger GetLogger(Type typeToLogFor)
		{
			log4net.ILog log4netLogger = log4net.LogManager.GetLogger(typeToLogFor.ToString());

			return new Logger(log4netLogger);
		}

		public static void InitLogger(string logfileName)
		{
			string logFilePath = Path.Combine(DirUtils.WorkingDirectory, logfileName);
			FileInfo fileInfo = new FileInfo(logFilePath);

			log4net.Config.XmlConfigurator.ConfigureAndWatch(fileInfo);
		}
	}
}
