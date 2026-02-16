using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using BOG.SwissArmyKnife;
using BOG.TextFileSearch.Entity;

namespace BOG.TextFileSearch
{
	public partial class MainForm : Form
	{
		private const string FormStateFileName = "state_persist.json";
		private const string ConfigEditorFileName = "config.json";

		private const string ResultsFoundFormat = "Occurrences found: {0}. Showing {1} lines.";

		private const string CopiedLineFormat = "File: {0}\nLine: {1}\nContent: {2}";
		private const string EmptyFieldAlertFormat = "The {0} field must have a value.";
		private const string ResultsTimeFormat = "Results returned in: {0}";
		private const string ErrorMessageFormat = "An error occurred while processing your search. Error: {0}";
		private const string RegexTestString = "S";
		private const int MaxLineSize = 250;
		private const int MaxItemsInlvwFound = 0;
		private const int NumberOfBreakLineCharsToSkip = 2;

		private static string PathDelimiter = "\\";

		private readonly Stopwatch Stopwatch = new();

		private string AppDataFolderPath = string.Empty;

		private string FormStateFile = string.Empty;
		private string ConfigEditorFile = string.Empty;

		private string FormStateFileHA256Hash = string.Empty;
		private string ConfigEditorFileHA256Hash = string.Empty;

		private bool FormStateChanged = false;
		private bool ConfigChanged = false;

		private FormState _FormStateObj = FormStateFactory.CreateDefaultObject();
		private ConfigOptions _ConfigOptionsObj = ConfigOptionsFactory.CreateDefaultObject();

		private List<FileMatchInformation> _ListOfFileMatches = new();
		private List<FileErrorInformation> _ListOfErrors = new();
		private Dictionary<string, FileOccurrence> _FileSearchResults = new();
		private List<string> _AllFileNames = new();
		private string[] _FilePatterns = new string[1];
		private string[] _IgnoredDirectories = new string[1];
		private int _TotalOccurrences = 0;
		private bool _ReachedLimit = false;

		public MainForm()
		{
			InitializeComponent();

			PathDelimiter = Environment.OSVersion.Platform == PlatformID.Win32NT ? "\\" : "/";
			AppDataFolderPath = Path.Combine(
				Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
				"Bits Of Genius",
				"BOG.TextFileSearch");
			if (!Directory.Exists(AppDataFolderPath))
			{
				try
				{
					Directory.CreateDirectory(AppDataFolderPath);
				}
				catch (Exception ex)
				{
					MessageBox.Show(
						DetailedException.WithMachineContent(
							ref ex,
							$"An error occurred while trying to create the application data folder:  {AppDataFolderPath}",
							"Can't not start the application"
						),
						"Folder not created",
						MessageBoxButtons.OK,
						MessageBoxIcon.Error
					);
				}
			}
			FormStateFile = Path.Combine(AppDataFolderPath, FormStateFileName);
			ConfigEditorFile = Path.Combine(AppDataFolderPath, ConfigEditorFileName);
		}

		private async void Search(object sender, EventArgs e)
		{
			if (!TextBoxValidate(txtFilePatterns))
			{
				return;
			}

			if (!TextBoxValidate(txtFolder))
			{
				return;
			}

			if (!TextBoxValidate(txtSearchPattern))
			{
				return;
			}

			if (chkSearchAsRegex.Checked && !RegexPatternIsValid(txtSearchPattern.Text))
			{
				MessageBox.Show("The search pattern is not a valid Regex pattern.", "Warning");
				return;
			}

			if (string.IsNullOrWhiteSpace(txtFilePatterns.Text)) return;

			var filePatterns = txtFilePatterns.Text.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
			if (filePatterns.Contains("*.*")) return;

			if (!Directory.Exists(txtFolder.Text))
				return;

			Search(txtFolder.Text, txtSearchPattern.Text);
		}

		public bool RegexPatternIsValid(string pattern)
		{
			try
			{
				_ = Regex.IsMatch(RegexTestString, pattern);
				return true;
			}
			catch
			{
				return false;
			}
		}

		public static bool TextBoxValidate(TextBox element)
		{
			if (string.IsNullOrEmpty(element.Text) || string.IsNullOrWhiteSpace(element.Text))
			{
				MessageBox.Show(
				string.Format(EmptyFieldAlertFormat, element.Tag),
				"Warning");
				return false;
			}
			return true;
		}

