<Query Kind="Statements" />

using var reader = new System.IO.FileStream("D:\\Workitems\\285534\\Encrypted backup\\data", FileMode.Open, FileAccess.Read);
Console.WriteLine($"Length: {reader.Length}");
Console.WriteLine($"Position: {reader.Position}");
reader.Seek(0, SeekOrigin.End);
Console.WriteLine($"Position: {reader.Position}");
Console.WriteLine($"Can seek: {reader.CanSeek}");
Console.WriteLine($"Can read: {reader.CanRead}");
byte[] a = new byte[5];
var bytes = await reader.ReadAsync(a, 0, 5);
Console.WriteLine($"Bytes read: {bytes}");
reader.Seek(2000000, SeekOrigin.Begin);

Console.WriteLine($"Position: {reader.Position}");