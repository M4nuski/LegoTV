namespace SPI_FLASH
{
    partial class MainForm
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
            this.log_textbox = new System.Windows.Forms.TextBox();
            this.LD_device_button = new System.Windows.Forms.Button();
            this.RD_status_button = new System.Windows.Forms.Button();
            this.RD_JEDEDID_button = new System.Windows.Forms.Button();
            this.RD_status123_button = new System.Windows.Forms.Button();
            this.WR_enable_button = new System.Windows.Forms.Button();
            this.WR_disable_button = new System.Windows.Forms.Button();
            this.RST_device_button = new System.Windows.Forms.Button();
            this.Erease_4K_button = new System.Windows.Forms.Button();
            this.Erease_32K_button = new System.Windows.Forms.Button();
            this.Erease_CHIP_button = new System.Windows.Forms.Button();
            this.Erease_64K_button = new System.Windows.Forms.Button();
            this.Address_textbox = new System.Windows.Forms.TextBox();
            this.RD_bytes_button = new System.Windows.Forms.Button();
            this.WR_bytes_button = new System.Windows.Forms.Button();
            this.label_address = new System.Windows.Forms.Label();
            this.label_numBytes = new System.Windows.Forms.Label();
            this.numBytes_textbox = new System.Windows.Forms.TextBox();
            this.HEX_bytesData_toWrite_textbox = new System.Windows.Forms.TextBox();
            this.JEDEC_ID_textbox = new System.Windows.Forms.TextBox();
            this.label_JEDEC_ID = new System.Windows.Forms.Label();
            this.loadTFTDTA_and_flash_chip_button = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.label_FlashStatusLine1 = new System.Windows.Forms.Label();
            this.label_FlashStatusLine2 = new System.Windows.Forms.Label();
            this.label_FlashStatusLine3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.log_textbox.Location = new System.Drawing.Point(12, 265);
            this.log_textbox.Multiline = true;
            this.log_textbox.Name = "log_textbox";
            this.log_textbox.ReadOnly = true;
            this.log_textbox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.log_textbox.Size = new System.Drawing.Size(749, 109);
            this.log_textbox.TabIndex = 4;
            // 
            // button1
            // 
            this.LD_device_button.Location = new System.Drawing.Point(12, 12);
            this.LD_device_button.Name = "LD_device_button";
            this.LD_device_button.Size = new System.Drawing.Size(123, 23);
            this.LD_device_button.TabIndex = 3;
            this.LD_device_button.Text = "Load Device";
            this.LD_device_button.UseVisualStyleBackColor = true;
            this.LD_device_button.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.RD_status_button.Location = new System.Drawing.Point(12, 41);
            this.RD_status_button.Name = "RD_status_button";
            this.RD_status_button.Size = new System.Drawing.Size(123, 23);
            this.RD_status_button.TabIndex = 5;
            this.RD_status_button.Text = "Read Status";
            this.RD_status_button.UseVisualStyleBackColor = true;
            this.RD_status_button.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.RD_JEDEDID_button.Location = new System.Drawing.Point(12, 70);
            this.RD_JEDEDID_button.Name = "RD_JEDEDID_button";
            this.RD_JEDEDID_button.Size = new System.Drawing.Size(123, 23);
            this.RD_JEDEDID_button.TabIndex = 6;
            this.RD_JEDEDID_button.Text = "Read JEDEC ID";
            this.RD_JEDEDID_button.UseVisualStyleBackColor = true;
            this.RD_JEDEDID_button.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.RD_status123_button.Location = new System.Drawing.Point(141, 41);
            this.RD_status123_button.Name = "RD_status123_button";
            this.RD_status123_button.Size = new System.Drawing.Size(123, 23);
            this.RD_status123_button.TabIndex = 7;
            this.RD_status123_button.Text = "Read SSR1-2-3";
            this.RD_status123_button.UseVisualStyleBackColor = true;
            this.RD_status123_button.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.WR_enable_button.Location = new System.Drawing.Point(12, 99);
            this.WR_enable_button.Name = "WR_enable_button";
            this.WR_enable_button.Size = new System.Drawing.Size(123, 23);
            this.WR_enable_button.TabIndex = 8;
            this.WR_enable_button.Text = "Write Enable";
            this.WR_enable_button.UseVisualStyleBackColor = true;
            this.WR_enable_button.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.WR_disable_button.Location = new System.Drawing.Point(141, 99);
            this.WR_disable_button.Name = "WR_disable_button";
            this.WR_disable_button.Size = new System.Drawing.Size(123, 23);
            this.WR_disable_button.TabIndex = 9;
            this.WR_disable_button.Text = "Write Disable";
            this.WR_disable_button.UseVisualStyleBackColor = true;
            this.WR_disable_button.Click += new System.EventHandler(this.button6_Click);
            // 
            // button7
            // 
            this.RST_device_button.Location = new System.Drawing.Point(141, 12);
            this.RST_device_button.Name = "RST_device_button";
            this.RST_device_button.Size = new System.Drawing.Size(123, 23);
            this.RST_device_button.TabIndex = 10;
            this.RST_device_button.Text = "Reset Device";
            this.RST_device_button.UseVisualStyleBackColor = true;
            this.RST_device_button.Click += new System.EventHandler(this.button7_Click);
            // 
            // button8
            // 
            this.Erease_4K_button.Location = new System.Drawing.Point(12, 128);
            this.Erease_4K_button.Name = "Erease_4K_button";
            this.Erease_4K_button.Size = new System.Drawing.Size(123, 23);
            this.Erease_4K_button.TabIndex = 11;
            this.Erease_4K_button.Text = "Erase 4K";
            this.Erease_4K_button.UseVisualStyleBackColor = true;
            this.Erease_4K_button.Click += new System.EventHandler(this.button8_Click);
            // 
            // button9
            // 
            this.Erease_32K_button.Location = new System.Drawing.Point(141, 128);
            this.Erease_32K_button.Name = "Erease_32K_button";
            this.Erease_32K_button.Size = new System.Drawing.Size(123, 23);
            this.Erease_32K_button.TabIndex = 12;
            this.Erease_32K_button.Text = "Erase 32K";
            this.Erease_32K_button.UseVisualStyleBackColor = true;
            this.Erease_32K_button.Click += new System.EventHandler(this.button9_Click);
            // 
            // button10
            // 
            this.Erease_CHIP_button.Location = new System.Drawing.Point(399, 128);
            this.Erease_CHIP_button.Name = "Erease_CHIP_button";
            this.Erease_CHIP_button.Size = new System.Drawing.Size(123, 23);
            this.Erease_CHIP_button.TabIndex = 13;
            this.Erease_CHIP_button.Text = "Erase Chip";
            this.Erease_CHIP_button.UseVisualStyleBackColor = true;
            this.Erease_CHIP_button.Click += new System.EventHandler(this.button10_Click);
            // 
            // button11
            // 
            this.Erease_64K_button.Location = new System.Drawing.Point(270, 128);
            this.Erease_64K_button.Name = "Erease_64K_button";
            this.Erease_64K_button.Size = new System.Drawing.Size(123, 23);
            this.Erease_64K_button.TabIndex = 14;
            this.Erease_64K_button.Text = "Erase 64K";
            this.Erease_64K_button.UseVisualStyleBackColor = true;
            this.Erease_64K_button.Click += new System.EventHandler(this.button11_Click);
            // 
            // textBox2
            // 
            this.Address_textbox.Location = new System.Drawing.Point(621, 130);
            this.Address_textbox.Name = "Address_textbox";
            this.Address_textbox.Size = new System.Drawing.Size(100, 20);
            this.Address_textbox.TabIndex = 15;
            this.Address_textbox.Text = "0";
            // 
            // button12
            // 
            this.RD_bytes_button.Location = new System.Drawing.Point(12, 157);
            this.RD_bytes_button.Name = "RD_bytes_button";
            this.RD_bytes_button.Size = new System.Drawing.Size(123, 23);
            this.RD_bytes_button.TabIndex = 16;
            this.RD_bytes_button.Text = "Read Byte(s)";
            this.RD_bytes_button.UseVisualStyleBackColor = true;
            this.RD_bytes_button.Click += new System.EventHandler(this.button12_Click);
            // 
            // button13
            // 
            this.WR_bytes_button.Location = new System.Drawing.Point(12, 186);
            this.WR_bytes_button.Name = "WR_bytes_button";
            this.WR_bytes_button.Size = new System.Drawing.Size(123, 23);
            this.WR_bytes_button.TabIndex = 17;
            this.WR_bytes_button.Text = "Write Byte(s)";
            this.WR_bytes_button.UseVisualStyleBackColor = true;
            this.WR_bytes_button.Click += new System.EventHandler(this.button13_Click);
            // 
            // label1
            // 
            this.label_address.AutoSize = true;
            this.label_address.Location = new System.Drawing.Point(567, 133);
            this.label_address.Name = "label_address";
            this.label_address.Size = new System.Drawing.Size(48, 13);
            this.label_address.TabIndex = 18;
            this.label_address.Text = "Address:";
            // 
            // label2
            // 
            this.label_numBytes.AutoSize = true;
            this.label_numBytes.Location = new System.Drawing.Point(559, 163);
            this.label_numBytes.Name = "label_numBytes";
            this.label_numBytes.Size = new System.Drawing.Size(56, 13);
            this.label_numBytes.TabIndex = 20;
            this.label_numBytes.Text = "numBytes:";
            // 
            // textBox3
            // 
            this.numBytes_textbox.Location = new System.Drawing.Point(621, 160);
            this.numBytes_textbox.Name = "numBytes_textbox";
            this.numBytes_textbox.Size = new System.Drawing.Size(100, 20);
            this.numBytes_textbox.TabIndex = 19;
            this.numBytes_textbox.Text = "1";
            // 
            // textBox4
            // 
            this.HEX_bytesData_toWrite_textbox.Location = new System.Drawing.Point(141, 189);
            this.HEX_bytesData_toWrite_textbox.Name = "HEX_bytesData_toWrite_textbox";
            this.HEX_bytesData_toWrite_textbox.Size = new System.Drawing.Size(580, 20);
            this.HEX_bytesData_toWrite_textbox.TabIndex = 21;
            this.HEX_bytesData_toWrite_textbox.Text = "00, FF, AA, 55, F0, 0F";
            // 
            // textBox5
            // 
            this.JEDEC_ID_textbox.Location = new System.Drawing.Point(141, 73);
            this.JEDEC_ID_textbox.Name = "JEDEC_ID_textbox";
            this.JEDEC_ID_textbox.ReadOnly = true;
            this.JEDEC_ID_textbox.Size = new System.Drawing.Size(123, 20);
            this.JEDEC_ID_textbox.TabIndex = 22;
            // 
            // label3
            // 
            this.label_JEDEC_ID.AutoSize = true;
            this.label_JEDEC_ID.Location = new System.Drawing.Point(270, 76);
            this.label_JEDEC_ID.Name = "label_JEDEC_ID";
            this.label_JEDEC_ID.Size = new System.Drawing.Size(0, 13);
            this.label_JEDEC_ID.TabIndex = 23;
            // 
            // button14
            // 
            this.loadTFTDTA_and_flash_chip_button.Location = new System.Drawing.Point(141, 215);
            this.loadTFTDTA_and_flash_chip_button.Name = "loadTFTDTA_and_flash_chip_button";
            this.loadTFTDTA_and_flash_chip_button.Size = new System.Drawing.Size(123, 23);
            this.loadTFTDTA_and_flash_chip_button.TabIndex = 24;
            this.loadTFTDTA_and_flash_chip_button.Text = "Load and Flash";
            this.loadTFTDTA_and_flash_chip_button.UseVisualStyleBackColor = true;
            this.loadTFTDTA_and_flash_chip_button.Click += new System.EventHandler(this.button14_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "TFT data|*.tft_dta";
            // 
            // label4
            // 
            this.label_FlashStatusLine1.AutoSize = true;
            this.label_FlashStatusLine1.Location = new System.Drawing.Point(270, 220);
            this.label_FlashStatusLine1.Name = "label_FlashStatusLine1";
            this.label_FlashStatusLine1.Size = new System.Drawing.Size(10, 13);
            this.label_FlashStatusLine1.TabIndex = 25;
            this.label_FlashStatusLine1.Text = "-";
            // 
            // label5
            // 
            this.label_FlashStatusLine2.AutoSize = true;
            this.label_FlashStatusLine2.Location = new System.Drawing.Point(270, 233);
            this.label_FlashStatusLine2.Name = "label_FlashStatusLine2";
            this.label_FlashStatusLine2.Size = new System.Drawing.Size(10, 13);
            this.label_FlashStatusLine2.TabIndex = 26;
            this.label_FlashStatusLine2.Text = "-";
            // 
            // label6
            // 
            this.label_FlashStatusLine3.AutoSize = true;
            this.label_FlashStatusLine3.Location = new System.Drawing.Point(270, 246);
            this.label_FlashStatusLine3.Name = "label_FlashStatusLine3";
            this.label_FlashStatusLine3.Size = new System.Drawing.Size(10, 13);
            this.label_FlashStatusLine3.TabIndex = 27;
            this.label_FlashStatusLine3.Text = "-";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(844, 386);
            this.Controls.Add(this.label_FlashStatusLine3);
            this.Controls.Add(this.label_FlashStatusLine2);
            this.Controls.Add(this.label_FlashStatusLine1);
            this.Controls.Add(this.loadTFTDTA_and_flash_chip_button);
            this.Controls.Add(this.label_JEDEC_ID);
            this.Controls.Add(this.JEDEC_ID_textbox);
            this.Controls.Add(this.HEX_bytesData_toWrite_textbox);
            this.Controls.Add(this.label_numBytes);
            this.Controls.Add(this.numBytes_textbox);
            this.Controls.Add(this.label_address);
            this.Controls.Add(this.WR_bytes_button);
            this.Controls.Add(this.RD_bytes_button);
            this.Controls.Add(this.Address_textbox);
            this.Controls.Add(this.Erease_64K_button);
            this.Controls.Add(this.Erease_CHIP_button);
            this.Controls.Add(this.Erease_32K_button);
            this.Controls.Add(this.Erease_4K_button);
            this.Controls.Add(this.RST_device_button);
            this.Controls.Add(this.WR_disable_button);
            this.Controls.Add(this.WR_enable_button);
            this.Controls.Add(this.RD_status123_button);
            this.Controls.Add(this.RD_JEDEDID_button);
            this.Controls.Add(this.RD_status_button);
            this.Controls.Add(this.log_textbox);
            this.Controls.Add(this.LD_device_button);
            this.Name = "MainForm";
            this.Text = "Flash Control trough SPI/USB";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox log_textbox;
        private System.Windows.Forms.Button LD_device_button;
        private System.Windows.Forms.Button RD_status_button;
        private System.Windows.Forms.Button RD_JEDEDID_button;
        private System.Windows.Forms.Button RD_status123_button;
        private System.Windows.Forms.Button WR_enable_button;
        private System.Windows.Forms.Button WR_disable_button;
        private System.Windows.Forms.Button RST_device_button;
        private System.Windows.Forms.Button Erease_4K_button;
        private System.Windows.Forms.Button Erease_32K_button;
        private System.Windows.Forms.Button Erease_CHIP_button;
        private System.Windows.Forms.Button Erease_64K_button;
        private System.Windows.Forms.TextBox Address_textbox;
        private System.Windows.Forms.Button RD_bytes_button;
        private System.Windows.Forms.Button WR_bytes_button;
        private System.Windows.Forms.Label label_address;
        private System.Windows.Forms.Label label_numBytes;
        private System.Windows.Forms.TextBox numBytes_textbox;
        private System.Windows.Forms.TextBox HEX_bytesData_toWrite_textbox;
        private System.Windows.Forms.TextBox JEDEC_ID_textbox;
        private System.Windows.Forms.Label label_JEDEC_ID;
        private System.Windows.Forms.Button loadTFTDTA_and_flash_chip_button;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label label_FlashStatusLine1;
        private System.Windows.Forms.Label label_FlashStatusLine2;
        private System.Windows.Forms.Label label_FlashStatusLine3;
    }
}

