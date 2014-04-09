using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ModestMT.Core.Collection
{
	public class ObservableList<T> : IList<T>, INotifyPropertyChanged where T : INotifyPropertyChanged
	{
		List<T> list;

		public ObservableList()
		{
			list = new List<T>();
		}

		public ObservableList(IEnumerable<T> collection)
		{
			list = new List<T>(collection);
		}

		public event PropertyChangedEventHandler PropertyChanged;

		public int IndexOf(T item)
		{
			return list.IndexOf(item);
		}

		public void Insert(int index, T item)
		{
			list.Insert(index, item);

			((INotifyPropertyChanged)item).PropertyChanged += ObservableList_PropertyChanged;

			PropertyChange();
		}

		void ObservableList_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			PropertyChange(sender, e);
		}

		public void RemoveAt(int index)
		{
			list.RemoveAt(index);
			PropertyChange();
		}

		public T this[int index]
		{
			get
			{
				return list[index];
			}
			set
			{
				if (list[index].Equals(value))
				{
					list[index] = value;
					PropertyChange();
				}
			}
		}

		public void Add(T item)
		{
			list.Add(item);
			PropertyChange();
		}

		public void Clear()
		{
			list.Clear();
			PropertyChange();
		}

		public bool Contains(T item)
		{
			return list.Contains(item);
		}

		public void CopyTo(T[] array, int arrayIndex)
		{
			list.CopyTo(array, arrayIndex);
		}

		public int Count
		{
			get { return list.Count; }
		}

		public bool IsReadOnly
		{
			get { return false; }
		}

		public bool Remove(T item)
		{
			bool result = list.Remove(item);
			PropertyChange();
			return result;
		}

		public IEnumerator<T> GetEnumerator()
		{
			return list.GetEnumerator();
		}

		public List<T> GetList()
		{
			return list;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return list.GetEnumerator();
		}

		private void PropertyChange()
		{
			PropertyChange(this, new PropertyChangedEventArgs("List"));
		}

		private void PropertyChange(object sender, PropertyChangedEventArgs e)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(sender, e);
			}
		}
	}
}
