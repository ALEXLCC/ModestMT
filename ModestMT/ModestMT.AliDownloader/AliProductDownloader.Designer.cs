namespace ModestMT.AliDownloader
{
	partial class AliProductDownloader
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.reDownload = new System.Windows.Forms.ToolStripMenuItem();
			this.openDownloadFolder = new System.Windows.Forms.ToolStripMenuItem();
			this.copyUrl = new System.Windows.Forms.ToolStripMenuItem();
			this.tbStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Title = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.tbUrl = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dgMain = new System.Windows.Forms.DataGridView();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.btnNew = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.btnStart = new System.Windows.Forms.ToolStripButton();
			this.btnCancel = new System.Windows.Forms.ToolStripButton();
			this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.btnOpen = new System.Windows.Forms.ToolStripButton();
			this.contextMenuStrip.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgMain)).BeginInit();
			this.tableLayoutPanel1.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// contextMenuStrip
			// 
			this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.reDownload,
            this.openDownloadFolder,
            this.copyUrl});
			this.contextMenuStrip.Name = "contextMenuStrip";
			this.contextMenuStrip.Size = new System.Drawing.Size(125, 70);
			this.contextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip_Opening);
			// 
			// reDownload
			// 
			this.reDownload.Name = "reDownload";
			this.reDownload.Size = new System.Drawing.Size(124, 22);
			this.reDownload.Text = "重新下载";
			this.reDownload.Click += new System.EventHandler(this.reDownload_Click);
			// 
			// openDownloadFolder
			// 
			this.openDownloadFolder.Name = "openDownloadFolder";
			this.openDownloadFolder.Size = new System.Drawing.Size(124, 22);
			this.openDownloadFolder.Text = "打开目录";
			this.openDownloadFolder.Click += new System.EventHandler(this.openDownloadFolder_Click);
			// 
			// copyUrl
			// 
			this.copyUrl.Name = "copyUrl";
			this.copyUrl.Size = new System.Drawing.Size(124, 22);
			this.copyUrl.Text = "复制链接";
			this.copyUrl.Click += new System.EventHandler(this.copyUrl_Click);
			// 
			// tbStatus
			// 
			this.tbStatus.DataPropertyName = "StatusString";
			this.tbStatus.HeaderText = "状态";
			this.tbStatus.Name = "tbStatus";
			this.tbStatus.ReadOnly = true;
			this.tbStatus.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			// 
			// Title
			// 
			this.Title.DataPropertyName = "Title";
			this.Title.HeaderText = "标题";
			this.Title.Name = "Title";
			this.Title.ReadOnly = true;
			this.Title.Width = 300;
			// 
			// tbUrl
			// 
			this.tbUrl.DataPropertyName = "Url";
			this.tbUrl.HeaderText = "地址";
			this.tbUrl.Name = "tbUrl";
			this.tbUrl.ReadOnly = true;
			this.tbUrl.Width = 350;
			// 
			// dgMain
			// 
			this.dgMain.AllowUserToAddRows = false;
			this.dgMain.AllowUserToDeleteRows = false;
			this.dgMain.AllowUserToOrderColumns = true;
			this.dgMain.AllowUserToResizeRows = false;
			this.dgMain.BackgroundColor = System.Drawing.SystemColors.ControlDark;
			this.dgMain.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.dgMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgMain.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.tbUrl,
            this.Title,
            this.tbStatus});
			this.dgMain.ContextMenuStrip = this.contextMenuStrip;
			this.dgMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dgMain.GridColor = System.Drawing.SystemColors.Control;
			this.dgMain.Location = new System.Drawing.Point(0, 25);
			this.dgMain.Margin = new System.Windows.Forms.Padding(0);
			this.dgMain.Name = "dgMain";
			this.dgMain.ReadOnly = true;
			this.dgMain.RowHeadersVisible = false;
			this.dgMain.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dgMain.Size = new System.Drawing.Size(822, 583);
			this.dgMain.TabIndex = 1;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.toolStrip1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.dgMain, 0, 1);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.Size = new System.Drawing.Size(822, 608);
			this.tableLayoutPanel1.TabIndex = 3;
			// 
			// toolStrip1
			// 
			this.toolStrip1.BackColor = System.Drawing.SystemColors.ControlLight;
			this.toolStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.toolStripSeparator3,
            this.btnNew,
            this.toolStripSeparator2,
            this.btnStart,
            this.btnCancel,
            this.toolStripButton1,
            this.toolStripSeparator1,
            this.btnOpen});
			this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(822, 25);
			this.toolStrip1.TabIndex = 3;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// toolStripLabel1
			// 
			this.toolStripLabel1.Image = global::ModestMT.AliDownloader.Properties.Resources._1;
			this.toolStripLabel1.Name = "toolStripLabel1";
			this.toolStripLabel1.Size = new System.Drawing.Size(72, 22);
			this.toolStripLabel1.Text = "产品下载";
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
			// 
			// btnNew
			// 
			this.btnNew.Image = global::ModestMT.AliDownloader.Properties.Resources._new;
			this.btnNew.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnNew.Name = "btnNew";
			this.btnNew.Size = new System.Drawing.Size(52, 22);
			this.btnNew.Text = "新建";
			this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
			// 
			// btnStart
			// 
			this.btnStart.Image = global::ModestMT.AliDownloader.Properties.Resources.start;
			this.btnStart.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnStart.Name = "btnStart";
			this.btnStart.Size = new System.Drawing.Size(52, 22);
			this.btnStart.Text = "开始";
			this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Image = global::ModestMT.AliDownloader.Properties.Resources.stop;
			this.btnCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(52, 22);
			this.btnCancel.Text = "取消";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// toolStripButton1
			// 
			this.toolStripButton1.Image = global::ModestMT.AliDownloader.Properties.Resources.cancel;
			this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButton1.Name = "toolStripButton1";
			this.toolStripButton1.Size = new System.Drawing.Size(52, 22);
			this.toolStripButton1.Text = "删除";
			this.toolStripButton1.Click += new System.EventHandler(this.btnDelete_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// btnOpen
			// 
			this.btnOpen.Image = global::ModestMT.AliDownloader.Properties.Resources.openfolder;
			this.btnOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnOpen.Name = "btnOpen";
			this.btnOpen.Size = new System.Drawing.Size(76, 22);
			this.btnOpen.Text = "打开目录";
			this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
			// 
			// AliProductDownloader
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.Controls.Add(this.tableLayoutPanel1);
			this.Name = "AliProductDownloader";
			this.Size = new System.Drawing.Size(822, 608);
			this.Load += new System.EventHandler(this.AliDownloader_Load);
			this.contextMenuStrip.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dgMain)).EndInit();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem reDownload;
		private System.Windows.Forms.ToolStripMenuItem openDownloadFolder;
		private System.Windows.Forms.ToolStripMenuItem copyUrl;
		private System.Windows.Forms.DataGridViewTextBoxColumn tbStatus;
		private System.Windows.Forms.DataGridViewTextBoxColumn Title;
		private System.Windows.Forms.DataGridViewTextBoxColumn tbUrl;
		private System.Windows.Forms.DataGridView dgMain;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripLabel toolStripLabel1;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripButton btnNew;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripButton btnStart;
		private System.Windows.Forms.ToolStripButton btnCancel;
		private System.Windows.Forms.ToolStripButton toolStripButton1;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripButton btnOpen;
	}
}
