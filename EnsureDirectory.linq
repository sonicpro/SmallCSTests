<Query Kind="Program" />

void Main()
{
    
    EnsureDirectory("temp");
}

// Define other methods and classes here
private static void EnsureDirectory(string directoryName)
{
    var directory = Path.Combine(new FileInfo(Assembly.GetExecutingAssembly().Location).DirectoryName, directoryName);
    Directory.CreateDirectory(directory);
}
