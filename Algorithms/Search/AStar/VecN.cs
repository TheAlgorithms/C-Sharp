using System;
using System.Runtime.InteropServices.ComTypes;

namespace AStar
{
    public struct VecN
    {
        public int Dimensions => data.Length;
        private float[] data;

        public VecN(params float[] vals)
        {
            data = vals;
        }

        public float SqrLength()
        {
            float ret = 0;
            for (int i = 0; i < data.Length; i++)
            {
                ret += data[i] * data[i];
            }

            return ret;
        }

        public float Length()
        {
            return (float)Math.Sqrt(SqrLength());
        }

        public float Distance(VecN other)
        {
            VecN delta = Substract(other);
            return delta.Length();
        }

        public float SqrDistance(VecN other)
        {
            VecN delta = Substract(other);
            return delta.SqrLength();
        }

        public VecN Substract(VecN other)
        {
            float[] dd = new float[Math.Max(this.data.Length, other.data.Length)];
            for (int i = 0; i < dd.Length; i++)
            {
                float val = 0;
                if (data.Length > i) val = data[i];
                if (other.data.Length > i) val -= other.data[i];
                dd[i] = val;
            }

            return new VecN(dd);
        }


        public override string ToString()
        {
            string ret = "[";
            for (int i = 0; i < data.Length; i++)
            {
                ret += data[i];
                if (i != data.Length - 1) ret += ", ";
            }

            ret += "]";
            return ret;
        }
    }
}