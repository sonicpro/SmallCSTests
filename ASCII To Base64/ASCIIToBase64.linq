<Query Kind="Statements" />

byte[] Man = new[] 
{
	(byte)77,
	(byte)97,
	(byte)110
};
Console.WriteLine(Convert.ToBase64String(Man));