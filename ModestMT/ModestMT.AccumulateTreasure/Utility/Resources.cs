using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ModestMT.AccumulateTreasure.Utility
{
	internal class Resources
	{
		internal static Bitmap GetImage(string name)
		{
			return (System.Drawing.Bitmap)ModestMT.AccumulateTreasure.Properties.Resources.ResourceManager.GetObject(name);
		}
	}
}
