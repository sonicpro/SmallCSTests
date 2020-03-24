using System;
using System.Collections.Generic;
using System.Text;

namespace ContravariantGenericTypeInterface
{
    class Circle : Shape
    {
        private readonly double r;

        public Circle(double radius)
        {
            r = radius;
        }

        public override double Area => Math.PI * Math.Pow(r, 2.0);
    }
}
