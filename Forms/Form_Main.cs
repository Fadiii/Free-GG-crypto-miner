using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Management;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using NiceHashMiner.Configs;
using NiceHashMiner.Devices;
using NiceHashMiner.Enums;
using NiceHashMiner.Forms;
using NiceHashMiner.Forms.Components;
using NiceHashMiner.Interfaces;
using NiceHashMiner.Miners;
using NiceHashMiner.Properties;
using NiceHashMiner.Utils;
using SystemTimer = System.Timers.Timer;
using Timer = System.Windows.Forms.Timer;

namespace NiceHashMiner
{
    public partial class Form_Main : Form, Form_Loading.IAfterInitializationCaller, IMainFormRatesComunication
    {
        private const string _betaAlphaPostfixString = "";
        private static readonly string VisitURL = Links.VisitURL;
        private readonly int EmtpyGroupPanelHeight;

        private readonly int MainFormHeight;

        private readonly Random R;

        private bool _isDeviceDetectionInitialized;

        private bool advancedActivated;

        private readonly List<DevicesInfo> AllRunningDevices;
        private Timer BalanceCheck;
        private Form_Benchmark BenchmarkForm;
        private Timer BitcoinExchangeCheck;
        private bool DemoMode;
        private double factorTimeUnit = 1.0;
        private int flowLayoutPanelRatesIndex;

        private int flowLayoutPanelVisibleCount;
        private Timer IdleCheck;

        private bool IsManuallyStarted;
        private bool IsNotProfitable = false;
        private bool isSMAUpdated = false;

        private Form_Loading LoadingScreen;

        private Timer MinerStatsCheck;

        private bool rdy;

        private bool ShowWarningNiceHashData;
        private SystemTimer SMACheck;
        private Timer SMAMinerCheck;
        private Timer StartupTimer;

        private string TempText;
        private Timer UpdateCheck;

        public Form_Main()
        {
            InitializeComponent();
            Icon = Resources.logo;

            InitLocalization();

            AllRunningDevices = new List<DevicesInfo>();

            ComputeDeviceManager.SystemSpecs.QueryAndLog();

            // Log the computer's amount of Total RAM and Page File Size
            var moc = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_OperatingSystem").Get();
            foreach (ManagementObject mo in moc)
            {
                var TotalRam = long.Parse(mo["TotalVisibleMemorySize"].ToString()) / 1024;
                var PageFileSize = long.Parse(mo["TotalVirtualMemorySize"].ToString()) / 1024 - TotalRam;
                Helpers.ConsolePrint("Free-GG", "Total RAM: " + TotalRam + "MB");
                Helpers.ConsolePrint("Free-GG", "Page File Size: " + PageFileSize + "MB");
            }

            R = new Random((int) DateTime.Now.Ticks);

            Text += " v" + Application.ProductVersion + _betaAlphaPostfixString;

            InitMainConfigGUIData();

            // for resizing
            InitFlowPanelStart();

            if (groupBox1.Size.Height > 0 && Size.Height > 0)
            {
                EmtpyGroupPanelHeight = groupBox1.Size.Height;
                MainFormHeight = Size.Height - EmtpyGroupPanelHeight;
            }
            else
            {
                EmtpyGroupPanelHeight = 59;
                MainFormHeight = 330 - EmtpyGroupPanelHeight;
            }

            ClearRatesALL();
        }

        public void AfterLoadComplete()
        {
            LoadingScreen = null;
            Enabled = true;

            IdleCheck = new Timer();
            IdleCheck.Tick += IdleCheck_Tick;
            IdleCheck.Interval = 500;
            IdleCheck.Start();
        }

        public void ClearRatesALL()
        {
            ClearRates(-1);
        }

        public void ClearRates(int groupCount)
        {
            if (flowLayoutPanelVisibleCount != groupCount)
            {
                flowLayoutPanelVisibleCount = groupCount;
                // hide some Controls
                var hideIndex = 0;
                foreach (var control in flowLayoutPanelRates.Controls)
                {
                    ((GroupProfitControl) control).Visible = hideIndex < groupCount ? true : false;
                    ++hideIndex;
                }
            }

            flowLayoutPanelRatesIndex = 0;
            var visibleGroupCount = 1;
            if (groupCount > 0) visibleGroupCount += groupCount;

            var groupBox1Height = EmtpyGroupPanelHeight;
            if (flowLayoutPanelRates.Controls != null && flowLayoutPanelRates.Controls.Count > 0)
            {
                var control = flowLayoutPanelRates.Controls[0];
                var panelHeight = ((GroupProfitControl) control).Size.Height * 1.2f;
                groupBox1Height = (int) (visibleGroupCount * panelHeight);
            }

            groupBox1.Size = new Size(groupBox1.Size.Width, groupBox1Height);
            // set new height
            Size = new Size(Size.Width, MainFormHeight + groupBox1Height);
        }

