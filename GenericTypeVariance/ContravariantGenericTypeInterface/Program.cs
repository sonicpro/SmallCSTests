using System;
using System.Collections.Generic;

namespace ContravariantGenericTypeInterface
{
    class Program
    {
        static void Main(string[] args)
        {
            // SortedSet<Circle> expects IComparer<Circle>.
            // IComparer<in T> has its type parameter contravariant, to IComparer<Shape> suits as well.
            var sortedAscending = new SortedSet<Circle>(new ShapeAreaComparer())
            {
                new Circle(15),
                new Circle(100),
                new Circle(0.1)
            };

            foreach (var c in sortedAscending)
            {
                // Must print 0.0314, then 706.8583 (225 * Pi) and finally 31,415.9265 (Pi * 10000).
                Console.WriteLine($"{c.Area:N4}");
            }
        }
    }
}
