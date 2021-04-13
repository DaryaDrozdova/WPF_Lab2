using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Runtime.Serialization;

namespace DataLibrary
{
    [Serializable]
    public class DataItem: ISerializable
    {
        public System.Numerics.Vector2 coord { get; set; }
        public System.Numerics.Complex val { get; set; }
        public DataItem (Vector2 coord, Complex val)
        {
            this.coord = coord;
            this.val = val;
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("coord_X", coord.X);
            info.AddValue("coord_Y", coord.Y);
            info.AddValue("field_val", val);
        }

        public DataItem(SerializationInfo info, StreamingContext streamingContext)
        {
            float x = info.GetSingle("coord_X");
            float y = info.GetSingle("coord_Y");
            coord = new Vector2(x, y);
            val = (Complex)info.GetValue("field_val", typeof(System.Numerics.Complex));
        }

        public override string ToString()
        {
            return coord + " " + val;
        }
        public string ToString(string format)
        {
            return coord.ToString(format) + " " + val.ToString(format) + " " + Complex.Abs(val).ToString(format);
        }
    }
}
