<Query Kind="Statements">
  <Namespace>System.Collections.ObjectModel</Namespace>
</Query>

ReadOnlyCollection<string> dinos = new ReadOnlyCollection<string>(new[] { "Veloc", "Trex" });
string[] array = new string[dinos.Count + 2];
dinos.CopyTo(array, 1);
Console.WriteLine(array.Length);
foreach(var d in array)
{
    Console.WriteLine(d);
}