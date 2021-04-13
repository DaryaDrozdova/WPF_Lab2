using System;
using System.Collections.Generic;
using System.Collections;
using System.Numerics;
using System.IO;
using System.Globalization;
using System.Text;

namespace DataLibrary
{
	[Serializable]
    public class V4DataOnGrid: V4Data, IEnumerable<DataItem>
    {
        public Grid2D grid { get; set; }
		public Complex[,] values { get; set; }
		public V4DataOnGrid(string info, double freq, Grid2D grid) : base(info, freq)
		{
			this.grid = grid;
			values = new Complex[grid.num_Ox, grid.num_Oy];  //creating values in the constructor
		}
		public V4DataOnGrid(string info, double freq, string filename): base(info,freq)
		{
			FileStream fs = null;
			try
			{
				fs = new FileStream(filename, FileMode.Open);
				StreamReader strReader = new StreamReader(fs);
				double real;
				double imaginary;
				string str_complex;
				string[] split_complex;
				CultureInfo.CurrentCulture = new CultureInfo("ru-RU");  //setting current culture to ru-RU
				CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator = ",";  //setting decimal separator to ","
				try
				{
					float st_Ox = Convert.ToSingle(strReader.ReadLine(),null); //using current culture as the parameter
					float st_Oy = Convert.ToSingle(strReader.ReadLine(),null);
					int n_Ox = Convert.ToInt32(strReader.ReadLine(),null);
					int n_Oy = Convert.ToInt32(strReader.ReadLine(),null);
					this.grid = new Grid2D(st_Ox, st_Oy, n_Ox, n_Oy);
					values = new Complex[n_Ox, n_Oy];
					for (int i = 0; i < n_Ox; i++)
						for (int j = 0; j < n_Oy; j++)
						{
							str_complex = strReader.ReadLine();
							split_complex = str_complex.Split(new Char[] { ' ' });
							real = Convert.ToDouble(split_complex[0],null);
							imaginary = Convert.ToDouble(split_complex[1],null);
							values[i, j] = new Complex(real, imaginary);
						}
				}
				catch (FormatException ex) //checking errors with the format in the input file
				{ Console.WriteLine(ex.Message); }
				strReader.Close();
			}
			catch (Exception ex)
            {
				throw ex;
				//Console.WriteLine(ex.Message);
            }
			finally
            {
				if (fs != null) fs.Close();
            }
		}
		public void InitRandom(double minValue, double maxValue)
		{
			Random rand = new Random();
			double real, imaginary;
			for (int i = 0; i < grid.num_Ox; i++)
				for (int j = 0; j < grid.num_Oy; j++)
				{
					real = minValue + rand.NextDouble() * (maxValue - minValue);
					imaginary = minValue + rand.NextDouble() * (maxValue - minValue);
					values[i, j] = new Complex(real, imaginary);
				}
		}

		public static explicit operator V4DataCollection(V4DataOnGrid obj1)
		{
			V4DataCollection obj2 = new V4DataCollection(obj1.info, obj1.freq);
			Dictionary<Vector2, Complex> dict = new Dictionary<Vector2, Complex>();
			float x = 0;
			float y = 0;
			Vector2 vec;
			for (int i = 0; i < obj1.grid.num_Ox; i++)
			{
				for (int j = 0; j < obj1.grid.num_Oy; j++)
				{
					vec = new Vector2(x, y);
					dict.Add(vec, obj1.values[i, j]);
					y += obj1.grid.step_Oy;
				}
				y = 0;
				x += obj1.grid.step_Ox;
			}
			obj2.dict = dict;
			return obj2;
		}

		public override Complex[] NearMax(float eps)
		{
			int i, j, k = 0;
			double max = Complex.Abs(values[0, 0]);
			Complex[] near;
			for (i = 0; i < grid.num_Ox; i++)
				for (j = 0; j < grid.num_Oy; j++)
					if (Complex.Abs(values[i, j]) > max)
						max = Complex.Abs(values[i, j]);
			for (i = 0; i < grid.num_Ox; i++)
				for (j = 0; j < grid.num_Oy; j++)
					if (max - Complex.Abs(values[i, j]) <= eps)
						k++;
			near = new Complex[k];
			k = 0;
			for (i = 0; i < grid.num_Ox; i++)
				for (j = 0; j < grid.num_Oy; j++)
					if (max - Complex.Abs(values[i, j]) <= eps)
					{
						near[k] = values[i, j];
						k++;
					}
			return near;
		}

		public override string ToString()
		{
			return "V4DataOnGrid\n" + base.info + " " + base.freq + " " + grid + "\n";
		}

		public override string ToLongString()
		{
			string str = ToString() + "\n";
			string longstr = "";
			float x = 0;
			float y = 0;
			for (int i = 0; i < grid.num_Ox; i++)
			{
				for (int j = 0; j < grid.num_Oy; j++)
				{
					longstr += x + " " + y + " " + values[i, j] + "\n";
					y += grid.step_Oy;
				}
				y = 0;
				x += grid.step_Ox;
			}
			return str + longstr;
		}
        public override string ToLongString(string format)
        {
			string str = "V4DataOnGrid\n" + base.info + " " + base.freq + " " + grid.ToString(format) + "\n";
			string longstr = "";
			float x = 0;
			float y = 0;
			for (int i = 0; i < grid.num_Ox; i++)
			{
				for (int j = 0; j < grid.num_Oy; j++)
				{
					longstr += x.ToString(format) + " " + y.ToString(format) + " " + values[i, j].ToString(format) + " " + Complex.Abs(values[i,j]).ToString(format) + "\n"; //added format parametr to values
					y += grid.step_Oy;
				}
				y = 0;
				x += grid.step_Ox;
			}
			return str + longstr;
		}

        public override IEnumerator<DataItem> GetEnumerator()
        {
			float x = 0;
			float y = 0;
			Vector2 vec;
			DataItem elem;
			for (int i = 0; i < grid.num_Ox; i++)
			{
				for (int j = 0; j < grid.num_Oy; j++)
				{
					vec = new Vector2(x, y);
					elem = new DataItem(vec, values[i, j]);
					yield return elem;
					y += grid.step_Oy;
				}
				y = 0;
				x += grid.step_Ox;
			}
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}
	}
}