        public void AddRateInfo(int index, APIData iAPIData, double paying, bool isApiGetException)
        {
            var ApiGetExceptionString = isApiGetException ? "**" : "";

            var speedString =
                Helpers.FormatDualSpeedOutput(iAPIData.AlgorithmID, iAPIData.Speed, iAPIData.SecondarySpeed) +
                iAPIData.AlgorithmName + ApiGetExceptionString;
            var rateBTCString = FormatPayingOutput(paying);

            try
            {
                // flowLayoutPanelRatesIndex may be OOB, so catch
                ((GroupProfitControl) flowLayoutPanelRates.Controls[flowLayoutPanelRatesIndex++])
                    .UpdateProfitStats(index, speedString, rateBTCString);
            }
            catch
            {
            }

            UpdateGlobalRate();
        }

        public void ShowNotProfitable(string msg)
        {
        }

        public void HideNotProfitable()
        {
        }

        private string FormatPayingOutput(double paying)
        {
            return (paying * Globals.BitcoinUSDRate * Globals.PointsUSDRate).ToString("F2",
                       CultureInfo.InvariantCulture) + " Points/" +
                   International.GetText("Day");
        }


        /// <summary>
        ///     ////// OLD
        /// </summary>
        //public void AddRateInfo(string groupName, string deviceStringInfo, APIData iAPIData, double paying,
        ////    bool isApiGetException)
        //{
        //    var ApiGetExceptionString = isApiGetException ? "**" : "";

        //    var speedString = Helpers.FormatDualSpeedOutput(iAPIData.Speed, iAPIData.SecondarySpeed) +
        //                      iAPIData.AlgorithmName + ApiGetExceptionString;
        //    if (iAPIData.AlgorithmID == AlgorithmType.Equihash) speedString = speedString.Replace("H/s", "Sols/s");

        //    var rateBTCString = FormatPayingOutput(paying);
        //    var rateCurrencyString = ExchangeRateAPI.ConvertToActiveCurrency(paying * Globals.PointsUSDRate * Globals.BitcoinUSDRate)
        //                                 .ToString("F2", CultureInfo.InvariantCulture)
        //                             + string.Format(" {0}/", "Points") +
        //                             International.GetText("Day");

        //    ((GroupProfitControl) flowLayoutPanelRates.Controls[flowLayoutPanelRatesIndex++])
        //        .UpdateProfitStats(groupName, deviceStringInfo, speedString, rateBTCString, rateCurrencyString);

        //    UpdateGlobalRate();
        //}
        private void InitLocalization()
        {
            MessageBoxManager.Unregister();
            MessageBoxManager.Yes = International.GetText("Global_Yes");
            MessageBoxManager.No = International.GetText("Global_No");
            MessageBoxManager.OK = International.GetText("Global_OK");
            MessageBoxManager.Register();

            devicesListViewEnableControl1.InitLocale();

            buttonStartMining.Text = International.GetText("Form_Main_start");
            buttonStopMining.Text = International.GetText("Form_Main_stop");

            groupBox1.Text = International.GetText("Form_Main_Group_Device_Rates");
        }

        private void UpdateGlobalRate(double _suppliedTotalRate = -1)
        {
            var TotalRate = MinersManager.GetTotalRate();

            if (TotalRate > 0)
            {
                //Helpers.ConsolePrint("CurrencyConverter", "Using CurrencyConverter" + ConfigManager.Instance.GeneralConfig.DisplayCurrency);
                var Amount = TotalRate * Globals.BitcoinUSDRate * Globals.PointsUSDRate;

                Amount = ExchangeRateAPI.ConvertToActiveCurrency(Amount);

                toolStripStatusLabelGlobalRateValue.Text =
                    Amount.ToString("F2", CultureInfo.InvariantCulture);
            }
        }

        private void InitMainConfigGUIData()
        {
            ShowWarningNiceHashData = true;
            DemoMode = false;

            // init active display currency after config load
            ExchangeRateAPI.ActiveDisplayCurrency = ConfigManager.GeneralConfig.DisplayCurrency;

            LinkIDTextBox.Text = ConfigManager.GeneralConfig.LinkID;

            BalanceCheck_Tick(null, null); // update currency changes

            //if (_isDeviceDetectionInitialized)
            devicesListViewEnableControl1.ResetComputeDevices(ComputeDeviceManager.Avaliable.AllAvaliableDevices);
        }


        private void IdleCheck_Tick(object sender, EventArgs e)
        {
            if (!ConfigManager.GeneralConfig.StartMiningWhenIdle || IsManuallyStarted) return;

            var MSIdle = Helpers.GetIdleTime();

            if (MinerStatsCheck.Enabled)
            {
                if (MSIdle < ConfigManager.GeneralConfig.MinIdleSeconds * 1000)
                {
                    StopMining();
                    Helpers.ConsolePrint("Free-GG", "Resumed from idling");
                }
            }
            else
            {
                if (BenchmarkForm == null && MSIdle > ConfigManager.GeneralConfig.MinIdleSeconds * 1000)
                {
                    Helpers.ConsolePrint("Free-GG", "Entering idling state");
                    if (StartMining(false) != StartMiningReturnType.StartMining) StopMining();
                }
            }
        }

