using ModestMT.AccumulateTreasure.Plugin.Interface;
using ModestMT.AccumulateTreasure.Plugin.MenuComponent;
using ModestMT.AccumulateTreasure.Utility;
using ModestMT.Plugin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MenuComponent = ModestMT.AccumulateTreasure.Plugin.MenuComponent;

namespace ModestMT.AccumulateTreasure
{
	public partial class Main : Form, IFramework
	{
		private List<IFrameworkPlugin> plugins = new List<IFrameworkPlugin>();

		private List<MenuComponent.MenuItem> menuItems = new List<MenuComponent.MenuItem>();

		private Dictionary<string, IFrameworkPlugin> menuItemMapPlugin = new Dictionary<string, IFrameworkPlugin>();

		private Setting settingForm;
		private ToolStripButton btnSetting;
		private ToolStripSeparator toolStripSeparator1;

		public Main()
		{
			InitializeComponent();
		}

		private void Main_Load(object sender, EventArgs e)
		{
			PluginManager pluginManager = PluginManager.Create(this.GetType().Assembly, typeof(IFrameworkPlugin), this);

			List<MenuItemAttribute> menuItemAttributes = new List<MenuItemAttribute>();

			foreach (IPlugin plugin in pluginManager.Plugins)
			{
				IFrameworkPlugin imagePickerPlugin = plugin as IFrameworkPlugin;

				object[] attributes = imagePickerPlugin.GetType().GetCustomAttributes(typeof(MenuItemAttribute), false);
				if (attributes.Length == 0 || attributes.Length > 1)
				{
					throw new AccumulateTreasureException("每个插件只允许注册一个菜单按钮.");
				}
				MenuItemAttribute menuItemAttribute = (MenuItemAttribute)attributes[0];

				if (menuItemMapPlugin.ContainsKey(menuItemAttribute.Name))
				{
					throw new AccumulateTreasureException(string.Format("{0} 插件名重复.", imagePickerPlugin.Name));
				}

				menuItemAttributes.Add(menuItemAttribute);

				menuItemMapPlugin.Add(menuItemAttribute.Name, imagePickerPlugin);

				plugins.Add(imagePickerPlugin);
			}

			// add the parent menuitem.
			foreach (MenuComponent.MenuItemAttribute menuItemAttribute in menuItemAttributes)
			{
				if (string.IsNullOrEmpty(menuItemAttribute.ParentName) == true)
				{
					menuItems.Add(new MenuComponent.MenuItem(menuItemAttribute.Name, menuItemAttribute.Image));
				}
			}

			foreach (MenuComponent.MenuItemAttribute menuItemAttribute in menuItemAttributes)
			{
				if (string.IsNullOrEmpty(menuItemAttribute.ParentName) == false)
				{
					MenuComponent.MenuItem menuItem = GetMenuItem(menuItemAttribute.ParentName);
					if (menuItem == null)
					{
						MenuComponent.MenuItem parentItem = new MenuComponent.MenuItem(menuItemAttribute.ParentName, menuItemAttribute.ParentImage);
						parentItem.ChildMenuItem.Add(new MenuComponent.MenuItem(menuItemAttribute.Name, menuItemAttribute.Image));

						menuItems.Add(parentItem);
					}
					else
					{
						menuItem.ChildMenuItem.Add(new MenuComponent.MenuItem(menuItemAttribute.Name, menuItemAttribute.Image));
					}
				}
			}

			foreach (MenuComponent.MenuItem menuItem in menuItems)
			{
				if (menuItem.ChildMenuItem.Count == 0)
				{
					ToolStripButton button = new ToolStripButton();
					button.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
					button.Image = Resources.GetImage(menuItem.Image);
					button.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
					button.ImageTransparentColor = System.Drawing.Color.Magenta;
					button.Name = menuItem.Name;
					button.Text = menuItem.Name;

					RegistEvent(menuItemMapPlugin[menuItem.Name], button);

					this.menu.Items.AddRange(new ToolStripItem[] { button });
				}
				else
				{
					ToolStripDropDownButton dropDownButton = new ToolStripDropDownButton();
					dropDownButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.ImageAndText;
					dropDownButton.Image = Resources.GetImage(menuItem.Image);
					dropDownButton.ImageTransparentColor = System.Drawing.Color.Magenta;
					dropDownButton.Name = menuItem.Name;
					dropDownButton.Text = menuItem.Name;
					List<ToolStripMenuItem> childItems = new List<ToolStripMenuItem>();
					foreach (MenuComponent.MenuItem item in menuItem.ChildMenuItem)
					{
						ToolStripMenuItem button = new ToolStripMenuItem();
						button.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
						button.Image = Resources.GetImage(item.Image);
						button.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
						button.ImageTransparentColor = System.Drawing.Color.Magenta;
						button.Name = item.Name;
						button.Text = item.Name;

						if (menuItemMapPlugin.ContainsKey(item.Name))
						{
							RegistEvent(menuItemMapPlugin[item.Name], button);
						}
						childItems.Add(button);
					}
					dropDownButton.DropDownItems.AddRange(childItems.ToArray());

					this.menu.Items.AddRange(new ToolStripItem[] { dropDownButton });
				}
			}

			LoadSettingMenuItem();

			// Execute plugin 1;
			if (plugins.Count > 0)
			{
				bool handled = false;
				plugins[0].Execute(plugins[0].Name, ref handled);
			}
		}

