using System;
using System.Text;

namespace AStar
{
    /// <summary>
    /// Vector Struct with N Dimensions.
    /// </summary>
    public struct VecN : IEquatable<VecN>
    {
        private static readonly StringBuilder Builder = new StringBuilder();
        private readonly float[] data;

        /// <summary>
        /// Initializes a new instance of the <see cref="VecN"/> struct.
        /// Constructor.
        /// </summary>
        /// <param name="vals">Vector components as array.</param>
        public VecN(params float[] vals) => data = vals;

        /// <summary>
        /// Gets the dimension count of this vector.
        /// </summary>
        public int Dimensions => data.Length;

        /// <summary>
        /// Returns the Length squared.
        /// </summary>
        /// <returns>The squared length of the vector.</returns>
        public float SqrLength()
        {
            float ret = 0;
            for (var i = 0; i < data.Length; i++)
            {
                ret += data[i] * data[i];
            }

            return ret;
        }

        /// <summary>
        /// Returns the Length of the vector.
        /// </summary>
        /// <returns>Length of the Vector.</returns>
        public float Length() => (float)Math.Sqrt(SqrLength());

        /// <summary>
        /// Returns the Distance between this and other.
        /// </summary>
        /// <param name="other">Other vector.</param>
        /// <returns>The distance between this and other.</returns>
        public float Distance(VecN other)
        {
            var delta = Subtract(other);
            return delta.Length();
        }

        /// <summary>
        /// Returns the squared Distance between this and other.
        /// </summary>
        /// <param name="other">Other vector.</param>
        /// <returns>The squared distance between this and other.</returns>
        public float SqrDistance(VecN other)
        {
            var delta = Subtract(other);
            return delta.SqrLength();
        }

        /// <summary>
        /// Substracts other from this vector.
        /// </summary>
        /// <param name="other">Other vector.</param>
        /// <returns>The new vector.</returns>
        public VecN Subtract(VecN other)
        {
            var dd = new float[Math.Max(data.Length, other.data.Length)];
            for (var i = 0; i < dd.Length; i++)
            {
                float val = 0;
                if (data.Length > i)
                {
                    val = data[i];
                }

                if (other.data.Length > i)
                {
                    val -= other.data[i];
                }

                dd[i] = val;
            }

            return new VecN(dd);
        }

        /// <summary>
        /// Overridden ToString method to give better console output.
        /// </summary>
        /// <returns>Vector string representation.</returns>
        public override string ToString()
        {
            _ = Builder.Clear();
            _ = Builder.Append('[');
            for (var i = 0; i < data.Length; i++)
            {
                _ = Builder.Append(data[i]);
                if (i != data.Length - 1)
                {
                    _ = Builder.Append(", ");
                }
            }

            _ = Builder.Append(']');
            return Builder.ToString();
        }

        /// <summary>
        /// Is used to compare Vectors with each other.
        /// </summary>
        /// <param name="other">The vector to be compared.</param>
        /// <returns>A value indicating if other has the same values as this.</returns>
        public bool Equals(VecN other)
        {
            if (other.Dimensions != Dimensions)
            {
                return false;
            }

            for (var i = 0; i < other.data.Length; i++)
            {
                if (Math.Abs(data[i] - other.data[i]) > 0.001f)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
