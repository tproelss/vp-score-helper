
namespace VPScoreHelper
{
    partial class VPScoreHelper
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VPScoreHelper));
            this.sharpClipboard1 = new WK.Libraries.SharpClipboardNS.SharpClipboard(this.components);
            this.tbGoogleSheet = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btAttach = new System.Windows.Forms.Button();
            this.rtbLog = new System.Windows.Forms.RichTextBox();
            this.btOpenBrowser = new System.Windows.Forms.Button();
            this.panelFoundScore = new System.Windows.Forms.Panel();
            this.btSaveScore = new System.Windows.Forms.Button();
            this.rtbNewScore = new System.Windows.Forms.RichTextBox();
            this.panelFoundScore.SuspendLayout();
            this.SuspendLayout();
            // 
            // sharpClipboard1
            // 
            this.sharpClipboard1.MonitorClipboard = true;
            this.sharpClipboard1.ObservableFormats.All = true;
            this.sharpClipboard1.ObservableFormats.Files = false;
            this.sharpClipboard1.ObservableFormats.Images = false;
            this.sharpClipboard1.ObservableFormats.Others = false;
            this.sharpClipboard1.ObservableFormats.Texts = true;
            this.sharpClipboard1.ObserveLastEntry = true;
            this.sharpClipboard1.Tag = null;
            this.sharpClipboard1.ClipboardChanged += new System.EventHandler<WK.Libraries.SharpClipboardNS.SharpClipboard.ClipboardChangedEventArgs>(this.sharpClipboard1_ClipboardChanged);
            // 
            // tbGoogleSheet
            // 
            this.tbGoogleSheet.Location = new System.Drawing.Point(12, 28);
            this.tbGoogleSheet.Name = "tbGoogleSheet";
            this.tbGoogleSheet.Size = new System.Drawing.Size(613, 20);
            this.tbGoogleSheet.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Silver;
            this.label1.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 14);
            this.label1.TabIndex = 2;
            this.label1.Text = "score sheet url";
            // 
            // btAttach
            // 
            this.btAttach.Location = new System.Drawing.Point(12, 54);
            this.btAttach.Name = "btAttach";
            this.btAttach.Size = new System.Drawing.Size(75, 23);
            this.btAttach.TabIndex = 1;
            this.btAttach.Text = "attach";
            this.btAttach.UseVisualStyleBackColor = true;
            this.btAttach.Click += new System.EventHandler(this.btAttach_Click);
            // 
            // rtbLog
            // 
            this.rtbLog.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbLog.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbLog.Location = new System.Drawing.Point(12, 165);
            this.rtbLog.Name = "rtbLog";
            this.rtbLog.ReadOnly = true;
            this.rtbLog.Size = new System.Drawing.Size(613, 129);
            this.rtbLog.TabIndex = 4;
            this.rtbLog.Text = "";
            // 
            // btOpenBrowser
            // 
            this.btOpenBrowser.Location = new System.Drawing.Point(102, 54);
            this.btOpenBrowser.Name = "btOpenBrowser";
            this.btOpenBrowser.Size = new System.Drawing.Size(75, 23);
            this.btOpenBrowser.TabIndex = 2;
            this.btOpenBrowser.Text = "browser";
            this.btOpenBrowser.UseVisualStyleBackColor = true;
            this.btOpenBrowser.Click += new System.EventHandler(this.btOpenBrowser_Click);
            // 
            // panelFoundScore
            // 
            this.panelFoundScore.BackColor = System.Drawing.Color.Transparent;
            this.panelFoundScore.Controls.Add(this.btSaveScore);
            this.panelFoundScore.Controls.Add(this.rtbNewScore);
            this.panelFoundScore.Location = new System.Drawing.Point(12, 98);
            this.panelFoundScore.Name = "panelFoundScore";
            this.panelFoundScore.Size = new System.Drawing.Size(613, 61);
            this.panelFoundScore.TabIndex = 6;
            this.panelFoundScore.Visible = false;
            // 
            // btSaveScore
            // 
            this.btSaveScore.Location = new System.Drawing.Point(512, 0);
            this.btSaveScore.Name = "btSaveScore";
            this.btSaveScore.Size = new System.Drawing.Size(89, 56);
            this.btSaveScore.TabIndex = 0;
            this.btSaveScore.Text = "save to sheet";
            this.btSaveScore.UseVisualStyleBackColor = true;
            this.btSaveScore.Click += new System.EventHandler(this.btSaveScore_Click);
            // 
            // rtbNewScore
            // 
            this.rtbNewScore.Font = new System.Drawing.Font("Courier New", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbNewScore.Location = new System.Drawing.Point(3, 0);
            this.rtbNewScore.Name = "rtbNewScore";
            this.rtbNewScore.ReadOnly = true;
            this.rtbNewScore.Size = new System.Drawing.Size(503, 56);
            this.rtbNewScore.TabIndex = 0;
            this.rtbNewScore.Text = "";
            // 
            // VPScoreHelper
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(637, 306);
            this.Controls.Add(this.panelFoundScore);
            this.Controls.Add(this.btOpenBrowser);
            this.Controls.Add(this.rtbLog);
            this.Controls.Add(this.btAttach);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbGoogleSheet);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "VPScoreHelper";
            this.Text = "TEVPWC ScoreHelper";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.VPScoreHelper_Load);
            this.panelFoundScore.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private WK.Libraries.SharpClipboardNS.SharpClipboard sharpClipboard1;
        private System.Windows.Forms.TextBox tbGoogleSheet;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btAttach;
        private System.Windows.Forms.RichTextBox rtbLog;
        private System.Windows.Forms.Button btOpenBrowser;
        private System.Windows.Forms.Panel panelFoundScore;
        private System.Windows.Forms.RichTextBox rtbNewScore;
        private System.Windows.Forms.Button btSaveScore;
    }
}

