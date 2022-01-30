using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VPScoreHelper
{
    public partial class VPScoreHelper : Form
    {
        public VPScoreHelper()
        {
            InitializeComponent();
        }

        private ScoreEntry ScoreEntry 
        {
            get {
                return this.ScoreEntry;
            }
            set {
                this._scoreEntry = value;

                if (this._scoreEntry != null)
                    ScoreEntryToRtfBox();

                this.panelFoundScore.Visible = _scoreEntry != null;
            }
        }
        private ScoreEntry _scoreEntry;

        private void ScoreEntryToRtfBox(string decoration = "")
        {
            rtbNewScore.Text = $"{_scoreEntry.Name}\n{ConversionHelper.GetStringFromScore(_scoreEntry.Score)} {decoration}";
        }

        private GoogleSheet _googleSheet = new GoogleSheet();

        private void sharpClipboard1_ClipboardChanged(object sender, WK.Libraries.SharpClipboardNS.SharpClipboard.ClipboardChangedEventArgs e)
        {
            if (!_googleSheet.IsAttached)
                return;

            string clipboardText = e.Content as string;

            ScoreEntry se = ClipboardParser.GetNewScoreEntryFromClipboardContent(clipboardText, _googleSheet.ScoreEntries);

            if (se == null)
            {
                Log.Instance.WriteLine(LogLevel.Info, "Can not match clipboard content to score entry");
                this.ScoreEntry = null;
                clipboardText = clipboardText.Replace(Environment.NewLine, " ");
                LogClipboardHint(clipboardText);
            }
            else 
            {
                Log.Instance.WriteLine(LogLevel.Info, $"Found score. Name={se.Name}, Score={ConversionHelper.GetStringFromScore(se.Score)}");
                this.ScoreEntry = se;
                rtbNewScore.BackColor = Color.LightBlue;
                this.WindowState = FormWindowState.Normal;                
                this.BringToFront();
                this.btSaveScore.Focus();
            }
        }

        private void LogClipboardHint(string clipboardText)
        {
            int maxLen = 100;

            if (string.IsNullOrEmpty(clipboardText))
                return;

            if (clipboardText.Length > maxLen)
            {
                clipboardText = clipboardText.Substring(0, maxLen);
                clipboardText += "..";
            }

            Log.Instance.WriteLine(LogLevel.Info, "Clipboard: " + clipboardText);                
        }

        private void VPScoreHelper_Load(object sender, EventArgs e)
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            Log.Instance.NewLogEntry += Instance_NewLogEntry;

            if (!File.Exists(GoogleSheet.CredentialsFile))
            {
                Log.Instance.WriteLine(LogLevel.Error, $"Credentials file {GoogleSheet.CredentialsFile} not found.");
                
                this.Text = "ERROR - no credentials found";
                MessageBox.Show("Credentials missing - app can not access the spread sheet",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            try
            {
                tbGoogleSheet.Text = Properties.Settings.Default["tbGoogleSheet"] as string;
            }
            catch (Exception) { };

            Log.Instance.WriteLine(LogLevel.Info, "Welcome!");

            _googleSheet.Attached += _googleSheet_Attached;

            if (!string.IsNullOrEmpty(tbGoogleSheet.Text))
                btAttach_Click(null, null);

            try
            {
                string lastSavedScore = Properties.Settings.Default["lastSavedScore"] as string;
                if (!string.IsNullOrEmpty(lastSavedScore))
                    Log.Instance.WriteLine(LogLevel.Info, $"Last saved score was {lastSavedScore}");
            }
            catch (Exception) { };

            btOpenBrowser.Focus();
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var ex = e.ExceptionObject as Exception;
            if (ex == null)
            {
                Log.Instance.WriteLine(LogLevel.Error, $"Unhandled Exception occured");
                return;
            }

            Log.Instance.WriteLine(LogLevel.Error, $"Unhandled Exception: {ex.Message}\n{ex.StackTrace}");
        }

        private void _googleSheet_Attached(object sender, EventArgs e)
        {
            this.Text = $"TEVPWC ScoreHelper - {_googleSheet.Title}";
        }

        private StringBuilder _sbLog = new StringBuilder();

        private void Instance_NewLogEntry(object sender, LogEventArgs e)
        {
            _sbLog.AppendLine(e.Text);            
            this.rtbLog.Text = _sbLog.ToString();
            this.rtbLog.SelectionStart = rtbLog.Text.Length;
            this.rtbLog.ScrollToCaret();
        }

        private void btAttach_Click(object sender, EventArgs e)
        {            
            Properties.Settings.Default["tbGoogleSheet"] = tbGoogleSheet.Text;
            Properties.Settings.Default.Save();

            var spreadSheetId = ConversionHelper.GetSpreadSheetId(tbGoogleSheet.Text);

            try
            {
                _googleSheet.Attach(spreadSheetId);
            }
            catch (Exception ex)
            {
                this.Text = "TEVPWC ScoreHelper - Error";
                Log.Instance.WriteLine(LogLevel.Error, ex.Message);
                Log.Instance.WriteLine(LogLevel.Error, ex.StackTrace);

                MessageBox.Show("There was an error connecting the google sheet. Maybe URL is wrong? Or permission is not granted for this tool?", 
                    "Error", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Error);
            }
        }

        private void btOpenBrowser_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(tbGoogleSheet.Text);
        }

        private void btSaveScore_Click(object sender, EventArgs e)
        {
            if (_scoreEntry == null)
                return;

            bool result = _googleSheet.WriteToSheet(_scoreEntry);

            if (result == true)
            {
                ScoreEntryToRtfBox("(SAVED)");
                rtbNewScore.BackColor = Color.LightGreen;

                Properties.Settings.Default["lastSavedScore"] = $"{ConversionHelper.GetStringFromScore(_scoreEntry.Score)} for {_scoreEntry.Name}";
                Properties.Settings.Default.Save();

                Log.Instance.WriteLine(LogLevel.Info, $"saved {ConversionHelper.GetStringFromScore(_scoreEntry.Score)} for {_scoreEntry.Name} at {_scoreEntry.ScoreCell}");
            }
            else
            {
                ScoreEntryToRtfBox("(ERROR) Plz. restart this app");
                rtbNewScore.BackColor = Color.Red;
            }
        }
    }
}