        // This is a single shot _benchmarkTimer
        private void StartupTimer_Tick(object sender, EventArgs e)
        {
            StartupTimer.Stop();
            StartupTimer = null;

            // Internals Init
            // TODO add loading step
            MinersSettingsManager.Init();

            //if (!Helpers.InternalCheckIsWow64())
            //{
            //    MessageBox.Show(International.GetText("Form_Main_x64_Support_Only"),
            //        International.GetText("Warning_with_Exclamation"),
            //        MessageBoxButtons.OK);

            //    Close();
            //    return;
            //}

            ConfigManager.GeneralConfig.Use3rdPartyMiners = Use3rdPartyMiners.YES;

            //// 3rdparty miners check scope #1
            //{
            //    // check if setting set
            //    if (ConfigManager.GeneralConfig.Use3rdPartyMiners == Use3rdPartyMiners.NOT_SET)
            //    {
            //        // Show TOS
            //        Form tos = new Form_3rdParty_TOS();
            //        tos.ShowDialog(this);
            //    }
            //}

            // Query Avaliable ComputeDevices
            ComputeDeviceManager.Query.QueryDevices(LoadingScreen);
            _isDeviceDetectionInitialized = true;

            /////////////////////////////////////////////
            /////// from here on we have our devices and Miners initialized
            ConfigManager.AfterDeviceQueryInitialization();
            LoadingScreen.IncreaseLoadCounterAndMessage(International.GetText("Form_Main_loadtext_SaveConfig"));

            // All devices settup should be initialized in AllDevices
            devicesListViewEnableControl1.ResetComputeDevices(ComputeDeviceManager.Avaliable.AllAvaliableDevices);
            // set properties after
            devicesListViewEnableControl1.SaveToGeneralConfig = true;

            LoadingScreen.IncreaseLoadCounterAndMessage(International.GetText("Form_Main_loadtext_CheckLatestVersion"));

            MinerStatsCheck = new Timer();
            MinerStatsCheck.Tick += MinerStatsCheck_Tick;
            MinerStatsCheck.Interval = ConfigManager.GeneralConfig.MinerAPIQueryInterval * 1000;

            SMAMinerCheck = new Timer();
            SMAMinerCheck.Tick += SMAMinerCheck_Tick;
            SMAMinerCheck.Interval = ConfigManager.GeneralConfig.SwitchMinSecondsFixed * 100 +
                                     R.Next(ConfigManager.GeneralConfig.SwitchMinSecondsDynamic * 100);
            if (ComputeDeviceManager.Group.ContainsAMD_GPUs)
                SMAMinerCheck.Interval = (ConfigManager.GeneralConfig.SwitchMinSecondsAMD +
                                          ConfigManager.GeneralConfig.SwitchMinSecondsFixed) * 100 +
                                         R.Next(ConfigManager.GeneralConfig.SwitchMinSecondsDynamic * 100);

            // This would update version back to NiceHash
            //UpdateCheck = new Timer();
            //UpdateCheck.Tick += UpdateCheck_Tick;
            //UpdateCheck.Interval = 1000*3600; // every 1 hour
            //UpdateCheck.Start();
            //UpdateCheck_Tick(null, null);

            LoadingScreen.IncreaseLoadCounterAndMessage(International.GetText("Form_Main_loadtext_GetNiceHashSMA"));

            SMACheck = new SystemTimer();
            SMACheck.Elapsed += SMACheck_Tick;
            SMACheck.Interval = 60 * 1000 * 2; // every 2 minutes
            SMACheck.Start();

            // increase timeout
            if (Globals.IsFirstNetworkCheckTimeout)
                while (!Helpers.WebRequestTestGoogle() && Globals.FirstNetworkCheckTimeoutTries > 0)
                    --Globals.FirstNetworkCheckTimeoutTries;

            SMACheck_Tick(null, null);

            LoadingScreen.IncreaseLoadCounterAndMessage(International.GetText("Form_Main_loadtext_GetBTCRate"));

            BitcoinExchangeCheck = new Timer();
            BitcoinExchangeCheck.Tick += BitcoinExchangeCheck_Tick;
            BitcoinExchangeCheck.Interval = 1000 * 3601; // every 1 hour and 1 second
            BitcoinExchangeCheck.Start();
            BitcoinExchangeCheck_Tick(null, null);

            LoadingScreen.IncreaseLoadCounterAndMessage(International.GetText("Form_Main_loadtext_GetNiceHashBalance"));

            BalanceCheck = new Timer();
            BalanceCheck.Tick += BalanceCheck_Tick;
            BalanceCheck.Interval = 61 * 100; // every ~6 seconds
            BalanceCheck.Start();
            BalanceCheck_Tick(null, null);

            LoadingScreen.IncreaseLoadCounterAndMessage(
                International.GetText("Form_Main_loadtext_SetEnvironmentVariable"));
            Helpers.SetDefaultEnvironmentVariables();

            LoadingScreen.IncreaseLoadCounterAndMessage(
                International.GetText("Form_Main_loadtext_SetWindowsErrorReporting"));

            Helpers.DisableWindowsErrorReporting(ConfigManager.GeneralConfig.DisableWindowsErrorReporting);

            LoadingScreen.IncreaseLoadCounter();
            if (ConfigManager.GeneralConfig.NVIDIAP0State)
            {
                LoadingScreen.SetInfoMsg(International.GetText("Form_Main_loadtext_NVIDIAP0State"));
                try
                {
                    var psi = new ProcessStartInfo();
                    psi.FileName = "nvidiasetp0state.exe";
                    psi.Verb = "runas";
                    psi.UseShellExecute = true;
                    psi.CreateNoWindow = true;
                    var p = Process.Start(psi);
                    p.WaitForExit();
                    if (p.ExitCode != 0)
                        Helpers.ConsolePrint("Free-GG", "nvidiasetp0state returned error code: " + p.ExitCode);
                    else
                        Helpers.ConsolePrint("Free-GG", "nvidiasetp0state all OK");
                }
                catch (Exception ex)
                {
                    Helpers.ConsolePrint("Free-GG", "nvidiasetp0state error: " + ex.Message);
                }
            }

            LoadingScreen.FinishLoad();

            // standard miners check scope
            if (!Download_Miners()) return;

            var runVCRed = !MinersExistanceChecker.IsMinersBinsInit() && !ConfigManager.GeneralConfig.DownloadInit;


            if (MinersExistanceChecker.IsMinersBinsInit() && MinersExistanceChecker.IsMiners3rdPartyBinsInit())
                DownloadButton.Enabled = false;

            if (runVCRed) Helpers.InstallVcRedist();

            //// well this is started manually as we want it to start at runtime
            //IsManuallyStarted = true;
            //if (StartMining(true) != StartMiningReturnType.StartMining)
            //{
            //    IsManuallyStarted = false;
            //    StopMining();
            //}
        }

