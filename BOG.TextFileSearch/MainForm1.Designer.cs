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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			statusStrip1 = new StatusStrip();
			toolStripStatusLabel1 = new ToolStripStatusLabel();
			scMain = new SplitContainer();
			splitContainer1 = new SplitContainer();
			btnDelete = new Button();
			btnRename = new Button();
			btnClone = new Button();
			btnSave = new Button();
			btnLoad = new Button();
			cbxSearchSetName = new ComboBox();
			lblSearchSetName = new Label();
			chkAccessed = new CheckBox();
			dtpAccessedEndDate = new DateTimePicker();
			label3 = new Label();
			dtpAccessedStartDate = new DateTimePicker();
			label4 = new Label();
			chkUpdated = new CheckBox();
			dtpUpdatedEndDate = new DateTimePicker();
			label1 = new Label();
			dtpUpdatedStartDate = new DateTimePicker();
			label2 = new Label();
			chkCreated = new CheckBox();
			dtpCreatedEndDate = new DateTimePicker();
			lblEndDateTime = new Label();
			dtpCreatedStartDate = new DateTimePicker();
			lblStartDateTime = new Label();
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
			((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
			splitContainer1.Panel1.SuspendLayout();
			splitContainer1.Panel2.SuspendLayout();
			splitContainer1.SuspendLayout();
			tabcResults.SuspendLayout();
			tabpFound.SuspendLayout();
			tabpErrors.SuspendLayout();
			SuspendLayout();
			// 
			// statusStrip1
			// 
			statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1 });
			statusStrip1.Location = new Point(0, 769);
			statusStrip1.Name = "statusStrip1";
			statusStrip1.Size = new Size(1127, 22);
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
			scMain.FixedPanel = FixedPanel.Panel1;
			scMain.Location = new Point(0, 0);
			scMain.Name = "scMain";
			scMain.Orientation = Orientation.Horizontal;
			// 
			// scMain.Panel1
			// 
			scMain.Panel1.Controls.Add(splitContainer1);
			// 
			// scMain.Panel2
			// 
			scMain.Panel2.Controls.Add(tabcResults);
			scMain.Size = new Size(1127, 769);
			scMain.SplitterDistance = 245;
			scMain.TabIndex = 1;
			// 
			// splitContainer1
			// 
			splitContainer1.Dock = DockStyle.Fill;
			splitContainer1.FixedPanel = FixedPanel.Panel1;
			splitContainer1.Location = new Point(0, 0);
			splitContainer1.Name = "splitContainer1";
			splitContainer1.Orientation = Orientation.Horizontal;
			// 
			// splitContainer1.Panel1
			// 
			splitContainer1.Panel1.Controls.Add(btnDelete);
			splitContainer1.Panel1.Controls.Add(btnRename);
			splitContainer1.Panel1.Controls.Add(btnClone);
			splitContainer1.Panel1.Controls.Add(btnSave);
			splitContainer1.Panel1.Controls.Add(btnLoad);
			splitContainer1.Panel1.Controls.Add(cbxSearchSetName);
			splitContainer1.Panel1.Controls.Add(lblSearchSetName);
			// 
			// splitContainer1.Panel2
			// 
			splitContainer1.Panel2.Controls.Add(chkAccessed);
			splitContainer1.Panel2.Controls.Add(dtpAccessedEndDate);
			splitContainer1.Panel2.Controls.Add(label3);
			splitContainer1.Panel2.Controls.Add(dtpAccessedStartDate);
			splitContainer1.Panel2.Controls.Add(label4);
			splitContainer1.Panel2.Controls.Add(chkUpdated);
			splitContainer1.Panel2.Controls.Add(dtpUpdatedEndDate);
			splitContainer1.Panel2.Controls.Add(label1);
			splitContainer1.Panel2.Controls.Add(dtpUpdatedStartDate);
			splitContainer1.Panel2.Controls.Add(label2);
			splitContainer1.Panel2.Controls.Add(chkCreated);
			splitContainer1.Panel2.Controls.Add(dtpCreatedEndDate);
			splitContainer1.Panel2.Controls.Add(lblEndDateTime);
			splitContainer1.Panel2.Controls.Add(dtpCreatedStartDate);
			splitContainer1.Panel2.Controls.Add(lblStartDateTime);
			splitContainer1.Panel2.Controls.Add(chkIncludeSystem);
			splitContainer1.Panel2.Controls.Add(chkIncludeHidden);
			splitContainer1.Panel2.Controls.Add(txtIgnoreFolders);
			splitContainer1.Panel2.Controls.Add(lblIgnoreFolders);
			splitContainer1.Panel2.Controls.Add(chkSearchAsRegex);
			splitContainer1.Panel2.Controls.Add(txtSearchPattern);
			splitContainer1.Panel2.Controls.Add(lblSearchPattern);
			splitContainer1.Panel2.Controls.Add(txtFilePatterns);
			splitContainer1.Panel2.Controls.Add(lblFilePatterns);
			splitContainer1.Panel2.Controls.Add(txtFolder);
			splitContainer1.Panel2.Controls.Add(btnFolder);
			splitContainer1.Panel2.Controls.Add(btnSearch);
			splitContainer1.Panel2.Controls.Add(chkRecurse);
			splitContainer1.Size = new Size(1127, 245);
			splitContainer1.SplitterDistance = 33;
			splitContainer1.TabIndex = 25;
			// 
			// btnDelete
			// 
			btnDelete.AccessibleRole = AccessibleRole.Alert;
			btnDelete.Location = new Point(682, 6);
			btnDelete.Name = "btnDelete";
			btnDelete.Size = new Size(75, 23);
			btnDelete.TabIndex = 26;
			btnDelete.Text = "&Delete";
			btnDelete.UseVisualStyleBackColor = true;
			btnDelete.Click += btnDelete_Click;
			// 
			// btnRename
			// 
			btnRename.Location = new Point(600, 6);
			btnRename.Name = "btnRename";
			btnRename.Size = new Size(75, 23);
			btnRename.TabIndex = 25;
			btnRename.Text = "&Rename";
			btnRename.UseVisualStyleBackColor = true;
			btnRename.Click += btnRename_Click;
			// 
			// btnClone
			// 
			btnClone.Location = new Point(438, 6);
			btnClone.Name = "btnClone";
			btnClone.Size = new Size(75, 23);
			btnClone.TabIndex = 24;
			btnClone.Text = "&Clone";
			btnClone.UseVisualStyleBackColor = true;
			btnClone.Click += btnClone_Click;
			// 
			// btnSave
			// 
			btnSave.Location = new Point(519, 6);
			btnSave.Name = "btnSave";
			btnSave.Size = new Size(75, 23);
			btnSave.TabIndex = 23;
			btnSave.Text = "Sa&ve";
			btnSave.UseVisualStyleBackColor = true;
			btnSave.Click += btnSave_Click;
			// 
			// btnLoad
			// 
			btnLoad.Location = new Point(357, 6);
			btnLoad.Name = "btnLoad";
			btnLoad.Size = new Size(75, 23);
			btnLoad.TabIndex = 22;
			btnLoad.Text = "&Load";
			btnLoad.UseVisualStyleBackColor = true;
			btnLoad.Click += btnLoad_Click;
			// 
			// cbxSearchSetName
			// 
			cbxSearchSetName.AutoCompleteSource = AutoCompleteSource.ListItems;
			cbxSearchSetName.FormattingEnabled = true;
			cbxSearchSetName.Location = new Point(123, 6);
			cbxSearchSetName.Name = "cbxSearchSetName";
			cbxSearchSetName.Size = new Size(214, 23);
			cbxSearchSetName.TabIndex = 21;
			cbxSearchSetName.Text = "(Default)";
			cbxSearchSetName.SelectedIndexChanged += cbxSearchSetName_SelectedIndexChanged;
			// 
			// lblSearchSetName
			// 
			lblSearchSetName.AutoSize = true;
			lblSearchSetName.Location = new Point(9, 9);
			lblSearchSetName.Name = "lblSearchSetName";
			lblSearchSetName.Size = new Size(96, 15);
			lblSearchSetName.TabIndex = 20;
			lblSearchSetName.Text = "Search Set Name";
			// 
			// chkAccessed
			// 
			chkAccessed.AutoSize = true;
			chkAccessed.Location = new Point(17, 180);
			chkAccessed.Name = "chkAccessed";
			chkAccessed.Size = new Size(75, 19);
			chkAccessed.TabIndex = 52;
			chkAccessed.Text = "Accessed";
			chkAccessed.UseVisualStyleBackColor = true;
			chkAccessed.CheckStateChanged += chkAccessed_CheckStateChanged;
			// 
			// dtpAccessedEndDate
			// 
			dtpAccessedEndDate.Location = new Point(405, 176);
			dtpAccessedEndDate.Name = "dtpAccessedEndDate";
			dtpAccessedEndDate.Size = new Size(200, 23);
			dtpAccessedEndDate.TabIndex = 51;
			dtpAccessedEndDate.ValueChanged += dtpAccessedEndDate_ValueChanged;
			// 
			// label3
			// 
			label3.AutoSize = true;
			label3.Location = new Point(370, 182);
			label3.Name = "label3";
			label3.Size = new Size(29, 15);
			label3.TabIndex = 50;
			label3.Text = "And";
			// 
			// dtpAccessedStartDate
			// 
			dtpAccessedStartDate.Location = new Point(157, 176);
			dtpAccessedStartDate.Name = "dtpAccessedStartDate";
			dtpAccessedStartDate.Size = new Size(200, 23);
			dtpAccessedStartDate.TabIndex = 49;
			dtpAccessedStartDate.ValueChanged += dtpAccessedStartDate_ValueChanged;
			// 
			// label4
			// 
			label4.AutoSize = true;
			label4.Location = new Point(99, 182);
			label4.Name = "label4";
			label4.Size = new Size(52, 15);
			label4.TabIndex = 48;
			label4.Text = "Between";
			// 
			// chkUpdated
			// 
			chkUpdated.AutoSize = true;
			chkUpdated.Location = new Point(20, 152);
			chkUpdated.Name = "chkUpdated";
			chkUpdated.Size = new Size(71, 19);
			chkUpdated.TabIndex = 47;
			chkUpdated.Text = "Updated";
			chkUpdated.UseVisualStyleBackColor = true;
			chkUpdated.CheckStateChanged += chkUpdated_CheckStateChanged;
			// 
			// dtpUpdatedEndDate
			// 
			dtpUpdatedEndDate.Location = new Point(405, 147);
			dtpUpdatedEndDate.Name = "dtpUpdatedEndDate";
			dtpUpdatedEndDate.Size = new Size(200, 23);
			dtpUpdatedEndDate.TabIndex = 46;
			dtpUpdatedEndDate.ValueChanged += dtpUpdatedEndDate_ValueChanged;
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new Point(370, 153);
			label1.Name = "label1";
			label1.Size = new Size(29, 15);
			label1.TabIndex = 45;
			label1.Text = "And";
			// 
			// dtpUpdatedStartDate
			// 
			dtpUpdatedStartDate.Location = new Point(157, 147);
			dtpUpdatedStartDate.Name = "dtpUpdatedStartDate";
			dtpUpdatedStartDate.Size = new Size(200, 23);
			dtpUpdatedStartDate.TabIndex = 44;
			dtpUpdatedStartDate.ValueChanged += dtpUpdatedStartDate_ValueChanged;
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Location = new Point(99, 153);
			label2.Name = "label2";
			label2.Size = new Size(52, 15);
			label2.TabIndex = 43;
			label2.Text = "Between";
			// 
			// chkCreated
			// 
			chkCreated.AutoSize = true;
			chkCreated.Location = new Point(20, 123);
			chkCreated.Name = "chkCreated";
			chkCreated.Size = new Size(67, 19);
			chkCreated.TabIndex = 42;
			chkCreated.Text = "Created";
			chkCreated.UseVisualStyleBackColor = true;
			chkCreated.CheckStateChanged += chkCreated_CheckStateChanged;
			// 
			// dtpCreatedEndDate
			// 
			dtpCreatedEndDate.Location = new Point(405, 119);
			dtpCreatedEndDate.Name = "dtpCreatedEndDate";
			dtpCreatedEndDate.Size = new Size(200, 23);
			dtpCreatedEndDate.TabIndex = 41;
			dtpCreatedEndDate.ValueChanged += dtpCreatedEndDate_ValueChanged;
			// 
			// lblEndDateTime
			// 
			lblEndDateTime.AutoSize = true;
			lblEndDateTime.Location = new Point(370, 125);
			lblEndDateTime.Name = "lblEndDateTime";
			lblEndDateTime.Size = new Size(29, 15);
			lblEndDateTime.TabIndex = 40;
			lblEndDateTime.Text = "And";
			// 
			// dtpCreatedStartDate
			// 
			dtpCreatedStartDate.Location = new Point(157, 119);
			dtpCreatedStartDate.Name = "dtpCreatedStartDate";
			dtpCreatedStartDate.Size = new Size(200, 23);
			dtpCreatedStartDate.TabIndex = 39;
			dtpCreatedStartDate.ValueChanged += dtpCreatedStartDate_ValueChanged;
			// 
			// lblStartDateTime
			// 
			lblStartDateTime.AutoSize = true;
			lblStartDateTime.Location = new Point(99, 125);
			lblStartDateTime.Name = "lblStartDateTime";
			lblStartDateTime.Size = new Size(52, 15);
			lblStartDateTime.TabIndex = 38;
			lblStartDateTime.Text = "Between";
			// 
			// chkIncludeSystem
			// 
			chkIncludeSystem.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			chkIncludeSystem.AutoSize = true;
			chkIncludeSystem.Location = new Point(785, 123);
			chkIncludeSystem.Name = "chkIncludeSystem";
			chkIncludeSystem.Size = new Size(106, 19);
			chkIncludeSystem.TabIndex = 37;
			chkIncludeSystem.Text = "Include &System";
			chkIncludeSystem.UseVisualStyleBackColor = true;
			// 
			// chkIncludeHidden
			// 
			chkIncludeHidden.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			chkIncludeHidden.AutoSize = true;
			chkIncludeHidden.Location = new Point(641, 152);
			chkIncludeHidden.Name = "chkIncludeHidden";
			chkIncludeHidden.Size = new Size(107, 19);
			chkIncludeHidden.TabIndex = 35;
			chkIncludeHidden.Text = "Include &Hidden";
			chkIncludeHidden.UseVisualStyleBackColor = true;
			// 
			// txtIgnoreFolders
			// 
			txtIgnoreFolders.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			txtIgnoreFolders.Location = new Point(96, 32);
			txtIgnoreFolders.MinimumSize = new Size(600, 23);
			txtIgnoreFolders.Name = "txtIgnoreFolders";
			txtIgnoreFolders.Size = new Size(1019, 23);
			txtIgnoreFolders.TabIndex = 28;
			txtIgnoreFolders.Tag = "Ignore Folders";
			// 
			// lblIgnoreFolders
			// 
			lblIgnoreFolders.AutoSize = true;
			lblIgnoreFolders.Location = new Point(9, 35);
			lblIgnoreFolders.Name = "lblIgnoreFolders";
			lblIgnoreFolders.Size = new Size(82, 15);
			lblIgnoreFolders.TabIndex = 27;
			lblIgnoreFolders.Tag = "Ignore Folders";
			lblIgnoreFolders.Text = "Ignore Folders";
			// 
			// chkSearchAsRegex
			// 
			chkSearchAsRegex.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			chkSearchAsRegex.AutoSize = true;
			chkSearchAsRegex.Location = new Point(785, 152);
			chkSearchAsRegex.Name = "chkSearchAsRegex";
			chkSearchAsRegex.Size = new Size(160, 19);
			chkSearchAsRegex.TabIndex = 33;
			chkSearchAsRegex.Text = "Eval as regular expression";
			chkSearchAsRegex.UseVisualStyleBackColor = true;
			// 
			// txtSearchPattern
			// 
			txtSearchPattern.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			txtSearchPattern.Location = new Point(96, 90);
			txtSearchPattern.MinimumSize = new Size(600, 23);
			txtSearchPattern.Name = "txtSearchPattern";
			txtSearchPattern.Size = new Size(1019, 23);
			txtSearchPattern.TabIndex = 32;
			txtSearchPattern.Tag = "Search Pattern";
			// 
			// lblSearchPattern
			// 
			lblSearchPattern.AutoSize = true;
			lblSearchPattern.Location = new Point(9, 94);
			lblSearchPattern.Name = "lblSearchPattern";
			lblSearchPattern.Size = new Size(83, 15);
			lblSearchPattern.TabIndex = 31;
			lblSearchPattern.Text = "Search Pattern";
			// 
			// txtFilePatterns
			// 
			txtFilePatterns.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			txtFilePatterns.Location = new Point(96, 61);
			txtFilePatterns.MinimumSize = new Size(600, 23);
			txtFilePatterns.Name = "txtFilePatterns";
			txtFilePatterns.Size = new Size(1019, 23);
			txtFilePatterns.TabIndex = 30;
			txtFilePatterns.Tag = "File Patterns";
			// 
			// lblFilePatterns
			// 
			lblFilePatterns.AutoSize = true;
			lblFilePatterns.Location = new Point(9, 64);
			lblFilePatterns.Name = "lblFilePatterns";
			lblFilePatterns.Size = new Size(71, 15);
			lblFilePatterns.TabIndex = 29;
			lblFilePatterns.Text = "File Patterns";
			// 
			// txtFolder
			// 
			txtFolder.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			txtFolder.Location = new Point(96, 3);
			txtFolder.MinimumSize = new Size(600, 23);
			txtFolder.Name = "txtFolder";
			txtFolder.ReadOnly = true;
			txtFolder.Size = new Size(1019, 23);
			txtFolder.TabIndex = 26;
			txtFolder.Tag = "Folder";
			// 
			// btnFolder
			// 
			btnFolder.Location = new Point(5, 3);
			btnFolder.Name = "btnFolder";
			btnFolder.Size = new Size(87, 23);
			btnFolder.TabIndex = 25;
			btnFolder.Text = "&Folder";
			btnFolder.UseVisualStyleBackColor = true;
			btnFolder.Click += btnFolder_Click;
			// 
			// btnSearch
			// 
			btnSearch.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			btnSearch.Location = new Point(951, 119);
			btnSearch.Name = "btnSearch";
			btnSearch.Size = new Size(164, 63);
			btnSearch.TabIndex = 36;
			btnSearch.Text = "&Search";
			btnSearch.UseVisualStyleBackColor = true;
			btnSearch.Click += btnSearch_Click;
			// 
			// chkRecurse
			// 
			chkRecurse.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			chkRecurse.AutoSize = true;
			chkRecurse.Location = new Point(641, 123);
			chkRecurse.Name = "chkRecurse";
			chkRecurse.Size = new Size(126, 19);
			chkRecurse.TabIndex = 34;
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
			tabcResults.Size = new Size(1127, 520);
			tabcResults.TabIndex = 0;
			// 
			// tabpFound
			// 
			tabpFound.Controls.Add(lvwFound);
			tabpFound.Location = new Point(4, 24);
			tabpFound.Name = "tabpFound";
			tabpFound.Padding = new Padding(3);
			tabpFound.Size = new Size(1119, 492);
			tabpFound.TabIndex = 0;
			tabpFound.Tag = "";
			tabpFound.Text = "Found";
			tabpFound.UseVisualStyleBackColor = true;
			// 
			// lvwFound
			// 
			lvwFound.Dock = DockStyle.Fill;
			lvwFound.Location = new Point(3, 3);
			lvwFound.Name = "lvwFound";
			lvwFound.Size = new Size(1113, 486);
			lvwFound.TabIndex = 0;
			lvwFound.UseCompatibleStateImageBehavior = false;
			// 
			// tabpErrors
			// 
			tabpErrors.Controls.Add(lvwErrors);
			tabpErrors.Location = new Point(4, 24);
			tabpErrors.Name = "tabpErrors";
			tabpErrors.Padding = new Padding(3);
			tabpErrors.Size = new Size(1119, 492);
			tabpErrors.TabIndex = 1;
			tabpErrors.Tag = "";
			tabpErrors.Text = "Errors";
			tabpErrors.UseVisualStyleBackColor = true;
			// 
			// lvwErrors
			// 
			lvwErrors.Dock = DockStyle.Fill;
			lvwErrors.Location = new Point(3, 3);
			lvwErrors.Name = "lvwErrors";
			lvwErrors.Size = new Size(1113, 486);
			lvwErrors.TabIndex = 0;
			lvwErrors.UseCompatibleStateImageBehavior = false;
			// 
			// MainForm
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(1127, 791);
			Controls.Add(scMain);
			Controls.Add(statusStrip1);
			Icon = (Icon)resources.GetObject("$this.Icon");
			MinimumSize = new Size(100, 100);
			Name = "MainForm";
			Text = "BOG.TextFileSearch";
			FormClosing += MainForm_FormClosing;
			Load += MainForm_Load;
			statusStrip1.ResumeLayout(false);
			statusStrip1.PerformLayout();
			scMain.Panel1.ResumeLayout(false);
			scMain.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)scMain).EndInit();
			scMain.ResumeLayout(false);
			splitContainer1.Panel1.ResumeLayout(false);
			splitContainer1.Panel1.PerformLayout();
			splitContainer1.Panel2.ResumeLayout(false);
			splitContainer1.Panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
			splitContainer1.ResumeLayout(false);
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
		private FolderBrowserDialog folderBrowserDialog1;
		private SplitContainer splitContainer1;
		private Button btnDelete;
		private Button btnRename;
		private Button btnClone;
		private Button btnSave;
		private Button btnLoad;
		private ComboBox cbxSearchSetName;
		private Label lblSearchSetName;
		private DateTimePicker dtpCreatedEndDate;
		private Label lblEndDateTime;
		private DateTimePicker dtpCreatedStartDate;
		private Label lblStartDateTime;
		private CheckBox chkIncludeSystem;
		private CheckBox chkIncludeHidden;
		private TextBox txtIgnoreFolders;
		private Label lblIgnoreFolders;
		private CheckBox chkSearchAsRegex;
		private TextBox txtSearchPattern;
		private Label lblSearchPattern;
		private TextBox txtFilePatterns;
		private Label lblFilePatterns;
		private TextBox txtFolder;
		private Button btnFolder;
		private Button btnSearch;
		private CheckBox chkRecurse;
		private CheckBox chkCreated;
		private CheckBox chkUpdated;
		private DateTimePicker dtpUpdatedEndDate;
		private Label label1;
		private DateTimePicker dtpUpdatedStartDate;
		private Label label2;
		private CheckBox chkAccessed;
		private DateTimePicker dtpAccessedEndDate;
		private Label label3;
		private DateTimePicker dtpAccessedStartDate;
		private Label label4;
	}
}
