<Query Kind="Program" />

void Main()
{
	// lambda takes a type that has ToLower() method and returns anonymous object.
	// Compiler cannot infer the type of the lambda.
	
	// var transformer = x => new { Original = x, Normalized = x.ToLower() }; //CS0815 "Cannot assign lambda expression to an implicitly-typed variable."
	
	// sligntly better, but anonymous object is masked as an Object instance.
	var transformer = LambdaWrapper<string, object>(x => new { Original = x, Normalized = x.ToLower() });
	
	var transformerTypeInferredCorrectly = LambdaWrapper((string x) => new { Original = x, Normalized = x.ToLower() });
	
}

// Define other methods and classes here
private static Func<T, TResult> LambdaWrapper<T, TResult>(Func<T, TResult> matchingLambda)
{
	return matchingLambda;
}