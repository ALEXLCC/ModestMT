using System;
using System.Collections.Generic;
using System.Text;

namespace ModestMT.AccumulateTreasure.Plugin.MenuComponent
{
	public class MenuItemAttribute : Attribute
	{
		private string name;

		private string parentName;

		private string image;

		private string parentImage;

		public MenuItemAttribute(string name, string parentName, string image)
			: this(name, parentName, image, null)
		{
		}

		public MenuItemAttribute(string name, string parentName, string image, string parentImage)
		{
			this.name = name;
			this.parentName = parentName;
			this.image = image;
			this.parentImage = parentImage;
		}

		public string Name { get { return this.name; } }

		public string ParentName { get { return this.parentName; } }

		public string Image { get { return this.image; } }

		public string ParentImage { get { return this.parentImage; } }
	}
}
