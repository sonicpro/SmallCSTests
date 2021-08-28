using System;
using System.Collections.Generic;
using System.Text;

namespace DamnItOperator
{
	public sealed class Delivery
	{
		public Person Recipient { get; }

		public Address Address { get; }

		// We just want to pass argument checking to the constructor with two parameters.
		// When Nullable is enabled for the project, the compiler produces warning on "recepient?.HomeAddress".
		public Delivery(Person recipient)
			: this(recipient, recipient?.HomeAddress!)
		{
		}

		public Delivery(Person recipient, Address address)
		{
			Recipient = recipient ?? throw new ArgumentNullException(nameof(recipient));
			Address = address ?? throw new ArgumentNullException(nameof(address));
		}
	}
}
