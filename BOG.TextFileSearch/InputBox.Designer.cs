namespace BOG.TextFileSearch
{
	partial class InputBox
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
			components = new System.ComponentModel.Container();
			btnAccept = new Button();
			btnReset = new Button();
			btnCancel = new Button();
			txtInput = new TextBox();
			tmrOneSecond = new System.Windows.Forms.Timer(components);
			lblCountDown = new Label();
			SuspendLayout();
			// 
			// btnAccept
			// 
			btnAccept.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			btnAccept.Location = new Point(140, 42);
			btnAccept.Name = "btnAccept";
			btnAccept.Size = new Size(75, 23);
			btnAccept.TabIndex = 1;
			btnAccept.Text = "Accept";
			btnAccept.UseVisualStyleBackColor = true;
			btnAccept.Click += btnAccept_Click;
			// 
			// btnReset
			// 
			btnReset.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			btnReset.Location = new Point(221, 42);
			btnReset.Name = "btnReset";
			btnReset.Size = new Size(75, 23);
			btnReset.TabIndex = 2;
			btnReset.Text = "Reset";
			btnReset.UseVisualStyleBackColor = true;
			btnReset.Click += btnReset_Click;
			// 
			// btnCancel
			// 
			btnCancel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			btnCancel.Location = new Point(302, 42);
			btnCancel.Name = "btnCancel";
			btnCancel.Size = new Size(75, 23);
			btnCancel.TabIndex = 3;
			btnCancel.Text = "Cancel";
			btnCancel.UseVisualStyleBackColor = true;
			btnCancel.Click += btnCancel_Click;
			// 
			// txtInput
			// 
			txtInput.Location = new Point(14, 12);
			txtInput.Name = "txtInput";
			txtInput.Size = new Size(363, 23);
			txtInput.TabIndex = 0;
			txtInput.KeyPress += txtInput_KeyPress;
			// 
			// tmrOneSecond
			// 
			tmrOneSecond.Tick += tmrOneSecond_Tick;
			// 
			// lblCountDown
			// 
			lblCountDown.AutoSize = true;
			lblCountDown.BackColor = SystemColors.Info;
			lblCountDown.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
			lblCountDown.Location = new Point(14, 44);
			lblCountDown.Name = "lblCountDown";
			lblCountDown.Size = new Size(45, 17);
			lblCountDown.TabIndex = 4;
			lblCountDown.Text = "label1";
			lblCountDown.Visible = false;
			// 
			// InputBox
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(389, 69);
			ControlBox = false;
			Controls.Add(lblCountDown);
			Controls.Add(txtInput);
			Controls.Add(btnCancel);
			Controls.Add(btnReset);
			Controls.Add(btnAccept);
			MaximizeBox = false;
			MaximumSize = new Size(405, 130);
			MinimizeBox = false;
			MinimumSize = new Size(405, 108);
			Name = "InputBox";
			ShowIcon = false;
			Text = "InputBox";
			Load += InputBox_Load;
			KeyPress += InputBox_KeyPress;
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Button btnAccept;
		private Button btnReset;
		private Button btnCancel;
		private TextBox txtInput;
		private System.Windows.Forms.Timer tmrOneSecond;
		private Label lblCountDown;
	}
}