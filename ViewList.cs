using System;
using System.IO;
using System.Speech.Synthesis;
using System.Windows.Forms;

namespace End_of_Day_Income
{
    /// <summary>
    /// Form for viewing income reports (today or all-time)
    /// </summary>
    public partial class ViewList : Form
    {
        private readonly SpeechSynthesizer _moneyTalks;

        // Speech synthesizer constants
        private const int SpeechRate = 3;
        private const int SpeechVolume = 100;

        public ViewList()
        {
            InitializeComponent();
            _moneyTalks = Form1.DrawerTalk;
            InitializeSpeechSynthesizer();
            LoadReport(true); // Load today's report by default
        }

        /// <summary>
        /// Loads and displays the income report
        /// </summary>
        /// <param name="isToday">True to load today's report, false for all-time report</param>
        private void LoadReport(bool isToday)
        {
            try
            {
                if (isToday)
                {
                    if (!File.Exists(Form1.TodayPath))
                    {
                        IncomeRTB.Text = "Today's report not found.";
                        return;
                    }

                    string todayReport = File.ReadAllText(Form1.TodayPath);
                    allTimeToolStripMenuItem.Checked = false;
                    todayToolStripMenuItem.Checked = true;
                    IncomeRTB.Text = todayReport;
                }
                else
                {
                    if (!File.Exists(Form1.FullPath))
                    {
                        IncomeRTB.Text = "All-time report not found.";
                        return;
                    }

                    string allTimeReport = File.ReadAllText(Form1.FullPath);
                    allTimeToolStripMenuItem.Checked = true;
                    todayToolStripMenuItem.Checked = false;
                    IncomeRTB.Text = allTimeReport;
                }
            }
            catch (IOException ex)
            {
                MessageBox.Show($"Error loading report: {ex.Message}", "File Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                IncomeRTB.Text = "Error loading report.";
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show($"Access denied: {ex.Message}", "Access Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                IncomeRTB.Text = "Access denied to report file.";
            }
        }

        /// <summary>
        /// Configures the speech synthesizer settings
        /// </summary>
        private void InitializeSpeechSynthesizer()
        {
            if (_moneyTalks != null)
            {
                _moneyTalks.SetOutputToDefaultAudioDevice();
                _moneyTalks.Rate = SpeechRate;
                _moneyTalks.Volume = SpeechVolume;
            }
        }

        /// <summary>
        /// Disposes resources used by the form
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                components?.Dispose();
                // Note: Don't dispose _moneyTalks here as it's shared with Form1
            }
            base.Dispose(disposing);
        }

        private void allTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadReport(false); // Load all-time report
        }

        private void todayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadReport(true); // Load today's report
        }

        private void closeDialogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}