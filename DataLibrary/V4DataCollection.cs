using System;
using System.Collections.Generic;
using System.Collections;
using System.Numerics;
using System.Text;
using System.Runtime.Serialization;

namespace DataLibrary
{
	[Serializable]
    public class V4DataCollection: V4Data, IEnumerable<DataItem>, ISerializable
    {
		public Dictionary<System.Numerics.Vector2, System.Numerics.Complex> dict { get; set; }
		public V4DataCollection(string info, double freq) : base(info, freq)
		{

			this.dict = new Dictionary<System.Numerics.Vector2, System.Numerics.Complex>();
		}

		public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
			float[] x = new float[dict.Count];
			float[] y = new float[dict.Count];
			Complex[] val = new Complex[dict.Count];
			int i = 0;
			foreach (KeyValuePair<Vector2, Complex> elem in dict)
			{
				x[i] = elem.Key.X;
				y[i] = elem.Key.Y;
				val[i] = elem.Value;
				i++;
			}
			info.AddValue("array_X", x);
			info.AddValue("array_Y", y);
			info.AddValue("values", val);
			info.AddValue("info", base.info);
			info.AddValue("freq", base.freq);
		}

		public V4DataCollection(SerializationInfo info, StreamingContext streamingContext):
			base(info.GetValue("info", typeof(string)) as string, (double)info.GetValue("freq", typeof(double)))
        {
			float[] x = info.GetValue("array_X", typeof(float[])) as float[];
			float[] y = info.GetValue("array_Y", typeof(float[])) as float[];
			Complex[] val = info.GetValue("values", typeof(Complex[])) as Complex[];
			dict = new Dictionary<Vector2, Complex>();
			for (int i = 0; i < x.Length; i++)
            {
				Vector2 vec = new Vector2(x[i], y[i]);
				dict.Add(vec, val[i]);
            }
		}

		public void InitRandom(int nItems, float xmax, float ymax, double minValue, double maxValue)
		{
			Random rand = new Random();
			double real, imaginary;
			float x, y;
			Complex value;
			Vector2 vec;
			for (int i = 0; i < nItems; i++)
			{
				x = (float)rand.NextDouble() * xmax;
				y = (float)rand.NextDouble() * ymax;
				real = minValue + rand.NextDouble() * (maxValue - minValue);
				imaginary = minValue + rand.NextDouble() * (maxValue - minValue);
				value = new Complex(real, imaginary);
				vec = new Vector2(x, y);
				dict.Add(vec, value);
			}
		}

		public override Complex[] NearMax(float eps)
		{
			double max = 0;
			int k = 0;
			Complex[] near;
			foreach (KeyValuePair<Vector2, Complex> elem in dict)
				if (Complex.Abs(elem.Value) > max)
					max = Complex.Abs(elem.Value);
			foreach (KeyValuePair<Vector2, Complex> elem in dict)
				if (max - Complex.Abs(elem.Value) <= eps)
					k++;
			near = new Complex[k];
			k = 0;
			foreach (KeyValuePair<Vector2, Complex> elem in dict)
				if (max - Complex.Abs(elem.Value) <= eps)
				{
					near[k] = elem.Value;
					k++;
				}
			return near;
		}

		public override string ToString()
		{
			return "V4DataCollection\n" + base.info + " " + base.freq + " " + dict.Count + "\n";
		}

		public override string ToLongString()
		{
			string str = ToString() + "\n";
			string longstr = "";
			foreach (KeyValuePair<Vector2, Complex> elem in dict)
				longstr += elem.Key + " " + elem.Value + "\n";
			return str + longstr;
		}
        public override string ToLongString(string format)
        {
			string str = ToString();
			string longstr = "";
			foreach (KeyValuePair<Vector2, Complex> elem in dict)
				longstr += elem.Key.ToString(format) + " " + elem.Value.ToString(format) +" " + Complex.Abs(elem.Value).ToString(format) + "\n"; //added format parameter to values
			return str + longstr;
		}

        public override IEnumerator<DataItem> GetEnumerator()
        {
			DataItem new_elem;
			foreach (KeyValuePair<Vector2, Complex> elem in dict)
			{
				new_elem = new DataItem(elem.Key, elem.Value);
				yield return new_elem;
			}
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}
	}
}
