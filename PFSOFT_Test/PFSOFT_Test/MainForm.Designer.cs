namespace PFSOFT_Test
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
            this.menuStripMain = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMain = new System.Windows.Forms.ToolStrip();
            this.toolStripCurve = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLine = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripRectangle = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripCircle = new System.Windows.Forms.ToolStripButton();
            this.comboBoxThickness = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.butColor = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.butBackColor = new System.Windows.Forms.Button();
            this.checkBoxTransparent = new System.Windows.Forms.CheckBox();
            this.butInvert = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.menuStripMain.SuspendLayout();
            this.toolStripMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStripMain
            // 
            this.menuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStripMain.Location = new System.Drawing.Point(0, 0);
            this.menuStripMain.Name = "menuStripMain";
            this.menuStripMain.Size = new System.Drawing.Size(784, 24);
            this.menuStripMain.TabIndex = 1;
            this.menuStripMain.Text = "menuStrip2";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.closeToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.closeToolStripMenuItem.Text = "Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // toolStripMain
            // 
            this.toolStripMain.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripCurve,
            this.toolStripSeparator1,
            this.toolStripLine,
            this.toolStripSeparator2,
            this.toolStripRectangle,
            this.toolStripSeparator3,
            this.toolStripCircle});
            this.toolStripMain.Location = new System.Drawing.Point(9, 24);
            this.toolStripMain.Name = "toolStripMain";
            this.toolStripMain.Size = new System.Drawing.Size(122, 25);
            this.toolStripMain.TabIndex = 2;
            this.toolStripMain.Text = "toolStrip1";
            // 
            // toolStripPen
            // 
            this.toolStripCurve.AutoToolTip = false;
            this.toolStripCurve.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripCurve.Image = global::PFSOFT_Test.Properties.Resources.curve;
            this.toolStripCurve.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripCurve.Name = "toolStripPen";
            this.toolStripCurve.Size = new System.Drawing.Size(23, 22);
            this.toolStripCurve.Text = "Pencil";
            this.toolStripCurve.ToolTipText = "Pen";
            this.toolStripCurve.Click += new System.EventHandler(this.toolStripCurve_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLine
            // 
            this.toolStripLine.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripLine.Image = global::PFSOFT_Test.Properties.Resources.line;
            this.toolStripLine.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripLine.Name = "toolStripLine";
            this.toolStripLine.Size = new System.Drawing.Size(23, 22);
            this.toolStripLine.Text = "Line";
            this.toolStripLine.Click += new System.EventHandler(this.toolStripLine_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripRectangle
            // 
            this.toolStripRectangle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripRectangle.Image = global::PFSOFT_Test.Properties.Resources.rect;
            this.toolStripRectangle.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripRectangle.Name = "toolStripRectangle";
            this.toolStripRectangle.Size = new System.Drawing.Size(23, 22);
            this.toolStripRectangle.Text = "toolStripButton3";
            this.toolStripRectangle.ToolTipText = "Rectangle";
            this.toolStripRectangle.Click += new System.EventHandler(this.toolStripRectangle_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripCircle
            // 
            this.toolStripCircle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripCircle.Image = global::PFSOFT_Test.Properties.Resources.circle;
            this.toolStripCircle.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripCircle.Name = "toolStripCircle";
            this.toolStripCircle.Size = new System.Drawing.Size(23, 22);
            this.toolStripCircle.Text = "toolStripButton4";
            this.toolStripCircle.ToolTipText = "Circle";
            this.toolStripCircle.Click += new System.EventHandler(this.toolStripCircle_Click);
            // 
            // comboBoxThickness
            // 
            this.comboBoxThickness.FormattingEnabled = true;
            this.comboBoxThickness.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "6",
            "8",
            "10",
            "12",
            "14",
            "16",
            "18",
            "20",
            "22",
            "24"});
            this.comboBoxThickness.Location = new System.Drawing.Point(220, 26);
            this.comboBoxThickness.Name = "comboBoxThickness";
            this.comboBoxThickness.Size = new System.Drawing.Size(47, 21);
            this.comboBoxThickness.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(154, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Thickness:";
            // 
            // butColor
            // 
            this.butColor.Location = new System.Drawing.Point(330, 19);
            this.butColor.Name = "butColor";
            this.butColor.Size = new System.Drawing.Size(40, 30);
            this.butColor.TabIndex = 5;
            this.butColor.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(293, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Color:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.SystemColors.Control;
            this.label3.Location = new System.Drawing.Point(400, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Background color:";
            // 
            // butBackColor
            // 
            this.butBackColor.BackColor = System.Drawing.SystemColors.Control;
            this.butBackColor.Location = new System.Drawing.Point(497, 19);
            this.butBackColor.Name = "butBackColor";
            this.butBackColor.Size = new System.Drawing.Size(40, 30);
            this.butBackColor.TabIndex = 8;
            this.butBackColor.UseVisualStyleBackColor = false;
            // 
            // checkBoxTransparent
            // 
            this.checkBoxTransparent.AutoSize = true;
            this.checkBoxTransparent.Checked = true;
            this.checkBoxTransparent.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxTransparent.Location = new System.Drawing.Point(556, 28);
            this.checkBoxTransparent.Name = "checkBoxTransparent";
            this.checkBoxTransparent.Size = new System.Drawing.Size(83, 17);
            this.checkBoxTransparent.TabIndex = 9;
            this.checkBoxTransparent.Text = "Transparent";
            this.checkBoxTransparent.UseVisualStyleBackColor = true;
            // 
            // butInvert
            // 
            this.butInvert.Location = new System.Drawing.Point(656, 24);
            this.butInvert.Name = "butInvert";
            this.butInvert.Size = new System.Drawing.Size(110, 23);
            this.butInvert.TabIndex = 10;
            this.butInvert.Text = "Invert";
            this.butInvert.UseVisualStyleBackColor = true;
            this.butInvert.Click += new System.EventHandler(this.butInvert_Click);
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.Location = new System.Drawing.Point(12, 49);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(760, 10);
            this.progressBar.TabIndex = 11;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 612);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.butInvert);
            this.Controls.Add(this.checkBoxTransparent);
            this.Controls.Add(this.butBackColor);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.butColor);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBoxThickness);
            this.Controls.Add(this.toolStripMain);
            this.Controls.Add(this.menuStripMain);
            this.MinimumSize = new System.Drawing.Size(800, 200);
            this.Name = "MainForm";
            this.Text = "Super Paint";
            this.menuStripMain.ResumeLayout(false);
            this.menuStripMain.PerformLayout();
            this.toolStripMain.ResumeLayout(false);
            this.toolStripMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStripMain;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStripMain;
        private System.Windows.Forms.ToolStripButton toolStripCurve;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripLine;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolStripRectangle;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton toolStripCircle;
        private System.Windows.Forms.ComboBox comboBoxThickness;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button butColor;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button butBackColor;
        private System.Windows.Forms.CheckBox checkBoxTransparent;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.Button butInvert;
        private System.Windows.Forms.ProgressBar progressBar;

    }
}

