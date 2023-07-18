<Query Kind="Statements" />

byte[] a = new[] { 
    (byte)115,
    (byte)116,
    (byte)114,
    (byte)105,
    (byte)110,
    (byte)103,
    (byte)32,
    (byte)112,
    (byte)97,
    (byte)116,
    (byte)116,
    (byte)101,
    (byte)114,
    (byte)110,
    };
using (var f = File.OpenWrite("C:\\temp\\a.txt"))
using (var s = new MemoryStream(a))
{
    s.CopyTo(f);
}