		private void SearchProcess(string folder, string search)
		{
			_AllFileNames.Clear();
			_ListOfFileMatches.Clear();
			_FileSearchResults.Clear();

			if (ContainsIgnoredDirectories(folder, _IgnoredDirectories)) return;

			foreach (string pattern in _FilePatterns)
			{
				toolStripStatusLabel1.Text = $"Loading file list {pattern} in {folder}";
				this.statusStrip1.Refresh();
				try
				{
					_AllFileNames.AddRange(
						Directory.GetFiles(folder, pattern, new EnumerationOptions
						{
							MatchCasing = MatchCasing.PlatformDefault,
							RecurseSubdirectories = false,
							IgnoreInaccessible = true,
							AttributesToSkip = (!chkIncludeHidden.Checked ? FileAttributes.Hidden : 0) | (!chkIncludeSystem.Checked ? FileAttributes.System : 0)
						})
					);
				}
				catch (Exception ex)
				{
					_ListOfErrors.Add(new FileErrorInformation
					{
						FileName = pattern,
						Error = ex.InnerException?.Message ?? ex.Message
					});
				}
			}

			foreach (string filePath in _AllFileNames)
			{
				if (string.IsNullOrEmpty(filePath) || ContainsIgnoredDirectories(filePath, _IgnoredDirectories))
					continue;

				string fileContent = File.ReadAllTextAsync(filePath).GetAwaiter().GetResult();
				List<int> indexes = new();
				if (chkSearchAsRegex.Checked)
				{
					MatchCollection matches = Regex.Matches(fileContent, txtSearchPattern.Text);
					for (var index = 0; index < matches.Count; index++)
					{
						indexes.Add(matches[index].Index);
					}
				}
				else
				{
					var offset = 0;
					while ((offset = fileContent.IndexOf(search, offset, StringComparison.OrdinalIgnoreCase)) != -1)
					{
						indexes.Add(offset);
						offset += search.Length;
					}
				}
				_TotalOccurrences += indexes.Count;

				if (indexes.Count > 0)
				{
					_FileSearchResults.Add(filePath, new FileOccurrence(fileContent, indexes));
				}
			}

			foreach (KeyValuePair<string, FileOccurrence> file in _FileSearchResults)
			{
				if (_ReachedLimit)
					break;

				Dictionary<string, List<int>> fileNameToLineNumber = new();

				fileNameToLineNumber.TryAdd(file.Key, new List<int>());

				foreach (int idx in file.Value.MatchIndexes)
				{
					if (MaxItemsInlvwFound > 0 && _ListOfFileMatches.Count > MaxItemsInlvwFound)
					{
						_ReachedLimit = true;
						break;
					}

					int lineNumber = default;
					string lineContent = string.Empty;

					var hasSuccess = TryReadWholeLine(
								file.Value.FileContent,
								idx,
								out lineNumber,
								out lineContent);
					if (!hasSuccess) break;

					if (fileNameToLineNumber[file.Key].Contains(lineNumber)) continue;

					_ListOfFileMatches.Add(new(file.Key, lineNumber, lineContent));

					fileNameToLineNumber[file.Key].Add(lineNumber);
				}
			}
			if (_ListOfFileMatches.Count > 0)
			{
				UpdatelvwFound(_ListOfFileMatches);
				_ListOfFileMatches.Clear();
			}
			if (chkRecurse.Checked)
			{
				foreach (var subdir in Directory.GetDirectories(folder, "*", SearchOption.TopDirectoryOnly))
				{
					SearchProcess(subdir, search);
				}
			}
		}

		private void Search(string folder, string search)
		{
			lvwFound.Items.Clear();
			lvwErrors.Items.Clear();
			this.Refresh();

			try
			{
				Stopwatch.Start();

				_FileSearchResults.Clear();
				_FilePatterns = (txtFilePatterns.Text ?? string.Empty).Split(';');
				_IgnoredDirectories = (txtIgnoreFolders.Text ?? string.Empty).Split(';');
				_ReachedLimit = false;
				_TotalOccurrences = 0;

				SearchProcess(folder, search);
			}
			catch (Exception ex)
			{
				MessageBox.Show(
					string.Format(
						ErrorMessageFormat,
						ex.InnerException?.Message ?? ex.Message),
						"Error"
					);
			}
			finally
			{
				UpdatelvwFound(_ListOfFileMatches);

				Stopwatch.Stop();

				lvwFound.AutoResizeColumn(0, ColumnHeaderAutoResizeStyle.HeaderSize);
				lvwFound.AutoResizeColumn(1, ColumnHeaderAutoResizeStyle.HeaderSize);

				toolStripStatusLabel1.Text = $"Done ... {string.Format(ResultsFoundFormat, _TotalOccurrences, lvwFound.Items.Count.ToString())} ... {UpdateResultsReturnedLabelWithStopwatchElapsedTime()}";
				this.statusStrip1.Refresh();
			}
		}

