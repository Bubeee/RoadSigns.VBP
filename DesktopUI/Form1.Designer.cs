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
            this.resultPictureBox = new System.Windows.Forms.PictureBox();
            this.thresholdInput = new System.Windows.Forms.TextBox();
            this.sourcePictureBox = new System.Windows.Forms.PictureBox();
            this.filterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.haussToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.medianToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.button1 = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.resultPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sourcePictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.filterToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1306, 24);
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
            this.thresholdInput.Location = new System.Drawing.Point(109, 291);
            this.thresholdInput.Name = "thresholdInput";
            this.thresholdInput.Size = new System.Drawing.Size(100, 20);
            this.thresholdInput.TabIndex = 3;
            this.thresholdInput.Text = "50";
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
            this.haussToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.haussToolStripMenuItem.Text = "Gaussian";
            this.haussToolStripMenuItem.Click += new System.EventHandler(this.haussToolStripMenuItem_Click);
            // 
            // medianToolStripMenuItem
            // 
            this.medianToolStripMenuItem.Name = "medianToolStripMenuItem";
            this.medianToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.medianToolStripMenuItem.Text = "Median";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(85, 347);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ImageRecognitor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1323, 741);
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
  }
}

