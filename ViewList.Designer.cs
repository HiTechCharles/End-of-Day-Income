namespace End_of_Day_Income
{
    partial class ViewList
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.IncomeRTB = new System.Windows.Forms.RichTextBox();
            this.ViewListMST = new System.Windows.Forms.MenuStrip();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToClipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.speakReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeDialogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reportsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.allTimeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.todayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewListMST.SuspendLayout();
            this.SuspendLayout();
            // 
            // IncomeRTB
            // 
            this.IncomeRTB.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.IncomeRTB.BackColor = System.Drawing.Color.White;
            this.IncomeRTB.Font = new System.Drawing.Font("Consolas", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IncomeRTB.Location = new System.Drawing.Point(12, 54);
            this.IncomeRTB.Name = "IncomeRTB";
            this.IncomeRTB.ReadOnly = true;
            this.IncomeRTB.Size = new System.Drawing.Size(610, 695);
            this.IncomeRTB.TabIndex = 4;
            this.IncomeRTB.Text = "";
            this.IncomeRTB.WordWrap = false;
            // 
            // ViewListMST
            // 
            this.ViewListMST.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold);
            this.ViewListMST.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsToolStripMenuItem,
            this.reportsToolStripMenuItem});
            this.ViewListMST.Location = new System.Drawing.Point(0, 0);
            this.ViewListMST.Name = "ViewListMST";
            this.ViewListMST.Size = new System.Drawing.Size(634, 37);
            this.ViewListMST.TabIndex = 5;
            this.ViewListMST.Text = "menuStrip1";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToClipboardToolStripMenuItem,
            this.speakReportToolStripMenuItem,
            this.closeDialogToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(117, 33);
            this.optionsToolStripMenuItem.Text = "&Options";
            // 
            // copyToClipboardToolStripMenuItem
            // 
            this.copyToClipboardToolStripMenuItem.Name = "copyToClipboardToolStripMenuItem";
            this.copyToClipboardToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.copyToClipboardToolStripMenuItem.Size = new System.Drawing.Size(391, 34);
            this.copyToClipboardToolStripMenuItem.Text = "&Copy to Clipboard";
            // 
            // speakReportToolStripMenuItem
            // 
            this.speakReportToolStripMenuItem.Name = "speakReportToolStripMenuItem";
            this.speakReportToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.speakReportToolStripMenuItem.Size = new System.Drawing.Size(391, 34);
            this.speakReportToolStripMenuItem.Text = "&Speak Report";
            // 
            // closeDialogToolStripMenuItem
            // 
            this.closeDialogToolStripMenuItem.Name = "closeDialogToolStripMenuItem";
            this.closeDialogToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.closeDialogToolStripMenuItem.Size = new System.Drawing.Size(391, 34);
            this.closeDialogToolStripMenuItem.Text = "Clos&e Dialog";
            this.closeDialogToolStripMenuItem.Click += new System.EventHandler(this.closeDialogToolStripMenuItem_Click);
            // 
            // reportsToolStripMenuItem
            // 
            this.reportsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.allTimeToolStripMenuItem,
            this.todayToolStripMenuItem});
            this.reportsToolStripMenuItem.Name = "reportsToolStripMenuItem";
            this.reportsToolStripMenuItem.Size = new System.Drawing.Size(118, 33);
            this.reportsToolStripMenuItem.Text = "&Reports";
            // 
            // allTimeToolStripMenuItem
            // 
            this.allTimeToolStripMenuItem.Name = "allTimeToolStripMenuItem";
            this.allTimeToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
            this.allTimeToolStripMenuItem.Size = new System.Drawing.Size(275, 34);
            this.allTimeToolStripMenuItem.Text = "A&ll-Time";
            this.allTimeToolStripMenuItem.Click += new System.EventHandler(this.allTimeToolStripMenuItem_Click);
            // 
            // todayToolStripMenuItem
            // 
            this.todayToolStripMenuItem.Name = "todayToolStripMenuItem";
            this.todayToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
            this.todayToolStripMenuItem.Size = new System.Drawing.Size(275, 34);
            this.todayToolStripMenuItem.Text = "&Today";
            this.todayToolStripMenuItem.Click += new System.EventHandler(this.todayToolStripMenuItem_Click);
            // 
            // ViewList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(15F, 29F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(634, 761);
            this.Controls.Add(this.IncomeRTB);
            this.Controls.Add(this.ViewListMST);
            this.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold);
            this.ForeColor = System.Drawing.Color.White;
            this.MainMenuStrip = this.ViewListMST;
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(400, 400);
            this.Name = "ViewList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "View Drawer Activity Reports";
            this.ViewListMST.ResumeLayout(false);
            this.ViewListMST.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.RichTextBox IncomeRTB;
        private System.Windows.Forms.MenuStrip ViewListMST;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reportsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToClipboardToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem speakReportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeDialogToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem allTimeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem todayToolStripMenuItem;
    }
}