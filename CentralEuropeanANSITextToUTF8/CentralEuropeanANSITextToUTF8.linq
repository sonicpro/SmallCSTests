<Query Kind="Statements" />

using (var writer = new StreamWriter("C:\\Workitems\\283675\\Translations\\Czech_unicode.txt", false, Encoding.UTF8))
{
    var text = File.ReadLines("C:\\Workitems\\283675\\Translations\\Czech.txt", Encoding.GetEncoding(1250));
    foreach (var line in text)
    {
        writer.WriteLine(line);
    }
}