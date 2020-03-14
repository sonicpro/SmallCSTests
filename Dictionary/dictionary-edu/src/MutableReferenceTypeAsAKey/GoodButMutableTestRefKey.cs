using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MutableReferenceTypeAsAKey
{
    class GoodButMutableTestRefKey
    {
        public int Key;

        public GoodButMutableTestRefKey(int key)
        {
            Key = key;
        }

        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                return true;
            }

            if (obj is GoodButMutableTestRefKey otherKey)
            {
                return this.Key == otherKey.Key;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return this.Key.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("{0}", this.Key);
        }
    }
}
