using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace ModestMT.Core.WPF
{
	[ValueConversion(typeof(double), typeof(String))]
	public class WidthConverter : IValueConverter
	{
		public double Interval { get; set; }

		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			double index = (double)value;
			return index - Interval;

		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			double index;

			if (double.TryParse(value.ToString(), out index))
			{
				return index - Interval;
			}
			else
			{
				return value;
			}
		}
	}
}
