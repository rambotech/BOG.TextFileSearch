namespace BOG.TextFileSearch
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			statusStrip1 = new StatusStrip();
			toolStripStatusLabel1 = new ToolStripStatusLabel();
			scMain = new SplitContainer();
			chkIncludeSystem = new CheckBox();
			chkIncludeHidden = new CheckBox();
			txtIgnoreFolders = new TextBox();
			lblIgnoreFolders = new Label();
			chkSearchAsRegex = new CheckBox();
			txtSearchPattern = new TextBox();
			lblSearchPattern = new Label();
			txtFilePatterns = new TextBox();
			lblFilePatterns = new Label();
			txtFolder = new TextBox();
			btnFolder = new Button();
			btnSearch = new Button();
			chkRecurse = new CheckBox();
			tabcResults = new TabControl();
			tabpFound = new TabPage();
			lvwFound = new ListView();
			tabpErrors = new TabPage();
			lvwErrors = new ListView();
			folderBrowserDialog1 = new FolderBrowserDialog();
			statusStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)scMain).BeginInit();
			scMain.Panel1.SuspendLayout();
			scMain.Panel2.SuspendLayout();
			scMain.SuspendLayout();
			tabcResults.SuspendLayout();
			tabpFound.SuspendLayout();
			tabpErrors.SuspendLayout();
			SuspendLayout();
			// 
			// statusStrip1
			// 
			statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1 });
			statusStrip1.Location = new Point(0, 673);
			statusStrip1.Name = "statusStrip1";
			statusStrip1.Size = new Size(1121, 22);
			statusStrip1.TabIndex = 0;
			statusStrip1.Text = "statusStrip1";
			// 
			// toolStripStatusLabel1
			// 
			toolStripStatusLabel1.Name = "toolStripStatusLabel1";
			toolStripStatusLabel1.Size = new Size(118, 17);
			toolStripStatusLabel1.Text = "toolStripStatusLabel1";
			// 
			// scMain
			// 
			scMain.Dock = DockStyle.Fill;
			scMain.IsSplitterFixed = true;
			scMain.Location = new Point(0, 0);
			scMain.Name = "scMain";
			scMain.Orientation = Orientation.Horizontal;
			// 
			// scMain.Panel1
			// 
			scMain.Panel1.Controls.Add(chkIncludeSystem);
			scMain.Panel1.Controls.Add(chkIncludeHidden);
			scMain.Panel1.Controls.Add(txtIgnoreFolders);
			scMain.Panel1.Controls.Add(lblIgnoreFolders);
			scMain.Panel1.Controls.Add(chkSearchAsRegex);
			scMain.Panel1.Controls.Add(txtSearchPattern);
			scMain.Panel1.Controls.Add(lblSearchPattern);
			scMain.Panel1.Controls.Add(txtFilePatterns);
			scMain.Panel1.Controls.Add(lblFilePatterns);
			scMain.Panel1.Controls.Add(txtFolder);
			scMain.Panel1.Controls.Add(btnFolder);
			scMain.Panel1.Controls.Add(btnSearch);
			scMain.Panel1.Controls.Add(chkRecurse);
			// 
			// scMain.Panel2
			// 
			scMain.Panel2.Controls.Add(tabcResults);
			scMain.Size = new Size(1121, 673);
			scMain.SplitterDistance = 151;
			scMain.TabIndex = 1;
			// 
			// chkIncludeSystem
			// 
			chkIncludeSystem.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			chkIncludeSystem.AutoSize = true;
			chkIncludeSystem.Location = new Point(971, 109);
			chkIncludeSystem.Name = "chkIncludeSystem";
			chkIncludeSystem.Size = new Size(106, 19);
			chkIncludeSystem.TabIndex = 11;
			chkIncludeSystem.Text = "Include &System";
			chkIncludeSystem.UseVisualStyleBackColor = true;
			// 
			// chkIncludeHidden
			// 
			chkIncludeHidden.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			chkIncludeHidden.AutoSize = true;
			chkIncludeHidden.Location = new Point(970, 84);
			chkIncludeHidden.Name = "chkIncludeHidden";
			chkIncludeHidden.Size = new Size(107, 19);
			chkIncludeHidden.TabIndex = 10;
			chkIncludeHidden.Text = "Include &Hidden";
			chkIncludeHidden.UseVisualStyleBackColor = true;
			// 
			// txtIgnoreFolders
			// 
			txtIgnoreFolders.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			txtIgnoreFolders.Location = new Point(107, 41);
			txtIgnoreFolders.MinimumSize = new Size(600, 23);
			txtIgnoreFolders.Name = "txtIgnoreFolders";
			txtIgnoreFolders.Size = new Size(830, 23);
			txtIgnoreFolders.TabIndex = 3;
			txtIgnoreFolders.Tag = "Ignore Folders";
			// 
			// lblIgnoreFolders
			// 
			lblIgnoreFolders.AutoSize = true;
			lblIgnoreFolders.Location = new Point(18, 44);
			lblIgnoreFolders.Name = "lblIgnoreFolders";
			lblIgnoreFolders.Size = new Size(82, 15);
			lblIgnoreFolders.TabIndex = 2;
			lblIgnoreFolders.Tag = "Ignore Folders";
			lblIgnoreFolders.Text = "Ignore Folders";
			// 
			// chkSearchAsRegex
			// 
			chkSearchAsRegex.AutoSize = true;
			chkSearchAsRegex.Location = new Point(107, 129);
			chkSearchAsRegex.Name = "chkSearchAsRegex";
			chkSearchAsRegex.Size = new Size(225, 19);
			chkSearchAsRegex.TabIndex = 8;
			chkSearchAsRegex.Text = "Search Pattern is a regular expressiom";
			chkSearchAsRegex.UseVisualStyleBackColor = true;
			// 
			// txtSearchPattern
			// 
			txtSearchPattern.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			txtSearchPattern.Location = new Point(107, 100);
			txtSearchPattern.MinimumSize = new Size(600, 23);
			txtSearchPattern.Name = "txtSearchPattern";
			txtSearchPattern.Size = new Size(830, 23);
			txtSearchPattern.TabIndex = 7;
			txtSearchPattern.Tag = "Search Pattern";
			// 
			// lblSearchPattern
			// 
			lblSearchPattern.AutoSize = true;
			lblSearchPattern.Location = new Point(18, 103);
			lblSearchPattern.Name = "lblSearchPattern";
			lblSearchPattern.Size = new Size(83, 15);
			lblSearchPattern.TabIndex = 6;
			lblSearchPattern.Text = "Search Pattern";
			// 
			// txtFilePatterns
			// 
			txtFilePatterns.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			txtFilePatterns.Location = new Point(107, 70);
			txtFilePatterns.MinimumSize = new Size(600, 23);
			txtFilePatterns.Name = "txtFilePatterns";
			txtFilePatterns.Size = new Size(830, 23);
			txtFilePatterns.TabIndex = 5;
			txtFilePatterns.Tag = "File Patterns";
			// 
			// lblFilePatterns
			// 
			lblFilePatterns.AutoSize = true;
			lblFilePatterns.Location = new Point(18, 73);
			lblFilePatterns.Name = "lblFilePatterns";
			lblFilePatterns.Size = new Size(71, 15);
			lblFilePatterns.TabIndex = 4;
			lblFilePatterns.Text = "File Patterns";
			// 
			// txtFolder
			// 
			txtFolder.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			txtFolder.Location = new Point(107, 12);
			txtFolder.MinimumSize = new Size(600, 23);
			txtFolder.Name = "txtFolder";
			txtFolder.Size = new Size(830, 23);
			txtFolder.TabIndex = 1;
			txtFolder.Tag = "Folder";
			// 
			// btnFolder
			// 
			btnFolder.Location = new Point(14, 12);
			btnFolder.Name = "btnFolder";
			btnFolder.Size = new Size(87, 23);
			btnFolder.TabIndex = 0;
			btnFolder.Text = "&Folder";
			btnFolder.UseVisualStyleBackColor = true;
			btnFolder.Click += btnFolder_Click;
			// 
			// btnSearch
			// 
			btnSearch.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			btnSearch.Location = new Point(956, 12);
			btnSearch.Name = "btnSearch";
			btnSearch.Size = new Size(153, 36);
			btnSearch.TabIndex = 12;
			btnSearch.Text = "&Search";
			btnSearch.UseVisualStyleBackColor = true;
			btnSearch.Click += btnSearch_Click;
			// 
			// chkRecurse
			// 
			chkRecurse.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			chkRecurse.AutoSize = true;
			chkRecurse.Location = new Point(970, 59);
			chkRecurse.Name = "chkRecurse";
			chkRecurse.Size = new Size(126, 19);
			chkRecurse.TabIndex = 9;
			chkRecurse.Text = "&Include sub folders";
			chkRecurse.UseVisualStyleBackColor = true;
			// 
			// tabcResults
			// 
			tabcResults.Controls.Add(tabpFound);
			tabcResults.Controls.Add(tabpErrors);
			tabcResults.Dock = DockStyle.Fill;
			tabcResults.Location = new Point(0, 0);
			tabcResults.Name = "tabcResults";
			tabcResults.SelectedIndex = 0;
			tabcResults.Size = new Size(1121, 518);
			tabcResults.TabIndex = 0;
			// 
			// tabpFound
			// 
			tabpFound.Controls.Add(lvwFound);
			tabpFound.Location = new Point(4, 24);
			tabpFound.Name = "tabpFound";
			tabpFound.Padding = new Padding(3);
			tabpFound.Size = new Size(1113, 490);
			tabpFound.TabIndex = 0;
			tabpFound.Text = "Found";
			tabpFound.UseVisualStyleBackColor = true;
			// 
			// lvwFound
			// 
			lvwFound.Dock = DockStyle.Fill;
			lvwFound.Location = new Point(3, 3);
			lvwFound.Name = "lvwFound";
			lvwFound.Size = new Size(1107, 484);
			lvwFound.TabIndex = 0;
			lvwFound.UseCompatibleStateImageBehavior = false;
			// 
			// tabpErrors
			// 
			tabpErrors.Controls.Add(lvwErrors);
			tabpErrors.Location = new Point(4, 24);
			tabpErrors.Name = "tabpErrors";
			tabpErrors.Padding = new Padding(3);
			tabpErrors.Size = new Size(1113, 490);
			tabpErrors.TabIndex = 1;
			tabpErrors.Text = "Errors";
			tabpErrors.UseVisualStyleBackColor = true;
			// 
			// lvwErrors
			// 
			lvwErrors.Dock = DockStyle.Fill;
			lvwErrors.Location = new Point(3, 3);
			lvwErrors.Name = "lvwErrors";
			lvwErrors.Size = new Size(1107, 484);
			lvwErrors.TabIndex = 0;
			lvwErrors.UseCompatibleStateImageBehavior = false;
			// 
			// MainForm
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(1121, 695);
			Controls.Add(scMain);
			Controls.Add(statusStrip1);
			Name = "MainForm";
			Text = "BOG.TextFileSearch";
			Load += OnFormLoad;
			statusStrip1.ResumeLayout(false);
			statusStrip1.PerformLayout();
			scMain.Panel1.ResumeLayout(false);
			scMain.Panel1.PerformLayout();
			scMain.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)scMain).EndInit();
			scMain.ResumeLayout(false);
			tabcResults.ResumeLayout(false);
			tabpFound.ResumeLayout(false);
			tabpErrors.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private StatusStrip statusStrip1;
        private SplitContainer scMain;
        private TabControl tabcResults;
        private TabPage tabpFound;
        private TabPage tabpErrors;
        private ListView lvwFound;
        private ListView lvwErrors;
        private ToolStripStatusLabel toolStripStatusLabel1;
		private TextBox txtSearchPattern;
		private Label lblSearchPattern;
		private TextBox txtFilePatterns;
		private Label lblFilePatterns;
		private TextBox txtFolder;
		private Button btnFolder;
		private Button btnSearch;
		private CheckBox chkRecurse;
		private CheckBox chkSearchAsRegex;
		private TextBox txtIgnoreFolders;
		private Label lblIgnoreFolders;
		private FolderBrowserDialog folderBrowserDialog1;
		private CheckBox chkIncludeSystem;
		private CheckBox chkIncludeHidden;
	}
}
