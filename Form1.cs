using System;
using System.Windows.Forms;
using System.Diagnostics;  //to run processes like notepad
using System.Media;  //allows system sounds to play
using System.IO;
using System.Speech.Synthesis;

namespace End_of_Day_Income
{
    public partial class Form1 : Form
    {
        public static Stopwatch AppTimer= new Stopwatch();
        public Form1()
        {
            InitializeComponent();
            AppTimer.Start();
        }

        #region NumericUpDown ValueChanged
        private void FiftiesNUD_ValueChanged(object sender, EventArgs e)
        {
            //multiply the numeric box value by the dollar amount
            //also stores total $ value for each category
            Program.DollarAmounts[0] = FiftiesNUD.Value * 50;
            FiftiesTB.Text = "$" + Program.DollarAmounts[0];
            UpdateValues();  //adds values together to get totals
        }

        private void TwentiesNUD_ValueChanged(object sender, EventArgs e)
        {
            Program.DollarAmounts[1] = TwentiesNUD.Value * 20;
            TwentiesTB.Text = "$" + Program.DollarAmounts[1];
            UpdateValues();
        }

        private void TensNUD_ValueChanged(object sender, EventArgs e)
        {
            Program.DollarAmounts[2] = TensNUD.Value * 10;
            TensTB.Text = "$" + Program.DollarAmounts[2];
            UpdateValues();
        }

        private void FivesNUD_ValueChanged(object sender, EventArgs e)
        {
            Program.DollarAmounts[3] = FivesNUD.Value * 5;
            FivesTB.Text = "$" + Program.DollarAmounts[3];
            UpdateValues();
        }

        private void OnesNUD_ValueChanged(object sender, EventArgs e)
        {
            Program.DollarAmounts[4] = OnesNUD.Value * 1;
            OnesTB.Text = "$" + Program.DollarAmounts[4];
            UpdateValues();
        }

        private void QuartersNUD_ValueChanged(object sender, EventArgs e)
        {
            Program.DollarAmounts[5] = QuartersNUD.Value * 0.25M;
            QuartersTB.Text = "$" + Program.DollarAmounts[5];
            UpdateValues();
        }

        private void DimesNUD_ValueChanged(object sender, EventArgs e)
        {
            Program.DollarAmounts[6] = DimesNUD.Value * 0.10M;
            DimesTB.Text = "$" + Program.DollarAmounts[6];
            UpdateValues();
        }

        private void NickelsNUD_ValueChanged(object sender, EventArgs e)
        {
            Program.DollarAmounts[7] = NickelsNUD.Value * 0.05M;
            NickelsTB.Text = "$" + Program.DollarAmounts[7];
            UpdateValues();
        }

        private void PenniesNUD_ValueChanged(object sender, EventArgs e)
        {
            Program.DollarAmounts[8] = PenniesNUD.Value * 0.01M;
            PenniesTB.Text = "$" + Program.DollarAmounts[8];
            UpdateValues();
        }

        private void QuarterRollNUD_ValueChanged(object sender, EventArgs e)
        {
            Program.DollarAmounts[9] = QuarterRollNUD.Value * 10;
            QuarterRollTB.Text = "$" + Program.DollarAmounts[9];
            UpdateValues();
        }

        private void DimeRollNUD_ValueChanged(object sender, EventArgs e)
        {
            Program.DollarAmounts[10] = DimeRollNUD.Value * 5;
            DimeRollTB.Text = "$" + Program.DollarAmounts[10];
            UpdateValues();
        }

        private void NickelRollNUD_ValueChanged(object sender, EventArgs e)
        {
            Program.DollarAmounts[11] = NickelRollNUD.Value * 2;
            NickelRollTB.Text = "$" + Program.DollarAmounts[11];
            UpdateValues();
        }

        private void PennyRollNUD_ValueChanged(object sender, EventArgs e)
        {
            Program.DollarAmounts[12] = PennyRollNUD.Value * 0.50M;
            PennyRollTB.Text = "$" + Program.DollarAmounts[12];
            UpdateValues();
        }

        private void RegisterNUD_ValueChanged(object sender, EventArgs e)
        {
            UpdateValues();
        }
        #endregion

