<Query Kind="Program">
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

void Main()
{
    Console.WriteLine(FailingRoutineWrapper().Result);
}

// You can define other methods, fields, classes and namespaces here
private Task<int> FailingRoutineWrapper()
{
    return Task.Run(() => FailingRoutine());
}
private int FailingRoutine()
{
    throw new System.Exception();
    return 1;
}