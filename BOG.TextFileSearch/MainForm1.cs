using System.Diagnostics;
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

		public MainForm()
		{
			InitializeComponent();

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

			string filePatterns = CleanSemiColonString(txtFilePatterns.Text);

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

			if (string.IsNullOrEmpty(filePatterns) || filePatterns.Contains("*.*"))
				return;

			if (!Directory.Exists(txtFolder.Text))
				return;

			await Search(txtFolder.Text, txtSearchPattern.Text);
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

		public static string CleanSemiColonString(string str)
			=> new Regex("^;{1,}|;{2,}|;$").Replace(str, "");

		private async Task Search(string folder, string search)
		{
			lvwFound.Items.Clear();
			lvwErrors.Items.Clear();
			this.Refresh();
			toolStripStatusLabel1.Text = $"Loading file list ... {folder}";
			this.statusStrip1.Refresh();

			int totalOccurrences = 0;
			bool reachedLimit = false;

			List<FileMatchInformation> listOfFileMatches = new();
			List<FileErrorInformation> listOfErrors = new();

			try
			{
				Stopwatch.Start();

				Dictionary<string, FileOccurrence> fileSearchResults = new();
				// Dictionary<string, FileOccurrence> fileContents = new();

				string[] filePatterns = CleanSemiColonString(txtFilePatterns.Text).Split(';');

				string ignoredDirectories =
					string.Join("|", CleanSemiColonString(txtIgnoreFolders.Text).Split(';'));

				List<string> allFileNames = new();

				foreach (string pattern in filePatterns)
				{
					try
					{
						allFileNames.AddRange(
							Directory.GetFiles(folder, pattern, new EnumerationOptions
							{
								MatchCasing = MatchCasing.PlatformDefault,
								RecurseSubdirectories = chkRecurse.Checked,
								IgnoreInaccessible = true,
								AttributesToSkip = (!chkIncludeHidden.Checked ? FileAttributes.Hidden : 0) | (!chkIncludeSystem.Checked ? FileAttributes.System : 0)
							})
						);
					}
					catch (Exception ex)
					{
						listOfErrors.Add(new FileErrorInformation
						{
							FileName = pattern,
							Error = ex.InnerException?.Message ?? ex.Message
						});
					}
					toolStripStatusLabel1.Text = $"Loading file list ... {pattern}: {allFileNames.Count} / {listOfErrors.Count}";
					this.statusStrip1.Refresh();
				}

				foreach (string filePath in allFileNames)
				{
					if (ContainsIgnoredDirectories(filePath, ignoredDirectories) || string.IsNullOrEmpty(filePath))
						continue;

					string fileContent = await File.ReadAllTextAsync(filePath);
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
					totalOccurrences += indexes.Count;

					if (indexes.Count > 0)
					{
						fileSearchResults.Add(filePath, new FileOccurrence(fileContent, indexes));
						toolStripStatusLabel1.Text = $"Matching file list ... {filePath}";
						this.statusStrip1.Refresh();
					}
				}

				foreach (KeyValuePair<string, FileOccurrence> file in fileSearchResults)
				{
					if (reachedLimit)
						break;

					Dictionary<string, List<int>> fileNameToLineNumber = new();

					fileNameToLineNumber.TryAdd(file.Key, new List<int>());

					foreach (int idx in file.Value.MatchIndexes)
					{
						if (MaxItemsInlvwFound > 0 && listOfFileMatches.Count > MaxItemsInlvwFound)
						{
							reachedLimit = true;
							break;
						}

						int lineNumber = default;
						string lineContent = string.Empty;

						if (!await Task.Run(
								() => TryReadWholeLine(
									file.Value.FileContent, idx, out lineNumber, out lineContent)
							)
						   )
							break;

						if (fileNameToLineNumber[file.Key].Contains(lineNumber))
							continue;

						listOfFileMatches.Add(new(file.Key, lineNumber, lineContent));

						fileNameToLineNumber[file.Key].Add(lineNumber);
					}
				}
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
				UpdatelvwFound(listOfFileMatches);

				Stopwatch.Stop();

				lvwFound.AutoResizeColumn(0, ColumnHeaderAutoResizeStyle.HeaderSize);
				lvwFound.AutoResizeColumn(1, ColumnHeaderAutoResizeStyle.HeaderSize);

				toolStripStatusLabel1.Text = $"Done ... {string.Format(ResultsFoundFormat, totalOccurrences, lvwFound.Items.Count.ToString())} ... {UpdateResultsReturnedLabelWithStopwatchElapsedTime()}";
				this.statusStrip1.Refresh();
			}
		}

		public static bool ContainsIgnoredDirectories(string filePath, string ignoredDirectories)
			=> Regex.IsMatch(filePath, string.Concat(@"\\", ignoredDirectories, @"\\"));

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

			if (File.Exists(ConfigEditorFileName))
			{
				LoadConfigOptions();
			}
			else
			{
				SaveConfigOptions();
			}
			//			AddToolStripMenuItemToContextMenuStrip($"Open in {_ConfigOptionsObj.EditorPrograms[0].EditorName}", OpenInProgram_Click);

			AddToolStripMenuItemToContextMenuStrip("Copy File Path", CopyFilePathToClipboard_Click);
			AddToolStripMenuItemToContextMenuStrip("Copy Formatted", CopyFormattedContentToClipboard_Click);

			if (File.Exists(FormStateFileName))
			{
				SaveFormState();
			}
			else
			{
				LoadFormState();
			}
		}

		private void LoadConfigOptions()
		{
			if (File.Exists(ConfigEditorFileName))
			{
				_ConfigOptionsObj = ObjectJsonSerializer<ConfigOptions>.LoadDocumentFormat(ConfigEditorFileName);
			}
		}

		private void SaveConfigOptions()
		{
			ObjectJsonSerializer<ConfigOptions>.SaveDocumentFormat(_ConfigOptionsObj, ConfigEditorFileName, true);
		}

		private void LoadFormState()
		{
			if (!File.Exists(FormStateFile))
			{
				_FormStateObj = ObjectJsonSerializer<FormState>.LoadDocumentFormat(FormStateFile);
			}
		}
		private void SaveFormState()
		{
			ObjectJsonSerializer<FormState>.SaveDocumentFormat(_FormStateObj, FormStateFile, true);
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
			}
			else
			{
				MessageBox.Show(
					$"The search metric configuration with name {searchMetricName} could not be found",
					"Error",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error);
			}
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

