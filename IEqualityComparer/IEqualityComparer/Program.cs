using System;
using System.Linq;

// Inspired by Alex Turok's article https://www.codeproject.com/Articles/762203/Csharp-LINQ-and-IEqualityComparer.

namespace IEqualityComparerForLINQ
{
	class Program
	{
		static void Main(string[] args)
		{
			var actors = MovieActor.CreateSome();
			actors.Add(new MovieActor()
			{
				FirstName = "George",
				LastName = "Clooney",
				CharacterName = "Dany"
			});
			actors.Add(new MovieActor()
			{
				FirstName = "William",
				LastName = "Baldwin",
				CharacterName = "Joe"
			});

			NaiveDistinctActorPrinter(actors);
			DistinctActorPrinter(actors);
			Console.ReadKey();
		}

		private static void DistinctActorPrinter(System.Collections.Generic.List<MovieActor> actors)
		{
			// Demo for the first part of Alex's article.
			//var distinct = actors.Distinct(new ActorComparer());

			// This KeySelector leaves only the first of Baldwin brothers in the output.
			var distinct = actors.Distinct(new ActorComparerGeneralized() { KeySelector = actor => actor.LastName });
			Console.WriteLine(String.Format("\n{0} distinct actors.", distinct.Count()));

			foreach (var actor in distinct)
			{
				Console.WriteLine(actor);
			}

			// This KeySelector leave both Alec and William in the output.
			distinct = actors.Distinct(new ActorComparerGeneralized() { KeySelector = actor => new { actor.FirstName, actor.LastName } });
			Console.WriteLine(String.Format("\n{0} distinct actors.", distinct.Count()));

			foreach (var actor in distinct)
			{
				Console.WriteLine(actor);
			}
		}

		private static void NaiveDistinctActorPrinter(System.Collections.Generic.List<MovieActor> actors)
		{
			// Prints "7 distinct actors" in the second output line.
			var distinct = actors.Distinct();
			Console.WriteLine(
			 String.Format("\n{0} distinct actors.", distinct.Count()));

			foreach (var actor in distinct)
			{
				Console.WriteLine(actor);
			}
		}
	}
}
