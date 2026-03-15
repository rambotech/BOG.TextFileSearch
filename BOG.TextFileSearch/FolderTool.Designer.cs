namespace BOG.TextFileSearch
{
	partial class FolderTool
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			scForm = new SplitContainer();
			txtFolderCollection = new TextBox();
			btnCancel = new Button();
			btnSave = new Button();
			((System.ComponentModel.ISupportInitialize)scForm).BeginInit();
			scForm.Panel1.SuspendLayout();
			scForm.Panel2.SuspendLayout();
			scForm.SuspendLayout();
			SuspendLayout();
			// 
			// scForm
			// 
			scForm.Dock = DockStyle.Fill;
			scForm.IsSplitterFixed = true;
			scForm.Location = new Point(0, 0);
			scForm.Name = "scForm";
			scForm.Orientation = Orientation.Horizontal;
			// 
			// scForm.Panel1
			// 
			scForm.Panel1.Controls.Add(txtFolderCollection);
			// 
			// scForm.Panel2
			// 
			scForm.Panel2.Controls.Add(btnCancel);
			scForm.Panel2.Controls.Add(btnSave);
			scForm.Size = new Size(638, 521);
			scForm.SplitterDistance = 460;
			scForm.TabIndex = 0;
			// 
			// txtFolderCollection
			// 
			txtFolderCollection.AllowDrop = true;
			txtFolderCollection.Dock = DockStyle.Fill;
			txtFolderCollection.Location = new Point(0, 0);
			txtFolderCollection.Multiline = true;
			txtFolderCollection.Name = "txtFolderCollection";
			txtFolderCollection.ScrollBars = ScrollBars.Both;
			txtFolderCollection.Size = new Size(638, 460);
			txtFolderCollection.TabIndex = 0;
			txtFolderCollection.DragDrop += txtFolderCollection_DragDrop;
			txtFolderCollection.DragEnter += txtFolderCollection_DragEnter;
			// 
			// btnCancel
			// 
			btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			btnCancel.Location = new Point(474, 12);
			btnCancel.Name = "btnCancel";
			btnCancel.Size = new Size(139, 33);
			btnCancel.TabIndex = 2;
			btnCancel.Text = "&Cancel";
			btnCancel.UseVisualStyleBackColor = true;
			btnCancel.Click += btnCancel_Click;
			// 
			// btnSave
			// 
			btnSave.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			btnSave.Location = new Point(311, 12);
			btnSave.Name = "btnSave";
			btnSave.Size = new Size(139, 33);
			btnSave.TabIndex = 1;
			btnSave.Text = "&Save";
			btnSave.UseVisualStyleBackColor = true;
			btnSave.Click += btnSave_Click;
			// 
			// FolderTool
			// 
			AllowDrop = true;
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(638, 521);
			Controls.Add(scForm);
			Name = "FolderTool";
			Text = "FolderTool";
			scForm.Panel1.ResumeLayout(false);
			scForm.Panel1.PerformLayout();
			scForm.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)scForm).EndInit();
			scForm.ResumeLayout(false);
			ResumeLayout(false);
		}

		#endregion

		private SplitContainer scForm;
		private Button btnCancel;
		private Button btnSave;
		private TextBox txtFolderCollection;
	}
}