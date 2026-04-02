namespace End_of_Day_Income
{
    partial class AboutForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutForm));
            this.CompanyTB = new System.Windows.Forms.TextBox();
            this.HelpTextTB = new System.Windows.Forms.TextBox();
            this.OKBTN = new System.Windows.Forms.Button();
            this.LogoPB = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.LogoPB)).BeginInit();
            this.SuspendLayout();
            // 
            // CompanyTB
            // 
            this.CompanyTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CompanyTB.BackColor = System.Drawing.Color.Black;
            this.CompanyTB.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold);
            this.CompanyTB.ForeColor = System.Drawing.Color.White;
            this.CompanyTB.Location = new System.Drawing.Point(220, 25);
            this.CompanyTB.Margin = new System.Windows.Forms.Padding(2);
            this.CompanyTB.Multiline = true;
            this.CompanyTB.Name = "CompanyTB";
            this.CompanyTB.ReadOnly = true;
            this.CompanyTB.Size = new System.Drawing.Size(253, 187);
            this.CompanyTB.TabIndex = 1;
            this.CompanyTB.Text = "Brew for You\r\nEnd of Day Drawer\r\n\r\nV8.9, Developed using c# via Visual Studio 202" +
    "6";
            // 
            // HelpTextTB
            // 
            this.HelpTextTB.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.HelpTextTB.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.HelpTextTB.Location = new System.Drawing.Point(11, 243);
            this.HelpTextTB.Multiline = true;
            this.HelpTextTB.Name = "HelpTextTB";
            this.HelpTextTB.ReadOnly = true;
            this.HelpTextTB.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.HelpTextTB.Size = new System.Drawing.Size(462, 228);
            this.HelpTextTB.TabIndex = 2;
            this.HelpTextTB.Text = resources.GetString("HelpTextTB.Text");
            // 
            // OKBTN
            // 
            this.OKBTN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OKBTN.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OKBTN.ForeColor = System.Drawing.Color.Black;
            this.OKBTN.Location = new System.Drawing.Point(357, 477);
            this.OKBTN.Name = "OKBTN";
            this.OKBTN.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.OKBTN.Size = new System.Drawing.Size(116, 55);
            this.OKBTN.TabIndex = 3;
            this.OKBTN.Text = "&OK";
            this.OKBTN.UseVisualStyleBackColor = true;
            this.OKBTN.Click += new System.EventHandler(this.OKBTN_Click);
            // 
            // LogoPB
            // 
            this.LogoPB.AccessibleDescription = "Brew for you Coffee cup logo";
            this.LogoPB.Image = global::End_of_Day_Income.Properties.Resources.bfy_logo;
            this.LogoPB.Location = new System.Drawing.Point(11, 26);
            this.LogoPB.Margin = new System.Windows.Forms.Padding(2);
            this.LogoPB.Name = "LogoPB";
            this.LogoPB.Size = new System.Drawing.Size(195, 186);
            this.LogoPB.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.LogoPB.TabIndex = 0;
            this.LogoPB.TabStop = false;
            // 
            // AboutForm
            // 
            this.AcceptButton = this.OKBTN;
            this.AutoScaleDimensions = new System.Drawing.SizeF(15F, 29F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.CancelButton = this.OKBTN;
            this.ClientSize = new System.Drawing.Size(482, 540);
            this.Controls.Add(this.OKBTN);
            this.Controls.Add(this.HelpTextTB);
            this.Controls.Add(this.CompanyTB);
            this.Controls.Add(this.LogoPB);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold);
            this.ForeColor = System.Drawing.Color.White;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(9);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(400, 400);
            this.Name = "AboutForm";
            this.Padding = new System.Windows.Forms.Padding(28, 25, 28, 25);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "About End of Day Drawer";
            ((System.ComponentModel.ISupportInitialize)(this.LogoPB)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox LogoPB;
        private System.Windows.Forms.TextBox CompanyTB;
        private System.Windows.Forms.TextBox HelpTextTB;
        private System.Windows.Forms.Button OKBTN;
    }
}