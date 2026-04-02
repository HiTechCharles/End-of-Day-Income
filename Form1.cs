using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Media;
using System.Speech.Synthesis;
using System.Windows.Forms;

namespace End_of_Day_Income
{
    public partial class Form1 : Form
    {
        // Constants
        private const string APPLICATION_FOLDER = "brew for you";
        private const string DRAWER_FOLDER = "End of Day Drawer";
        private const string REPORT_FOLDER = "Report";
        private const string TODAY_FILE = "Today.txt";
        private const string FULL_LOG_FILE = "Full.txt";

        // Static directories
        public static string BaseDirectory;
        public static string ReportDirectory;
        public static string TodayPath;
        public static string FullPath;
        public static string CompanyName;

        // Public static fields
        public static string DSReport;
        public static SpeechSynthesizer DrawerTalk = new SpeechSynthesizer();
        private static readonly Stopwatch AppTimer = new Stopwatch();

        // Multipliers in the same order as Program.DollarAmounts (13 items)
        private readonly decimal[] _multipliers = {
            50M, 20M, 10M, 5M, 1M, 0.25M, 0.10M, 0.05M, 0.01M, 10M, 5M, 2M, 0.50M
        };

        // Cached input controls in same order as multipliers / Program.DollarAmounts
        private NumericUpDown[] _inputs;

        public Form1()
        {
            InitializeComponent();
            CompanyName = BusinessNameManager.GetBusinessName();
            this.Text = $"{CompanyName} - End of Day Drawer";            
            InitializeDirectories();
            InitializeSpeechSynthesizer();
            InitializeInputControls();
            AppTimer.Start();
        }

        private void InitializeDirectories()
        {
            BaseDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), CompanyName, "End of Day Drawer");
            ReportDirectory = Path.Combine(BaseDirectory, REPORT_FOLDER);
            TodayPath = Path.Combine(ReportDirectory, TODAY_FILE);
            FullPath = Path.Combine(ReportDirectory, FULL_LOG_FILE);

