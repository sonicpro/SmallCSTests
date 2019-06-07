<Query Kind="Statements" />

var a = "#87053e";
string opacity = null;
var hexString = a.Substring(1).ToCharArray();
var bytes = hexString.Zip(hexString.Skip(1), (highHalf, lowHalf) => new string(new[] { highHalf, lowHalf}))
	.Where((pair, index) => index % 2 == 0)
	.ToArray();
var red = Convert.ToByte(bytes[0], 16);
var green = Convert.ToByte(bytes[1], 16);
var blue = Convert.ToByte(bytes[2], 16);
if (opacity == null)
{
	Console.WriteLine($"rgb({red},{green},{blue})");
}
else
{
	Console.WriteLine($"rgba({red},{green},{blue},{opacity})");
}