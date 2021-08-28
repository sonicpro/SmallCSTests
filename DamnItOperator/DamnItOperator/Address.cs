using System;

/// <summary>
/// This is from Jon Skeet blog's https://codeblog.jonskeet.uk/2019/05/25/lying-to-the-compiler/
/// To make the compiler produce warning at safe navigation operator, enable Nullable on Build tab of csproj.
/// </summary>
namespace DamnItOperator
{
	public sealed class Address
	{
		public string AddressLine1 { get; set; }

		public string AddressLine2 { get; set; }

		public string PostCode { get; set; }
	}
}
