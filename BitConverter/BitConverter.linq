<Query Kind="Statements" />

byte[] a = new byte[4];
a[3] = 0x0A; // 0x0A000000 - 10 * 16^6 Dec. Notice low-endian byte order (big end is stored last at the highest storage address). 
Console.WriteLine(BitConverter.ToInt32(a,0));