namespace BarlugoFX.Utils
{
    /// <summary>
    /// This class implements a mutable pair, so it is possible to change both its
    /// values. Be careful though that this Pair is not safe for any set.
    ///
    /// Note: Great care must be exercised if mutable objects are used as set
    /// elements. The behavior of a set is not specified if the value of an object is
    /// changed in a manner that affects equals comparisons while the object is an
    /// element in the set. A special case of this prohibition is that it is not
    /// permissible for a set to contain itself as an element.
    /// </summary>
    /// <typeparam name="F">the first value type</typeparam>
    /// <typeparam name="S">the second value type</typeparam>
    public class MutablePair<F, S>
    {
        /// <summary>
        /// The first parameter of the pair
        /// </summary>
        public F First { get; set; }
        /// <summary>
        /// The second parameter of the pair
        /// </summary>
        public S Second { get; set; }

        ///<summary>
        ///The class constructor.
        ///</summary>
        ///<param name="first"> the first value of the pair</param>
        ///<param name="second">second the second value of the pair</param>
        ///
        public MutablePair(F first, S second)
        {
            First = first;
            Second = second;
        }

        public override int GetHashCode()
        {
            int prime = 31;
            int result = 1;
            result = prime * result + (First == null ? 0 : First.GetHashCode());
            result = prime * result + (Second == null ? 0 : Second.GetHashCode());
            return result;
        }

        public override bool Equals(object obj)
        {
            if (this == obj)
            {
                return true;
            }

            if (obj == null)
            {
                return false;
            }

            if (!(obj is MutablePair<F, S>))
            {
                return false;
            }

            var other = (MutablePair<F, S>) obj;
            if (First == null)
            {
                if (other.First != null)
                {
                    return false;
                }
            }
            else if (!First.Equals(other.First))
            {
                return false;
            }

            if (Second == null)
            {
                if (other.Second != null)
                {
                    return false;
                }
            }
            else if (!Second.Equals(other.Second))
            {
                return false;
            }

            return true;
        }

        public override string ToString()
        {
            return "MutablePair [first=" + First + ", second=" + Second + "]";
        }

    }
}