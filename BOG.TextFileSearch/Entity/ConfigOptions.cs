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
		public string[] Extensions { get; set; } = new string[] { "*" };

		[JsonProperty(
			ObjectCreationHandling = ObjectCreationHandling.Replace,
			Required = Required.Default)]
		public bool EditorExists { get; set;  } = false;

		[JsonProperty(
			ObjectCreationHandling = ObjectCreationHandling.Replace,
			Required = Required.Always)]
		public DateTime Created { get; set; } = DateTime.UtcNow;

		[JsonProperty(
			ObjectCreationHandling = ObjectCreationHandling.Replace,
			NullValueHandling = NullValueHandling.Include,
			Required = Required.Default)]
		public DateTime Updated { get; set; } = DateTime.UtcNow;
	}

	[JsonObject(MemberSerialization.OptIn)]
	public class ConfigOptions
	{
		[JsonProperty(
			ObjectCreationHandling = ObjectCreationHandling.Replace,
			Required = Required.Always)]
		public List<EditorProgram> EditorPrograms { get; set; } = new List<EditorProgram>();

		[JsonProperty(
			ObjectCreationHandling = ObjectCreationHandling.Replace,
			Required = Required.Always)]
		public DateTime Created { get; set; } = DateTime.UtcNow;

		[JsonProperty(
			ObjectCreationHandling = ObjectCreationHandling.Replace,
			NullValueHandling = NullValueHandling.Include,
			Required = Required.Default)]
		public DateTime Updated { get; set; } = DateTime.UtcNow;
	}

	public static class ConfigOptionsFactory
	{
		public static ConfigOptions CreateDefaultObject()
		{
			var result = new ConfigOptions
			{
				EditorPrograms = new List<EditorProgram>
				{
					new EditorProgram
					{
						EditorName = "Notepad",
						ExeFile = "C:\\Windows\\notepad.exe",
						CommandLineArguments = "\"#{target_file}#\"",
						Extensions = new string[] { "*" },
						Created = DateTime.UtcNow,
						Updated = DateTime.UtcNow
					},
					new EditorProgram
					{
						EditorName = "Notepad++",
						ExeFile = "C:\\Program Files\\Notepad++\\notepad++.exe",
						CommandLineArguments = "\"#{target_file}#\" -n#{line_number}# -multiInst",
						Extensions = new string[] { ".txt", ".log", ".md", ".xml", ".xsl", ".xslt", ".xaml", "json", ".cs", ".sql" },
						Created = DateTime.UtcNow,
						Updated = DateTime.UtcNow
					},
					new EditorProgram
					{
						EditorName = "SQl Server Management Studio",
						ExeFile = "C:\\Program Files (x86)\\Microsoft SQL Server Management Studio 20\\Common7\\IDE\\SSMS.exe",
						CommandLineArguments = "\"#{target_file}#\"",
						Created = DateTime.UtcNow,
						Updated = DateTime.UtcNow
					}
				}
			};
			return result;
		}

		public static void MarkExisitngEditorPrograms(ref ConfigOptions currentObj)
		{
			foreach (var editorProgram in currentObj.EditorPrograms)
			{
				if (System.IO.File.Exists(editorProgram.ExeFile.Trim()))
				{
					editorProgram.EditorExists = true;
				}
			}
		}
	}
}