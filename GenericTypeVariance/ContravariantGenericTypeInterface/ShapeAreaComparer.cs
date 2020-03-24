using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace ContravariantGenericTypeInterface
{
    // IComparer<in T> uses contravariant type parameter; it is used in Compare<T>(T, T) function.
    // Therefore it is safe to pass the ShapeAreaComparer created for less specific class to, say,
    // List<Circle>.Sort(IComparer<Shape>) method.
    class ShapeAreaComparer : IComparer<Shape>
    {
        public int Compare([AllowNull] Shape x, [AllowNull] Shape y)
        {
            // consider null references equal,
            // consider null less than any non-null shape;
            return x == null ? (y == null ? 0 : -1) :
                y == null ? 1 : x.Area.CompareTo(y.Area);
        }
    }
}
