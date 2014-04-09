using ModestMT.Core.Collection;
using ModestMT.WinForm.ImagePicker.Plugin.Products.AliDownloader.Bll;
using ModestMT.WinForm.ImagePicker.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ModestMT.WinForm.ImagePicker.Plugin.Products.AliDownloader
{
	public partial class AddUrlListForm : Form
	{
		private IResourceManager resourceManager;

		Regex urlRegex = new Regex(@"http://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?");

		public AddUrlListForm()
		{
			InitializeComponent();
		}

		public AddUrlListForm(IResourceManager resourceManager)
			: this()
		{
			this.resourceManager = resourceManager;
		}

		private void btnAdd_Click(object sender, EventArgs e)
		{
			string[] webSites = this.tbUrls.Text.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

			ObservableList<WebSiteInfo> webSiteInfo = resourceManager.WebSiteInfoList;

			List<string> errorUrl = new List<string>();

			foreach (string url in webSites)
			{
				if (urlRegex.IsMatch(url) == true)
				{
					if (ResourceManager.Contains(url) == false)
					{
						webSiteInfo.Add(new WebSiteInfo() { Url = url, Status = StatusCode.Wait });
					}
				}
				else
				{
					errorUrl.Add(url);
				}
			}

			this.Close();
		}
	}
}