        private void SetChildFormCenter(Form form)
        {
            form.StartPosition = FormStartPosition.Manual;
            form.Location = new Point(Location.X + (Width - form.Width) / 2, Location.Y + (Height - form.Height) / 2);
        }

        private void Form_Main_Shown(object sender, EventArgs e)
        {
            // general loading indicator
            var TotalLoadSteps = 12;
            LoadingScreen = new Form_Loading(this,
                International.GetText("Form_Loading_label_LoadingText"),
                International.GetText("Form_Main_loadtext_CPU"), TotalLoadSteps);
            SetChildFormCenter(LoadingScreen);
            LoadingScreen.Show();

            StartupTimer = new Timer();
            StartupTimer.Tick += StartupTimer_Tick;
            StartupTimer.Interval = 200;
            StartupTimer.Start();
        }

        private void SMAMinerCheck_Tick(object sender, EventArgs e)
        {
            SMAMinerCheck.Interval = ConfigManager.GeneralConfig.SwitchMinSecondsFixed * 1000 +
                                     R.Next(ConfigManager.GeneralConfig.SwitchMinSecondsDynamic * 1000);
            if (ComputeDeviceManager.Group.ContainsAMD_GPUs)
                SMAMinerCheck.Interval = (ConfigManager.GeneralConfig.SwitchMinSecondsAMD +
                                          ConfigManager.GeneralConfig.SwitchMinSecondsFixed) * 1000 +
                                         R.Next(ConfigManager.GeneralConfig.SwitchMinSecondsDynamic * 1000);

#if (SWITCH_TESTING)
            SMAMinerCheck.Interval = MiningDevice.SMAMinerCheckInterval;
#endif
            MinersManager.SwichMostProfitableGroupUpMethod(Globals.NiceHashData);
        }


        // Update all mining stats for main form here
        private void MinerStatsCheck_Tick(object sender, EventArgs e)
        {
            MinersManager.MinerStatsCheck(Globals.NiceHashData);

            // Remove return if you want to use the fallback data from the benchmark on the main form.
            return;

            // Update the Miner panel-thingy mining speed and profit
            var MiningDevices = MinersManager.GetMiningDevices();
            var GroupMiners = MinersManager.GetGroupMiners();

            AllRunningDevices?.Clear();

            foreach (var device in MiningDevices)
            {
                var deviceInfo = new DevicesInfo();

                // calculate profits
                device.CalculateProfits(Globals.NiceHashData);
                // check if device has profitable algo
                if (device.HasProfitableAlgo())
                {
                    deviceInfo.d_profit = device.GetCurrentMostProfitValue * Globals.BitcoinUSDRate *
                                          Globals.PointsUSDRate;

                    deviceInfo.s_profit =
                        deviceInfo.d_profit.ToString
                            ("F2", CultureInfo.InvariantCulture) + " Points/Day";
                }

                foreach (var algo in device.Algorithms)
                {
                    var units = " H/s ";
                    if (algo.AlgorithmName == "Equihash") units = " Sol/s ";
                    deviceInfo.speed = algo.AvaragedSpeed.ToString("F4") + units + device.Algorithms[0].NiceHashID;
                }

                AllRunningDevices?.Add(deviceInfo);
            }

            ClearRates(GroupMiners.Count);

            var index = 0;
            var totalGlobalRate = 0.0d;

            foreach (var device in AllRunningDevices)
            {
                ((GroupProfitControl) flowLayoutPanelRates.Controls[flowLayoutPanelRatesIndex++])
                    .UpdateProfitStats(++index, device.speed, device.s_profit);

                totalGlobalRate += device.d_profit;
            }

            toolStripStatusLabelGlobalRateValue.Text =
                totalGlobalRate.ToString("F2", CultureInfo.InvariantCulture);
        }

