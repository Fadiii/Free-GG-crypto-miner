namespace NiceHashMiner.Forms {
    partial class Form_Benchmark {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.StartStopBtn = new System.Windows.Forms.Button();
            this.CloseBtn = new System.Windows.Forms.Button();
            this.groupBoxBenchmarkProgress = new System.Windows.Forms.GroupBox();
            this.benchmarkStatusLabel = new System.Windows.Forms.Label();
            this.labelBenchmarkSteps = new System.Windows.Forms.Label();
            this.progressBarBenchmarkSteps = new System.Windows.Forms.ProgressBar();
            this.benchmarkAllButton = new System.Windows.Forms.Button();
            this.algorithmsListView1 = new NiceHashMiner.Forms.Components.AlgorithmsListView();
            this.devicesListViewEnableControl1 = new NiceHashMiner.Forms.Components.DevicesListViewEnableControl();
            this.groupBoxBenchmarkProgress.SuspendLayout();
            this.SuspendLayout();
            // 
            // StartStopBtn
            // 
            this.StartStopBtn.Location = new System.Drawing.Point(169, 295);
            this.StartStopBtn.Name = "StartStopBtn";
            this.StartStopBtn.Size = new System.Drawing.Size(83, 23);
            this.StartStopBtn.TabIndex = 100;
            this.StartStopBtn.Text = "&Start";
            this.StartStopBtn.UseVisualStyleBackColor = true;
            this.StartStopBtn.Click += new System.EventHandler(this.StartStopBtn_Click);
            // 
            // CloseBtn
            // 
            this.CloseBtn.Location = new System.Drawing.Point(263, 295);
            this.CloseBtn.Name = "CloseBtn";
            this.CloseBtn.Size = new System.Drawing.Size(83, 23);
            this.CloseBtn.TabIndex = 101;
            this.CloseBtn.Text = "&Close";
            this.CloseBtn.UseVisualStyleBackColor = true;
            this.CloseBtn.Click += new System.EventHandler(this.CloseBtn_Click);
            // 
            // groupBoxBenchmarkProgress
            // 
            this.groupBoxBenchmarkProgress.Controls.Add(this.benchmarkStatusLabel);
            this.groupBoxBenchmarkProgress.Controls.Add(this.labelBenchmarkSteps);
            this.groupBoxBenchmarkProgress.Controls.Add(this.progressBarBenchmarkSteps);
            this.groupBoxBenchmarkProgress.Location = new System.Drawing.Point(12, 242);
            this.groupBoxBenchmarkProgress.Name = "groupBoxBenchmarkProgress";
            this.groupBoxBenchmarkProgress.Size = new System.Drawing.Size(393, 47);
            this.groupBoxBenchmarkProgress.TabIndex = 108;
            this.groupBoxBenchmarkProgress.TabStop = false;
            this.groupBoxBenchmarkProgress.Text = "Benchmark progress status:";
            // 
            // benchmarkStatusLabel
            // 
            this.benchmarkStatusLabel.Location = new System.Drawing.Point(7, 20);
            this.benchmarkStatusLabel.Name = "benchmarkStatusLabel";
            this.benchmarkStatusLabel.Size = new System.Drawing.Size(87, 17);
            this.benchmarkStatusLabel.TabIndex = 110;
            this.benchmarkStatusLabel.Text = "Ready";
            this.benchmarkStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelBenchmarkSteps
            // 
            this.labelBenchmarkSteps.AutoSize = true;
            this.labelBenchmarkSteps.Location = new System.Drawing.Point(100, 22);
            this.labelBenchmarkSteps.Name = "labelBenchmarkSteps";
            this.labelBenchmarkSteps.Size = new System.Drawing.Size(36, 13);
            this.labelBenchmarkSteps.TabIndex = 109;
            this.labelBenchmarkSteps.Text = "(0/10)";
            // 
            // progressBarBenchmarkSteps
            // 
            this.progressBarBenchmarkSteps.Location = new System.Drawing.Point(151, 16);
            this.progressBarBenchmarkSteps.Name = "progressBarBenchmarkSteps";
            this.progressBarBenchmarkSteps.Size = new System.Drawing.Size(226, 23);
            this.progressBarBenchmarkSteps.TabIndex = 108;
            // 
            // benchmarkAllButton
            // 
            this.benchmarkAllButton.Location = new System.Drawing.Point(74, 295);
            this.benchmarkAllButton.Name = "benchmarkAllButton";
            this.benchmarkAllButton.Size = new System.Drawing.Size(83, 23);
            this.benchmarkAllButton.TabIndex = 110;
            this.benchmarkAllButton.Text = "Start All";
            this.benchmarkAllButton.UseVisualStyleBackColor = true;
            this.benchmarkAllButton.Click += new System.EventHandler(this.benchmarkAllButton_Click);
            // 
            // algorithmsListView1
            // 
            this.algorithmsListView1.BenchmarkCalculation = null;
            this.algorithmsListView1.ComunicationInterface = null;
            this.algorithmsListView1.IsInBenchmark = false;
            this.algorithmsListView1.Location = new System.Drawing.Point(12, 104);
            this.algorithmsListView1.Name = "algorithmsListView1";
            this.algorithmsListView1.Size = new System.Drawing.Size(393, 127);
            this.algorithmsListView1.TabIndex = 109;
            this.algorithmsListView1.Load += new System.EventHandler(this.algorithmsListView1_Load);
            // 
            // devicesListViewEnableControl1
            // 
            this.devicesListViewEnableControl1.BenchmarkCalculation = null;
            this.devicesListViewEnableControl1.FirstColumnText = "Benckmark";
            this.devicesListViewEnableControl1.IsInBenchmark = false;
            this.devicesListViewEnableControl1.IsMining = false;
            this.devicesListViewEnableControl1.Location = new System.Drawing.Point(12, 15);
            this.devicesListViewEnableControl1.Name = "devicesListViewEnableControl1";
            this.devicesListViewEnableControl1.SaveToGeneralConfig = false;
            this.devicesListViewEnableControl1.Size = new System.Drawing.Size(393, 78);
            this.devicesListViewEnableControl1.TabIndex = 0;
            // 
            // Form_Benchmark
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(420, 326);
            this.Controls.Add(this.benchmarkAllButton);
            this.Controls.Add(this.algorithmsListView1);
            this.Controls.Add(this.groupBoxBenchmarkProgress);
            this.Controls.Add(this.StartStopBtn);
            this.Controls.Add(this.CloseBtn);
            this.Controls.Add(this.devicesListViewEnableControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form_Benchmark";
            this.Text = "Benchmark";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormBenchmark_New_FormClosing);
            this.groupBoxBenchmarkProgress.ResumeLayout(false);
            this.groupBoxBenchmarkProgress.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Components.DevicesListViewEnableControl devicesListViewEnableControl1;
        private System.Windows.Forms.Button StartStopBtn;
        private System.Windows.Forms.Button CloseBtn;
        private System.Windows.Forms.GroupBox groupBoxBenchmarkProgress;
        private System.Windows.Forms.Label labelBenchmarkSteps;
        private System.Windows.Forms.ProgressBar progressBarBenchmarkSteps;
        private Components.AlgorithmsListView algorithmsListView1;
        private System.Windows.Forms.Button benchmarkAllButton;
        private System.Windows.Forms.Label benchmarkStatusLabel;
    }
}