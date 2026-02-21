using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BOG.TextFileSearch.Entity
{
	[JsonObject(MemberSerialization.OptIn)]
	public class SearchMetric
	{
		[JsonProperty(
			ObjectCreationHandling = ObjectCreationHandling.Replace,
			Required = Required.Always)]
		public string Folder { get; set; } = string.Empty;

		[JsonProperty(
			ObjectCreationHandling = ObjectCreationHandling.Replace,
			Required = Required.Always)]
		public string FilePatterns { get; set; } = string.Empty;

		[JsonProperty(
			ObjectCreationHandling = ObjectCreationHandling.Replace,
			Required = Required.Always)]
		public string IgnoredFolders { get; set; } = string.Empty;

		[JsonProperty(
			ObjectCreationHandling = ObjectCreationHandling.Replace,
			Required = Required.Always)]
		public bool IncludeHidden { get; set; } = false;

		[JsonProperty(
			ObjectCreationHandling = ObjectCreationHandling.Replace,
			Required = Required.Always)]
		public bool IncludeSystem { get; set; } = false;

		[JsonProperty(
			ObjectCreationHandling = ObjectCreationHandling.Replace,
			Required = Required.Always)]
		public bool IncludeSubfolders { get; set; } = false;

		[JsonProperty(
			ObjectCreationHandling = ObjectCreationHandling.Replace,
			Required = Required.Default)]
		public bool FilterOnCreated { get; set; } = false;

		[JsonProperty(
			ObjectCreationHandling = ObjectCreationHandling.Replace,
			Required = Required.Default)]
		public DateTime FilterCreatedBeginDate { get; set; } = DateTime.MinValue;

		[JsonProperty(
			ObjectCreationHandling = ObjectCreationHandling.Replace,
			Required = Required.Default)]
		public DateTime FilterCreatedEndDate { get; set; } = DateTime.MaxValue;

		[JsonProperty(
			ObjectCreationHandling = ObjectCreationHandling.Replace,
			Required = Required.Default)]
		public bool FilterOnUpdated { get; set; } = false;

		[JsonProperty(
			ObjectCreationHandling = ObjectCreationHandling.Replace,
			Required = Required.Default)]
		public DateTime FilterUpdatedBeginDate { get; set; } = DateTime.MinValue;

		[JsonProperty(
			ObjectCreationHandling = ObjectCreationHandling.Replace,
			Required = Required.Default)]
		public DateTime FilterUpdatedEndDate { get; set; } = DateTime.MaxValue;

		[JsonProperty(
			ObjectCreationHandling = ObjectCreationHandling.Replace,
			Required = Required.Default)]
		public bool FilterOnAccessed { get; set; } = false;

		[JsonProperty(
			ObjectCreationHandling = ObjectCreationHandling.Replace,
			Required = Required.Default)]
		public DateTime FilterAccessedBeginDate { get; set; } = DateTime.MinValue;

		[JsonProperty(
			ObjectCreationHandling = ObjectCreationHandling.Replace,
			Required = Required.Default)]
		public DateTime FilterAccessedEndDate { get; set; } = DateTime.MaxValue;

		[JsonProperty(
			ObjectCreationHandling = ObjectCreationHandling.Replace,
			Required = Required.Always)]
		public string SearchText { get; set; } = string.Empty;

		[JsonProperty(
			ObjectCreationHandling = ObjectCreationHandling.Replace,
			Required = Required.Always)]
		public bool SearchAsRegex { get; set; } = false;

		[JsonProperty(
			ObjectCreationHandling = ObjectCreationHandling.Replace,
			Required = Required.Always)]
		public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;

		[JsonProperty(
			ObjectCreationHandling = ObjectCreationHandling.Replace,
			Required = Required.AllowNull)]
		public DateTime? UpdatedAtUtc { get; set; }
	}

	[JsonObject(MemberSerialization.OptIn)]
	public class FormState
	{
		[JsonProperty(
			ObjectCreationHandling = ObjectCreationHandling.Replace,
			Required = Required.Always)]
		public string ActiveSearchMetric { get; set; } = "(Default)";

		[JsonProperty(
			ObjectCreationHandling = ObjectCreationHandling.Replace,
			Required = Required.Always)]
		public Dictionary<string, SearchMetric> SearchMetricList { get; set; } = new();

		[JsonProperty(
			ObjectCreationHandling = ObjectCreationHandling.Replace,
			Required = Required.Always)]
		public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;

		[JsonProperty(
			ObjectCreationHandling = ObjectCreationHandling.Replace,
			Required = Required.AllowNull)]
		public DateTime? UpdatedAtUtc { get; set; } = null;
	}

	public static class FormStateFactory
	{
		public static FormState CreateDefaultObject()
		{
			var defaultSearchMetric = new SearchMetric
			{
				Folder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
				FilePatterns = "*.txt",
				IgnoredFolders = @"C:\Windows;C:\Program Files;C:\Program Files (x86)",
				IncludeHidden = false,
				IncludeSystem = false,
				IncludeSubfolders = true,
				FilterOnCreated = false,
				FilterCreatedBeginDate = DateTime.MinValue,
				FilterCreatedEndDate = DateTime.MaxValue,
				FilterOnUpdated = false,
				FilterUpdatedBeginDate = DateTime.MinValue,
				FilterUpdatedEndDate = DateTime.MaxValue,
				FilterOnAccessed = false,
				FilterAccessedBeginDate = DateTime.MinValue,
				FilterAccessedEndDate = DateTime.MaxValue,
				SearchText = "TODO",
				SearchAsRegex = false,
			};

			var result = new FormState
			{
				ActiveSearchMetric = "(Default)",
				SearchMetricList = new Dictionary<string, SearchMetric> { { "(Default)", defaultSearchMetric } },
				CreatedAtUtc = DateTime.UtcNow,
				UpdatedAtUtc = null
			};
			return result;
		}
	}
}