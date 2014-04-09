namespace ModestMT.WinForm.ImagePicker.Plugin.Products.AliDownloader
{
	partial class AliDownloader
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
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.dgMain = new System.Windows.Forms.DataGridView();
			this.btnNew = new System.Windows.Forms.ToolStripButton();
			this.btnStart = new System.Windows.Forms.ToolStripButton();
			this.btnCancel = new System.Windows.Forms.ToolStripButton();
			this.btnOpen = new System.Windows.Forms.ToolStripButton();
			this.tbUrl = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.tbStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
			this.tableLayoutPanel1.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgMain)).BeginInit();
			this.SuspendLayout();
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
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.Size = new System.Drawing.Size(822, 608);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// toolStrip1
			// 
			this.toolStrip1.BackColor = System.Drawing.SystemColors.ControlLight;
			this.toolStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
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
			this.toolStrip1.Size = new System.Drawing.Size(822, 26);
			this.toolStrip1.TabIndex = 0;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 26);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 26);
			// 
			// dgMain
			// 
			this.dgMain.AllowUserToAddRows = false;
			this.dgMain.AllowUserToDeleteRows = false;
			this.dgMain.AllowUserToOrderColumns = true;
			this.dgMain.AllowUserToResizeRows = false;
			this.dgMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgMain.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.tbUrl,
            this.tbStatus});
			this.dgMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dgMain.Location = new System.Drawing.Point(3, 29);
			this.dgMain.Name = "dgMain";
			this.dgMain.ReadOnly = true;
			this.dgMain.RowHeadersVisible = false;
			this.dgMain.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dgMain.Size = new System.Drawing.Size(816, 576);
			this.dgMain.TabIndex = 1;
			// 
			// btnNew
			// 
			this.btnNew.Image = global::ModestMT.WinForm.ImagePicker.Properties.Resources._new;
			this.btnNew.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnNew.Name = "btnNew";
			this.btnNew.Size = new System.Drawing.Size(51, 23);
			this.btnNew.Text = "新建";
			this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
			// 
			// btnStart
			// 
			this.btnStart.Image = global::ModestMT.WinForm.ImagePicker.Properties.Resources.start;
			this.btnStart.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnStart.Name = "btnStart";
			this.btnStart.Size = new System.Drawing.Size(51, 23);
			this.btnStart.Text = "开始";
			this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Image = global::ModestMT.WinForm.ImagePicker.Properties.Resources.stop;
			this.btnCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(51, 23);
			this.btnCancel.Text = "取消";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnOpen
			// 
			this.btnOpen.Image = global::ModestMT.WinForm.ImagePicker.Properties.Resources.openfolder;
			this.btnOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnOpen.Name = "btnOpen";
			this.btnOpen.Size = new System.Drawing.Size(75, 23);
			this.btnOpen.Text = "打开目录";
			this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
			// 
			// tbUrl
			// 
			this.tbUrl.DataPropertyName = "Url";
			this.tbUrl.HeaderText = "地址";
			this.tbUrl.Name = "tbUrl";
			this.tbUrl.ReadOnly = true;
			this.tbUrl.Width = 700;
			// 
			// tbStatus
			// 
			this.tbStatus.DataPropertyName = "StatusString";
			this.tbStatus.HeaderText = "状态";
			this.tbStatus.Name = "tbStatus";
			this.tbStatus.ReadOnly = true;
			// 
			// toolStripButton1
			// 
			this.toolStripButton1.Image = global::ModestMT.WinForm.ImagePicker.Properties.Resources.cancel;
			this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButton1.Name = "toolStripButton1";
			this.toolStripButton1.Size = new System.Drawing.Size(51, 23);
			this.toolStripButton1.Text = "删除";
			this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
			// 
			// AliDownloader
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tableLayoutPanel1);
			this.Name = "AliDownloader";
			this.Size = new System.Drawing.Size(822, 608);
			this.Load += new System.EventHandler(this.AliDownloader_Load);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgMain)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton btnStart;
		private System.Windows.Forms.ToolStripButton btnNew;
		private System.Windows.Forms.ToolStripButton btnCancel;
		private System.Windows.Forms.ToolStripButton btnOpen;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.DataGridView dgMain;
		private System.Windows.Forms.DataGridViewTextBoxColumn tbUrl;
		private System.Windows.Forms.DataGridViewTextBoxColumn tbStatus;
		private System.Windows.Forms.ToolStripButton toolStripButton1;

	}
}