        private void UpdateValues()
        {
            //create temporary addition value
            //for bills, it adds value of 50,20,10,5,1
            decimal BT = Program.DollarAmounts[0] +
                       Program.DollarAmounts[1] +
                       Program.DollarAmounts[2] +
                       Program.DollarAmounts[3] +
                       Program.DollarAmounts[4];
            BillsTotalTB.Text = "$" + BT.ToString();

            //coins 0.25 0.1 0.05 0.01
            BT = Program.DollarAmounts[5] +
                 Program.DollarAmounts[6] +
                 Program.DollarAmounts[7] +
                 Program.DollarAmounts[8];
            CoinsTotalTB.Text = "$" + BT.ToString();

            //Rolls 0.5 2 5 10
            BT = Program.DollarAmounts[9] +
                 Program.DollarAmounts[10] +
                 Program.DollarAmounts[11] +
                 Program.DollarAmounts[12];
            RollsTotalTB.Text = "$" + BT.ToString();

            BT = 0;  //loop through all values for grand total
            for (int I = 0; I < 13; I++)
            {
                BT += Program.DollarAmounts[I];
            }
            GrandTotalNUD.Value = BT;

            decimal GT = BT;  //these two values get compared
            decimal ST = RegisterNUD.Value;
            decimal Diff;  //difference between grand total and reg total
            if (GT > ST)  //drawer has extram money
            {
                Diff = GT - ST;
                DrawerStateTB.Text = "Drawer is over by $" + Diff.ToString();
            }
            if (ST > GT)  //drawer short
            {
                Diff = ST - GT;
                DrawerStateTB.Text = "Drawer is Short by $" + Diff.ToString();
            }
            if (ST == GT)  //drawer matches register total
            {
                DrawerStateTB.Text = "Drawer is Perfect!";
            }
        }

        private void SaveToFile()
        {
            /* writes all non-zero categories and totals to file
            located in user's documents folder.
            There is a log file for today, and one for 
            every time the program is used.  */

            string LogPath = Environment.GetEnvironmentVariable("onedriveconsumer") + "\\documents\\brew for you\\End of Day Income\\";

            Directory.CreateDirectory(LogPath);

            //Today log file full path
            string TodayLogPath = LogPath + "Today.txt";
            //Full log file pat            string FullLogPath = System.IO.Path.Combine(Environment.GetFolderPath(
            string FullLogPath = LogPath + "Full.txt";
            System.IO.StreamWriter TodayLog = new System.IO.StreamWriter(TodayLogPath);
            DateTime end = DateTime.Now;

            TodayLog.WriteLine("End of Day Income Report");
            TodayLog.WriteLine(end);  //print date
            TodayLog.WriteLine();

            //checks all NumericUpDown controls for value > 0
            //only controls with positive value get printed to file

            if (FiftiesNUD.Value > 0)
                TodayLog.WriteLine(FiftiesNUD.Value + " " + FiftiesLBL.Text + " = " + FiftiesTB.Text);
            if (TwentiesNUD.Value > 0)
                TodayLog.WriteLine(TwentiesNUD.Value + " " + TwentiesLBL.Text + " = " + TwentiesTB.Text);
            if (TensNUD.Value > 0)
                TodayLog.WriteLine(TensNUD.Value + " " + TensLBL.Text + " = " + TensTB.Text);
            if (FivesNUD.Value > 0)
                TodayLog.WriteLine(FivesNUD.Value + " " + FivesLBL.Text + " = " + FivesTB.Text);
            if (OnesNUD.Value > 0)
                TodayLog.WriteLine(OnesNUD.Value + " " + OnesLBL.Text + " = " + OnesTB.Text);

            if (QuartersNUD.Value > 0)
                TodayLog.WriteLine(QuartersNUD.Value + " " + QuartersLBL.Text + " = " + QuartersTB.Text);
            if (DimesNUD.Value > 0)
                TodayLog.WriteLine(DimesNUD.Value + " " + DimesLBL.Text + " = " + DimesTB.Text);
            if (NickelsNUD.Value > 0)
                TodayLog.WriteLine(NickelsNUD.Value + " " + NickelsLBL.Text + " = " + NickelsTB.Text);
            if (PenniesNUD.Value > 0)
                TodayLog.WriteLine(PenniesNUD.Value + " " + PenniesLBL.Text + " = " + PenniesTB.Text);

            if (QuarterRollNUD.Value > 0)
                TodayLog.WriteLine(QuarterRollNUD.Value + " " + QuarterRollLBL.Text + " = " + QuarterRollTB.Text);
            if (DimeRollNUD.Value > 0)
                TodayLog.WriteLine(DimeRollNUD.Value + " " + DimeRollLBL.Text + " = " + DimeRollTB.Text);
            if (NickelRollNUD.Value > 0)
                TodayLog.WriteLine(NickelRollNUD.Value + " " + NickelRollLBL.Text + " = " + NickelRollTB.Text);
            if (PennyRollNUD.Value > 0)
                TodayLog.WriteLine(PennyRollNUD.Value + " " + PennyRollLBL.Text + " = " + PennyRollTB.Text);
            TodayLog.WriteLine();

            TodayLog.WriteLine(" Bills Total:  " + BillsTotalTB.Text);
            TodayLog.WriteLine(" Coins Total:  " + CoinsTotalTB.Text);
            TodayLog.WriteLine(" Rolls Total:  " + RollsTotalTB.Text);
            TodayLog.WriteLine(" Grand Total:  $" + GrandTotalNUD.Value.ToString());

            AppTimer.Stop();
            TimeSpan ts = AppTimer.Elapsed;
            string TimeMessage = String.Format("{0} Hours, {1} Minutes, {2} Seconds",
            ts.Hours, ts.Minutes, ts.Seconds);
            TodayLog.WriteLine("Count Time:  " + TimeMessage);
            TodayLog.WriteLine("Square Total:  $" + RegisterNUD.Value.ToString());
            TodayLog.WriteLine(DrawerStateTB.Text); //seperates records
            TodayLog.WriteLine("=================");  //in log file
            TodayLog.WriteLine();
            TodayLog.Close();  //close file, we're done!

            //copy today file to string, and add it to bottom of 
            //perminent log file
            string s = File.ReadAllText(TodayLogPath);
            File.AppendAllText(FullLogPath, s);

            //opens today's log with the program
            //assigned to read text files  (notepad, notepad++
            Process.Start(TodayLogPath);

        }

