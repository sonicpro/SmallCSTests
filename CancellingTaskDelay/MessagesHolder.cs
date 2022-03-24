using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CancellingTaskDelay
{
    public class MessagesHolder<T>
    {
        private readonly ConcurrentDictionary<string, T> dict = new();

        public void Add(string key, T item)
        {
            this.dict.TryAdd(key, item);
        }

        public async Task<IEnumerable<T>> RequestMessages(CancellationToken cancellationToken)
        {
            var list = new List<T>();
            while (this.dict.IsEmpty)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    break;
                }

                await Task.Delay(50, cancellationToken);
            }

            foreach (var pair in this.dict)
            {
                list.Add(pair.Value);
                this.dict.TryRemove(pair);
            }

            return list;
        }
    }
}
