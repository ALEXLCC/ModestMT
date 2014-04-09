using ModestMT.AliDownloader.Bll;
using ModestMT.AliDownloader.Model;
using ModestMT.Core.Collection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ModestMT.AliDownloader
{
	public partial class AddUrlListForm : Form
	{
		private static string preAlibaba = "http://detail.1688.com/offer";

		private IProductResourceManager productResourceManager;
		//private IStoreResourceManager storeResourceManager;

		Regex urlRegex = new Regex(@"http://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?");

		public AddUrlListForm()
		{
			InitializeComponent();
			ErrorUrlList = new List<string>();
		}

		public AddUrlListForm(IProductResourceManager resourceManager)
			: this()
		{
			this.productResourceManager = resourceManager;
		}

		public List<string> ErrorUrlList { get; set; }

		//public AddUrlListForm(IStoreResourceManager resourceManager)
		//	: this()
		//{
		//	this.storeResourceManager = resourceManager;
		//}

		private void btnAdd_Click(object sender, EventArgs e)
		{
			string[] webSites = this.tbUrls.Text.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

			BindingList<WebSiteInfo> webSiteInfo = productResourceManager.ProductWebSiteInfoList; //== null ? storeResourceManager.StoreWebSiteInfoList : productResourceManager.ProductWebSiteInfoList;

			foreach (string url in webSites)
			{
				if (urlRegex.IsMatch(url) == true)
				{
					bool exist = false;
					//if (productResourceManager == null)
					//{
					//	exist = ResourceManager.ContainsStore(url);
					//}
					//else
					//{
					exist = ResourceManager.ContainsProduct(url);
					//}

					// 仅限阿里巴巴产品
					if (url.StartsWith(preAlibaba) == false)
					{
						ErrorUrlList.Add(url);
						continue;
					}
					if (exist == false)
					{
						webSiteInfo.Add(new WebSiteInfo() { Url = GetUrl(url), Status = StatusCode.Wait });
					}
					else
					{
						ErrorUrlList.Add(url);
					}
				}
				else
				{
					ErrorUrlList.Add(url);
				}
			}

			this.Close();
		}

		private void AddUrlListForm_Load(object sender, EventArgs e)
		{
			string str = Clipboard.GetText();
			string[] webSites = str.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

			IList<string> list = new List<string>();

			StringBuilder sb = new StringBuilder();

			foreach (string url in webSites)
			{
				if (urlRegex.IsMatch(url) == true)
				{
					if (url.StartsWith(preAlibaba) == false)
					{
						continue;
					}
					string realUrl = GetUrl(url);

					if (list.Contains(realUrl) == false)
					{
						sb.Append(realUrl).Append(Environment.NewLine);
					}

					list.Add(realUrl);
				}
			}

			this.tbUrls.Text = sb.ToString();
		}

		private string GetUrl(string url)
		{
			int index = url.IndexOf(".html");
			return url.Substring(0, index + 5);
		}
	}
}
