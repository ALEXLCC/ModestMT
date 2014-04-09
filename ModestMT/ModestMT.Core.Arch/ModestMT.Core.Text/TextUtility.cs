using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ModestMT.Core.Text
{
	public class TextUtility
	{
		public static string RemoveSpecialChar(string str)
		{
			string reg = @"\:" + @"|\;" + @"|\/" + @"|\\" + @"|\|" + @"|\," + @"|\*" + @"|\?" + @"|\""" + @"|\<" + @"|\>";
			Regex regex = new Regex(reg);
			return regex.Replace(str, "_");
		}
	}
}
