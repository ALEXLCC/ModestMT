using System;

namespace ModestMT.Core.Logger
{
	/// <summary>
	/// Implements ICASStopWatch but does nothing
	/// This class exists to improve performance when Trace logging is disabled. Previously StopWatches etc were created for every call
	/// </summary>
	public class StopWatchNoOpDummy : IStopWatch
	{
		public StopWatchNoOpDummy(Logger parentLogger)
		{
		}

		public void Start()
		{
		}

		public void Stop()
		{
		}

		public void LogTimeAndMessageToTrace(object value)
		{
		}

		public void StopAndLogTimeToTrace()
		{
		}

		public void StopAndLogTimeAndMessageToTrace(object value)
		{
		}

		public void StopAndLogTimeAndMessageToTrace(string message, object[] objects)
		{
		}

		public long ElapsedMilliseconds
		{
			get
			{
				return -1;
			}
		}

		public long Interval()
		{
			return -1;
		}
	}
}
