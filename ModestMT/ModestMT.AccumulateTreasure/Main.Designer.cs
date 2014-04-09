namespace ModestMT.AccumulateTreasure
{
	partial class Main
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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
			this.menu = new System.Windows.Forms.ToolStrip();
			this.content = new System.Windows.Forms.Panel();
			this.btnSetting = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.SuspendLayout();
			// 
			// menu
			// 
			this.menu.Location = new System.Drawing.Point(0, 0);
			this.menu.Name = "menu";
			this.menu.Size = new System.Drawing.Size(1009, 25);
			this.menu.TabIndex = 0;
			this.menu.Text = "Menu";
			// 
			// content
			// 
			this.content.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.content.Dock = System.Windows.Forms.DockStyle.Fill;
			this.content.Location = new System.Drawing.Point(0, 25);
			this.content.Name = "content";
			this.content.Size = new System.Drawing.Size(1009, 632);
			this.content.TabIndex = 2;
			// 
			// btnSetting
			// 
			this.btnSetting.Name = "btnSetting";
			this.btnSetting.Size = new System.Drawing.Size(23, 23);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 6);
			// 
			// Main
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1009, 657);
			this.Controls.Add(this.content);
			this.Controls.Add(this.menu);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimumSize = new System.Drawing.Size(800, 600);
			this.Name = "Main";
			this.Text = "聚财宝";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
			this.Load += new System.EventHandler(this.Main_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStrip menu;
		private System.Windows.Forms.Panel content;
	}
}

