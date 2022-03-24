using System;
using System.Threading;
using System.Threading.Tasks;

namespace CancellingTaskDelay
{
    class Program
    {
        static void Main(string[] args)
        {
            var cts = new CancellationTokenSource();
            var holder = new MessagesHolder<string>();
            var t = Task.Run(async delegate
            {
                await holder.RequestMessages(cts.Token);
            });
            cts.Cancel();
            t.Wait();
        }
    }
}
