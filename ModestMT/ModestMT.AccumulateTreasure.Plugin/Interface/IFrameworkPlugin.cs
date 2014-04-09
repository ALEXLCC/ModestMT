using ModestMT.Plugin;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace ModestMT.AccumulateTreasure.Plugin.Interface
{
	public interface IFrameworkPlugin : IPlugin, ICommandTarget
	{
		Control GetSetting();
	}
}
