using System;
using System.Collections.Generic;

namespace IEqualityComparerForLINQ
{
	// Assume that the object returned by KeySelector() overrides Object.Equals() correctly.
	class ActorComparerGeneralized : IEqualityComparer<MovieActor>
	{
		public Func<MovieActor, object> KeySelector { get; set; }

		public bool Equals(MovieActor x, MovieActor y)
		{
			return KeySelector(x).Equals(KeySelector(y));
		}

		public int GetHashCode(MovieActor obj)
		{
			return KeySelector(obj).GetHashCode();
		}
	}
}
