using System.Text.Json;
using Newtonsoft.Json;

namespace BOG.TextFileSearch.Entity
{
	[JsonObject(MemberSerialization.OptIn)]
	public class EditorProgram
	{
		[JsonProperty(
			ObjectCreationHandling = ObjectCreationHandling.Replace,
			Required = Required.Always)]
		public string EditorName { get; set; } = string.Empty;

		[JsonProperty(
			ObjectCreationHandling = ObjectCreationHandling.Replace,
			Required = Required.Always)]
		public string ExeFile { get; set; } = string.Empty;

		[JsonProperty(
			ObjectCreationHandling = ObjectCreationHandling.Replace,
			Required = Required.Always)]
		public string CommandLineArguments { get; set; } = string.Empty;

		[JsonProperty(
			ObjectCreationHandling = ObjectCreationHandling.Replace,
			Required = Required.Always)]
		public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;

		[JsonProperty(
			ObjectCreationHandling = ObjectCreationHandling.Replace,
			NullValueHandling = NullValueHandling.Include,
			Required = Required.Default)]
		public DateTime? UpdatedAtUtc { get; set; }
	}

	[JsonObject(MemberSerialization.OptIn)]
	public class ConfigOptions
	{
		[JsonProperty(
			ObjectCreationHandling = ObjectCreationHandling.Replace,
			Required = Required.Always)]
		public Dictionary<string, EditorProgram> EditorPrograms { get; set; } = new Dictionary<string, EditorProgram>();

		// key: extention (e.g., .txt), value: editor program config object (e.g., Notepad++)
		[JsonProperty(
			ObjectCreationHandling = ObjectCreationHandling.Replace,
			Required = Required.Always)]
		public Dictionary<string, List<string>> EditorMappings { get; set; } = new Dictionary<string, List<string>>();
	}

	public static class ConfigOptionsFactory
	{
		public static ConfigOptions CreateDefaultObject()
		{
			var NotePadPlusPlus = new EditorProgram
			{
				EditorName = "Notepad++",
				ExeFile = "\"C:\\Program Files\\Notepad++\\notepad++.exe\"",
				CommandLineArguments = "#{target_file}# -n#{line_number}#",
				CreatedAtUtc = DateTime.UtcNow
			};
			var SqlSMS = new EditorProgram
			{
				EditorName = "SQl Server Management Studio",
				ExeFile = "\"C:\\Program Files (x86)\\Microsoft SQL Server Management Studio 20\\Common7\\IDE\\Ssms.exe\"",
				CommandLineArguments = "#{target_file}#",
				CreatedAtUtc = DateTime.UtcNow
			};
			var result = new ConfigOptions
			{
				EditorPrograms = new Dictionary<string, EditorProgram>
				{
					{ "Notepad++", NotePadPlusPlus},
					{ "SQl Server Management Studio", SqlSMS }
				},
				EditorMappings = new Dictionary<string, List<string>>
				{
					{ ".txt", new List<string>( new string[] {"Notepad++" })},
					{ ".log",  new List<string>( new string[] {"Notepad++" })},
					{ ".md",  new List<string>( new string[] {"Notepad++" })},
					{ ".sql",  new List<string>( new string[] {"Notepad++" ,"SqlSMS"})},
				}
			};
			return result;
		}
	}
}
