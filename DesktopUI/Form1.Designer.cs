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
            this.GausCheckBox = new System.Windows.Forms.CheckBox();
            this.medianCheckBox = new System.Windows.Forms.CheckBox();
            this.medianNumeric = new System.Windows.Forms.NumericUpDown();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.resultPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sourcePictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.medianNumeric)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.filterToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1743, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(44, 24);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(114, 24);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.OpenToolStripMenuItemClick);
            // 
            // filterToolStripMenuItem
            // 
            this.filterToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.haussToolStripMenuItem,
            this.medianToolStripMenuItem});
            this.filterToolStripMenuItem.Name = "filterToolStripMenuItem";
            this.filterToolStripMenuItem.Size = new System.Drawing.Size(54, 24);
            this.filterToolStripMenuItem.Text = "Filter";
            // 
            // haussToolStripMenuItem
            // 
            this.haussToolStripMenuItem.Name = "haussToolStripMenuItem";
            this.haussToolStripMenuItem.Size = new System.Drawing.Size(136, 24);
            this.haussToolStripMenuItem.Text = "Gaussian";
            this.haussToolStripMenuItem.Click += new System.EventHandler(this.haussToolStripMenuItem_Click);
            // 
            // medianToolStripMenuItem
            // 
            this.medianToolStripMenuItem.Name = "medianToolStripMenuItem";
            this.medianToolStripMenuItem.Size = new System.Drawing.Size(136, 24);
            this.medianToolStripMenuItem.Text = "Median";
            // 
            // resultPictureBox
            // 
            this.resultPictureBox.Location = new System.Drawing.Point(449, 33);
            this.resultPictureBox.Margin = new System.Windows.Forms.Padding(4);
            this.resultPictureBox.Name = "resultPictureBox";
            this.resultPictureBox.Size = new System.Drawing.Size(1264, 930);
            this.resultPictureBox.TabIndex = 2;
            this.resultPictureBox.TabStop = false;
            // 
            // thresholdInput
            // 
            this.thresholdInput.Location = new System.Drawing.Point(235, 568);
            this.thresholdInput.Margin = new System.Windows.Forms.Padding(4);
            this.thresholdInput.Name = "thresholdInput";
            this.thresholdInput.Size = new System.Drawing.Size(132, 22);
            this.thresholdInput.TabIndex = 3;
            this.thresholdInput.Text = "50";
            this.thresholdInput.TextChanged += new System.EventHandler(this.ThresholdInputTextChanged);
            // 
            // sourcePictureBox
            // 
            this.sourcePictureBox.Location = new System.Drawing.Point(16, 33);
            this.sourcePictureBox.Margin = new System.Windows.Forms.Padding(4);
            this.sourcePictureBox.Name = "sourcePictureBox";
            this.sourcePictureBox.Size = new System.Drawing.Size(404, 306);
            this.sourcePictureBox.TabIndex = 1;
            this.sourcePictureBox.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(48, 553);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 28);
            this.button1.TabIndex = 4;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // SobelCheckBox
            // 
            this.SobelCheckBox.AutoSize = true;
            this.SobelCheckBox.Location = new System.Drawing.Point(48, 429);
            this.SobelCheckBox.Name = "SobelCheckBox";
            this.SobelCheckBox.Size = new System.Drawing.Size(78, 21);
            this.SobelCheckBox.TabIndex = 5;
            this.SobelCheckBox.Text = "Собель";
            this.SobelCheckBox.UseVisualStyleBackColor = true;
            // 
            // GausCheckBox
            // 
            this.GausCheckBox.AutoSize = true;
            this.GausCheckBox.Enabled = false;
            this.GausCheckBox.Location = new System.Drawing.Point(48, 375);
            this.GausCheckBox.Name = "GausCheckBox";
            this.GausCheckBox.Size = new System.Drawing.Size(145, 21);
            this.GausCheckBox.TabIndex = 6;
            this.GausCheckBox.Text = "Размытие Гаусса";
            this.GausCheckBox.UseVisualStyleBackColor = true;
            // 
            // medianCheckBox
            // 
            this.medianCheckBox.AutoSize = true;
            this.medianCheckBox.Location = new System.Drawing.Point(48, 402);
            this.medianCheckBox.Name = "medianCheckBox";
            this.medianCheckBox.Size = new System.Drawing.Size(107, 21);
            this.medianCheckBox.TabIndex = 7;
            this.medianCheckBox.Text = "Медианный";
            this.medianCheckBox.UseVisualStyleBackColor = true;
            // 
            // medianNumeric
            // 
            this.medianNumeric.Location = new System.Drawing.Point(168, 401);
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
            this.medianNumeric.Size = new System.Drawing.Size(120, 22);
            this.medianNumeric.TabIndex = 8;
            this.medianNumeric.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // ImageRecognitor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1764, 912);
            this.Controls.Add(this.medianNumeric);
            this.Controls.Add(this.medianCheckBox);
            this.Controls.Add(this.GausCheckBox);
            this.Controls.Add(this.SobelCheckBox);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.thresholdInput);
            this.Controls.Add(this.resultPictureBox);
            this.Controls.Add(this.sourcePictureBox);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ImageRecognitor";
            this.Text = "ImageRecognitor";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.resultPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sourcePictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.medianNumeric)).EndInit();
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
    private System.Windows.Forms.CheckBox GausCheckBox;
    private System.Windows.Forms.CheckBox medianCheckBox;
    private System.Windows.Forms.NumericUpDown medianNumeric;
  }
}

