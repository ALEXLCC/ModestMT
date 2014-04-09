using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ModestMT.Core.Web
{
	public class HtmlUtitly
	{
		public static string ExecRepaceHTML(string html)
		{
			html = Regex.Replace(html, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
			html = Regex.Replace(html, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
			html = Regex.Replace(html, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
			html = Regex.Replace(html, @"-->", "", RegexOptions.IgnoreCase);
			html = Regex.Replace(html, @"<!--.*\n", "", RegexOptions.IgnoreCase);
			html = Regex.Replace(html, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
			html = Regex.Replace(html, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
			html = Regex.Replace(html, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
			html = Regex.Replace(html, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
			html = Regex.Replace(html, @"&(nbsp|#160);", " ", RegexOptions.IgnoreCase);
			html = Regex.Replace(html, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
			html = Regex.Replace(html, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
			html = Regex.Replace(html, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
			html = Regex.Replace(html, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);
			html = Regex.Replace(html, @"&#(\d+);", "", RegexOptions.IgnoreCase);
			html = Regex.Replace(html, @"\s", ""); html.Replace("<", ""); html.Replace(">", "");
			html.Replace("\r\n", "");
			return html;
		}
	}
}
