using ModestMT.Core.Exception;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModestMT.AccumulateTreasure.Plugin.MenuComponent
{
	public class MenuItem
	{
		private string name;
		private List<MenuItem> childMenuItem;
		private string image;

		public MenuItem(string name, string image)
		{
			if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(image))
			{
				throw new BasicException("插件名或图片资源不能配置为空。");
			}

			this.name = name;
			this.image = image;

			this.childMenuItem = new List<MenuItem>();
		}

		public string Name { get { return this.name; } }

		public List<MenuItem> ChildMenuItem { get { return this.childMenuItem; } }

		public string Image { get { return this.image; } }
	}
}
