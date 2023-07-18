<Query Kind="Program" />

void Main()
{
    loopThread = new Thread(Loop) ;
    loopThread.Start();
}

// Define other methods and classes here
private Thread loopThread;
private void Loop()
{
    while (true)
    {
        try
        {
            throw new System.Exception();
        }
        catch
        {
            Console.WriteLine("Again here");
            // No need to abort and produce more threads. Just continue.
            continue;
        }
    }
}