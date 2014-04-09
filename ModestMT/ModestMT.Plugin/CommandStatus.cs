using System;
using System.Collections.Generic;
using System.Text;

namespace ModestMT.Plugin
{
	public enum CommandStatus
	{
		Unsupported = 0,
		Supported = 1,
		Enabled = 2,
		Latched = 4,
		Ninched = 8,
		Invisible = 16,
	}
}
