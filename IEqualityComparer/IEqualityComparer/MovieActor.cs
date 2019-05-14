using System.Collections.Generic;

namespace IEqualityComparerForLINQ
{
	class MovieActor
	{
		public string LastName { get; set; }

		public string FirstName { get; set; }

		public string CharacterName { get; set; }

		#region System.Object overrides
		public override string ToString()
		{
			return string.Format(
				"{0} \"{1}\" {2}", FirstName, CharacterName, LastName);
		}

		#endregion

		public static List<MovieActor> CreateSome()
		{
			return new List<MovieActor>()
			{
				new MovieActor()
				{
					FirstName = "Brad",
					LastName = "Pitt",
					CharacterName = "Rusty"},
				new MovieActor() {
					FirstName = "Andy",
					LastName = "Garcia",
					CharacterName = "Terry"},
				new MovieActor() {
					FirstName = "George",
					LastName = "Clooney",
					CharacterName = "Dany"},
				new MovieActor() {
					FirstName = "Alec",
					LastName = "Baldwin",
					CharacterName = "Jack"},
				new MovieActor() {
					FirstName = "Julia",
					LastName = "Roberts",
					CharacterName = "Tess"}
			};
		}
	}
}
