using System;

namespace Algorithms.Search.AStar
{
    /// <summary>
    /// Vector Struct with N Dimensions.
    /// </summary>
    public struct VecN : IEquatable<VecN>
    {
        private readonly double[] data;

        /// <summary>
        /// Initializes a new instance of the <see cref="VecN"/> struct.
        /// </summary>
        /// <param name="vals">Vector components as array.</param>
        public VecN(params double[] vals) => data = vals;

        /// <summary>
        /// Gets the dimension count of this vector.
        /// </summary>
        public int N => data.Length;

        /// <summary>
        /// Returns the Length squared.
        /// </summary>
        /// <returns>The squared length of the vector.</returns>
        public double SqrLength()
        {
            double ret = 0;
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
        public double Length() => Math.Sqrt(SqrLength());

        /// <summary>
        /// Returns the Distance between this and other.
        /// </summary>
        /// <param name="other">Other vector.</param>
        /// <returns>The distance between this and other.</returns>
        public double Distance(VecN other)
        {
            var delta = Subtract(other);
            return delta.Length();
        }

        /// <summary>
        /// Returns the squared Distance between this and other.
        /// </summary>
        /// <param name="other">Other vector.</param>
        /// <returns>The squared distance between this and other.</returns>
        public double SqrDistance(VecN other)
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
            var dd = new double[Math.Max(data.Length, other.data.Length)];
            for (var i = 0; i < dd.Length; i++)
            {
                double val = 0;
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
        /// Is used to compare Vectors with each other.
        /// </summary>
        /// <param name="other">The vector to be compared.</param>
        /// <returns>A value indicating if other has the same values as this.</returns>
        public bool Equals(VecN other)
        {
            if (other.N != N)
            {
                return false;
            }

            for (var i = 0; i < other.data.Length; i++)
            {
                if (Math.Abs(data[i] - other.data[i]) > 0.000001)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
