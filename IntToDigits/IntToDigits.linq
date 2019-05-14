<Query Kind="Program" />

void Main()
{
	IntToDigits(112220).Dump();
}

// Define other methods and classes here
List<int> IntToDigits(int number) {
	List<int> digits = new List<int>();
	while(number != 0) {
		digits.Add(number % 10);
		number /= 10;
	}
	return digits;
}