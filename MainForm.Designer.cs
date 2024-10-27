namespace Paint
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            menuStrip1 = new MenuStrip();
            MenuFile = new ToolStripMenuItem();
            editToolStripMenuItem = new ToolStripMenuItem();
            canvasSizeToolStripMenuItem = new ToolStripMenuItem();
            MainToolBar = new ToolStrip();
            StatusBar = new StatusStrip();
            pbCanvas = new PictureBox();
            pnlColorPalete = new Panel();
            UserPalete = new Controls.ColorPalete();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbCanvas).BeginInit();
            pnlColorPalete.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { MenuFile, editToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(800, 28);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // MenuFile
            // 
            MenuFile.Name = "MenuFile";
            MenuFile.Size = new Size(46, 24);
            MenuFile.Text = "File";
            // 
            // editToolStripMenuItem
            // 
            editToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { canvasSizeToolStripMenuItem });
            editToolStripMenuItem.Name = "editToolStripMenuItem";
            editToolStripMenuItem.Size = new Size(49, 24);
            editToolStripMenuItem.Text = "Edit";
            // 
            // canvasSizeToolStripMenuItem
            // 
            canvasSizeToolStripMenuItem.Name = "canvasSizeToolStripMenuItem";
            canvasSizeToolStripMenuItem.Size = new Size(169, 26);
            canvasSizeToolStripMenuItem.Text = "Canvas Size";
            // 
            // MainToolBar
            // 
            MainToolBar.ImageScalingSize = new Size(20, 20);
            MainToolBar.Location = new Point(0, 28);
            MainToolBar.Name = "MainToolBar";
            MainToolBar.Size = new Size(800, 25);
            MainToolBar.TabIndex = 1;
            MainToolBar.Text = "toolStrip1";
            // 
            // StatusBar
            // 
            StatusBar.ImageScalingSize = new Size(20, 20);
            StatusBar.Location = new Point(0, 426);
            StatusBar.Name = "StatusBar";
            StatusBar.Size = new Size(800, 24);
            StatusBar.TabIndex = 2;
            StatusBar.Text = "statusStrip1";
            // 
            // pbCanvas
            // 
            pbCanvas.BackColor = Color.FromArgb(203, 213, 228);
            pbCanvas.Dock = DockStyle.Fill;
            pbCanvas.Location = new Point(0, 53);
            pbCanvas.Name = "pbCanvas";
            pbCanvas.Size = new Size(712, 373);
            pbCanvas.TabIndex = 3;
            pbCanvas.TabStop = false;
            pbCanvas.Paint += pbCanvas_Paint;
            pbCanvas.MouseDown += pbCanvas_MouseDown;
            pbCanvas.MouseHover += pbCanvas_MouseHover;
            pbCanvas.MouseMove += pbCanvas_MouseMove;
            pbCanvas.MouseUp += pbCanvas_MouseUp;
            // 
            // pnlColorPalete
            // 
            pnlColorPalete.Controls.Add(UserPalete);
            pnlColorPalete.Dock = DockStyle.Right;
            pnlColorPalete.Location = new Point(712, 53);
            pnlColorPalete.Name = "pnlColorPalete";
            pnlColorPalete.Size = new Size(88, 373);
            pnlColorPalete.TabIndex = 4;
            // 
            // UserPalete
            // 
            UserPalete.ItemCount = 30;
            UserPalete.Location = new Point(6, 3);
            UserPalete.Name = "UserPalete";
            UserPalete.Size = new Size(76, 369);
            UserPalete.TabIndex = 0;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(pbCanvas);
            Controls.Add(pnlColorPalete);
            Controls.Add(StatusBar);
            Controls.Add(MainToolBar);
            Controls.Add(menuStrip1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip1;
            Name = "MainForm";
            Text = "Paint";
            WindowState = FormWindowState.Maximized;
            FormClosing += MainForm_FormClosing;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pbCanvas).EndInit();
            pnlColorPalete.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem MenuFile;
        private ToolStrip MainToolBar;
        private StatusStrip StatusBar;
        private PictureBox pbCanvas;
        private Panel pnlColorPalete;
        private Controls.ColorPalete UserPalete;
        private ToolStripMenuItem editToolStripMenuItem;
        private ToolStripMenuItem canvasSizeToolStripMenuItem;
    }
}
