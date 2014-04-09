using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ModestMT.WinForm.ImagePicker.Plugin.Products.AliDownloader.Bll
{
	public class BaseEntity : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public void NotifyPropretyChanged(string propertyName)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
