using System.Diagnostics;
using System;

namespace ModestMT.Core.Logger
{
	public class StopWatch : IStopWatch
	{
		private static String formatString = @"h\hm\ms\.fff\s";
		private long lastIntervalCheck;

		public StopWatch(Logger parentLogger)
		{
			this._parentLogger = parentLogger;
			this._stopwatch = new Stopwatch();
		}
		private readonly Logger _parentLogger;

		Stopwatch _stopwatch;

		public void Start()
		{
			this._stopwatch.Reset();  // Reset. We decided to auto-start the stopwatch so it makes sense to reset before starting
			this._stopwatch.Start();
		}

		public void Stop()
		{
			this._stopwatch.Stop();
		}

		public void StopAndLogTimeToTrace()
		{
			this._stopwatch.Stop();

			this._parentLogger.Trace(this.StopwatchTimeElapsedMessage());
		}

		public void LogTimeAndMessageToTrace(object value)
		{
			this._parentLogger.Trace("RETURN " + value + " " + this.StopwatchTimeElapsedMessage());
		}

		public void StopAndLogTimeAndMessageToTrace(object value)
		{
			this._stopwatch.Stop();

			this._parentLogger.Trace("RETURN " + value + " " + this.StopwatchTimeElapsedMessage());
		}

		public void StopAndLogTimeAndMessageToTrace(string message, object[] objects)
		{
			this._stopwatch.Stop();

			message = string.Format(message, objects);

			this._parentLogger.Trace(message + " " + this.StopwatchTimeElapsedMessage());
		}

		private string StopwatchTimeElapsedMessage()
		{
			return "Stopwatch time elapsed: " + this._stopwatch.Elapsed.ToString() + " (" + this._stopwatch.ElapsedMilliseconds + " ms, " + this._stopwatch.ElapsedTicks + " ticks)";
		}

		public override string ToString()
		{
			return StopwatchTimeElapsedMessage();
		}

		public long ElapsedMilliseconds
		{
			get
			{
				return this._stopwatch.ElapsedMilliseconds;
			}
		}

		public long Interval()
		{
			long overallDuraton = this._stopwatch.ElapsedTicks;
			long elapsed = overallDuraton - this.lastIntervalCheck;

			this.lastIntervalCheck = overallDuraton;

			return (elapsed / TimeSpan.TicksPerMillisecond);
		}

		public static String Duration(long milliseconds)
		{
			TimeSpan durationSpan = new TimeSpan(milliseconds * TimeSpan.TicksPerMillisecond);
			// TODO: return durationSpan.ToString(formatString);
			return durationSpan.ToString();
		}
	}
}