		public static bool ContainsIgnoredDirectories(string filePath, string[] ignoredDirectories)
		{
			foreach (var ignoredDir in ignoredDirectories)
			{
				if (string.IsNullOrWhiteSpace(ignoredDir)) continue;
				if (filePath.IndexOf(PathDelimiter + ignoredDir.Trim() + PathDelimiter) >= 0)
				{
					return true;
				}
			}

			return false;
		}

		public static bool TryReadWholeLine(string input, int matchIndex, out int lineNumber, out string lineContent)
		{
			try
			{
				lineNumber = 1;

				int idx = 0;
				int matchLineIndex = 0;

				while (idx != matchIndex)
				{
					if (input[idx] == '\r')
					{
						lineNumber++;
						matchLineIndex = idx + NumberOfBreakLineCharsToSkip;
					}

					idx++;
				}

				StringBuilder lineContentStringBuilder = new();

				while (input[matchLineIndex] != '\r')
				{
					if (lineContentStringBuilder.Length == MaxLineSize)
						break;

					if (matchLineIndex == input.Length - 1)
					{
						lineContentStringBuilder.Append(input[matchLineIndex]);
						break;
					}

					lineContentStringBuilder.Append(input[matchLineIndex]);

					matchLineIndex++;
				}

				lineContent = lineContentStringBuilder.ToString().TrimStart();

				return true;
			}
			catch
			{
				lineNumber = default;
				lineContent = string.Empty;
				return false;
			}
		}

		private string UpdateResultsReturnedLabelWithStopwatchElapsedTime()
		{
			return string.Format(ResultsTimeFormat, Stopwatch.Elapsed.ToString(@"hh\:mm\:ss"));
		}

		private void UpdatelvwFound(List<FileMatchInformation> listLineNumberAndContent)
		{
			foreach (FileMatchInformation file in listLineNumberAndContent)
			{
				ListViewItem item = new(file.FileName);

				item.SubItems.Add(file.LineNumber.ToString());

				item.SubItems.Add(file.LineContent);

				lvwFound.Items.Add(item);
			}
			this.lvwFound.Refresh();
		}

		#region Events

		private async void OnFormLoad(object sender, EventArgs e)
		{
			lvwFound.View = View.Details;
			lvwFound.FullRowSelect = true;
			lvwFound.GridLines = true;

			lvwFound.Columns.Add("File", 400, HorizontalAlignment.Left);
			lvwFound.Columns.Add("Line", 100, HorizontalAlignment.Left);
			lvwFound.Columns.Add("Match", 1200, HorizontalAlignment.Left);

			lvwErrors.View = View.Details;
			lvwErrors.FullRowSelect = true;
			lvwErrors.GridLines = true;

			lvwErrors.Columns.Add("File", 400, HorizontalAlignment.Left);
			lvwErrors.Columns.Add("Error", 1300, HorizontalAlignment.Left);

			ContextMenuStrip = new();

			if (File.Exists(ConfigEditorFile))
			{
				LoadConfigOptions();
			}
			else
			{
				SaveConfigOptions();
			}
			//AddToolStripMenuItemToContextMenuStrip($"Open in {_ConfigOptionsObj.EditorPrograms[0].EditorName}", OpenInProgram_Click);
			AddToolStripMenuItemToContextMenuStrip($"Open in NotePad++", OpenInProgram_Click);
			AddToolStripMenuItemToContextMenuStrip("Copy File Path", CopyFilePathToClipboard_Click);
			AddToolStripMenuItemToContextMenuStrip("Copy Formatted", CopyFormattedContentToClipboard_Click);

			if (File.Exists(FormStateFile))
			{
				LoadFormState();
			}
			else
			{
				SaveFormState();
			}
			HydrateSearchMetricDropDown();
			SetFormFromState(_FormStateObj.ActiveSearchMetric);
			FormStateChanged = false;
		}

