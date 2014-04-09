using S = System;

namespace ModestMT.Core.Exception
{
	public class BasicException : S.Exception
	{
		public BasicException(string msg, S.Exception ex)
			: base(msg, ex)
		{
		}

		public BasicException(string msg)
			: base(msg)
		{
		}
	}
}
