<Query Kind="Program">
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

void Main()
{
	var sf = new SettingsFactory();
	var settings = sf.GetSettings();
	var folders = settings.Folders;
	folders += "/";
	Console.WriteLine($"Property returns value, cannot mutate. setting.Folders: {settings.Folders}.");
	Console.WriteLine($"Only the value copy is mutated. folders: {folders}.");
	
	var settingsByRef = sf.refSettings;
	var foldersByRef = settingsByRef.Folders;
	foldersByRef += "/";
	Console.WriteLine($"Even the field returns a value, cannot mutate the string property. settingByRef.Folders: {settingsByRef.Folders}.");
	Console.WriteLine($"Only the value copy is mutated. foldersByRef: {foldersByRef}.");
}

public class SettingsFactory
{
	public SettingsFactory()
	{
		refSettings = new Settings { Folders = "folders/structure" };
	}
	public Settings refSettings;
	public Settings GetSettings()
	{
		return new Settings { Folders = "itext-generated" };
	}
}

public class Settings
{
	public string Folders { get; set; } = "/";
}