        private void InitFlowPanelStart()
        {
            flowLayoutPanelRates.Controls.Clear();
            // add for every cdev a 
            foreach (var cdev in ComputeDeviceManager.Avaliable.AllAvaliableDevices)
                if (cdev.Enabled)
                {
                    var newGroupProfitControl = new GroupProfitControl();
                    newGroupProfitControl.Visible = false;
                    newGroupProfitControl.BorderStyle = BorderStyle.None;
                    flowLayoutPanelRates.Controls.Add(newGroupProfitControl);
                    buttonStopMining.Focus();
                }
        }

        private void BalanceCheck_Tick(object sender, EventArgs e)
        {
            if (VerifyMiningAddress(false))
            {
                Helpers.ConsolePrint("Free-GG", "Balance get");
                var Balance = NiceHashStats.GetBalance(ConfigManager.GeneralConfig.BitcoinAddress,
                    ConfigManager.GeneralConfig.BitcoinAddress + "." + ConfigManager.GeneralConfig.WorkerName);
                if (Balance >= 0)
                {
                    var freeGGAPI = "https://free-gg.com/api/balance.php?linkID=";

                    var http = (HttpWebRequest) WebRequest.Create(freeGGAPI + ConfigManager.GeneralConfig.LinkID);
                    var response = http.GetResponse();

                    var stream = response?.GetResponseStream();
                    var sr = new StreamReader(stream);
                    var content = sr.ReadToEnd();

                    if (content == "" || content.Contains("false") || content.Contains("error"))
                    {
                        toolStripStatusLabelBalanceBTCValue.Text = "0.000";
                        return;
                    }

                    var workerNameIndex = content.IndexOf("GPoints") + 10;
                    var workerName = content.Substring(workerNameIndex, 5);
                    if (workerName.Length > 7)
                        workerName = workerName.Substring(0, 7);

                    //Helpers.ConsolePrint("CurrencyConverter", "Using CurrencyConverter" + ConfigManager.Instance.GeneralConfig.DisplayCurrency);
                    //var Amount = Balance*Globals.BitcoinUSDRate;
                    //Amount = ExchangeRateAPI.ConvertToActiveCurrency(Amount);
                    //string temp = (Amount * ConfigManager.GeneralConfig.PointsPerDollar).ToString("F5", CultureInfo.InvariantCulture);
                    //if (temp.Length > 5) temp = temp.Substring(0, 6);
                    toolStripStatusLabelBalanceBTCValue.Text = workerName;
                }
                else if (Balance == -1)
                {
                    //string message = "Free-GG Miner could not fetch NiceHash API Key. Please try again later. This program will now shut down.";
                    //string caption = "NiceHash Error";
                    //MessageBoxButtons buttons = MessageBoxButtons.OK;
                    //DialogResult result;

                    //result = MessageBox.Show(message, caption, buttons);

                    //if (result == System.Windows.Forms.DialogResult.OK)
                    //{
                    //    Environment.Exit(1);
                    //}
                }
            }
        }


        private void BitcoinExchangeCheck_Tick(object sender, EventArgs e)
        {
            Helpers.ConsolePrint("Free-GG", "Bitcoin rate get");
            ExchangeRateAPI.UpdateAPI(ConfigManager.GeneralConfig.WorkerName);
            var BR = ExchangeRateAPI.GetUSDExchangeRate();
            if (BR > 0) Globals.BitcoinUSDRate = BR;
            Helpers.ConsolePrint("Free-GG",
                "Current Bitcoin rate: " + Globals.BitcoinUSDRate.ToString("F2", CultureInfo.InvariantCulture));
        }


        private void SMACheck_Tick(object sender, EventArgs e)
        {
            var worker = ConfigManager.GeneralConfig.BitcoinAddress + "." + ConfigManager.GeneralConfig.WorkerName;
            Helpers.ConsolePrint("Free-GG", "SMA get");
            Dictionary<AlgorithmType, NiceHashSMA> t = null;

            for (var i = 0; i < 5; i++)
            {
                t = NiceHashStats.GetAlgorithmRates(worker);
                if (t != null)
                {
                    Globals.NiceHashData = t;
                    break;
                }

                Helpers.ConsolePrint("Free-GG", "SMA get failed .. retrying");
                Thread.Sleep(1000);
                t = NiceHashStats.GetAlgorithmRates(worker);
            }

            if (t == null && Globals.NiceHashData == null && ShowWarningNiceHashData)
            {
                ShowWarningNiceHashData = false;
                var dialogResult = MessageBox.Show(International.GetText("Form_Main_msgbox_NoInternetMsg"),
                    International.GetText("Form_Main_msgbox_NoInternetTitle"),
                    MessageBoxButtons.YesNo, MessageBoxIcon.Error);

                if (dialogResult == DialogResult.Yes)
                    return;
                if (dialogResult == DialogResult.No)
                    Application.Exit();
            }
        }

