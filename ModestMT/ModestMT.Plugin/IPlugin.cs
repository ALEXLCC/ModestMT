using System;
using System.Collections.Generic;
using System.Text;

namespace ModestMT.Plugin
{
	public interface IPlugin
	{
		void OnConnection(object application);

		void OnDisconnection();
	}
}