		private void LoadConfigOptions()
		{
			if (File.Exists(ConfigEditorFile))
			{
				_ConfigOptionsObj = ObjectJsonSerializer<ConfigOptions>.LoadDocumentFormat(ConfigEditorFile);
				ConfigChanged = false;
			}
		}

		private void SaveConfigOptions()
		{
			ObjectJsonSerializer<ConfigOptions>.SaveDocumentFormat(_ConfigOptionsObj, ConfigEditorFile, true);
			ConfigChanged = false;
		}

		private void LoadFormState()
		{
			if (File.Exists(FormStateFile))
			{
				_FormStateObj = ObjectJsonSerializer<FormState>.LoadDocumentFormat(FormStateFile);
				FormStateChanged = false;
			}
		}

		private void SaveFormState()
		{
			_FormStateObj.UpdatedAtUtc = DateTime.UtcNow;
			ObjectJsonSerializer<FormState>.SaveDocumentFormat(_FormStateObj, FormStateFile, true);
			FormStateChanged = false;
		}

		private void SetFormFromState()
		{
			SetFormFromState(_FormStateObj.ActiveSearchMetric);
		}

		// Puts the values of the selected searchMetric object into the form fields.
		// If the name is not found, an error message is shown.
		private void SetFormFromState(string searchMetricName)
		{
			if (_FormStateObj.SearchMetricList.Keys.Contains(searchMetricName))
			{
				txtFolder.Text = _FormStateObj.SearchMetricList[searchMetricName].Folder;
				chkRecurse.Checked = _FormStateObj.SearchMetricList[searchMetricName].IncludeSubfolders;
				chkIncludeHidden.Checked = _FormStateObj.SearchMetricList[searchMetricName].IncludeHidden;
				chkIncludeSystem.Checked = _FormStateObj.SearchMetricList[searchMetricName].IncludeSystem;
				txtIgnoreFolders.Text = _FormStateObj.SearchMetricList[searchMetricName].IgnoredFolders;
				txtFilePatterns.Text = _FormStateObj.SearchMetricList[searchMetricName].FilePatterns;
				txtSearchPattern.Text = _FormStateObj.SearchMetricList[searchMetricName].SearchText;
				chkSearchAsRegex.Checked = _FormStateObj.SearchMetricList[searchMetricName].SearchAsRegex;

				_FormStateObj.ActiveSearchMetric = searchMetricName;
				FormStateChanged = false;
				ObjectEnabling(Searching: false);
			}
			else
			{
				MessageBox.Show($"The search metric configuration with name {searchMetricName} could not be found.", "Error");
			}
		}

		// Puts the values in the form of the selected searchMetric into the form object.
		// If the name is not found, an error message is shown.
		private void SetStateFromForm(string searchMetricName)
		{
			if (!_FormStateObj.SearchMetricList.Keys.Contains(searchMetricName))
			{
				_FormStateObj.SearchMetricList.Add(searchMetricName, new SearchMetric());
			}
			_FormStateObj.SearchMetricList[searchMetricName].Folder = txtFolder.Text;
			_FormStateObj.SearchMetricList[searchMetricName].IncludeSubfolders = chkRecurse.Checked;
			_FormStateObj.SearchMetricList[searchMetricName].IncludeHidden = chkIncludeHidden.Checked;
			_FormStateObj.SearchMetricList[searchMetricName].IncludeSystem = chkIncludeSystem.Checked;
			_FormStateObj.SearchMetricList[searchMetricName].IgnoredFolders = txtIgnoreFolders.Text;
			_FormStateObj.SearchMetricList[searchMetricName].FilePatterns = txtFilePatterns.Text;
			_FormStateObj.SearchMetricList[searchMetricName].SearchText = txtSearchPattern.Text;
			_FormStateObj.SearchMetricList[searchMetricName].SearchAsRegex = chkSearchAsRegex.Checked;

			_FormStateObj.ActiveSearchMetric = searchMetricName;
			FormStateChanged = false;
			ObjectEnabling(Searching: false);
		}

		private void HydrateSearchMetricDropDown()
		{
			cbxSearchSetName.Items.Clear();
			foreach (string searchMetricName in _FormStateObj.SearchMetricList.Keys)
			{
				cbxSearchSetName.Items.Add(searchMetricName);
			}
			cbxSearchSetName.SelectedItem = _FormStateObj.ActiveSearchMetric;
		}