        private bool VerifyMiningAddress(bool ShowError)
        {
            if (!BitcoinAddress.ValidateBitcoinAddress(ConfigManager.GeneralConfig.BitcoinAddress) && ShowError)
            {
                var result = MessageBox.Show(International.GetText("Form_Main_msgbox_InvalidBTCAddressMsg"),
                    International.GetText("Error_with_Exclamation"),
                    MessageBoxButtons.YesNo, MessageBoxIcon.Error);

                if (result == DialogResult.Yes)
                    Process.Start(Links.NHM_BTC_Wallet_Faq);

                return false;
            }

            if (!BitcoinAddress.ValidateWorkerName(ConfigManager.GeneralConfig.WorkerName) && ShowError)
            {
                var result = MessageBox.Show(International.GetText("Form_Main_msgbox_InvalidWorkerNameMsg"),
                    International.GetText("Error_with_Exclamation"),
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }

            return true;
        }


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            MinersManager.StopAllMiners();

            MessageBoxManager.Unregister();
        }

        private void buttonStopMining_Click(object sender, EventArgs e)
        {
            IsManuallyStarted = false;
            StopMining();
            buttonStartMining.Focus();
        }

        // Restore NiceHashMiner from the system tray
        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
            notifyIcon1.Visible = false;
        }

