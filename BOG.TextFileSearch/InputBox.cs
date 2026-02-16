using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Bson;

namespace BOG.TextFileSearch
{
	/// <summary>
	/// A simple string inout box with initial value, and optional timeout. 
	/// If timeout is set, the input box will automatically close after the specified seconds, and return the default value.
	/// </summary>
	public partial class InputBox : Form
	{
		private string _defaultValue = string.Empty;
		private int _timeoutSeconds = 0;
		private bool _formIsClosing = false;
		private bool _formTimeout = false;

		private Stopwatch _StopWatch = new Stopwatch();
		private int _RemainingSeconds = -1;

		public string Value { get; private set; } = string.Empty;
		public bool TimedOut { get; private set; } = false;

		public InputBox(string title, string defaultValue, int timeoutSeconds)
		{
			InitializeComponent();
			_defaultValue = defaultValue;
			_timeoutSeconds = timeoutSeconds;
		}

		public InputBox(string title, string defaultValue)
		{
			InitializeComponent();
			_defaultValue = defaultValue;
		}

		private void InputBox_Load(object sender, EventArgs e)
		{
			if (_timeoutSeconds > 0)
			{
				_StopWatch.Start();
				this.tmrOneSecond.Interval = 100;
				this.tmrOneSecond.Enabled = true;
				tmrOneSecond.Start();
				this.lblCountDown.Text = $"{_timeoutSeconds}";
				this.lblCountDown.Visible = true;
			}
			this.txtInput.Text = _defaultValue;
			this.txtInput.SelectAll();
		}

		private void tmrOneSecond_Tick(object sender, EventArgs e)
		{
			tmrOneSecond.Stop();
			var remainingSeconds = _timeoutSeconds - (int)_StopWatch.Elapsed.TotalSeconds;
			if (remainingSeconds < 0)
			{
				_StopWatch.Stop();
				TimedOut = true;
				this.DialogResult = DialogResult.Cancel;
				this.Value = _defaultValue;
				this.Close();
				return;
			}
			if (remainingSeconds != _RemainingSeconds)
			{
				this.lblCountDown.Text = $" {remainingSeconds}s ";
				_RemainingSeconds = remainingSeconds;
			}
			tmrOneSecond.Start();
		}

		private void btnAccept_Click(object sender, EventArgs e)
		{
			StopTimeout();
			Value = this.txtInput.Text;
			DialogResult = DialogResult.OK;
			this.Close();
		}

		private void btnReset_Click(object sender, EventArgs e)
		{
			StopTimeout();
			this.txtInput.Text = _defaultValue;
			this.txtInput.SelectAll();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			StopTimeout();
			DialogResult = DialogResult.Cancel;
			this.Close();
		}

		private void InputBox_KeyPress(object sender, KeyPressEventArgs e)
		{
			StopTimeout();
		}

		private void txtInput_KeyPress(object sender, KeyPressEventArgs e)
		{
			StopTimeout();
		}

		private void StopTimeout()
		{
			if (tmrOneSecond.Enabled)
			{
				tmrOneSecond.Stop();
				tmrOneSecond.Enabled = false;
				this.lblCountDown.Visible = false;
			}
		}
	}
}
