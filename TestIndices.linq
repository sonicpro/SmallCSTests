<Query Kind="Statements" />

var a = Enumerable.Range(1, 100);
var numberOfFakeCamerasPerCamera = 2;
var cameras = new[] {"cam1", "cam2", "cam3" };
var enumerator = a.GetEnumerator();
var loopIndex = 0;
foreach (string c in cameras)
{
    var fakeCameraIndices = a.Skip(loopIndex * numberOfFakeCamerasPerCamera).Take(numberOfFakeCamerasPerCamera);
    foreach (int index in fakeCameraIndices)
    {
        Console.WriteLine($"{c}({index})");
    }
    loopIndex++;
}