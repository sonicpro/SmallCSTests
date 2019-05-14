using System;
using System.Collections.Generic;

namespace IEqualityComparerForLINQ
{
	class ActorComparer : IEqualityComparer<MovieActor>
	{
		public bool Equals(MovieActor x, MovieActor y)
		{
			// Prints a debug output to prove that this method is the last point to decide about the objects equality.
			Console.WriteLine(
				"Equals called on " +
				x.ToString() + " " +
				y.ToString());
			return
				x.LastName == y.LastName &&
				x.FirstName == y.FirstName &&
				x.CharacterName == y.CharacterName;
		}

		public int GetHashCode(MovieActor obj)
		{
			// Bad implementation; returns different hashes for the object that we deem to be equal.

			//Console.WriteLine(
			//	"Hash called on " +
			//	obj.ToString() +
			//	" (" + obj.GetHashCode() + ")");
			//return obj.GetHashCode();

			// Because LastName is quite selective in real-worls scenarios, we can sacrifice some performance for code simplicity and instead of
			// calculating the hash code based on all three name parts ((obj.FirstName + obj.LastName + obj.CharacterName).GetHashCode()), just calculate obj.LastName.GetHashCode().
			Console.WriteLine(
				"Hash called on " +
				obj.ToString() +
				" (" + obj.LastName.GetHashCode() + ")");
			return obj.LastName.GetHashCode();
		}
	}
}
