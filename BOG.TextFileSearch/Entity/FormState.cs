using System.Text.Json;
using BOG.SwissArmyKnife.Extensions;
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
		public DateTime FilterCreatedBeginDate { get; set; } = DateTime.MinValue.Date;

		[JsonProperty(
			ObjectCreationHandling = ObjectCreationHandling.Replace,
			Required = Required.Default)]
		public DateTime FilterCreatedEndDate { get; set; } = DateTime.MaxValue.Date;

		[JsonProperty(
			ObjectCreationHandling = ObjectCreationHandling.Replace,
			Required = Required.Default)]
		public bool FilterOnUpdated { get; set; } = false;

		[JsonProperty(
			ObjectCreationHandling = ObjectCreationHandling.Replace,
			Required = Required.Default)]
		public DateTime FilterUpdatedBeginDate { get; set; } = DateTime.MinValue.Date;

		[JsonProperty(
			ObjectCreationHandling = ObjectCreationHandling.Replace,
			Required = Required.Default)]
		public DateTime FilterUpdatedEndDate { get; set; } = DateTime.MaxValue.Date;

		[JsonProperty(
			ObjectCreationHandling = ObjectCreationHandling.Replace,
			Required = Required.Default)]
		public bool FilterOnAccessed { get; set; } = false;

		[JsonProperty(
			ObjectCreationHandling = ObjectCreationHandling.Replace,
			Required = Required.Default)]
		public DateTime FilterAccessedBeginDate { get; set; } = DateTime.MinValue.Date;

		[JsonProperty(
			ObjectCreationHandling = ObjectCreationHandling.Replace,
			Required = Required.Default)]
		public DateTime FilterAccessedEndDate { get; set; } = DateTime.MaxValue.Date;

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
		public DateTime Created { get; set; } = DateTime.Now;

		[JsonProperty(
			ObjectCreationHandling = ObjectCreationHandling.Replace,
			Required = Required.AllowNull)]
		public DateTime? Updated { get; set; }
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
		public DateTime Created { get; set; } = DateTime.Now;

		[JsonProperty(
			ObjectCreationHandling = ObjectCreationHandling.Replace,
			Required = Required.AllowNull)]
		public DateTime? Updated { get; set; } = null;
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
				Created = DateTime.Now,
				Updated = null
			};
			return result;
		}
	}

	public static class FormStateHelper
	{
		public static bool FormStateChanged(FormState formState)
		{
			// get the latest create date or update date of every SerchMetric in the FormState object
			var searchMetricsLatestDate =
				(formState.SearchMetricList.Values.Select(x => x.Created).Max())
				.Latest(new DateTime[] { formState.SearchMetricList.Values.Select(x => x.Updated).Max() ?? DateTime.MinValue });

			// get the latest create date or update date of the FormState object.
			var formStateLatestDate = formState.Created.Latest(new DateTime[] { formState.Updated ?? DateTime.MinValue });

			var result = searchMetricsLatestDate > formStateLatestDate;

			return result;
		}
	}
}