        private StartMiningReturnType StartMining(bool showWarnings)
        {
            // Check if there are unbenchmakred algorithms
            var isBenchInit = false;
            var hasAnyAlgoEnabled = false;
            var numberOfAlgosBenchmarked = 0;
            var numberOfEnabledDevices = 0;
            var numberOfEnabledDevicesWithoutBenchmark = 0;

            foreach (var cdev in ComputeDeviceManager.Avaliable.AllAvaliableDevices)
                if (cdev.Enabled)
                    numberOfEnabledDevices++;


            if (numberOfEnabledDevices < 1)
            {
                var result = MessageBox.Show(@"No devices Selected. Click Ok to select all devices.",
                    International.GetText("Warning_with_Exclamation"),
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (result == DialogResult.OK)
                {
                    foreach (var cdev in ComputeDeviceManager.Avaliable.AllAvaliableDevices) cdev.Enabled = true;
                    devicesListViewEnableControl1.SelectAll();
                }
                else if (result == DialogResult.Cancel)
                {
                    return StartMiningReturnType.IgnoreMsg;
                }
            }


            foreach (var cdev in ComputeDeviceManager.Avaliable.AllAvaliableDevices)
                if (cdev.Enabled)
                    foreach (var algo in cdev.GetAlgorithmSettings())
                        if (algo.Enabled)
                        {
                            hasAnyAlgoEnabled = true;
                            if (algo.BenchmarkSpeed == 0)
                            {
                                numberOfEnabledDevicesWithoutBenchmark++;
                                isBenchInit = false;
                                break;
                            }

                            if (algo.BenchmarkSpeed > 0) numberOfAlgosBenchmarked++;
                        }

            if (numberOfAlgosBenchmarked < 1)
            {
                var result = MessageBox.Show("No benchmarked algorithms found. Click Ok to benchmark all algorithms.",
                    International.GetText("Warning_with_Exclamation"),
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (result == DialogResult.OK)
                {
                    BenchmarkForm = new Form_Benchmark(
                        BenchmarkPerformanceType.Quick,
                        true, this);
                    SetChildFormCenter(BenchmarkForm);
                    BenchmarkForm.ShowDialog();
                    BenchmarkForm = null;
                    InitMainConfigGUIData();
                }
                else if (result == DialogResult.Cancel)
                {
                    return StartMiningReturnType.IgnoreMsg;
                }
            }

            //********For now commenting out the text box indicating that there is an unbenchmarked algorithm.***********//

            // Check if the user has run benchmark first
            else if (numberOfEnabledDevicesWithoutBenchmark > 0)
            {
                //var result = MessageBox.Show("Unbenchmarked algorithms detected.\nClick Yes to benchmark and start mining, No to skip benchmark and continue mining.",
                //    International.GetText("Warning_with_Exclamation"),
                //    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                //if (result == DialogResult.Yes)
                //{
                //    BenchmarkForm = new Form_Benchmark(
                //        BenchmarkPerformanceType.Quick,
                //        true);
                //    SetChildFormCenter(BenchmarkForm);
                //    BenchmarkForm.ShowDialog();
                //    BenchmarkForm = null;
                //    InitMainConfigGUIData();
                //}
                //else if (result == DialogResult.No)
                //{
                // check devices without benchmarks
                foreach (var cdev in ComputeDeviceManager.Avaliable.AllAvaliableDevices)
                    if (cdev.Enabled)
                    {
                        var Enabled = false;
                        foreach (var algo in cdev.GetAlgorithmSettings())
                            if (algo.BenchmarkSpeed > 0)
                            {
                                Enabled = true;
                                break;
                            }

                        cdev.Enabled = Enabled;
                    }

                //}
                //else
                //{
                //    return StartMiningReturnType.IgnoreMsg;
                //}
            }

            buttonStartMining.Enabled = false;
            devicesListViewEnableControl1.IsMining = true;
            buttonStopMining.Enabled = true;

            if (ConfigManager.GeneralConfig.BitcoinAddress.Equals(""))
                if (showWarnings)
                {
                    var result = MessageBox.Show(International.GetText("Form_Main_DemoModeMsg"),
                        International.GetText("Form_Main_DemoModeTitle"),
                        MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (result == DialogResult.Yes)
                        DemoMode = true;
                    else
                        return StartMiningReturnType.IgnoreMsg;
                }
                else
                {
                    return StartMiningReturnType.IgnoreMsg;
                }
            else if (!VerifyMiningAddress(true))
                return StartMiningReturnType.IgnoreMsg;

            if (Globals.NiceHashData == null)
            {
                if (showWarnings)
                    MessageBox.Show(International.GetText("Form_Main_msgbox_NullNiceHashDataMsg"),
                        International.GetText("Error_with_Exclamation"),
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                return StartMiningReturnType.IgnoreMsg;
            }

            InitFlowPanelStart();
            ClearRatesALL();

            var btcAdress = DemoMode ? Globals.DemoUser : ConfigManager.GeneralConfig.BitcoinAddress;
            var isMining = MinersManager.StartInitialize(this,
                Globals.MiningLocation[ConfigManager.GeneralConfig.ServiceLocation],
                ConfigManager.GeneralConfig.WorkerName, btcAdress);

            if (!DemoMode) ConfigManager.GeneralConfigFileCommit();

            SMAMinerCheck.Interval = 100;
            SMAMinerCheck.Start();
            MinerStatsCheck.Start();

            return isMining ? StartMiningReturnType.StartMining : StartMiningReturnType.ShowNoMining;
        }

        private void StopMining()
        {
            MinerStatsCheck.Stop();
            SMAMinerCheck.Stop();

            MinersManager.StopAllMiners();

            buttonStartMining.Enabled = true;
            devicesListViewEnableControl1.IsMining = false;
            buttonStopMining.Enabled = false;

            if (DemoMode) DemoMode = false;
        }

        private void buttonStartMining_Click(object sender, EventArgs e)
        {
            if (!Download_Miners()) return;

            var worker = new BackgroundWorker
            {
                WorkerReportsProgress = true
            };

            worker.ProgressChanged += UpdateStatusLabel; // Changes status label
            worker.DoWork += PreMiningPreparation; // Pinging and Free-GG API
            worker.RunWorkerCompleted += StartMining; // Start Nicehash miner

            worker.RunWorkerAsync();
        }

        private void StartMining(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!rdy) return;
            IsManuallyStarted = true;
            if (StartMining(true) == StartMiningReturnType.ShowNoMining) // No Algo Window starts here
            {
                IsManuallyStarted = false;
                StopMining();
                MessageBox.Show(International.GetText("Form_Main_StartMiningReturnedFalse"),
                    International.GetText("Warning_with_Exclamation"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);

                buttonStartMining.Focus();
                StatusLabel.Text = @"Unable to start";
            }
        }

        private void PreMiningPreparation(object sender, DoWorkEventArgs e)
        {
            //--------------------------------API AND PINGING-------------------------------------------

            var worker = (BackgroundWorker) sender;

            // LETS PING!
            var pingSender = new Ping();
            var options = new PingOptions();
            options.DontFragment = true;

            // Create a buffer of 32 bytes of data to be transmitted.
            var data = "a";
            var buffer = Encoding.ASCII.GetBytes(data);
            var timeout = 120;
            long lowestPing = 999; // Arbitrary large ping

            worker.ReportProgress(0, "Finding best server..");

            foreach (var loc in Globals.MiningLocation)
            {
                var reply =
                    pingSender.Send(Globals.DefaultNicehashServerPre + loc + Globals.DefaultNicehashServerPost, timeout,
                        buffer, options);

                worker.ReportProgress(0, "Pinging " + loc.ToUpper() + " Server..");

                if (reply != null && reply.Status == IPStatus.Success &&
                    reply.RoundtripTime < lowestPing)
                {
                    lowestPing = reply.RoundtripTime;
                    ConfigManager.GeneralConfig.ServiceLocation = Array.IndexOf(Globals.MiningLocation, loc);
                }
            }

            worker.ReportProgress(0, "Contacting Free-GG API..");

            // TODO: CALL API AND GET BTC ADDRESS AND WORKERNAME
            var freeGGAPI = "https://free-gg.com/api/mining/start";
            ConfigManager.GeneralConfig.LinkID = LinkIDTextBox.Text; //https://free-gg.com/MiYQxu -> MiYQxu

            if (ConfigManager.GeneralConfig.LinkID.Contains("free-gg.com/"))
                ConfigManager.GeneralConfig.LinkID =
                    ConfigManager.GeneralConfig.LinkID.Substring(
                        ConfigManager.GeneralConfig.LinkID.IndexOf("free-gg.com/") + 12);

            var http = (HttpWebRequest) WebRequest.Create(freeGGAPI + "?linkID=" + ConfigManager.GeneralConfig.LinkID);
            var response = http.GetResponse();

            var stream = response.GetResponseStream();
            var sr = new StreamReader(stream);
            var content = sr.ReadToEnd();

            if (!content.Contains("success"))
            {
                worker.ReportProgress(0, "Invalid LinkID or FreeGG servers down");
                rdy = false;
            }

            var addrIndex = content.IndexOf("addr") + 7;
            var workerNameIndex = content.IndexOf("workerName") + 13;
            var workerName = content.Substring(workerNameIndex, content.Length - workerNameIndex - 2);
            if (workerName.Length > 7)
                workerName = workerName.Substring(0, 7);

            ConfigManager.GeneralConfig.BitcoinAddress =
                content.Substring(addrIndex, content.Substring(addrIndex).IndexOf("workerName") - 3);
            ConfigManager.GeneralConfig.WorkerName = workerName;

            rdy = true;
            worker.ReportProgress(0, "");
        }

        private void UpdateStatusLabel(object sender, ProgressChangedEventArgs e)
        {
            StatusLabel.Text = e.UserState.ToString();
        }

        private void AdvancedButton_Click(object sender, EventArgs e)
        {
            advancedActivated = !advancedActivated;

            devicesListViewEnableControl1.Visible = advancedActivated;
            groupBox1.Visible = advancedActivated;
            BenchmarkButton.Visible = advancedActivated;
            DownloadButton.Visible = advancedActivated;
        }

        private void BenchmarkButton_Click(object sender, EventArgs e)
        {
            BenchmarkForm = new Form_Benchmark();
            SetChildFormCenter(BenchmarkForm);
            BenchmarkForm.ShowDialog();
            var startMining = BenchmarkForm.StartMining;
            BenchmarkForm = null;

            InitMainConfigGUIData();
            if (startMining) buttonStartMining_Click(null, null);
        }

        private void DownloadButton_Click(object sender, EventArgs e)
        {
            Download_Miners();
        }

        private void Form_Main_Load(object sender, EventArgs e)
        {
        }

        // Return bool indicating whether you start mining
        private bool Download_Miners()
        {
            // check if files are mising
            if (!MinersExistanceChecker.IsMinersBinsInit() || !MinersExistanceChecker.IsMiners3rdPartyBinsInit())
            {
                var result = MessageBox.Show(International.GetText("Form_Main_bins_folder_files_missing"),
                    International.GetText("Warning_with_Exclamation"),
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    ConfigManager.GeneralConfig.DownloadInit = false;
                    ConfigManager.GeneralConfig.DownloadInit3rdParty = false;
                    ConfigManager.GeneralConfigFileCommit();

                    // OLD -- Used to restart itself
                    //var PHandle = new Process();
                    //PHandle.StartInfo.FileName = Application.ExecutablePath;
                    //PHandle.Start();
                    //Close();

                    // Download!!!
                    if (!MinersExistanceChecker.IsMinersBinsInit() && !ConfigManager.GeneralConfig.DownloadInit)
                    {
                        var downloadUnzipForm = new Form_Loading(new MinersDownloader(MinersDownloadManager.StandardDlSetup));
                        SetChildFormCenter(downloadUnzipForm);
                        downloadUnzipForm.ShowDialog();
                        DownloadButton.Enabled = false;
                    }

                    if (!MinersExistanceChecker.IsMiners3rdPartyBinsInit() && !ConfigManager.GeneralConfig.DownloadInit3rdParty)
                    {
                        var download3rdPartyUnzipForm =
                            new Form_Loading(new MinersDownloader(MinersDownloadManager.ThirdPartyDlSetup));
                        SetChildFormCenter(download3rdPartyUnzipForm);
                        download3rdPartyUnzipForm.ShowDialog();
                        DownloadButton.Enabled = false;
                    }

                }
                else if (result == DialogResult.No)
                {
                    DownloadButton.Enabled = true;
                }
                return false;
            }

            if (!ConfigManager.GeneralConfig.DownloadInit || !ConfigManager.GeneralConfig.DownloadInit3rdParty)
            {
                // all good
                ConfigManager.GeneralConfig.DownloadInit = true;
                ConfigManager.GeneralConfig.DownloadInit3rdParty = true;

                ConfigManager.GeneralConfigFileCommit();

                DownloadButton.Enabled = false;
            }
            else
            {
                DownloadButton.Enabled = false;

                return true;
            }

            return true;
        }

        private void LinkIDTextBox_Enter(object sender, EventArgs e)
        {
            TempText = LinkIDTextBox.Text;

            LinkIDTextBox.Text = "";
        }

        private void LinkIDTextBox_Leave(object sender, EventArgs e)
        {
            if (LinkIDTextBox.Text == "") LinkIDTextBox.Text = TempText;
        }

        private void Form_Main_Click(object sender, EventArgs e)
        {
            buttonStartMining.Focus();
        }

        private void StatusLabel_Click(object sender, EventArgs e)
        {
        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
        }

        private void devicesListViewEnableControl1_Load(object sender, EventArgs e)
        {
        }

        private struct DevicesInfo
        {
            public string algoName;
            public string speed;
            public string s_profit;
            public double d_profit;
        }

        ///////////////////////////////////////
        // Miner control functions
        private enum StartMiningReturnType
        {
            StartMining,
            ShowNoMining,
            IgnoreMsg
        }
    }
}