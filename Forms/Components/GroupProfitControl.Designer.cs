namespace NiceHashMiner.Forms.Components {
    partial class GroupProfitControl {
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.groupBoxMinerGroup = new System.Windows.Forms.GroupBox();
            this.labelSpeedIndicator = new System.Windows.Forms.Label();
            this.labelCurentcyPerDayVaue = new System.Windows.Forms.Label();
            this.labelSpeedValue = new System.Windows.Forms.Label();
            this.groupBoxMinerGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxMinerGroup
            // 
            this.groupBoxMinerGroup.Controls.Add(this.labelSpeedIndicator);
            this.groupBoxMinerGroup.Controls.Add(this.labelCurentcyPerDayVaue);
            this.groupBoxMinerGroup.Controls.Add(this.labelSpeedValue);
            this.groupBoxMinerGroup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBoxMinerGroup.Location = new System.Drawing.Point(0, 0);
            this.groupBoxMinerGroup.Name = "groupBoxMinerGroup";
            this.groupBoxMinerGroup.Size = new System.Drawing.Size(332, 37);
            this.groupBoxMinerGroup.TabIndex = 108;
            this.groupBoxMinerGroup.TabStop = false;
            this.groupBoxMinerGroup.Text = "Mining Devices";
            // 
            // labelSpeedIndicator
            // 
            this.labelSpeedIndicator.AutoSize = true;
            this.labelSpeedIndicator.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelSpeedIndicator.Location = new System.Drawing.Point(6, 16);
            this.labelSpeedIndicator.Name = "labelSpeedIndicator";
            this.labelSpeedIndicator.Size = new System.Drawing.Size(47, 13);
            this.labelSpeedIndicator.TabIndex = 108;
            this.labelSpeedIndicator.Text = "Speed:";
            // 
            // labelCurentcyPerDayVaue
            // 
            this.labelCurentcyPerDayVaue.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelCurentcyPerDayVaue.AutoSize = true;
            this.labelCurentcyPerDayVaue.Location = new System.Drawing.Point(242, 16);
            this.labelCurentcyPerDayVaue.Name = "labelCurentcyPerDayVaue";
            this.labelCurentcyPerDayVaue.Size = new System.Drawing.Size(84, 13);
            this.labelCurentcyPerDayVaue.TabIndex = 104;
            this.labelCurentcyPerDayVaue.Text = "0.00 Points/Day";
            this.labelCurentcyPerDayVaue.Click += new System.EventHandler(this.labelCurentcyPerDayVaue_Click_1);
            // 
            // labelSpeedValue
            // 
            this.labelSpeedValue.AutoSize = true;
            this.labelSpeedValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelSpeedValue.Location = new System.Drawing.Point(59, 16);
            this.labelSpeedValue.Name = "labelSpeedValue";
            this.labelSpeedValue.Size = new System.Drawing.Size(30, 13);
            this.labelSpeedValue.TabIndex = 107;
            this.labelSpeedValue.Text = "N/A";
            // 
            // GroupProfitControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Controls.Add(this.groupBoxMinerGroup);
            this.Name = "GroupProfitControl";
            this.Size = new System.Drawing.Size(330, 40);
            this.groupBoxMinerGroup.ResumeLayout(false);
            this.groupBoxMinerGroup.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxMinerGroup;
        private System.Windows.Forms.Label labelSpeedIndicator;
        private System.Windows.Forms.Label labelCurentcyPerDayVaue;
        private System.Windows.Forms.Label labelSpeedValue;


    }
}