		private void SaveStateOfForm()
		{
			SaveStateOfForm(_FormStateObj.ActiveSearchMetric);
		}

		private void SaveStateOfForm(string searchMetricName)
		{
			if (_FormStateObj.SearchMetricList.Keys.Contains(searchMetricName))
			{
				_FormStateObj.SearchMetricList[searchMetricName].Folder = txtFolder.Text;
				_FormStateObj.SearchMetricList[searchMetricName].IncludeSubfolders = chkRecurse.Checked;
				_FormStateObj.SearchMetricList[searchMetricName].IncludeHidden = chkIncludeHidden.Checked;
				_FormStateObj.SearchMetricList[searchMetricName].IncludeSystem = chkIncludeSystem.Checked;
				_FormStateObj.SearchMetricList[searchMetricName].IgnoredFolders = txtIgnoreFolders.Text;
				_FormStateObj.SearchMetricList[searchMetricName].FilePatterns = txtFilePatterns.Text;
				_FormStateObj.SearchMetricList[searchMetricName].SearchText = txtSearchPattern.Text;
				_FormStateObj.SearchMetricList[searchMetricName].SearchAsRegex = chkSearchAsRegex.Checked;
				_FormStateObj.ActiveSearchMetric = searchMetricName;
				_FormStateObj.UpdatedAtUtc = DateTime.UtcNow;
			}
			else
			{
				var o = new SearchMetric
				{
					Folder = txtFolder.Text,
					IncludeSubfolders = chkRecurse.Checked,
					IncludeHidden = chkIncludeHidden.Checked,
					IncludeSystem = chkIncludeSystem.Checked,
					IgnoredFolders = txtIgnoreFolders.Text,
					FilePatterns = txtFilePatterns.Text,
					SearchText = txtSearchPattern.Text,
					SearchAsRegex = chkSearchAsRegex.Checked,
					CreatedAtUtc = DateTime.UtcNow
				};
				_FormStateObj.SearchMetricList.Add(searchMetricName, o);
				_FormStateObj.UpdatedAtUtc = DateTime.UtcNow;
			}
		}

		private void AddToolStripMenuItemToContextMenuStrip(string text, EventHandler clickEvent)
		{
			ToolStripMenuItem menuItem = new(text);
			menuItem.Text = text;
			menuItem.Click += clickEvent;

			ContextMenuStrip.Items.Add(menuItem);
		}

		private void CopyFilePathToClipboard_Click(object? sender, EventArgs e)
		{
			if (lvwFound.SelectedItems.Count > 0)
				Clipboard.SetText(lvwFound.SelectedItems[0].SubItems[0]?.Text ?? string.Empty);
		}

		private void CopyFormattedContentToClipboard_Click(object? sender, EventArgs e)
		{
			if (lvwFound.SelectedItems.Count == 0)
				return;

			ListViewItem selectedItem = lvwFound.SelectedItems[0];

			string formattedCopyContent =
				string.Format(CopiedLineFormat,
					selectedItem.SubItems[0]?.Text,
					selectedItem.SubItems[1].Text,
					selectedItem.SubItems[2].Text
				);

			Clipboard.SetText(formattedCopyContent);
		}

		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (FormStateChanged)
			{
				var answer = MessageBox.Show(
					"If you application now, recent changes to the search arguments will be lost.\r\n\r\nDo you wish to save the changes?",
					"Unsaved search form changes",
					MessageBoxButtons.YesNoCancel,
					MessageBoxIcon.Question);
				switch (answer)
				{
					case DialogResult.Yes:
						SaveStateOfForm();
						SaveFormState();
						break;
					case DialogResult.No:
						break;
					case DialogResult.Cancel:
						e.Cancel = true;
						break;
				}
				if (e.Cancel) return;
			}

			if (ConfigChanged)
			{
				var answer = MessageBox.Show(
					"If you application now, any recent changes to the configuration settings will be lost.\r\n\r\nDo you wish to save the changes?",
					"Unsaved changes in configuration settings",
					MessageBoxButtons.YesNoCancel,
					MessageBoxIcon.Question);
				switch (answer)
				{
					case DialogResult.Yes:
						SaveConfigOptions();
						break;
					case DialogResult.No:
						break;
					case DialogResult.Cancel:
						e.Cancel = true;
						break;
				}
				if (e.Cancel) return;
			}
		}

