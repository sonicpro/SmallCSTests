using System;
using System.Collections.Generic;
using System.Text;

namespace DamnItOperator
{
	public sealed class Person
	{
		public string Name { get; }

		public Address HomeAddress { get; }

		public Person(string name, Address homeAddress)
		{
			Name = name ?? throw new ArgumentNullException(nameof(name));

			HomeAddress = homeAddress ?? throw new ArgumentNullException(nameof(homeAddress));
		}
	}
}
