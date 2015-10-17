namespace DesktopUI
{
  partial class ImageRecognitor
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.filterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.haussToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.medianToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resultPictureBox = new System.Windows.Forms.PictureBox();
            this.thresholdInput = new System.Windows.Forms.TextBox();
            this.sourcePictureBox = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SobelCheckBox = new System.Windows.Forms.CheckBox();
            this.gausCheckBox = new System.Windows.Forms.CheckBox();
            this.medianCheckBox = new System.Windows.Forms.CheckBox();
            this.medianNumeric = new System.Windows.Forms.NumericUpDown();
            this.clarityСheckBox = new System.Windows.Forms.CheckBox();
            this.BinarizeCheckBox = new System.Windows.Forms.CheckBox();
            this.binarizeNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.resultPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sourcePictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.medianNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.binarizeNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.filterToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1285, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.OpenToolStripMenuItemClick);
            // 
            // filterToolStripMenuItem
            // 
            this.filterToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.haussToolStripMenuItem,
            this.medianToolStripMenuItem});
            this.filterToolStripMenuItem.Name = "filterToolStripMenuItem";
            this.filterToolStripMenuItem.Size = new System.Drawing.Size(45, 20);
            this.filterToolStripMenuItem.Text = "Filter";
            // 
            // haussToolStripMenuItem
            // 
            this.haussToolStripMenuItem.Name = "haussToolStripMenuItem";
            this.haussToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.haussToolStripMenuItem.Text = "Gaussian";
            this.haussToolStripMenuItem.Click += new System.EventHandler(this.haussToolStripMenuItem_Click);
            // 
            // medianToolStripMenuItem
            // 
            this.medianToolStripMenuItem.Name = "medianToolStripMenuItem";
            this.medianToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.medianToolStripMenuItem.Text = "Median";
            // 
            // resultPictureBox
            // 
            this.resultPictureBox.Location = new System.Drawing.Point(337, 27);
            this.resultPictureBox.Name = "resultPictureBox";
            this.resultPictureBox.Size = new System.Drawing.Size(948, 756);
            this.resultPictureBox.TabIndex = 2;
            this.resultPictureBox.TabStop = false;
            // 
            // thresholdInput
            // 
            this.thresholdInput.Location = new System.Drawing.Point(160, 454);
            this.thresholdInput.Name = "thresholdInput";
            this.thresholdInput.Size = new System.Drawing.Size(100, 20);
            this.thresholdInput.TabIndex = 3;
            this.thresholdInput.Text = "50";
            this.thresholdInput.Visible = false;
            this.thresholdInput.TextChanged += new System.EventHandler(this.ThresholdInputTextChanged);
            // 
            // sourcePictureBox
            // 
            this.sourcePictureBox.Location = new System.Drawing.Point(12, 27);
            this.sourcePictureBox.Name = "sourcePictureBox";
            this.sourcePictureBox.Size = new System.Drawing.Size(303, 249);
            this.sourcePictureBox.TabIndex = 1;
            this.sourcePictureBox.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(36, 449);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Apply";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // SobelCheckBox
            // 
            this.SobelCheckBox.AutoSize = true;
            this.SobelCheckBox.Location = new System.Drawing.Point(36, 370);
            this.SobelCheckBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.SobelCheckBox.Name = "SobelCheckBox";
            this.SobelCheckBox.Size = new System.Drawing.Size(63, 17);
            this.SobelCheckBox.TabIndex = 5;
            this.SobelCheckBox.Text = "Собель";
            this.SobelCheckBox.UseVisualStyleBackColor = true;
            // 
            // gausCheckBox
            // 
            this.gausCheckBox.AutoSize = true;
            this.gausCheckBox.Location = new System.Drawing.Point(36, 305);
            this.gausCheckBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.gausCheckBox.Name = "gausCheckBox";
            this.gausCheckBox.Size = new System.Drawing.Size(116, 17);
            this.gausCheckBox.TabIndex = 6;
            this.gausCheckBox.Text = "Размытие Гаусса";
            this.gausCheckBox.UseVisualStyleBackColor = true;
            // 
            // medianCheckBox
            // 
            this.medianCheckBox.AutoSize = true;
            this.medianCheckBox.Location = new System.Drawing.Point(36, 327);
            this.medianCheckBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.medianCheckBox.Name = "medianCheckBox";
            this.medianCheckBox.Size = new System.Drawing.Size(85, 17);
            this.medianCheckBox.TabIndex = 7;
            this.medianCheckBox.Text = "Медианный";
            this.medianCheckBox.UseVisualStyleBackColor = true;
            // 
            // medianNumeric
            // 
            this.medianNumeric.Location = new System.Drawing.Point(152, 326);
            this.medianNumeric.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.medianNumeric.Maximum = new decimal(new int[] {
            11,
            0,
            0,
            0});
            this.medianNumeric.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.medianNumeric.Name = "medianNumeric";
            this.medianNumeric.Size = new System.Drawing.Size(90, 20);
            this.medianNumeric.TabIndex = 8;
            this.medianNumeric.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // clarityСheckBox
            // 
            this.clarityСheckBox.AutoSize = true;
            this.clarityСheckBox.Location = new System.Drawing.Point(36, 349);
            this.clarityСheckBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.clarityСheckBox.Name = "clarityСheckBox";
            this.clarityСheckBox.Size = new System.Drawing.Size(74, 17);
            this.clarityСheckBox.TabIndex = 9;
            this.clarityСheckBox.Text = "Четкость";
            this.clarityСheckBox.UseVisualStyleBackColor = true;
            // 
            // BinarizeCheckBox
            // 
            this.BinarizeCheckBox.AutoSize = true;
            this.BinarizeCheckBox.Location = new System.Drawing.Point(36, 392);
            this.BinarizeCheckBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.BinarizeCheckBox.Name = "BinarizeCheckBox";
            this.BinarizeCheckBox.Size = new System.Drawing.Size(93, 17);
            this.BinarizeCheckBox.TabIndex = 10;
            this.BinarizeCheckBox.Text = "Бинаризация";
            this.BinarizeCheckBox.UseVisualStyleBackColor = true;
            // 
            // binarizeNumericUpDown
            // 
            this.binarizeNumericUpDown.Location = new System.Drawing.Point(152, 392);
            this.binarizeNumericUpDown.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.binarizeNumericUpDown.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.binarizeNumericUpDown.Name = "binarizeNumericUpDown";
            this.binarizeNumericUpDown.Size = new System.Drawing.Size(90, 20);
            this.binarizeNumericUpDown.TabIndex = 11;
            this.binarizeNumericUpDown.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // ImageRecognitor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1028, 609);
            this.Controls.Add(this.binarizeNumericUpDown);
            this.Controls.Add(this.BinarizeCheckBox);
            this.Controls.Add(this.clarityСheckBox);
            this.Controls.Add(this.medianNumeric);
            this.Controls.Add(this.medianCheckBox);
            this.Controls.Add(this.gausCheckBox);
            this.Controls.Add(this.SobelCheckBox);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.thresholdInput);
            this.Controls.Add(this.resultPictureBox);
            this.Controls.Add(this.sourcePictureBox);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "ImageRecognitor";
            this.Text = "ImageRecognitor";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.resultPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sourcePictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.medianNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.binarizeNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.MenuStrip menuStrip1;
    private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
    private System.Windows.Forms.PictureBox resultPictureBox;
    private System.Windows.Forms.TextBox thresholdInput;
    private System.Windows.Forms.PictureBox sourcePictureBox;
    private System.Windows.Forms.ToolStripMenuItem filterToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem haussToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem medianToolStripMenuItem;
    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.CheckBox SobelCheckBox;
    private System.Windows.Forms.CheckBox gausCheckBox;
    private System.Windows.Forms.CheckBox medianCheckBox;
    private System.Windows.Forms.NumericUpDown medianNumeric;
    private System.Windows.Forms.CheckBox clarityСheckBox;
    private System.Windows.Forms.CheckBox BinarizeCheckBox;
    private System.Windows.Forms.NumericUpDown binarizeNumericUpDown;
  }
}

