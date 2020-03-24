using System;
using System.Collections.Generic;

namespace CovariantGenericTypeInterface
{

    // Define other methods and classes here
    // IEnumerable<out T> has a covariant generic type,
    // we can asign IEnumerable<subtype> to the variable of the type IEnumerable<supertype>.
    class Base
    {
        public static void PrintBases(IEnumerable<Base> bases)
        {
            foreach (Base b in bases)
            {
                Console.WriteLine(b);
            }
        }
    }

    class Derived : Base
    {
        public static void Main()
        {
            List<Derived> dlist = new List<Derived>();

            // The method accepts the IEnumerable<more specific type>,
            // because the client can only expect the base functionality in the pulled from the iterator items.
            Base.PrintBases(dlist);

            // We can also directly assign the list of more specific type items to the variable of IEnumerable<out supertype>.
            IEnumerable<Base> lbase = dlist;
        }
    }
}
