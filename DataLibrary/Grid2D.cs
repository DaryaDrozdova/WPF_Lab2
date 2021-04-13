using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace DataLibrary
{
	[Serializable]
    public struct Grid2D
    {
		public float step_Ox { get; set; }
		public int num_Ox { get; set; }
		public float step_Oy { get; set; }
		public int num_Oy { get; set; }
		public Grid2D(float st_Ox, float st_Oy, int n_Ox, int n_Oy)
		{
			this.step_Ox = st_Ox;
			this.step_Oy = st_Oy;
			this.num_Ox = n_Ox;
			this.num_Oy = n_Oy;
		}
		public override string ToString()
		{
			return step_Ox + " " + num_Ox + " " + step_Oy + " " + num_Oy;
		}
		public string ToString(string format)
        {
			return step_Ox.ToString(format) + " " + num_Ox + " " + step_Oy.ToString(format) + num_Oy;

		}
	}
}
