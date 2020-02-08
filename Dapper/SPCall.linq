<Query Kind="Program">
  <NuGetReference Version="1.50.2">Dapper</NuGetReference>
  <Namespace>Dapper</Namespace>
</Query>

// using Dapper 1.50.2
void Main()
{
	IEnumerable<Make> make;
	using (var connection = new SqlConnection("Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=KendoDreamCarShopper.Models.EntityStore;Integrated Security=True;"))
	{
		connection.Open();
		GetByIdParameters prm = new GetByIdParameters();
		make = connection.Query<Make>("GetMakeById",
			prm,
			commandType: CommandType.StoredProcedure);
		Console.WriteLine(make.SingleOrDefault()?.Name);
	}
}

// Define other methods and classes here
public class Make
{
	public int Id { get; set; }
	public string Name { get; set; }
	public string ImageUrl { get; set; }
	public string Location { get; set; }
}

public class GetByIdParameters
{
	public int? Id { get; set; }
}