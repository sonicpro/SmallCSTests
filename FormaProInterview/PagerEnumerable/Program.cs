using System;
using System.Collections.Generic;
using System.Linq;

namespace PagerEnumerable
{
	class Program
	{
		static void Main(string[] args)
		{
			// Given 48 data items which are sequential numbers starting at 1.
			var data = Enumerable.Range(1, 48);
			// Split by 10 items per page and get the page 5 (items from 41 to 50).
			var page5 = GetPagesEnumerable(data, 10).Skip(4).First();
			page5.Select((elem, index) => new { elem, index }).ToList().ForEach(anon =>
				Console.WriteLine($"Element {anon.index} : {anon.elem}"));
		}

		private static IEnumerable<T[]> GetPagesEnumerable<T>(IEnumerable<T> data, int pageSize)
		{
			T[] chunk = new T[pageSize];
			int pageIndex = 0;
			while (chunk.Length == pageSize)
			{
				chunk = data.Skip(pageSize * pageIndex).Take(pageSize).ToArray();
				pageIndex++;
				yield return chunk;
			}
		}
	}
}
