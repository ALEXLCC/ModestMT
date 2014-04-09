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
	public partial class AliStoreDownloader : UserControl, IStoreResourceManager
	{
		private static ResourceManager RESOURCE_MANAGER = new ResourceManager();

		public AliStoreDownloader()
		{
			InitializeComponent();
		}

		public BindingList<WebSiteInfo> StoreWebSiteInfoList { get { return RESOURCE_MANAGER.StoreWebSiteInfoList; } }

		private void AliDownloader_Load(object sender, EventArgs e)
		{
			this.dgMain.AutoGenerateColumns = false;
			this.dgMain.DataSource = RESOURCE_MANAGER.StoreWebSiteInfoList;
		}

		private void btnNew_Click(object sender, EventArgs e)
		{
			AddUrlListForm form = new AddUrlListForm(this);
			form.ShowDialog();
		}

		private void btnStart_Click(object sender, EventArgs e)
		{
			IList<WebSiteInfo> downloadList = GetDownLoadList();
			AliStoreDownLoader downLoader = new AliStoreDownLoader();
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

		private IList<WebSiteInfo> GetDeleteList()
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
				if (webSiteInfo.Status == StatusCode.Finish)
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

		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			IList<WebSiteInfo> selectedList = GetDeleteList();
			if (selectedList.Count > 0)
			{
				if (MessageBox.Show(this, "你确定要删除这些任务么？", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
				{
					foreach (WebSiteInfo webSiteInfo in selectedList)
					{
						AbstractDownLoader.StopTask(webSiteInfo);
						RESOURCE_MANAGER.StoreWebSiteInfoList.Remove(webSiteInfo);
					}
				}
			}
		}

		private void btnOpen_Click(object sender, EventArgs e)
		{
			IList<WebSiteInfo> selectedList = GetCanOpenedList();
			string[] directories = Directory.GetDirectories(AbstractDownLoader.PRODUCTS_FOLDER);
			foreach (WebSiteInfo webSiteInfo in selectedList)
			{
				string directory = Exists(directories, webSiteInfo.GetHashCode().ToString().Replace('-', '_'));
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

		private string Exists(string[] directories, string name)
		{
			foreach (string directory in directories)
			{
				string directoryName = Path.GetFileName(directory);
				if (directoryName.Substring(2, directoryName.Length - 2) == name)
				{
					return directory;
				}
			}
			return null;
		}
	}
}
