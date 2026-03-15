using System.Data;
using System.Text;

namespace BOG.TextFileSearch
{
	public partial class FolderTool : Form
	{
		public string FoldersAsSingleLine => FoldersListToSingleLine();
		public bool Changed { get; set; } = false;

		public FolderTool(string foldersLine, string title)
		{
			InitializeComponent();
			this.Text = title;
			FoldersLoad(foldersLine);
			this.txtFolderCollection.Text = FoldersListRefresh();
		}

		private void FoldersLoad(string foldersLine)
		{
			this.txtFolderCollection.Text = string.Join("\r\n", foldersLine.Split(
			   separator: new char[] { ';' },
			   options: StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
			   .Select(o => o).Distinct().Order().ToArray());
		}

		private string FoldersListToSingleLine()
		{
			return string.Join(";", this.txtFolderCollection.Text.Split(
			   separator: "\r\n",
			   options: StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
			   .Select(o => o).Distinct().Order().ToArray());
		}

		private string FoldersListRefresh()
		{
			return string.Join("\r\n", this.txtFolderCollection.Text.Split(
			   separator: "\r\n",
			   options: StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
			   .Select(o => o).Distinct().Order().ToArray());
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			Changed = true;
			this.Close();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void txtFolderCollection_DragEnter(object sender, DragEventArgs e)
		{
			e.Effect = DragDropEffects.Copy;
		}

		private void txtFolderCollection_DragDrop(object sender, DragEventArgs e)
		{
			try
			{
				var files = (string[])e.Data.GetData(DataFormats.FileDrop);
				if (files == null) return;
				foreach (var f in files)
				{
					if (f == null) continue;
					if (Directory.Exists(f) || f.IndexOfAny(new char[] { '*', '?' }) >= 0)
					{
						this.txtFolderCollection.Text += "\r\n" + f + "\r\n";
					}
				}
				this.txtFolderCollection.Text = FoldersListRefresh();
			}
			catch (Exception err)
			{
				MessageBox.Show(
					$"An error occurred while trying to record a dropped folder name: {err.Message}",
					"Failure",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error);
			}
		}
	}
}
