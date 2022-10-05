<Query Kind="Program" />

void Main()
{
	// lambda takes a type that has ToLower() method and returns anonymous object.
	// Compiler cannot infer the type of the lambda.
	
	// var transformer = x => new { Original = x, Normalized = x.ToLower() }; //CS0815 "Cannot assign lambda expression to an implicitly-typed variable."
	
	// sligntly better, but anonymous object is masked as an Object instance.
	var transformer = LambdaWrapper<string, int, object>((x, length) => new { Original = x, Normalized = x.ToLower(), Length = length });
	
	var transformerTypeInferredCorrectly = LambdaWrapper((string x, int length) => new { Original = x, Normalized = x.ToLower(), Length = length });

    var namesWithIndexes = new[] { "Bill", "George", "Kite" }.Select(LambdaWrapper((string name, int index) => new { Name = name, Ordinal = index }));

    foreach (var name in namesWithIndexes)
    {
        Console.WriteLine($"{name.Name} has and index of {name.Ordinal}");
    }
}

// Define other methods and classes here
private static Func<T, U, TResult> LambdaWrapper<T, U, TResult>(Func<T, U, TResult> matchingLambda)
{
	return matchingLambda;
}