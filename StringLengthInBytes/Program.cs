var testedEncodings = new[]
{
    Encoding.ASCII,   // Note that '🡪' cannot be encoded in ASCII, data loss will occur
    Encoding.UTF8,    // This should always be your choice nowadays
    Encoding.Unicode, // This is UTF-16. It is used by .NET to store your strings in RAM when the application is running, but this isn't useful information unless you're trying to manipulate bytes in RAM
    Encoding.UTF32
};

string text = "aЛ";

Console.WriteLine($"Tested string: {text}");
Console.WriteLine($"String length: {text.Length}");
Console.WriteLine();

PrintTableHeader("Encoding", "Bytes", "Decoded string");

foreach (var encoding in testedEncodings)
{
    byte[] bytes = encoding.GetBytes(text);
    string decodedString = encoding.GetString(bytes);

    PrintTableRow(
        encoding.EncodingName,
        $"{bytes.Length} ({string.Join(' ', bytes)})",
        decodedString);
}

static void PrintTableHeader(params string[] values)
{
    PrintTableRow(values);
    Console.WriteLine(new string('-', 60));
}

static void PrintTableRow(params string[] values)
{
    Console.WriteLine("{0,-16} | {1,-24} | {2}", values);
}