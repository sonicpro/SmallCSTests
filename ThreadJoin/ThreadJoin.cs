using System;
using System.Threading;

public class Example
{
   static Thread thread1, thread2;

   public static void ExecuteInThreads()
   {
      thread1 = new Thread(ThreadProc);
      thread1.Name = "Thread1";
      thread1.Start();

      thread2 = new Thread(ThreadProc);
      thread2.Name = "Thread2";
      thread2.Start();
   }

   private static void ThreadProc()
   {
      Console.WriteLine("\nCurrent thread: {0}", Thread.CurrentThread.Name);
      if (Thread.CurrentThread.Name == "Thread1" && thread2.ThreadState != ThreadState.Unstarted)
      {
          Console.WriteLine("\nJoining Thread2 to the Thread1");
          thread2.Join();  // Blocks Thread1 until Thread2 executes ThreadProc and terminates.
      }

      Thread.Sleep(4000);
      Console.WriteLine("\nCurrent thread: {0}", Thread.CurrentThread.Name);
      Console.WriteLine("Thread1: {0}", thread1.ThreadState);
      Console.WriteLine("Thread2: {0}\n", thread2.ThreadState);
   }
}
