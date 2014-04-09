using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using ModestMT.AliDownloader.Bll;
using ModestMT.Core.Collection;
using ModestMT.AliDownloader.Model;

namespace ModestMT.AliDownloader
{
	public partial class AliProductDownloader : UserControl, IProductResourceManager
	{
		private static ResourceManager RESOURCE_MANAGER = new ResourceManager();

		public AliProductDownloader()
		{
			InitializeComponent();
		}

		public BindingList<WebSiteInfo> ProductWebSiteInfoList { get { return RESOURCE_MANAGER.ProductWebSiteInfoList; } }

		private void AliDownloader_Load(object sender, EventArgs e)
		{
			this.dgMain.AutoGenerateColumns = false;
			this.dgMain.DataSource = RESOURCE_MANAGER.ProductWebSiteInfoList;
		}

		private void btnNew_Click(object sender, EventArgs e)
		{
			AddUrlListForm form = new AddUrlListForm(this);
			form.ShowDialog();
			if (form.ErrorUrlList.Count > 0)
			{
				StringBuilder sb = new StringBuilder("您添加的下载列表中有非法链接(不可浏览的链接，非阿里巴巴产品链接，或者已经在下载列表中):\n");
				foreach (string url in form.ErrorUrlList)
				{
					sb.Append(url).Append("\n");
				}
				sb.Append("程序已经为您自动去除。");
				MessageBox.Show(this, sb.ToString(), "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

		private void btnStart_Click(object sender, EventArgs e)
		{
			IList<WebSiteInfo> downloadList = GetDownLoadList();
			AliProductDownLoader downLoader = new AliProductDownLoader();
			downLoader.DownLoad(downloadList);
		}

		private IList<WebSiteInfo> GetDownLoadList()
		{
			IList<WebSiteInfo> selectedList = new List<WebSiteInfo>();
			foreach (DataGridViewRow row in this.dgMain.SelectedRows)
			{
				WebSiteInfo webSiteInfo = (WebSiteInfo)row.DataBoundItem;
				// Only the wait task will be added to download list
				if (webSiteInfo.Status == StatusCode.Wait || webSiteInfo.Status == StatusCode.Failed)
				{
					selectedList.Add(webSiteInfo);
				}
			}
			return selectedList;
		}

		private IList<WebSiteInfo> GetStopList()
		{
			IList<WebSiteInfo> selectedList = new List<WebSiteInfo>();
			foreach (DataGridViewRow row in this.dgMain.SelectedRows)
			{
				WebSiteInfo webSiteInfo = (WebSiteInfo)row.DataBoundItem;
				if (webSiteInfo.Status == StatusCode.Process)
				{
					selectedList.Add(webSiteInfo);
				}
			}
			return selectedList;
		}

		private IList<WebSiteInfo> GetSelectedList()
		{
			IList<WebSiteInfo> selectedList = new List<WebSiteInfo>();
			foreach (DataGridViewRow row in this.dgMain.SelectedRows)
			{
				WebSiteInfo webSiteInfo = (WebSiteInfo)row.DataBoundItem;
				selectedList.Add(webSiteInfo);
			}
			return selectedList;
		}


		private IList<WebSiteInfo> GetCanOpenedList()
		{
			IList<WebSiteInfo> selectedList = new List<WebSiteInfo>();
			foreach (DataGridViewRow row in this.dgMain.SelectedRows)
			{
				WebSiteInfo webSiteInfo = (WebSiteInfo)row.DataBoundItem;
				// Only the wait task will be added to download list
				if (webSiteInfo.Status == StatusCode.Finish || webSiteInfo.Status == StatusCode.Process)
				{
					selectedList.Add(webSiteInfo);
				}
			}
			return selectedList;
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			IList<WebSiteInfo> selectedList = GetStopList();
			foreach (WebSiteInfo webSiteInfo in selectedList)
			{
				AbstractDownLoader.StopTask(webSiteInfo);
				webSiteInfo.Status = StatusCode.Wait;
			}
		}

		private void btnDelete_Click(object sender, EventArgs e)
		{
			IList<WebSiteInfo> selectedList = GetSelectedList();
			if (selectedList.Count > 0)
			{
				if (MessageBox.Show(this, "你确定要删除这些任务么？", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
				{
					foreach (WebSiteInfo webSiteInfo in selectedList)
					{
						AbstractDownLoader.StopTask(webSiteInfo);
						RESOURCE_MANAGER.ProductWebSiteInfoList.Remove(webSiteInfo);
					}
				}
			}
		}

		private void btnOpen_Click(object sender, EventArgs e)
		{
			IList<WebSiteInfo> selectedList = GetCanOpenedList();
			string[] directories = Directory.GetDirectories(Arguments.PRODUCTS_FOLDER);
			foreach (WebSiteInfo webSiteInfo in selectedList)
			{
				string directory = AliProductDownLoader.ExistedFolder(string.Format("[{0}]", webSiteInfo.GetHashCode().ToString().Replace('-', '_')), directories);
				if (directory != null)
				{
					Process.Start(directory);
				}
				else
				{
					// Do something
				}
			}
		}

		private void reDownload_Click(object sender, EventArgs e)
		{
			IList<WebSiteInfo> selectedList = GetSelectedList();
			AliProductDownLoader downLoader = new AliProductDownLoader();
			downLoader.DownLoad(selectedList);
		}

		private void contextMenuStrip_Opening(object sender, CancelEventArgs e)
		{
			IList<WebSiteInfo> selectedList = GetSelectedList();
			bool flag = true;
			foreach (WebSiteInfo webSiteInfo in selectedList)
			{
				if (webSiteInfo.Status == StatusCode.Process)
				{
					flag = false;
					break;
				}
			}

			this.reDownload.Enabled = flag;

			// 设置: 复制链接 的状态
			this.copyUrl.Enabled = selectedList.Count == 1;
		}

		private void openDownloadFolder_Click(object sender, EventArgs e)
		{
			btnOpen_Click(null, EventArgs.Empty);
		}

		private void copyUrl_Click(object sender, EventArgs e)
		{
			Clipboard.SetText(GetSelectedList()[0].Url);
		}
	}
}
