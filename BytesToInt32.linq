<Query Kind="Statements" />

byte[] bytes = { 255, 255,255,255  };
if(BitConverter.IsLittleEndian)
Array.Reverse(bytes);
Console.WriteLine(BitConverter.ToUInt32(bytes));