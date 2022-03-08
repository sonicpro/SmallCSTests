<Query Kind="Statements" />

var crs = System.Globalization.CultureInfo.GetCultures(System.Globalization.CultureTypes.SpecificCultures);
foreach (var c in crs)
{
	Console.WriteLine($"{c.DisplayName}: {c.Name}");
}