		public void UpdateContent(UserControl content)
		{
			if (this.content.Controls.Contains(content) == false)
			{
				this.content.Controls.Clear();
				content.Dock = DockStyle.Fill;
				this.content.Controls.Add(content);
			}
		}

		private void RegistEvent(IFrameworkPlugin imagePickerPlugin, ToolStripItem item)
		{
			item.Click += button_Click;
			CommandStatus status = CommandStatus.Unsupported;
			imagePickerPlugin.QueryStatus(item.Name, ref status);
		}

		private MenuComponent.MenuItem GetMenuItem(string name)
		{
			return GetMenuItem(name, menuItems);
		}

		private MenuComponent.MenuItem GetMenuItem(string name, List<MenuComponent.MenuItem> menuItems)
		{
			if (string.IsNullOrEmpty(name) == true)
			{
				return null;
			}

			MenuComponent.MenuItem result = null;

			foreach (MenuComponent.MenuItem menuItem in menuItems)
			{
				if (menuItem.Name == name)
				{
					result = menuItem;
				}
				else
				{
					result = GetMenuItem(name, menuItem.ChildMenuItem);
				}
			}
			return result;
		}

		private void Main_FormClosing(object sender, FormClosingEventArgs e)
		{
			IFrameworkPlugin plugin = null;
			try
			{
				foreach (IFrameworkPlugin p in menuItemMapPlugin.Values)
				{
					plugin = p;
					plugin.OnDisconnection();
				}
			}
			catch
			{
				MessageBox.Show(string.Format("插件 {0} 退出失败", plugin.Name), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void LoadSettingMenuItem()
		{
			// 
			// btnSetting
			// 
			this.btnSetting.Image = global::ModestMT.AccumulateTreasure.Properties.Resources.setting;
			this.btnSetting.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnSetting.Name = "btnSetting";
			this.btnSetting.Size = new System.Drawing.Size(52, 22);
			this.btnSetting.Text = "设置";
			this.btnSetting.Click += btnSetting_Click;
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);

			// add setting menu item
			this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this.toolStripSeparator1, this.btnSetting });
		}

		private void btnSetting_Click(object sender, EventArgs e)
		{
			if (settingForm == null)
			{
				settingForm = new Setting(this.plugins);
			}
			settingForm.ShowDialog(this);
		}

		private void button_Click(object sender, EventArgs e)
		{
			ToolStripItem item = (ToolStripItem)sender;
			bool handle = false;
			menuItemMapPlugin[item.Name].Execute(item.Name, ref handle);
			if (handle == false)
			{
				MessageBox.Show("命令未响应", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
	}
}
