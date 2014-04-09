using System;
using System.Collections.Generic;
using System.Text;

namespace ModestMT.AccumulateTreasure.Utility
{
	public class AccumulateTreasureException : Exception
	{
		public AccumulateTreasureException(string msg, Exception ex)
			: base(msg, ex)
		{
		}

		public AccumulateTreasureException(string msg)
			: base(msg)
		{
		}
	}
}
