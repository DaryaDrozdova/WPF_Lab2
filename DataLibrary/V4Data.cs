using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.ComponentModel;

namespace DataLibrary
{
	[Serializable]
	public abstract class V4Data: IEnumerable<DataItem>, INotifyPropertyChanged
	{
		[field: NonSerialized]
		public event PropertyChangedEventHandler PropertyChanged;

		private string Info;
		public string info
		{
			get { return Info; }
			set
		   {
				Info = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("info"));
			}
		}

		private double Freq;
		public double freq
        {
			get { return Freq; }
			set
			{
				Freq = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("freq"));
			}
		}
		public V4Data(string info, double freq)
		{
			this.info = info;
			this.freq = freq;
		}
		public abstract Complex[] NearMax(float eps);
		public abstract string ToLongString();
		public override string ToString()
		{
			return info + " " + freq;
		}
		public abstract string ToLongString(string format);
		public abstract IEnumerator<DataItem> GetEnumerator();
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}
	}
}
