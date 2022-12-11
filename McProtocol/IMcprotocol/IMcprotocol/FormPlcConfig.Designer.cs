namespace IntelligentFactory
{
    partial class FormPlcConfig
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
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label20 = new System.Windows.Forms.Label();
            this.lbReadValue = new System.Windows.Forms.Label();
            this.tbWriteValue = new System.Windows.Forms.TextBox();
            this.tbAddress = new System.Windows.Forms.TextBox();
            this.tbServerPort = new System.Windows.Forms.TextBox();
            this.btnWrite = new System.Windows.Forms.Button();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.cbAddressType = new System.Windows.Forms.ComboBox();
            this.tbServerip = new System.Windows.Forms.TextBox();
            this.btnRead = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cbWordCount = new System.Windows.Forms.ComboBox();
            this.btnApply = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnOK.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Bold);
            this.btnOK.ForeColor = System.Drawing.Color.Aquamarine;
            this.btnOK.Location = new System.Drawing.Point(306, 266);
            this.btnOK.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(199, 74);
            this.btnOK.TabIndex = 17;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCancel.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Bold);
            this.btnCancel.ForeColor = System.Drawing.Color.Red;
            this.btnCancel.Location = new System.Drawing.Point(506, 266);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(203, 74);
            this.btnCancel.TabIndex = 18;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label20
            // 
            this.label20.BackColor = System.Drawing.Color.Transparent;
            this.label20.Dock = System.Windows.Forms.DockStyle.Top;
            this.label20.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label20.Font = new System.Drawing.Font("Arial", 24.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.ForeColor = System.Drawing.Color.Aquamarine;
            this.label20.Location = new System.Drawing.Point(0, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(800, 55);
            this.label20.TabIndex = 824;
            this.label20.Text = "PLC Config";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbReadValue
            // 
            this.lbReadValue.BackColor = System.Drawing.Color.Transparent;
            this.lbReadValue.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbReadValue.Font = new System.Drawing.Font("Arial", 15F);
            this.lbReadValue.ForeColor = System.Drawing.Color.Aquamarine;
            this.lbReadValue.Location = new System.Drawing.Point(577, 205);
            this.lbReadValue.Name = "lbReadValue";
            this.lbReadValue.Size = new System.Drawing.Size(226, 49);
            this.lbReadValue.TabIndex = 848;
            this.lbReadValue.Text = "-";
            this.lbReadValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tbWriteValue
            // 
            this.tbWriteValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.tbWriteValue.Font = new System.Drawing.Font("Arial", 27F);
            this.tbWriteValue.ForeColor = System.Drawing.Color.Aquamarine;
            this.tbWriteValue.Location = new System.Drawing.Point(182, 204);
            this.tbWriteValue.Name = "tbWriteValue";
            this.tbWriteValue.Size = new System.Drawing.Size(218, 49);
            this.tbWriteValue.TabIndex = 846;
            this.tbWriteValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbAddress
            // 
            this.tbAddress.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.tbAddress.Font = new System.Drawing.Font("Arial", 27F);
            this.tbAddress.ForeColor = System.Drawing.Color.Aquamarine;
            this.tbAddress.Location = new System.Drawing.Point(577, 154);
            this.tbAddress.Name = "tbAddress";
            this.tbAddress.Size = new System.Drawing.Size(223, 49);
            this.tbAddress.TabIndex = 845;
            this.tbAddress.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbServerPort
            // 
            this.tbServerPort.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.tbServerPort.Font = new System.Drawing.Font("Arial", 27F);
            this.tbServerPort.ForeColor = System.Drawing.Color.Aquamarine;
            this.tbServerPort.Location = new System.Drawing.Point(182, 105);
            this.tbServerPort.Name = "tbServerPort";
            this.tbServerPort.Size = new System.Drawing.Size(220, 49);
            this.tbServerPort.TabIndex = 844;
            this.tbServerPort.Text = "100";
            this.tbServerPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnWrite
            // 
            this.btnWrite.BackColor = System.Drawing.Color.Red;
            this.btnWrite.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnWrite.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnWrite.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Aquamarine;
            this.btnWrite.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnWrite.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Bold);
            this.btnWrite.ForeColor = System.Drawing.Color.White;
            this.btnWrite.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnWrite.Location = new System.Drawing.Point(2, 204);
            this.btnWrite.Margin = new System.Windows.Forms.Padding(3, 10, 3, 10);
            this.btnWrite.Name = "btnWrite";
            this.btnWrite.Size = new System.Drawing.Size(179, 49);
            this.btnWrite.TabIndex = 843;
            this.btnWrite.Text = "Write";
            this.btnWrite.UseVisualStyleBackColor = false;
            this.btnWrite.Click += new System.EventHandler(this.btnWrite_Click);
            // 
            // label15
            // 
            this.label15.BackColor = System.Drawing.Color.Transparent;
            this.label15.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label15.Font = new System.Drawing.Font("Arial", 15F);
            this.label15.ForeColor = System.Drawing.Color.Aquamarine;
            this.label15.Location = new System.Drawing.Point(2, 104);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(174, 50);
            this.label15.TabIndex = 842;
            this.label15.Text = "SERVER PORT";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label16
            // 
            this.label16.BackColor = System.Drawing.Color.Transparent;
            this.label16.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label16.Font = new System.Drawing.Font("Arial", 15F);
            this.label16.ForeColor = System.Drawing.Color.Aquamarine;
            this.label16.Location = new System.Drawing.Point(403, 105);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(168, 50);
            this.label16.TabIndex = 841;
            this.label16.Text = "ADDRESS TYPE";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label19
            // 
            this.label19.BackColor = System.Drawing.Color.Transparent;
            this.label19.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label19.Font = new System.Drawing.Font("Arial", 15F);
            this.label19.ForeColor = System.Drawing.Color.Aquamarine;
            this.label19.Location = new System.Drawing.Point(403, 154);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(168, 50);
            this.label19.TabIndex = 840;
            this.label19.Text = "ADDRESS";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label13.Font = new System.Drawing.Font("Arial", 15F);
            this.label13.ForeColor = System.Drawing.Color.Aquamarine;
            this.label13.Location = new System.Drawing.Point(2, 55);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(174, 50);
            this.label13.TabIndex = 839;
            this.label13.Text = "SERVER IP";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cbAddressType
            // 
            this.cbAddressType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.cbAddressType.Font = new System.Drawing.Font("Arial", 27F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbAddressType.ForeColor = System.Drawing.Color.Aquamarine;
            this.cbAddressType.FormattingEnabled = true;
            this.cbAddressType.Items.AddRange(new object[] {
            "D"});
            this.cbAddressType.Location = new System.Drawing.Point(578, 104);
            this.cbAddressType.Name = "cbAddressType";
            this.cbAddressType.Size = new System.Drawing.Size(226, 49);
            this.cbAddressType.TabIndex = 838;
            this.cbAddressType.Text = "D";
            // 
            // tbServerip
            // 
            this.tbServerip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.tbServerip.Font = new System.Drawing.Font("Arial", 27F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbServerip.ForeColor = System.Drawing.Color.Aquamarine;
            this.tbServerip.Location = new System.Drawing.Point(182, 55);
            this.tbServerip.Name = "tbServerip";
            this.tbServerip.Size = new System.Drawing.Size(618, 49);
            this.tbServerip.TabIndex = 837;
            this.tbServerip.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnRead
            // 
            this.btnRead.BackColor = System.Drawing.Color.Red;
            this.btnRead.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRead.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnRead.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Aquamarine;
            this.btnRead.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnRead.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Bold);
            this.btnRead.ForeColor = System.Drawing.Color.White;
            this.btnRead.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRead.Location = new System.Drawing.Point(403, 205);
            this.btnRead.Margin = new System.Windows.Forms.Padding(3, 10, 3, 10);
            this.btnRead.Name = "btnRead";
            this.btnRead.Size = new System.Drawing.Size(173, 49);
            this.btnRead.TabIndex = 851;
            this.btnRead.Text = "Read";
            this.btnRead.UseVisualStyleBackColor = false;
            this.btnRead.Click += new System.EventHandler(this.btnRead_Click);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("Arial", 15F);
            this.label1.ForeColor = System.Drawing.Color.Aquamarine;
            this.label1.Location = new System.Drawing.Point(2, 154);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(174, 50);
            this.label1.TabIndex = 853;
            this.label1.Text = "ADDRESS TYPE";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cbWordCount
            // 
            this.cbWordCount.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.cbWordCount.Font = new System.Drawing.Font("Arial", 27F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbWordCount.ForeColor = System.Drawing.Color.Aquamarine;
            this.cbWordCount.FormattingEnabled = true;
            this.cbWordCount.Items.AddRange(new object[] {
            "1Word",
            "2Word"});
            this.cbWordCount.Location = new System.Drawing.Point(183, 153);
            this.cbWordCount.Name = "cbWordCount";
            this.cbWordCount.Size = new System.Drawing.Size(220, 49);
            this.cbWordCount.TabIndex = 852;
            this.cbWordCount.Text = "1Word";
            // 
            // btnApply
            // 
            this.btnApply.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnApply.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnApply.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Bold);
            this.btnApply.ForeColor = System.Drawing.Color.Yellow;
            this.btnApply.Location = new System.Drawing.Point(100, 266);
            this.btnApply.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(203, 74);
            this.btnApply.TabIndex = 914;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = false;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // FormPlcConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(800, 342);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbWordCount);
            this.Controls.Add(this.btnRead);
            this.Controls.Add(this.lbReadValue);
            this.Controls.Add(this.tbWriteValue);
            this.Controls.Add(this.tbAddress);
            this.Controls.Add(this.tbServerPort);
            this.Controls.Add(this.btnWrite);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.cbAddressType);
            this.Controls.Add(this.tbServerip);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FormPlcConfig";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "PLC CONFIG";
            this.Load += new System.EventHandler(this.FormPlcConfig_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label lbReadValue;
        private System.Windows.Forms.TextBox tbWriteValue;
        private System.Windows.Forms.TextBox tbAddress;
        private System.Windows.Forms.TextBox tbServerPort;
        private System.Windows.Forms.Button btnWrite;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox cbAddressType;
        private System.Windows.Forms.TextBox tbServerip;
        private System.Windows.Forms.Button btnRead;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbWordCount;
        private System.Windows.Forms.Button btnApply;
    }
}