        #region Button Click events
        private void SaveBTN_Click(object sender, EventArgs e)
        {
            //if all categories have 0, don't write a log file
            if (GrandTotalNUD.Value > 0)
            {
                SaveToFile();
                this.Close();  //closes the form
            }
            else
                SystemSounds.Exclamation.Play(); //play a sound
        }

        private void ActivityLogBTN_Click(object sender, EventArgs e)
        {
            string FullLogPath = Environment.GetEnvironmentVariable("onedriveconsumer") + "\\documents\\brew for you\\End of Day Income\\full.txt";

            //shows log of all program operations
            if (File.Exists(FullLogPath))
                Process.Start(@FullLogPath);
        }

        private void HelpBTN_Click(object sender, EventArgs e)
        {
            //messagebox describing how the program works.
            MessageBox.Show("For each money category, type in how many " +
                "there are.\nWhen done, use the Save button to exit and " +
                "display\nthe report file for viewing or printing.\n\n" +
                "The Activity log shows a record for each time the " +
                "program is run.  Newest records are at the end.",
                "End of Day Income Help", MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        } //end
        #endregion

        private void ReadBTN_Click(object sender, EventArgs e)
        {
            SpeechSynthesizer Chuck = new SpeechSynthesizer();

            if (FiftiesNUD.Value > 0)
            {
                Chuck.SpeakAsync(FiftiesNUD.Value + FiftiesLBL.Text + ".");
            }

            if (TwentiesNUD.Value > 0)
            {
                Chuck.SpeakAsync(TwentiesNUD.Value + TwentiesLBL.Text + ".");
            }

            if (TensNUD.Value > 0)
            {
                Chuck.SpeakAsync(TensNUD.Value + TensLBL.Text + ".");
            }

            if (FivesNUD.Value > 0)
            {
                Chuck.SpeakAsync(FivesNUD.Value + FivesLBL.Text + ".");
            }

            if (OnesNUD.Value > 0)
            {
                Chuck.SpeakAsync(OnesNUD.Value + OnesLBL.Text + ".");
            }

            if (QuartersNUD.Value > 0)
            {
                Chuck.SpeakAsync(QuartersNUD.Value + QuartersLBL.Text + ".");
            }

            if (DimesNUD.Value > 0)
            {
                Chuck.SpeakAsync(DimesNUD.Value + DimesLBL.Text + ".");
            }

            if (NickelsNUD.Value > 0)
            {
                Chuck.SpeakAsync(NickelsNUD.Value + NickelsLBL.Text + ".");
            }

            if (PenniesNUD.Value > 0)
            {
                Chuck.SpeakAsync(PenniesNUD.Value + PenniesLBL.Text  + ".");
            }

            Chuck.SpeakAsync(RegisterLBL.Text + " $" + RegisterNUD.Value + ".");

            if (QuarterRollNUD.Value > 0)
            {
                Chuck.SpeakAsync(QuarterRollNUD.Value + " Quarter rolls.");
            }

            if (DimeRollNUD.Value > 0)
            {
                Chuck.SpeakAsync(DimeRollNUD.Value + " Dime Rolls.");
            }

            if ( NickelRollNUD.Value > 0 ) 
            {
                Chuck.SpeakAsync(NickelRollNUD.Value + " Nickel rolls.");
            }

            if ( PennyRollNUD.Value > 0 )
            {
                Chuck.SpeakAsync(PennyRollNUD.Value + " Penny RolLS.");
            }
            
            Chuck.SpeakAsync(GrandTotalLBL.Text + " $" + GrandTotalNUD.Value + ".");
            Chuck.SpeakAsync(DrawerStateTB.Text + ".");
          }
        }
    }