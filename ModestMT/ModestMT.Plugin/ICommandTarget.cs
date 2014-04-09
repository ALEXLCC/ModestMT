using System;
using System.Collections.Generic;
using System.Text;

namespace ModestMT.Plugin
{
	public interface ICommandTarget
	{
		string Name { get; }

		void Execute(string cmdName, ref bool handled);

		void QueryStatus(string cmdName, ref CommandStatus StatusOption);
	}
}
