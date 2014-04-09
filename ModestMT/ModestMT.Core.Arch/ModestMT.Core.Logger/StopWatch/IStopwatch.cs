namespace ModestMT.Core.Logger
{
	public interface IStopWatch
	{
		void Start();

		void Stop();

		void LogTimeAndMessageToTrace(object value);

		void StopAndLogTimeAndMessageToTrace(object value);

		void StopAndLogTimeAndMessageToTrace(string message, params object[] objects);

		void StopAndLogTimeToTrace();

		long ElapsedMilliseconds { get; }

		long Interval();
	}
}
