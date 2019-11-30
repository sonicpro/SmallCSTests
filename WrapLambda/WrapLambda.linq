<Query Kind="Program" />

void Main()
{
	//CS0815 "Cannot assign lambda expression to an implicitly-typed variable."
//	var transformer = x => new { x, Normalized = x.ToLower() };
	
	// Strongly typed anonymous function parameter is necessary - otherwise another (CS0411) compiler error is shown.
	var transformer = WrapLambda((string x) => new { x, Normalized = x.ToLower() });
}


static Func<T,U> WrapLambda<T, U>(Func<T,U> func)
{
	return func;
}

static Expression WrapLambdaPrime(Expression expr)
{
	return expr;
}