using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Parsing
{
    class Program
    {
        private static string Directory = System.IO.Directory.GetCurrentDirectory();

        static void Main(string[] args)
        {
            foreach (var fileAndDummyTypedValue in Files())
            {
                DeserializeRootElement(fileAndDummyTypedValue.Item1, fileAndDummyTypedValue.Item2);
            }
        }

        private static void DeserializeRootElement<T>(string fileName, T type)
        {
            using var fileStream = File.OpenRead(Path.Combine(Directory, fileName));
            var document = JsonDocument.Parse(fileStream, new JsonDocumentOptions());
            var root = document.RootElement;
            Console.WriteLine($"JsonValueKind: {root.ValueKind}. Value: {root.ToObject<T>()}");
        }

        private static IEnumerable<(string, object)> Files()
        {
            yield return ("stringJson.json", string.Empty);
            yield return ("trueJson.json", true);
            yield return ("numberJson.json", 1e+10m);
        }
    }
}