		private void btnFolder_Click(object sender, EventArgs e)
		{
			DialogResult dialogResult = folderBrowserDialog1.ShowDialog();

			if (dialogResult == DialogResult.OK)
			{
				FormStateChanged = txtFolder.Text == folderBrowserDialog1.SelectedPath;
				if (FormStateChanged)
				{
					txtFolder.Text = folderBrowserDialog1.SelectedPath;
					ObjectEnabling(Searching: false);
				}
			}
		}

		//private void OpenInProgram_Click(object? sender, EventArgs? e)
		//{
		//}

		private void OpenInProgram_Click(object? sender, EventArgs? e)
		{
			try
			{
				ListViewItem selectedItem = lvwFound.SelectedItems[0];

				string filePath = selectedItem.SubItems[0].Text;
				var ext = Path.GetExtension(filePath);
				string lineNumber = selectedItem.SubItems[1].Text;

				//ProcessStartInfo processStartInfo = new(_ConfigOptionsObj.EditorMappings[ext])
				//{
				//	// This will always be true, because we depend on this to execute the program relative to the path indicated by the user.
				//	UseShellExecute = true,
				//	WorkingDirectory = TextEditorPath,
				//	Arguments = TextEditorCommandLineArgumentsFormat.Replace("[LN]", lineNumber).Replace("[FP]", filePath)
				//};

				ProcessStartInfo processStartInfo = new ProcessStartInfo
				{
					FileName = @"C:\Program Files\Notepad++\notepad++.exe",
					UseShellExecute = false,
					WorkingDirectory = Environment.CurrentDirectory,
					Arguments = $"\"{filePath}\" -n{lineNumber}"
				};

				Process.Start(processStartInfo);
			}
			catch (Exception ex)
			{
				MessageBox.Show($"An error occurred while trying to open the configured program. Error: {ex.InnerException?.Message ?? ex.Message}", "Error");
			}
		}
		#endregion

		private void btnSearch_Click(object sender, EventArgs e)
		{
			ObjectEnabling(Searching: true);

			Search(sender, e);

			ObjectEnabling(Searching: false);
		}

		private void ObjectEnabling(bool Searching)
		{
			txtFilePatterns.Enabled = !Searching;
			txtIgnoreFolders.Enabled = !Searching;
			txtSearchPattern.Enabled = !Searching;
			btnFolder.Enabled = !Searching;
			btnLoad.Enabled = !Searching && !FormStateChanged;
			btnSave.Enabled = !Searching && FormStateChanged;
			btnRename.Enabled = !Searching;
			btnDelete.Enabled = !Searching;
			btnClone.Enabled = !Searching;
			chkIncludeHidden.Enabled = !Searching;
			chkIncludeSystem.Enabled = !Searching;
			chkSearchAsRegex.Enabled = !Searching;
			btnSearch.Enabled = !Searching;
			this.Refresh();
		}

		private void btnLoad_Click(object sender, EventArgs e)
		{
			if (FormStateChanged)
			{
				var answer = MessageBox.Show(
					"If you load another search metric configuration now, recent changes to the current search arguments will be lost.\r\n\r\nDo you wish to proceed with loading ?",
					"There are unsaved changes",
					MessageBoxButtons.YesNo,
					MessageBoxIcon.Question);
				switch (answer)
				{
					case DialogResult.Yes:
						SetFormFromState(cbxSearchSetName.SelectedItem.ToString());
						SaveFormState();
						break;
					default:
						return;
				}
			}
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			if (FormStateChanged)
			{
				SetStateFromForm(_FormStateObj.ActiveSearchMetric);
				SaveFormState();
				FormStateChanged = false;
			}
		}

		private void btnClone_Click(object sender, EventArgs e)
		{
			// check for collision with existing names in the dropdown, if there is a collision, show an error message and do not proceed with the renaming.
			var tempNameFormat = $"{_FormStateObj.ActiveSearchMetric}_{{0:0#}}";
			var tempName = string.Empty;
			var index = 1;
			while (index < 100)
			{
				tempName = string.Format(tempNameFormat, index);
				if (!_FormStateObj.SearchMetricList.Keys.Contains(tempName)) break;
				index++;
			}

			var f = new InputBox("Search set name", tempName);
			var answer = f.ShowDialog();
			switch (answer)
			{
				case DialogResult.OK:
					if (_FormStateObj.SearchMetricList.Keys.Contains(f.Value))
					{
						MessageBox.Show(
							"The name provided is already in use.  Try a different name",
							"Conflict",
							MessageBoxButtons.OK,
							MessageBoxIcon.Information);
						return;
					}
					SetStateFromForm(f.Value);
					_FormStateObj.ActiveSearchMetric = f.Value;
					SaveFormState();
					HydrateSearchMetricDropDown();
					break;
				case DialogResult.Cancel:
					break;
			}
		}

