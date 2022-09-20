<Query Kind="Statements" />

var tu = new string(new[] { (char)0x30C4 });
var utf8 = System.Text.Encoding.UTF8.GetBytes(tu);
// char[] chars = new char[utf8.Length];
for (int index = 0; index < utf8.Length; index++)
{
    Console.WriteLine("{0:X}", utf8[index]);
    // chars[index] = (char)utf8[index];
}

// Console.WriteLine(new string(chars));