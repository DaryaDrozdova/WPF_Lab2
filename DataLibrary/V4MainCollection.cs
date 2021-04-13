using System;
using System.Collections.Generic;
using System.Collections;
using System.Collections.Specialized;
using System.Linq;
using System.Numerics;
using System.Text;
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace DataLibrary
{
	[Serializable]
    public class V4MainCollection: IEnumerable<V4Data>, INotifyCollectionChanged, INotifyPropertyChanged
    {
		private List<V4Data> list;

		[field: NonSerialized]
		public event NotifyCollectionChangedEventHandler CollectionChanged;
		[field: NonSerialized]
		public event PropertyChangedEventHandler PropertyChanged;

		public bool is_changed { get; set; }

		public int Count
		{
			get => list.Count;
		}
		public double aver_val
        {
			get => (from item0 in list from item1 in item0 select item1).Average(item1=>Complex.Abs(item1.val));
        }

		public double max_abs
        {
			get => (from item0 in list from item1 in item0 select item1).Max(item1 => Complex.Abs(item1.val));
        }

		public IEnumerable<Vector2> allUnique
		{
			get
			{
				var query = from item0 in list
							from item1 in item0
							group item1 by item1.coord into titleGroup
							where titleGroup.Count() == 1
							select titleGroup;
				IEnumerable<Vector2> res = from item0 in query
										   from item1 in item0
										   select item1.coord;
				return res;
			}
		}

		public V4Data this[int index]
		{
			get { return list[index]; }
			set
			{
				list[index] = value;
				if (CollectionChanged != null)
					CollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
			}
		}

		public V4MainCollection()
		{
			list = new List<V4Data>();
			is_changed = true;
			CollectionChanged += V4MainCollection_CollectionChanged;
		}

		private void V4MainCollection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			V4MainCollection obj = (V4MainCollection)sender;
			obj.is_changed = true;
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs("max_abs"));
		}

		public bool Save(string filename)
        {
			FileStream fileStream = null;
            try
            {
				fileStream = File.Create(filename);
				BinaryFormatter binaryFormatter = new BinaryFormatter();
				binaryFormatter.Serialize(fileStream, this);
				is_changed = false;
				return true;
            }
			catch (Exception ex)
            {
				Console.WriteLine("Save\n" + ex.Message);
				return false;
            }
			finally
            {
				if (fileStream != null) 
					fileStream.Close();
            }
        }

		public static V4MainCollection Load(string filename)
        {
			FileStream fileStream = null;
			V4MainCollection res = null;
			try
            {
				fileStream = File.OpenRead(filename);
				BinaryFormatter binaryFormatter = new BinaryFormatter();
				res = binaryFormatter.Deserialize(fileStream) as V4MainCollection;
				res.CollectionChanged += res.V4MainCollection_CollectionChanged;
            }
			catch (Exception ex)
            {
				Console.WriteLine("Load\n" + ex.Message);
            }
			finally
            {
				if (fileStream != null) fileStream.Close();
			}
			return res;
        }

		public bool Contains(string info)
        {
			foreach (V4Data elem in list)
				if (elem.info == info)
					return true;
			return false;
        }

        public void Add(V4Data item)
		{
			list.Add(item);
			if (CollectionChanged != null)
				CollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));

		}

		public void Remove_wpf(V4Data item)
        {
			list.Remove(item);
			if (CollectionChanged != null)
				CollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
		}

        public bool Remove(string id, double w)
		{
			bool flag = false;
			int k = 0;
			List<int> index = new List<int>();
			foreach (V4Data elem in list)
			{
				if (elem.info == id && elem.freq == w)
				{
					flag = true;
					index.Add(k);
				}
				k++;
			}
			if (flag)
			{
				for (int i = index.Count - 1; i >= 0; i--)
				{
					list.RemoveAt(index[i]);
				}
			}
			return flag;
		}

		public void AddDefaults()
		{
			Grid2D grid = new Grid2D(1, 1, 2, 2);
			V4DataOnGrid obj1 = new V4DataOnGrid("WPF1", 1, grid);
			V4DataCollection obj2 = new V4DataCollection("WPF2", 2);
			V4DataOnGrid obj3 = new V4DataOnGrid("WPF3", 1, grid);
			V4DataCollection obj4 = new V4DataCollection("WPF4", 2);
			obj1.InitRandom(2, 4);
			obj2.InitRandom(3, 2, 2, 2, 4);
			obj3.InitRandom(2, 4);
			obj4.InitRandom(3, 2, 2, 2, 4);
			list.Add(obj1);
			list.Add(obj2);
			list.Add(obj3);
			list.Add(obj4);		

			if (CollectionChanged != null)
				CollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
		}

		public void AddDefault_DataOnGrid()
        {
			Grid2D Grid = new Grid2D(1, 1, 2, 2);
			V4DataOnGrid DataOnGrid_Item = new V4DataOnGrid("Random item for Wpf", 2, Grid);
			DataOnGrid_Item.InitRandom(1, 3);
			list.Add(DataOnGrid_Item);
			if (CollectionChanged != null)
				CollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
		}

		public void AddDefault_DataCollection()
        {
			V4DataCollection DataCollection_Item = new V4DataCollection("Random item for Wpf", 2);
			DataCollection_Item.InitRandom(3, 2, 2, 5, 7);
			list.Add(DataCollection_Item);
			if (CollectionChanged != null)
				CollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
		}

		public override string ToString()
		{
			string str = "";
			foreach (V4Data elem in list)
				str += elem.ToString();
			return str;
		}
		public string ToLongString(string format)
        {
			string str = "";
			foreach (V4Data elem in list)
				str += elem.ToLongString(format);
			return str;
		}

		public IEnumerator<V4Data> GetEnumerator()
		{
			foreach (V4Data elem in list)
			{
				yield return elem;
			}
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		public IEnumerable<DataItem> NearZero(float R)
        {
			Vector2 zero = new Vector2(0, 0);
			IEnumerable<DataItem> Query = from item0 in list
										  from item1 in item0
										  where Vector2.Distance(item1.coord, zero) <= R
										  select item1;
			return Query;
        }

	}
}
