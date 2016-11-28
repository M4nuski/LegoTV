namespace TFT_Data_Manager
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
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.OpenDataMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveAsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FlashMemoryMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listView1 = new System.Windows.Forms.ListView();
            this.thumbnailList = new System.Windows.Forms.ImageList(this.components);
            this.addButton = new System.Windows.Forms.Button();
            this.loadSourceButton = new System.Windows.Forms.Button();
            this.fitAllButton = new System.Windows.Forms.Button();
            this.moveDownButton = new System.Windows.Forms.Button();
            this.moveUpButton = new System.Windows.Forms.Button();
            this.removeButton = new System.Windows.Forms.Button();
            this.previewLabel = new System.Windows.Forms.Label();
            this.sourceLabel = new System.Windows.Forms.Label();
            this.TopTextBox = new System.Windows.Forms.TextBox();
            this.LeftTextBox = new System.Windows.Forms.TextBox();
            this.WidthTextBox = new System.Windows.Forms.TextBox();
            this.HeightTextBox = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.leftTrackBar = new System.Windows.Forms.TrackBar();
            this.fitMostButton = new System.Windows.Forms.Button();
            this.rightTrackBar = new System.Windows.Forms.TrackBar();
            this.bottomTrackBar = new System.Windows.Forms.TrackBar();
            this.topTrackBar = new System.Windows.Forms.TrackBar();
            this.openLIBFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveLIBFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.saveDTAFileDialog2 = new System.Windows.Forms.SaveFileDialog();
            this.openImageFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.checkBox5 = new System.Windows.Forms.CheckBox();
            this.checkBox6 = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.addFromLibToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label5 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.leftTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rightTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bottomTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.topTrackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenDataMenuItem,
            this.addFromLibToolStripMenuItem,
            this.SaveAsMenuItem,
            this.FlashMemoryMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1265, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // OpenDataMenuItem
            // 
            this.OpenDataMenuItem.Name = "OpenDataMenuItem";
            this.OpenDataMenuItem.Size = new System.Drawing.Size(67, 20);
            this.OpenDataMenuItem.Text = "Open LIb";
            this.OpenDataMenuItem.Click += new System.EventHandler(this.OpenDataMenuItem_Click);
            // 
            // SaveAsMenuItem
            // 
            this.SaveAsMenuItem.Name = "SaveAsMenuItem";
            this.SaveAsMenuItem.Size = new System.Drawing.Size(78, 20);
            this.SaveAsMenuItem.Text = "Save Lib As";
            this.SaveAsMenuItem.Click += new System.EventHandler(this.SaveAsMenuItem_Click);
            // 
            // FlashMemoryMenuItem
            // 
            this.FlashMemoryMenuItem.Name = "FlashMemoryMenuItem";
            this.FlashMemoryMenuItem.Size = new System.Drawing.Size(79, 20);
            this.FlashMemoryMenuItem.Text = "Export Data";
            this.FlashMemoryMenuItem.Click += new System.EventHandler(this.FlashMemoryMenuItem_Click);
            // 
            // listView1
            // 
            this.listView1.GridLines = true;
            this.listView1.LargeImageList = this.thumbnailList;
            this.listView1.Location = new System.Drawing.Point(12, 27);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.ShowGroups = false;
            this.listView1.Size = new System.Drawing.Size(157, 714);
            this.listView1.TabIndex = 1;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.VirtualListSize = 1;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // thumbnailList
            // 
            this.thumbnailList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.thumbnailList.ImageSize = new System.Drawing.Size(80, 64);
            this.thumbnailList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(197, 27);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(75, 23);
            this.addButton.TabIndex = 2;
            this.addButton.Text = "Add";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // loadSourceButton
            // 
            this.loadSourceButton.Location = new System.Drawing.Point(372, 32);
            this.loadSourceButton.Name = "loadSourceButton";
            this.loadSourceButton.Size = new System.Drawing.Size(156, 23);
            this.loadSourceButton.TabIndex = 3;
            this.loadSourceButton.Text = "Load Source Image";
            this.loadSourceButton.UseVisualStyleBackColor = true;
            this.loadSourceButton.Click += new System.EventHandler(this.loadSourceButton_Click);
            // 
            // fitAllButton
            // 
            this.fitAllButton.Location = new System.Drawing.Point(372, 116);
            this.fitAllButton.Name = "fitAllButton";
            this.fitAllButton.Size = new System.Drawing.Size(156, 23);
            this.fitAllButton.TabIndex = 4;
            this.fitAllButton.Text = "Fit All";
            this.fitAllButton.UseVisualStyleBackColor = true;
            this.fitAllButton.Click += new System.EventHandler(this.fitAllButton_Click);
            // 
            // moveDownButton
            // 
            this.moveDownButton.Location = new System.Drawing.Point(197, 85);
            this.moveDownButton.Name = "moveDownButton";
            this.moveDownButton.Size = new System.Drawing.Size(75, 23);
            this.moveDownButton.TabIndex = 5;
            this.moveDownButton.Text = "Move Down";
            this.moveDownButton.UseVisualStyleBackColor = true;
            this.moveDownButton.Click += new System.EventHandler(this.moveDownButton_Click);
            // 
            // moveUpButton
            // 
            this.moveUpButton.Location = new System.Drawing.Point(197, 56);
            this.moveUpButton.Name = "moveUpButton";
            this.moveUpButton.Size = new System.Drawing.Size(75, 23);
            this.moveUpButton.TabIndex = 6;
            this.moveUpButton.Text = "Move Up";
            this.moveUpButton.UseVisualStyleBackColor = true;
            this.moveUpButton.Click += new System.EventHandler(this.moveUpButton_Click);
            // 
            // removeButton
            // 
            this.removeButton.Location = new System.Drawing.Point(197, 256);
            this.removeButton.Name = "removeButton";
            this.removeButton.Size = new System.Drawing.Size(75, 23);
            this.removeButton.TabIndex = 7;
            this.removeButton.Text = "Remove";
            this.removeButton.UseVisualStyleBackColor = true;
            this.removeButton.Click += new System.EventHandler(this.removeButton_Click);
            // 
            // previewLabel
            // 
            this.previewLabel.BackColor = System.Drawing.Color.Black;
            this.previewLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.previewLabel.Location = new System.Drawing.Point(194, 304);
            this.previewLabel.Name = "previewLabel";
            this.previewLabel.Size = new System.Drawing.Size(320, 256);
            this.previewLabel.TabIndex = 8;
            this.previewLabel.Text = "label1";
            this.previewLabel.Paint += new System.Windows.Forms.PaintEventHandler(this.previewLabel_Paint);
            // 
            // sourceLabel
            // 
            this.sourceLabel.BackColor = System.Drawing.Color.Black;
            this.sourceLabel.Location = new System.Drawing.Point(574, 80);
            this.sourceLabel.Name = "sourceLabel";
            this.sourceLabel.Size = new System.Drawing.Size(640, 480);
            this.sourceLabel.TabIndex = 9;
            this.sourceLabel.Text = "label1";
            this.sourceLabel.Paint += new System.Windows.Forms.PaintEventHandler(this.sourceLabel_Paint);
            // 
            // TopTextBox
            // 
            this.TopTextBox.Location = new System.Drawing.Point(467, 181);
            this.TopTextBox.Name = "TopTextBox";
            this.TopTextBox.ReadOnly = true;
            this.TopTextBox.Size = new System.Drawing.Size(61, 20);
            this.TopTextBox.TabIndex = 11;
            this.TopTextBox.Text = "0";
            // 
            // LeftTextBox
            // 
            this.LeftTextBox.Location = new System.Drawing.Point(467, 155);
            this.LeftTextBox.Name = "LeftTextBox";
            this.LeftTextBox.ReadOnly = true;
            this.LeftTextBox.Size = new System.Drawing.Size(61, 20);
            this.LeftTextBox.TabIndex = 12;
            this.LeftTextBox.Text = "0";
            // 
            // WidthTextBox
            // 
            this.WidthTextBox.Location = new System.Drawing.Point(467, 207);
            this.WidthTextBox.Name = "WidthTextBox";
            this.WidthTextBox.ReadOnly = true;
            this.WidthTextBox.Size = new System.Drawing.Size(61, 20);
            this.WidthTextBox.TabIndex = 13;
            this.WidthTextBox.Text = "0";
            // 
            // HeightTextBox
            // 
            this.HeightTextBox.Location = new System.Drawing.Point(467, 233);
            this.HeightTextBox.Name = "HeightTextBox";
            this.HeightTextBox.ReadOnly = true;
            this.HeightTextBox.Size = new System.Drawing.Size(61, 20);
            this.HeightTextBox.TabIndex = 14;
            this.HeightTextBox.Text = "0";
            // 
            // comboBox1
            // 
            this.comboBox1.Enabled = false;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "RGB666 SST:32 WB:256",
            "RGB444 SST:64 WB:512"});
            this.comboBox1.Location = new System.Drawing.Point(192, 171);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(156, 21);
            this.comboBox1.TabIndex = 15;
            // 
            // leftTrackBar
            // 
            this.leftTrackBar.Location = new System.Drawing.Point(565, 32);
            this.leftTrackBar.Maximum = 100;
            this.leftTrackBar.Name = "leftTrackBar";
            this.leftTrackBar.Size = new System.Drawing.Size(662, 45);
            this.leftTrackBar.TabIndex = 16;
            this.leftTrackBar.ValueChanged += new System.EventHandler(this.TrackBar_ValueChanged);
            // 
            // fitMostButton
            // 
            this.fitMostButton.Location = new System.Drawing.Point(372, 85);
            this.fitMostButton.Name = "fitMostButton";
            this.fitMostButton.Size = new System.Drawing.Size(156, 23);
            this.fitMostButton.TabIndex = 17;
            this.fitMostButton.Text = "Fit Most";
            this.fitMostButton.UseVisualStyleBackColor = true;
            this.fitMostButton.Click += new System.EventHandler(this.fitMostButton_Click);
            // 
            // rightTrackBar
            // 
            this.rightTrackBar.Location = new System.Drawing.Point(565, 563);
            this.rightTrackBar.Maximum = 100;
            this.rightTrackBar.Name = "rightTrackBar";
            this.rightTrackBar.Size = new System.Drawing.Size(662, 45);
            this.rightTrackBar.TabIndex = 18;
            this.rightTrackBar.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.rightTrackBar.Value = 100;
            this.rightTrackBar.ValueChanged += new System.EventHandler(this.TrackBar_ValueChanged);
            // 
            // bottomTrackBar
            // 
            this.bottomTrackBar.Location = new System.Drawing.Point(1220, 71);
            this.bottomTrackBar.Maximum = 0;
            this.bottomTrackBar.Minimum = -100;
            this.bottomTrackBar.Name = "bottomTrackBar";
            this.bottomTrackBar.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.bottomTrackBar.Size = new System.Drawing.Size(45, 500);
            this.bottomTrackBar.TabIndex = 19;
            this.bottomTrackBar.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.bottomTrackBar.Value = -100;
            this.bottomTrackBar.ValueChanged += new System.EventHandler(this.TrackBar_ValueChanged);
            // 
            // topTrackBar
            // 
            this.topTrackBar.Location = new System.Drawing.Point(534, 71);
            this.topTrackBar.Maximum = 0;
            this.topTrackBar.Minimum = -100;
            this.topTrackBar.Name = "topTrackBar";
            this.topTrackBar.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.topTrackBar.Size = new System.Drawing.Size(45, 500);
            this.topTrackBar.TabIndex = 20;
            this.topTrackBar.ValueChanged += new System.EventHandler(this.TrackBar_ValueChanged);
            // 
            // openLIBFileDialog1
            // 
            this.openLIBFileDialog1.DefaultExt = "tft_lib";
            this.openLIBFileDialog1.FileName = "openFileDialog1";
            this.openLIBFileDialog1.Filter = "TFT Library|*.tft_lib";
            // 
            // saveLIBFileDialog1
            // 
            this.saveLIBFileDialog1.DefaultExt = "tft_lib";
            this.saveLIBFileDialog1.Filter = "TFT Library|*.tft_lib";
            // 
            // saveDTAFileDialog2
            // 
            this.saveDTAFileDialog2.DefaultExt = "tft_dta";
            this.saveDTAFileDialog2.Filter = "TFT Data|*.tft_dta";
            // 
            // openImageFileDialog1
            // 
            this.openImageFileDialog1.DefaultExt = "jpg";
            this.openImageFileDialog1.FileName = "*.*";
            this.openImageFileDialog1.Filter = "jpg|*.jpg,*.jpeg|bitmap|*.bmp|PNG|*.png";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(585, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 13);
            this.label1.TabIndex = 21;
            this.label1.Text = "Source Image Clip/Crop";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(194, 289);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 13);
            this.label2.TabIndex = 22;
            this.label2.Text = "Result Image (X2)";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Enabled = false;
            this.checkBox1.Location = new System.Drawing.Point(382, 172);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(42, 17);
            this.checkBox1.TabIndex = 23;
            this.checkBox1.Text = "MY";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Enabled = false;
            this.checkBox2.Location = new System.Drawing.Point(382, 240);
            this.checkBox2.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(49, 17);
            this.checkBox2.TabIndex = 24;
            this.checkBox2.Text = "RGB";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Checked = true;
            this.checkBox3.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox3.Enabled = false;
            this.checkBox3.Location = new System.Drawing.Point(382, 206);
            this.checkBox3.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(42, 17);
            this.checkBox3.TabIndex = 25;
            this.checkBox3.Text = "MV";
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // checkBox4
            // 
            this.checkBox4.AutoSize = true;
            this.checkBox4.Enabled = false;
            this.checkBox4.Location = new System.Drawing.Point(382, 189);
            this.checkBox4.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Size = new System.Drawing.Size(42, 17);
            this.checkBox4.TabIndex = 26;
            this.checkBox4.Text = "MX";
            this.checkBox4.UseVisualStyleBackColor = true;
            // 
            // checkBox5
            // 
            this.checkBox5.AutoSize = true;
            this.checkBox5.Enabled = false;
            this.checkBox5.Location = new System.Drawing.Point(382, 223);
            this.checkBox5.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.checkBox5.Name = "checkBox5";
            this.checkBox5.Size = new System.Drawing.Size(41, 17);
            this.checkBox5.TabIndex = 27;
            this.checkBox5.Text = "ML";
            this.checkBox5.UseVisualStyleBackColor = true;
            // 
            // checkBox6
            // 
            this.checkBox6.AutoSize = true;
            this.checkBox6.Enabled = false;
            this.checkBox6.Location = new System.Drawing.Point(382, 257);
            this.checkBox6.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.checkBox6.Name = "checkBox6";
            this.checkBox6.Size = new System.Drawing.Size(43, 17);
            this.checkBox6.TabIndex = 28;
            this.checkBox6.Text = "MH";
            this.checkBox6.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(385, 158);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 13);
            this.label3.TabIndex = 29;
            this.label3.Text = "MADCTL";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(194, 155);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(158, 13);
            this.label4.TabIndex = 31;
            this.label4.Text = "RGB type / Max Image Quantity";
            // 
            // addFromLibToolStripMenuItem
            // 
            this.addFromLibToolStripMenuItem.Name = "addFromLibToolStripMenuItem";
            this.addFromLibToolStripMenuItem.Size = new System.Drawing.Size(91, 20);
            this.addFromLibToolStripMenuItem.Text = "Add From Lib";
            this.addFromLibToolStripMenuItem.Click += new System.EventHandler(this.addFromLibToolStripMenuItem_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(199, 720);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(42, 13);
            this.label5.TabIndex = 32;
            this.label5.Text = "0 / 255";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1265, 753);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.checkBox6);
            this.Controls.Add(this.checkBox5);
            this.Controls.Add(this.checkBox4);
            this.Controls.Add(this.checkBox3);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.topTrackBar);
            this.Controls.Add(this.bottomTrackBar);
            this.Controls.Add(this.rightTrackBar);
            this.Controls.Add(this.fitMostButton);
            this.Controls.Add(this.leftTrackBar);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.HeightTextBox);
            this.Controls.Add(this.WidthTextBox);
            this.Controls.Add(this.LeftTextBox);
            this.Controls.Add(this.TopTextBox);
            this.Controls.Add(this.sourceLabel);
            this.Controls.Add(this.previewLabel);
            this.Controls.Add(this.removeButton);
            this.Controls.Add(this.moveUpButton);
            this.Controls.Add(this.moveDownButton);
            this.Controls.Add(this.fitAllButton);
            this.Controls.Add(this.loadSourceButton);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "TFT Data Manager";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.leftTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rightTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bottomTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.topTrackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem OpenDataMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SaveAsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FlashMemoryMenuItem;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ImageList thumbnailList;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Button loadSourceButton;
        private System.Windows.Forms.Button fitAllButton;
        private System.Windows.Forms.Button moveDownButton;
        private System.Windows.Forms.Button moveUpButton;
        private System.Windows.Forms.Button removeButton;
        private System.Windows.Forms.Label previewLabel;
        private System.Windows.Forms.Label sourceLabel;
        private System.Windows.Forms.TextBox TopTextBox;
        private System.Windows.Forms.TextBox LeftTextBox;
        private System.Windows.Forms.TextBox WidthTextBox;
        private System.Windows.Forms.TextBox HeightTextBox;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.TrackBar leftTrackBar;
        private System.Windows.Forms.Button fitMostButton;
        private System.Windows.Forms.TrackBar rightTrackBar;
        private System.Windows.Forms.TrackBar bottomTrackBar;
        private System.Windows.Forms.TrackBar topTrackBar;
        private System.Windows.Forms.OpenFileDialog openLIBFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveLIBFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveDTAFileDialog2;
        private System.Windows.Forms.OpenFileDialog openImageFileDialog1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.CheckBox checkBox4;
        private System.Windows.Forms.CheckBox checkBox5;
        private System.Windows.Forms.CheckBox checkBox6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ToolStripMenuItem addFromLibToolStripMenuItem;
        private System.Windows.Forms.Label label5;
    }
}

