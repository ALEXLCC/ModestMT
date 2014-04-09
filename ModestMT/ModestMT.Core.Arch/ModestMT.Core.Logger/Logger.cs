using System;
using System.Collections;
using System.Reflection;
using System.Diagnostics;
using SYSTEM = System;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace ModestMT.Core.Logger
{
	public class Logger
	{
		private readonly static IStopWatch _dummyStopWatch = new StopWatchNoOpDummy(null);
		private const string UNKNOWN = "UNKNOWN";

		public bool AuditEnabled
		{
			get
			{
				if (!_AuditEnabled.HasValue) //_AuditEnabled is null
				{
					if (this._log4netLogger.IsInfoEnabled || this._log4netLogger.IsDebugEnabled)
					{
						_AuditEnabled = true;
					}
					else
					{
						_AuditEnabled = false;
					}
				}
				return _AuditEnabled.Value;
			}
		}
		private bool? _AuditEnabled = null;
		public Logger(log4net.ILog log4netLogger)
		{
			this._log4netLogger = log4netLogger;
		}

		readonly log4net.ILog _log4netLogger;


		#region Is Level X Enabled

		// Note: At the time of writing Trace and Debug are synonomous
		public bool IsTraceEnabled
		{
			get
			{
				return this._log4netLogger.IsDebugEnabled;
			}
		}

		public bool IsDebugEnabled
		{
			get
			{
				return this._log4netLogger.IsDebugEnabled;
			}
		}

		public bool IsInfoEnabled
		{
			get
			{
				return this._log4netLogger.IsInfoEnabled;
			}
		}

		public bool IsWarnEnabled
		{
			get
			{
				return this._log4netLogger.IsWarnEnabled;
			}
		}

		public bool IsErrorEnabled
		{
			get
			{
				return this._log4netLogger.IsErrorEnabled;
			}
		}

		public bool IsFatalEnabled
		{
			get
			{
				return this._log4netLogger.IsFatalEnabled;
			}
		}

		#endregion

		#region Log Messages

		public void Trace(string message, SYSTEM.Exception exception = null)
		{
			if (this._log4netLogger.IsDebugEnabled)
			{
				if (exception != null)
					this._log4netLogger.Debug(message, exception);
				else

					this._log4netLogger.Debug(message);
			}
		}

		public void Debug(string message, SYSTEM.Exception exception = null)
		{
			if (this._log4netLogger.IsDebugEnabled)
			{
				if (exception != null)
					this._log4netLogger.Debug(message, exception);
				else

					this._log4netLogger.Debug(message);
			}
		}

		public void Info(string message, SYSTEM.Exception exception = null)
		{
			if (this._log4netLogger.IsInfoEnabled)
			{
				if (exception != null)
					this._log4netLogger.Info(message, exception);
				else

					this._log4netLogger.Info(message);
			}
		}

		public void Warn(string message, SYSTEM.Exception exception = null)
		{
			if (this._log4netLogger.IsWarnEnabled)
			{
				if (exception != null)
					this._log4netLogger.Warn(message, exception);
				else
					this._log4netLogger.Warn(message);
			}
		}

		public void Error(string message, SYSTEM.Exception exception = null)
		{
			if (this._log4netLogger.IsErrorEnabled)
			{
				if (exception != null)
					this._log4netLogger.Error(message, exception);
				else
					this._log4netLogger.Error(message);
			}
		}

		public void Fatal(string message, SYSTEM.Exception exception = null)
		{
			if (this._log4netLogger.IsFatalEnabled)
			{
				if (exception != null)
					this._log4netLogger.Fatal(message, exception);
				else
					this._log4netLogger.Fatal(message);
			}
		}

		#endregion

		#region Log Messages with string.Format

		public void Trace(string message, params object[] objects)
		{
			if (this._log4netLogger.IsDebugEnabled)
			{
				message = string.Format(message, objects);
				this._log4netLogger.Debug(message);
			}
		}

		public void Debug(string message, params object[] objects)
		{
			if (this._log4netLogger.IsDebugEnabled)
			{
				message = string.Format(message, objects);
				this._log4netLogger.Debug(message);
			}
		}

		public void Info(string message, params object[] objects)
		{
			if (this._log4netLogger.IsInfoEnabled)
			{
				message = string.Format(message, objects);
				this._log4netLogger.Info(message);
			}
		}

		public void Warn(string message, params object[] objects)
		{
			if (this._log4netLogger.IsWarnEnabled)
			{
				message = string.Format(message, objects);
				this._log4netLogger.Warn(message);
			}
		}

		public void Error(string message, params object[] objects)
		{
			if (this._log4netLogger.IsErrorEnabled)
			{
				message = string.Format(message, objects);
				this._log4netLogger.Error(message);
			}
		}

		public void Fatal(string message, params object[] objects)
		{
			if (this._log4netLogger.IsFatalEnabled)
			{
				message = string.Format(message, objects);
				this._log4netLogger.Fatal(message);
			}
		}

		#endregion

		public void TraceIn(params object[] parameters)
		{
			if (AuditEnabled == false)
				return;

			try
			{

				string functionName = UNKNOWN;
				string className = UNKNOWN;
				MethodBase CallingBase = GetCallingMethodBase();
				if (CallingBase != null)
				{
					functionName = CallingBase.Name;
					className = CallingBase.DeclaringType.ToString();
				}
				string message = "TRACEIN Entering Function [" + functionName + "] in the class [" + className + "]";
				if (parameters.Length > 0)
				{
					message = message + " With parameter values ";
					foreach (object o in parameters)
					{
						message = message + " [" + o.GetType().ToString() + " : " + LoggingConvert(o) + "]";
					}
				}

				this._log4netLogger.Debug(message);
			}
			catch (SYSTEM.Exception ex)
			{
				this._log4netLogger.Warn("The following internal error occured during the TraceIn Audit Call: " + ex.Message, ex);
			}
		}

		public void TraceOut()
		{
			if (AuditEnabled == false)
				return;

			try
			{
				string functionName = UNKNOWN;
				string className = UNKNOWN;
				MethodBase CallingBase = GetCallingMethodBase();
				if (CallingBase != null)
				{
					functionName = CallingBase.Name;
					className = CallingBase.DeclaringType.ToString();
				}
				this._log4netLogger.Debug("TRACEOUT Leaving Function [" + functionName + "]");

			}
			catch (SYSTEM.Exception ex)
			{
				this._log4netLogger.Warn("The following internal error occured during the TraceOut Audit Call: " + ex.Message, ex);
			}
		}

		/// <summary>
		/// Use refelection to retrieve stack information for calling function. 
		/// </summary>
		/// <returns></returns>
		private MethodBase GetCallingMethodBase()
		{
			MethodBase CallingBase = null;
			StackTrace StackTrace = new StackTrace(true);
			for (int FrameIndex = 0; FrameIndex < StackTrace.FrameCount; FrameIndex++)
			{
				StackFrame StackFrame = StackTrace.GetFrame(FrameIndex);
				MethodBase Method = StackFrame.GetMethod();
				if (Method != null)
				{
					if (FrameIndex + 2 < StackTrace.FrameCount) //Check we have prev entries on stack. We use 2 because we are 2 calls from the original caller. 
					{
						StackFrame prevFrame = StackTrace.GetFrame(FrameIndex + 2);
						CallingBase = prevFrame.GetMethod();
						break;
					}
				}
			}
			return CallingBase;
		}

		public IStopWatch CreateStopWatch()
		{
			IStopWatch result;

			if (this._log4netLogger.IsDebugEnabled)
			{
				result = new StopWatch(this);
				result.Start(); // Integration Testing showed that many callers don't Start the StopWatch so we decided to autostart it
			}
			else
				result = Logger._dummyStopWatch;

			return result;
		}

		private string LoggingConvert(object o)
		{
			if (o is ICollection)
			{
				return "Size = " + ((ICollection)o).Count.ToString();
			}

			return Convert.ToString(o);
		}

	}
}
