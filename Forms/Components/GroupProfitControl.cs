using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace NiceHashMiner.Forms.Components
{
    public partial class GroupProfitControl : UserControl
    {
        public GroupProfitControl()
        {
            InitializeComponent();

            labelSpeedIndicator.Text = International.GetText("ListView_Speed");
        }


        public void UpdateProfitStats(int index, string speedString, string currencyRateString)
        {
            groupBoxMinerGroup.Text = "Mining Device: " + index.ToString();
            labelSpeedValue.Text = speedString;
            labelCurentcyPerDayVaue.Text = currencyRateString;
        }

        private void groupBoxMinerGroup_Enter(object sender, EventArgs e)
        {

        }

        private void labelCurentcyPerDayVaue_Click(object sender, EventArgs e)
        {

        }

        private void labelCurentcyPerDayVaue_Click_1(object sender, EventArgs e)
        {

        }
    }
}
