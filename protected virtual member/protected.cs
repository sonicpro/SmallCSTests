using System;


internal sealed class program
{

	class someBase
	{
		Int32 a;
		protected virtual Int32 GetA()
		{
			return a;
		}
	}

	class derived1 : someBase
	{
		protected override Int32 GetA()
		{
			return 1;
		}
	}

	class derived2 : derived1
	{
		protected override Int32 GetA()
		{
			return 2;
		}
		public Int32 accessA()
		{
			return GetA();
		}
	}
	
	public static void Main()
	{
		derived2 instance = new derived2();
		Console.WriteLine(instance.accessA());
	}
}