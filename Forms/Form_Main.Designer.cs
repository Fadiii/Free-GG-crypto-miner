using System.Collections.Generic;

namespace NiceHashMiner
{
    partial class Form_Main
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
            this.buttonStartMining = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelGlobalRateText = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelGlobalRateValue = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelBalanceText = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelBalanceBTCValue = new System.Windows.Forms.ToolStripStatusLabel();
            this.pointsBalanceLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.buttonStopMining = new System.Windows.Forms.Button();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.flowLayoutPanelRates = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.LinkIDTextBox = new System.Windows.Forms.TextBox();
            this.LinkIDLabel = new System.Windows.Forms.Label();
            this.StatusLabel = new System.Windows.Forms.Label();
            this.AdvancedButton = new System.Windows.Forms.Button();
            this.DownloadButton = new System.Windows.Forms.Button();
            this.BenchmarkButton = new System.Windows.Forms.Button();
            this.devicesListViewEnableControl1 = new NiceHashMiner.Forms.Components.DevicesListViewEnableControl();
            this.statusStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonStartMining
            // 
            this.buttonStartMining.Location = new System.Drawing.Point(141, 154);
            this.buttonStartMining.Name = "buttonStartMining";
            this.buttonStartMining.Size = new System.Drawing.Size(89, 23);
            this.buttonStartMining.TabIndex = 6;
            this.buttonStartMining.Text = "&Start";
            this.buttonStartMining.UseVisualStyleBackColor = true;
            this.buttonStartMining.Click += new System.EventHandler(this.buttonStartMining_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelGlobalRateText,
            this.toolStripStatusLabelGlobalRateValue,
            this.toolStripStatusLabelBalanceText,
            this.toolStripStatusLabelBalanceBTCValue,
            this.pointsBalanceLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 429);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.ShowItemToolTips = true;
            this.statusStrip1.Size = new System.Drawing.Size(369, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.Stretch = false;
            this.statusStrip1.TabIndex = 8;
            this.statusStrip1.Text = "       ";
            this.statusStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.statusStrip1_ItemClicked);
            // 
            // toolStripStatusLabelGlobalRateText
            // 
            this.toolStripStatusLabelGlobalRateText.Name = "toolStripStatusLabelGlobalRateText";
            this.toolStripStatusLabelGlobalRateText.Size = new System.Drawing.Size(79, 17);
            this.toolStripStatusLabelGlobalRateText.Text = " Earning Rate:";
            // 
            // toolStripStatusLabelGlobalRateValue
            // 
            this.toolStripStatusLabelGlobalRateValue.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.toolStripStatusLabelGlobalRateValue.Name = "toolStripStatusLabelGlobalRateValue";
            this.toolStripStatusLabelGlobalRateValue.Size = new System.Drawing.Size(31, 17);
            this.toolStripStatusLabelGlobalRateValue.Text = "0.00";
            // 
            // toolStripStatusLabelBalanceText
            // 
            this.toolStripStatusLabelBalanceText.Name = "toolStripStatusLabelBalanceText";
            this.toolStripStatusLabelBalanceText.Size = new System.Drawing.Size(178, 17);
            this.toolStripStatusLabelBalanceText.Text = "Points/Day                       Balance:";
            // 
            // toolStripStatusLabelBalanceBTCValue
            // 
            this.toolStripStatusLabelBalanceBTCValue.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.toolStripStatusLabelBalanceBTCValue.Name = "toolStripStatusLabelBalanceBTCValue";
            this.toolStripStatusLabelBalanceBTCValue.Size = new System.Drawing.Size(38, 17);
            this.toolStripStatusLabelBalanceBTCValue.Text = "0.000";
            // 
            // pointsBalanceLabel
            // 
            this.pointsBalanceLabel.Name = "pointsBalanceLabel";
            this.pointsBalanceLabel.Size = new System.Drawing.Size(40, 17);
            this.pointsBalanceLabel.Text = "Points";
            // 
            // buttonStopMining
            // 
            this.buttonStopMining.Enabled = false;
            this.buttonStopMining.Location = new System.Drawing.Point(141, 183);
            this.buttonStopMining.Name = "buttonStopMining";
            this.buttonStopMining.Size = new System.Drawing.Size(89, 23);
            this.buttonStopMining.TabIndex = 7;
            this.buttonStopMining.Text = "St&op";
            this.buttonStopMining.UseVisualStyleBackColor = true;
            this.buttonStopMining.Click += new System.EventHandler(this.buttonStopMining_Click);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.DoubleClick += new System.EventHandler(this.notifyIcon1_DoubleClick);
            // 
            // flowLayoutPanelRates
            // 
            this.flowLayoutPanelRates.AutoScroll = true;
            this.flowLayoutPanelRates.AutoSize = true;
            this.flowLayoutPanelRates.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanelRates.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanelRates.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanelRates.Location = new System.Drawing.Point(3, 16);
            this.flowLayoutPanelRates.Name = "flowLayoutPanelRates";
            this.flowLayoutPanelRates.Size = new System.Drawing.Size(335, 26);
            this.flowLayoutPanelRates.TabIndex = 107;
            this.flowLayoutPanelRates.WrapContents = false;
            // 
            // groupBox1
            // 
            this.groupBox1.AutoSize = true;
            this.groupBox1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox1.Controls.Add(this.flowLayoutPanelRates);
            this.groupBox1.Location = new System.Drawing.Point(14, 377);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 3, 3, 30);
            this.groupBox1.MinimumSize = new System.Drawing.Size(341, 45);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(341, 45);
            this.groupBox1.TabIndex = 108;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Group/Device Rates:";
            this.groupBox1.Visible = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::NiceHashMiner.Properties.Resources.logo_Big;
            this.pictureBox1.Location = new System.Drawing.Point(0, 1);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(373, 127);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 110;
            this.pictureBox1.TabStop = false;
            // 
            // LinkIDTextBox
            // 
            this.LinkIDTextBox.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.LinkIDTextBox.Location = new System.Drawing.Point(171, 128);
            this.LinkIDTextBox.Name = "LinkIDTextBox";
            this.LinkIDTextBox.Size = new System.Drawing.Size(143, 20);
            this.LinkIDTextBox.TabIndex = 111;
            this.LinkIDTextBox.Text = "Your Free-GG link..";
            this.LinkIDTextBox.Enter += new System.EventHandler(this.LinkIDTextBox_Enter);
            this.LinkIDTextBox.Leave += new System.EventHandler(this.LinkIDTextBox_Leave);
            // 
            // LinkIDLabel
            // 
            this.LinkIDLabel.AutoSize = true;
            this.LinkIDLabel.Location = new System.Drawing.Point(75, 131);
            this.LinkIDLabel.Name = "LinkIDLabel";
            this.LinkIDLabel.Size = new System.Drawing.Size(75, 13);
            this.LinkIDLabel.TabIndex = 112;
            this.LinkIDLabel.Text = "Referral Code:";
            // 
            // StatusLabel
            // 
            this.StatusLabel.Location = new System.Drawing.Point(37, 213);
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(297, 13);
            this.StatusLabel.TabIndex = 113;
            this.StatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.StatusLabel.Click += new System.EventHandler(this.StatusLabel_Click);
            // 
            // AdvancedButton
            // 
            this.AdvancedButton.Location = new System.Drawing.Point(141, 229);
            this.AdvancedButton.Name = "AdvancedButton";
            this.AdvancedButton.Size = new System.Drawing.Size(89, 23);
            this.AdvancedButton.TabIndex = 114;
            this.AdvancedButton.Text = "Advanced";
            this.AdvancedButton.UseVisualStyleBackColor = true;
            this.AdvancedButton.Click += new System.EventHandler(this.AdvancedButton_Click);
            // 
            // DownloadButton
            // 
            this.DownloadButton.AutoSize = true;
            this.DownloadButton.Location = new System.Drawing.Point(218, 258);
            this.DownloadButton.Name = "DownloadButton";
            this.DownloadButton.Size = new System.Drawing.Size(99, 23);
            this.DownloadButton.TabIndex = 115;
            this.DownloadButton.Text = "Download Miners";
            this.DownloadButton.UseVisualStyleBackColor = true;
            this.DownloadButton.Visible = false;
            this.DownloadButton.Click += new System.EventHandler(this.DownloadButton_Click);
            // 
            // BenchmarkButton
            // 
            this.BenchmarkButton.AutoSize = true;
            this.BenchmarkButton.Location = new System.Drawing.Point(54, 258);
            this.BenchmarkButton.Name = "BenchmarkButton";
            this.BenchmarkButton.Size = new System.Drawing.Size(99, 23);
            this.BenchmarkButton.TabIndex = 116;
            this.BenchmarkButton.Text = "Benchmark";
            this.BenchmarkButton.UseVisualStyleBackColor = true;
            this.BenchmarkButton.Visible = false;
            this.BenchmarkButton.Click += new System.EventHandler(this.BenchmarkButton_Click);
            // 
            // devicesListViewEnableControl1
            // 
            this.devicesListViewEnableControl1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.devicesListViewEnableControl1.BenchmarkCalculation = null;
            this.devicesListViewEnableControl1.FirstColumnText = "Enabled";
            this.devicesListViewEnableControl1.IsInBenchmark = false;
            this.devicesListViewEnableControl1.IsMining = false;
            this.devicesListViewEnableControl1.Location = new System.Drawing.Point(13, 289);
            this.devicesListViewEnableControl1.Margin = new System.Windows.Forms.Padding(0);
            this.devicesListViewEnableControl1.Name = "devicesListViewEnableControl1";
            this.devicesListViewEnableControl1.SaveToGeneralConfig = false;
            this.devicesListViewEnableControl1.Size = new System.Drawing.Size(341, 80);
            this.devicesListViewEnableControl1.TabIndex = 109;
            this.devicesListViewEnableControl1.Visible = false;
            this.devicesListViewEnableControl1.Load += new System.EventHandler(this.devicesListViewEnableControl1_Load);
            // 
            // Form_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(369, 451);
            this.Controls.Add(this.BenchmarkButton);
            this.Controls.Add(this.DownloadButton);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.devicesListViewEnableControl1);
            this.Controls.Add(this.AdvancedButton);
            this.Controls.Add(this.StatusLabel);
            this.Controls.Add(this.LinkIDLabel);
            this.Controls.Add(this.LinkIDTextBox);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.buttonStopMining);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.buttonStartMining);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(300, 330);
            this.Name = "Form_Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Free-GG Miner";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form_Main_Load);
            this.Shown += new System.EventHandler(this.Form_Main_Shown);
            this.Click += new System.EventHandler(this.Form_Main_Click);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonStartMining;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelGlobalRateValue;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelBalanceText;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelBalanceBTCValue;
        private System.Windows.Forms.Button buttonStopMining;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelRates;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label LinkIDLabel;
        private System.Windows.Forms.Label StatusLabel;
        private System.Windows.Forms.Button AdvancedButton;
        private Forms.Components.DevicesListViewEnableControl devicesListViewEnableControl1;
        private System.Windows.Forms.Button DownloadButton;
        private System.Windows.Forms.Button BenchmarkButton;
        private System.Windows.Forms.TextBox LinkIDTextBox;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelGlobalRateText;
        private System.Windows.Forms.ToolStripStatusLabel pointsBalanceLabel;
    }
}



