using System;
using System.Reflection;

namespace ModestMT.Core.System
{
	/// <summary>
	/// Light weight directory handling utils
	/// </summary>
	public sealed class DirUtils
	{
		/// <summary>
		/// Return the working directory for the calling application. 
		/// </summary>
		/// <returns></returns>
		public static string WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory;
	}
}
