<Query Kind="Statements" />

byte[] a = new[] { (byte)0x41, (byte)0x40 };
using (var f = File.OpenWrite("C:\\temp\\a.txt"))
using (var s = new MemoryStream(a))
{
    s.CopyTo(f);
}