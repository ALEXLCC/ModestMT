using ModestMT.AccumulateTreasure.Plugin.Interface;
using ModestMT.Plugin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;

namespace ModestMT.AccumulateTreasure
{
	public partial class Setting : Form
	{
		private List<IFrameworkPlugin> plugins;

		public Setting(List<IFrameworkPlugin> plugins)
		{
			InitializeComponent();
			this.plugins = plugins;

			foreach (IFrameworkPlugin plugin in plugins)
			{
				this.lbPlugins.Items.Add(plugin.Name);
			}

			if (this.lbPlugins.Items.Count > 0)
			{
				this.lbPlugins.SelectedIndex = 0;
			}
		}

		private void lbPlugins_DrawItem(object sender, DrawItemEventArgs e)
		{
			//e.DrawBackground();
			bool selected = ((e.State & DrawItemState.Selected) == DrawItemState.Selected);
			Brush brush = null;
			if (!selected)
			{
				brush = new SolidBrush(lbPlugins.BackColor);
			}
			else
			{
				brush = Brushes.LightGray;
			}
			e.Graphics.FillRectangle(brush, e.Bounds);
			e.Graphics.DrawRectangle(SystemPens.WindowText, e.Bounds);
			Rectangle r2 = e.Bounds;
			string displayText = (string)lbPlugins.Items[e.Index];
			SizeF size = e.Graphics.MeasureString(displayText, this.Font);
			r2.Y = (int)(r2.Height / 2) - (int)(size.Height / 2) + e.Bounds.Y;
			r2.X = 0;
			e.Graphics.DrawString(displayText, this.Font, Brushes.Black, r2);
			e.DrawFocusRectangle();
		}

		private void lbPlugins_MeasureItem(object sender, MeasureItemEventArgs e)
		{
			if (e.Index >= 0 && e.Index < this.lbPlugins.Items.Count)
			{
				e.ItemHeight = 24;
			}
		}

		private void lbPlugins_SelectedIndexChanged(object sender, EventArgs e)
		{
			IFrameworkPlugin plugin = GetPluginByName(lbPlugins.SelectedItem.ToString());
			this.settingPanel.Controls.Clear();
			Control setting = plugin.GetSetting();
			setting.Dock = DockStyle.Fill;
			this.settingPanel.Controls.Add(setting);
		}

		private IFrameworkPlugin GetPluginByName(string name)
		{
			foreach (IFrameworkPlugin plugin in plugins)
			{
				if (plugin.Name.Equals(name))
				{
					return plugin;
				}
			}
			return null;
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			foreach (IFrameworkPlugin plugin in plugins)
			{
				CommandStatus status = CommandStatus.Enabled;
				plugin.QueryStatus("Save", ref status);
			}
			this.Close();
		}
	}
}
