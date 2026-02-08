using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOG.TextFileSearch.Entity
{
	public class FormState
	{
		public string Folder { get; set; } = string.Empty;
		public bool IncludeSubfolders { get; set; } = false;
		public string IgnoredFolders { get; set; } = string.Empty;
		public bool IncludeHidden { get; set; } = false;
		public bool IncludeSystem { get; set; } = false;
		public string FilePatterns { get; set; } = string.Empty;
		public string SearchText { get; set; } = string.Empty;
		public bool SearchAsRegex { get; set; }
	}
}
