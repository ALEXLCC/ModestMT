using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;

namespace ModestMT.Core.WindowsAPI
{
	/// <summary>
	/// API about windows form
	/// </summary>
	public partial class WindowsApi
	{
		[StructLayout(LayoutKind.Sequential)]
		public struct POINT
		{
			public int X;
			public int Y;

			public POINT(int x, int y)
			{
				this.X = x;
				this.Y = y;
			}
			public POINT(Point pt)
			{
				X = Convert.ToInt32(pt.X);
				Y = Convert.ToInt32(pt.Y);
			}
		};

		[DllImport("user32.dll")]
		public static extern bool ClientToScreen(IntPtr hWnd, ref POINT lpPoint);

		[DllImport("user32.dll")]
		public static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

		[DllImport("user32.dll")]
		private static extern bool SetForegroundWindow(IntPtr hWnd);
		[DllImport("user32.dll")]
		private static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);
		[DllImport("user32.dll")]
		private static extern bool IsIconic(IntPtr hWnd);

		/// <summary>
		/// Raise one proccess's main window foreground.
		/// </summary>
		/// <param name="proc"></param>
		public static void RaiseOtherProcess(System.Diagnostics.Process proc)
		{
			IntPtr hWnd = proc.MainWindowHandle;
			if (IsIconic(hWnd))
			{
				ShowWindowAsync(hWnd, 9);
			}
			SetForegroundWindow(hWnd);
		}
	}
}
