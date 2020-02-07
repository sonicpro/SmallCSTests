<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.ComponentModel.dll</Reference>
  <Namespace>System</Namespace>
  <Namespace>System.ComponentModel</Namespace>
</Query>

void Main()
{
	KnownSortOrders sortBy;
	Enum.TryParse("ASC", true, out sortBy);
	Console.WriteLine((int)sortBy);
}

// Define other methods and classes here
public enum KnownSortOrders
{
	Asc = 1,
	Dec = -1
}

public static class EnumExtension
{
	public static string GetDescription(this Enum value)
	{
		var enumField = value.GetType()
			.GetFields(BindingFlags.Public | BindingFlags.Static)
			.Single(x => x.GetValue(null).Equals(value));
		var attribute = ((DescriptionAttribute)Attribute.GetCustomAttribute(enumField, typeof(DescriptionAttribute)));
		if (attribute == null)
		{
			throw new ArgumentException(nameof(value));
		}
		return attribute.Description;
	}
}