#if FALSE
				// ----------------------------

				txtFolder.Text = formState.Folder;
				chkRecurse.Checked = formState.IncludeSubfolders;
				chkIncludeHidden.Checked = formState.IncludeHidden;
				chkIncludeSystem.Checked = formState.IncludeSystem;
				txtIgnoreFolders.Text = formState.IgnoredFolders;
				txtFilePatterns.Text = formState.FilePatterns;
				txtSearchPattern.Text = formState.SearchText;
				chkSearchAsRegex.Checked = formState.SearchAsRegex;

				// ----------------------------

				txtFolder.Text = formState.Folder;
				chkRecurse.Checked = formState.IncludeSubfolders;
				chkIncludeHidden.Checked = formState.IncludeHidden;
				chkIncludeSystem.Checked = formState.IncludeSystem;
				txtIgnoreFolders.Text = formState.IgnoredFolders;
				txtFilePatterns.Text = formState.FilePatterns;
				txtSearchPattern.Text = formState.SearchText;
				chkSearchAsRegex.Checked = formState.SearchAsRegex;

				// ----------------------------
#endif

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
				Clipboard.SetText(
					lvwFound.SelectedItems[0].SubItems[0]?.Text ?? string.Empty
					);
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

		private void SearchTextBox_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				Search(sender, e);
			}
		}

		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			var formState = new FormState
			{
				Folder = txtFolder.Text,
				IncludeSubfolders = chkRecurse.Checked,
				IgnoredFolders = txtIgnoreFolders.Text,
				IncludeHidden = chkIncludeHidden.Checked,
				IncludeSystem = chkIncludeSystem.Checked,
				FilePatterns = txtFilePatterns.Text,
				SearchText = txtSearchPattern.Text,
				SearchAsRegex = chkSearchAsRegex.Checked
			};

			string serializedFormState = JsonSerializer.Serialize(formState);
			File.WriteAllBytes(FormStateFileName, Encoding.UTF8.GetBytes(serializedFormState));
		}

		private void btnFolder_Click(object sender, EventArgs e)
		{
			DialogResult dialogResult = folderBrowserDialog1.ShowDialog();

			if (dialogResult == DialogResult.OK)
			{
				txtFolder.Text = folderBrowserDialog1.SelectedPath;
			}
		}

		private void OpenInProgram_Click(object? sender, EventArgs? e)
		{
			try
			{
				ListViewItem selectedItem = lvwFound.SelectedItems[0];

				string filePath = selectedItem.SubItems[0].Text;
				string lineNumber = selectedItem.SubItems[1].Text;

				ProcessStartInfo processStartInfo = new(TextEditorProgramNameWithExtension)
				{
					// This will always be true, because we depend on this to execute the program relative to the path indicated by the user.
					UseShellExecute = true,
					WorkingDirectory = TextEditorPath,
					Arguments = TextEditorCommandLineArgumentsFormat.Replace("[LN]", lineNumber).Replace("[FP]", filePath)
				};

				Process.Start(processStartInfo);
			}
			catch (Exception ex)
			{
				MessageBox.Show($"An error occurred while trying to open the configured program. Error: {ex.InnerException?.Message ?? ex.Message}", "Error");
			}
		}
		#endregion

		private void btnDirectory_Click(object sender, EventArgs e)
		{

		}

		private void btnSearch_Click(object sender, EventArgs e)
		{
			btnSearch.Enabled = false;
			txtFilePatterns.Enabled = false;
			txtIgnoreFolders.Enabled = false;
			txtSearchPattern.Enabled = false;
			btnFolder.Enabled = false;
			chkIncludeHidden.Enabled = false;
			chkIncludeSystem.Enabled = false;
			chkSearchAsRegex.Enabled = false;
			this.Refresh();

			Search(sender, e);

			txtFilePatterns.Enabled = true;
			txtIgnoreFolders.Enabled = true;
			txtSearchPattern.Enabled = true;
			btnFolder.Enabled = true;
			chkIncludeHidden.Enabled = true;
			chkIncludeSystem.Enabled = true;
			chkSearchAsRegex.Enabled = true;
			btnSearch.Enabled = true;
			this.Refresh();
		}

		private void btnLoad_Click(object sender, EventArgs e)
		{

		}

		private void button1_Click(object sender, EventArgs e)
		{

		}

		private void btnClone_Click(object sender, EventArgs e)
		{

		}

		private void btnRename_Click(object sender, EventArgs e)
		{

		}

		private void btnDelete_Click(object sender, EventArgs e)
		{

		}

		private void chkRecurse_CheckedChanged(object sender, EventArgs e)
		{
			FormStateChanged = true;
		}

		private void chkIncludeHidden_CheckedChanged(object sender, EventArgs e)
		{
			FormStateChanged = true;
		}

		private void chkIncludeSystem_CheckedChanged(object sender, EventArgs e)
		{
			FormStateChanged = true;
		}

		private void chkSearchAsRegex_CheckedChanged(object sender, EventArgs e)
		{
			FormStateChanged = true;
		}

		private void txtFolder_TextChanged(object sender, EventArgs e)
		{
			FormStateChanged = true;
		}

		private void txtIgnoreFolders_TextChanged(object sender, EventArgs e)
		{
			FormStateChanged = true;
		}

		private void txtFilePatterns_TextChanged(object sender, EventArgs e)
		{
			FormStateChanged = true;
		}

		private void txtSearchPattern_TextChanged(object sender, EventArgs e)
		{
			FormStateChanged = true;
		}

		private void cbxSearchSetName_SelectedIndexChanged(object sender, EventArgs e)
		{

		}
	}
}
