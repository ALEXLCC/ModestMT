using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using ModestMT.AliDownloader.Bll;
using ModestMT.AliDownloader.Utility;

namespace ModestMT.AliDownloader
{
	public partial class Setting : UserControl
	{
		private FolderBrowserDialog folderBrowser;

		public Setting()
		{
			InitializeComponent();
		}

		private void btnView_Click(object sender, EventArgs e)
		{
			folderBrowser = new FolderBrowserDialog();
			folderBrowser.ShowNewFolderButton = true;
			if (DialogResult.OK == folderBrowser.ShowDialog(this))
			{
				this.tbDownloadFolder.Text = folderBrowser.SelectedPath;
			}
		}

		private void Setting_Load(object sender, EventArgs e)
		{
			this.tbDownloadFolder.Enabled = false;
			this.tbDownloadFolder.Text = ConfigManager.DOWNLOAD_FOLDER;
		}

		public void Save()
		{
			ConfigManager.DOWNLOAD_FOLDER = this.tbDownloadFolder.Text;
			Arguments.RefreshArguments();
			AliProductDownloaderPlugin.VerifyDirectory();
		}
	}
}