		private void btnRename_Click(object sender, EventArgs e)
		{
			if (FormStateChanged)
			{
				MessageBox.Show("This search set content has changed but has not been saved.  Save or reload before attempting to rename it", "Error");
				return;
			}
			var f = new InputBox("Replacement name for this search set", _FormStateObj.ActiveSearchMetric);
			var answer = f.ShowDialog();
			switch (answer)
			{
				case DialogResult.OK:
					if (string.IsNullOrWhiteSpace(f.Value))
					{
						MessageBox.Show("The new search set name can not be blank.", "Error");
						return;
					}
					if (_FormStateObj.SearchMetricList.ContainsKey(f.Value))
					{
						MessageBox.Show("This search set name is already used.", "Error");
						return;
					}
					var existingSetName = _FormStateObj.ActiveSearchMetric;
					_FormStateObj.SearchMetricList.Remove(existingSetName);
					SetStateFromForm(f.Value);
					_FormStateObj.ActiveSearchMetric = f.Value;
					SaveFormState();
					HydrateSearchMetricDropDown();
					break;
				case DialogResult.Cancel:
					break;
			}
		}

		private void btnDelete_Click(object sender, EventArgs e)
		{
			if (cbxSearchSetName.SelectedItem == null)
			{
				MessageBox.Show("No search metric configuration is selected to be deleted.", "Error");
				return;
			}
			if (cbxSearchSetName.Items.Count < 2)
			{
				MessageBox.Show("At least one item must remain in the list.", "Error");
				return;
			}
			var answer = MessageBox.Show(
				$"Permanently removes the search set {_FormStateObj.ActiveSearchMetric}.  Continue ?",
				"Warning",
				MessageBoxButtons.OKCancel,
				MessageBoxIcon.Warning);
			switch (answer)
			{
				case DialogResult.OK:
					_FormStateObj.SearchMetricList.Remove(_FormStateObj.ActiveSearchMetric);
					_FormStateObj.ActiveSearchMetric = _FormStateObj.SearchMetricList.Keys.First();
					HydrateSearchMetricDropDown();
					SaveFormState();
					break;
				case DialogResult.Cancel:
					break;
			}
		}

		private void chkRecurse_CheckedChanged(object sender, EventArgs e)
		{
			FormStateChanged = true;
			ObjectEnabling(Searching: false);
		}

		private void chkIncludeHidden_CheckedChanged(object sender, EventArgs e)
		{
			FormStateChanged = true;
			ObjectEnabling(Searching: false);
		}

		private void chkIncludeSystem_CheckedChanged(object sender, EventArgs e)
		{
			FormStateChanged = true;
			ObjectEnabling(Searching: false);
		}

		private void chkSearchAsRegex_CheckedChanged(object sender, EventArgs e)
		{
			FormStateChanged = true;
			ObjectEnabling(Searching: false);
		}

		private void txtFolder_TextChanged(object sender, EventArgs e)
		{
			FormStateChanged = true;
			ObjectEnabling(Searching: false);
		}

		private void txtIgnoreFolders_TextChanged(object sender, EventArgs e)
		{
			FormStateChanged = true;
			ObjectEnabling(Searching: false);
		}

		private void txtFilePatterns_TextChanged(object sender, EventArgs e)
		{
			FormStateChanged = true;
			ObjectEnabling(Searching: false);
		}

		private void txtSearchPattern_TextChanged(object sender, EventArgs e)
		{
			FormStateChanged = true;
			ObjectEnabling(Searching: false);
		}

		private void cbxSearchSetName_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (FormStateChanged)
			{
				var answer = MessageBox.Show(
					"If you load another search metric configuration now, recent changes to the current search arguments will be lost.\r\n\r\nDo you wish to proceed with loading ?",
					"There are unsaved changes",
					MessageBoxButtons.YesNo,
					MessageBoxIcon.Question);
				switch (answer)
				{
					case DialogResult.Yes:
						break;
					default:
						return;
				}
			}
			SetFormFromState(cbxSearchSetName.SelectedItem.ToString());
		}
	}
}