            Directory.CreateDirectory(BaseDirectory);
            Directory.CreateDirectory(ReportDirectory);
        }

        private void InitializeSpeechSynthesizer()
        {
            DrawerTalk.Volume = 100;
            DrawerTalk.Rate = 3;
        }

        private void InitializeInputControls()
        {
            // Set mapping in the same order as _multipliers and Program.DollarAmounts
            _inputs = new NumericUpDown[] {
                FiftiesNUD, TwentiesNUD, TensNUD, FivesNUD, OnesNUD,
                QuartersNUD, DimesNUD, NickelsNUD, PenniesNUD,
                QuarterRollNUD, DimeRollNUD, NickelRollNUD, PennyRollNUD
            };

            // Set Tag on inputs so CommonNudChanged can determine index
            for (int i = 0; i < _inputs.Length; i++)
            {
                _inputs[i].Tag = i;
            }
        }

        private void UpdateValues()
        {
            decimal bills = CalculateCategoryTotal(0, 5);
            decimal coins = CalculateCategoryTotal(5, 9);
            decimal rolls = CalculateCategoryTotal(9, 13);
            decimal grandTotal = CalculateGrandTotal();

            BillsTotalTB.Text = bills.ToString("C2");
            CoinsTotalTB.Text = coins.ToString("C2");
            RollsTotalTB.Text = rolls.ToString("C2");

            UpdateGrandTotal(grandTotal);
            UpdateDrawerStatus(grandTotal);
        }

        private decimal CalculateCategoryTotal(int startIndex, int endIndex)
        {
            decimal total = 0M;
            for (int i = startIndex; i < endIndex; i++)
            {
                total += Program.DollarAmounts[i];
            }
            return total;
        }

        private decimal CalculateGrandTotal()
        {
            decimal total = 0M;
            for (int i = 0; i < Program.DollarAmounts.Length; i++)
            {
                total += Program.DollarAmounts[i];
            }
            return total;
        }

        private void UpdateGrandTotal(decimal grandTotal)
        {
            // Clamp to control min/max
            if (grandTotal < GrandTotalNUD.Minimum)
                GrandTotalNUD.Value = GrandTotalNUD.Minimum;
            else if (grandTotal > GrandTotalNUD.Maximum)
                GrandTotalNUD.Value = GrandTotalNUD.Maximum;
            else
                GrandTotalNUD.Value = grandTotal;
        }

        private void UpdateDrawerStatus(decimal grandTotal)
        {
            decimal registerTotal = RegisterNUD.Value;
            decimal difference = grandTotal - registerTotal;

            if (difference > 0)
            {
                DrawerStateTB.Text = $"Drawer is over by {difference:C2}";
                DSReport = $"over by {difference:C2}";
            }
            else if (difference < 0)
            {
                DrawerStateTB.Text = $"Drawer is short by {Math.Abs(difference):C2}";
                DSReport = $"short by {Math.Abs(difference):C2}";
            }
            else
            {
                DrawerStateTB.Text = "Drawer is Perfect!";
                DSReport = "balanced";
            }
        }

        private void SaveToFile()
        {
            Directory.CreateDirectory(ReportDirectory);

            var lines = BuildReportLines();

            try
            {
                File.WriteAllLines(TodayPath, lines);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unable to write log file: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private List<string> BuildReportLines()
        {
            var lines = new List<string>
            {
                $"{CompanyName} - End of Day Drawer Report",
                DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToShortTimeString() + "\n"
                    };

            // Label mapping in same order as controls / multipliers
            var labels = new string[] {
                FiftiesLBL.Text, TwentiesLBL.Text, TensLBL.Text, FivesLBL.Text, OnesLBL.Text,
                QuartersLBL.Text, DimesLBL.Text, NickelsLBL.Text, PenniesLBL.Text,
                QuarterRollLBL.Text, DimeRollLBL.Text, NickelRollLBL.Text, PennyRollLBL.Text
            };

            for (int i = 0; i < Program.DollarAmounts.Length && i < _inputs.Length; i++)
            {
                decimal count = _inputs[i].Value;
                if (count > 0)
                {
                    lines.Add($"{count} {labels[i]} = {Program.DollarAmounts[i]:C2}");
                }
            }

            lines.Add(string.Empty);
            lines.Add($"   Bills Total:  {BillsTotalTB.Text}");
            lines.Add($"   Coins Total:  {CoinsTotalTB.Text}");
            lines.Add($"   Rolls Total:  {RollsTotalTB.Text}");
            lines.Add($"   Grand Total:  {GrandTotalNUD.Value:C2}");

            var ts = AppTimer.Elapsed;
            lines.Add($"    Count Time:  {ts.Hours} Hours, {ts.Minutes} Minutes, {ts.Seconds} Seconds");
            lines.Add($"     POS Total:  {RegisterNUD.Value:C2}");
            lines.Add($" Drawer Status:  {DSReport}");
            lines.Add(new string('-', 50));
            lines.Add(string.Empty);

            return lines;
        }

        #region Menu Items

        private void saveViewReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // If all categories have 0, don't write a log file
            if (GrandTotalNUD.Value > 0)
            {
                SaveToFile();
                saveViewReportToolStripMenuItem.Enabled = false; // Disable to prevent multiple saves without changes
            }
            else
            {
                SystemSounds.Exclamation.Play();
            }
        }

        private void eToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            Application.Exit();
        }

        private void openFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Open base directory in file explorer
            if (Directory.Exists(BaseDirectory))
            {
                Process.Start("explorer.exe", BaseDirectory);
            }
            else
            {
                MessageBox.Show("The directory does not exist.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void resetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ResetAllInputs();
            RegisterNUD.Value = 100;
            FiftiesNUD.Focus();
        }

        private void ResetAllInputs()
        {
            foreach (var input in _inputs)
            {
                input.Value = 0;
            }
        }

        private void ReadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (GrandTotalNUD.Value > 0)
            {
                UpdateValues();
                SaveToFile();

                if (File.Exists(TodayPath))
                {
                    string message = File.ReadAllText(TodayPath);
                    DrawerTalk.SpeakAsync(message);
                }
            }
            else
            {
                DrawerTalk.SpeakAsync("The drawer total is zero dollars. Please enter amounts before reading the report.");
                SystemSounds.Exclamation.Play();
            }
        }

        private void viewReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateValues();
            ViewList ViewListForm = new ViewList();
            ViewListForm.ShowDialog();  
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (AboutForm aboutForm = new AboutForm())
            {
                aboutForm.ShowDialog();
            }
        }

        #endregion

        #region Event Handlers

        private void CommonNudChanged(object sender, EventArgs e)
        {
            if (!(sender is NumericUpDown nud) || nud.Tag == null)
                return;

            int idx = (int)nud.Tag;
            if (idx < 0 || idx >= _multipliers.Length)
                return;

            Program.DollarAmounts[idx] = nud.Value * _multipliers[idx];
            UpdateValues();
        }

        private void RegisterNUD_ValueChanged(object sender, EventArgs e)
        {
            UpdateValues();
        }

        #endregion

    }
}