using System;

namespace BoxingWhenCastingToAnInterface
{
    public class Class1
    {
        double someMoney = 4.0;

        IComparable comparableMoney;

        public Class1()
        {
            comparableMoney = this.someMoney;
        }

        public IComparable ComparableMoney => comparableMoney;
    }
}
