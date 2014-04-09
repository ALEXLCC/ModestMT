using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using ModestMT.WinForm.ImagePicker.Plugin.Products.AliDownloader.Bll;
using ModestMT.WinForm.ImagePicker.Utility;
using System.IO;
using System.Diagnostics;
using ModestMT.Core.Collection;

namespace ModestMT.WinForm.ImagePicker.Plugin.Products.AliDownloader
{
	public delegate void UpdateDataSourceDelegate();

	public partial class AliDownloader : UserControl, IResourceManager
	{
		private static ResourceManager RESOURCE_MANAGER = new ResourceManager();

		public AliDownloader()
		{
			InitializeComponent();
		}

		public ObservableList<WebSiteInfo> WebSiteInfoList { get { return RESOURCE_MANAGER.WebSiteInfoList; } }

		private void AliDownloader_Load(object sender, EventArgs e)
		{
			RESOURCE_MANAGER.RegistePropertyChangedEvert(new PropertyChangedEventHandler(RESOURCE_MANAGER_PropertyChanged));
			this.dgMain.AutoGenerateColumns = false;
			this.dgMain.DataSource = RESOURCE_MANAGER.WebSiteInfoList.GetList();
		}

		void RESOURCE_MANAGER_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			this.dgMain.Invoke(new UpdateDataSourceDelegate(() =>
			{
				this.dgMain.DataSource = null;
				this.dgMain.DataSource = RESOURCE_MANAGER.WebSiteInfoList.GetList();
			}));

		}

		private void btnNew_Click(object sender, EventArgs e)
		{
			AddUrlListForm form = new AddUrlListForm(this);
			form.ShowDialog();
		}

		private void btnStart_Click(object sender, EventArgs e)
		{
			ObservableList<WebSiteInfo> downloadList = GetDownLoadList();
			AliDownLoader downLoader = new AliDownLoader(downloadList);
			downLoader.DownLoad();
		}

		private ObservableList<WebSiteInfo> GetDownLoadList()
		{
			ObservableList<WebSiteInfo> selectedList = new ObservableList<WebSiteInfo>();
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

		private ObservableList<WebSiteInfo> GetStopList()
		{
			ObservableList<WebSiteInfo> selectedList = new ObservableList<WebSiteInfo>();
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

		private List<WebSiteInfo> GetDeleteList()
		{
			List<WebSiteInfo> selectedList = new List<WebSiteInfo>();
			foreach (DataGridViewRow row in this.dgMain.SelectedRows)
			{
				WebSiteInfo webSiteInfo = (WebSiteInfo)row.DataBoundItem;
				selectedList.Add(webSiteInfo);
			}
			return selectedList;
		}


		private ObservableList<WebSiteInfo> GetCanOpenedList()
		{
			ObservableList<WebSiteInfo> selectedList = new ObservableList<WebSiteInfo>();
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
			ObservableList<WebSiteInfo> selectedList = GetStopList();
			foreach (WebSiteInfo webSiteInfo in selectedList)
			{
				AbstractDownLoader.StopTask(webSiteInfo);
				webSiteInfo.Status = StatusCode.Wait;
			}
		}

		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show(this, "你确定要删除这些任务么？", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
			{
				foreach (WebSiteInfo webSiteInfo in GetDeleteList())
				{
					AbstractDownLoader.StopTask(webSiteInfo);
					RESOURCE_MANAGER.WebSiteInfoList.Remove(webSiteInfo);
				}
			}
		}

		private void btnOpen_Click(object sender, EventArgs e)
		{
			ObservableList<WebSiteInfo> selectedList = GetCanOpenedList();
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
