<Query Kind="Statements" />

var ci = new System.Globalization.CultureInfo("fi");

System.Threading.Thread.CurrentThread.CurrentUICulture = ci;
System.Threading.Thread.CurrentThread.CurrentUICulture.Dump();
//Console.WriteLine(System.Threading.Thread.CurrentThread.CurrentUICulture.

