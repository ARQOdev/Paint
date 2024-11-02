using System.Reflection;

namespace MyPaint
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
            menuNewFile = new ToolStripMenuItem();
            menuOpen = new ToolStripMenuItem();
            menuSave = new ToolStripMenuItem();
            menuSaveAs = new ToolStripMenuItem();
            menuRecent = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            menuClose = new ToolStripMenuItem();
            editToolStripMenuItem = new ToolStripMenuItem();
            menuResize = new ToolStripMenuItem();
            MainToolBar = new ToolStrip();
            btnNew = new ToolStripButton();
            btnOpen = new ToolStripButton();
            btnSave = new ToolStripButton();
            StatusBar = new StatusStrip();
            pbCanvas = new PictureBox();
            pnlColorPalete = new Panel();
            UserPalete = new Controls.ColorPalete();
            panel1 = new Panel();
            LeftToolBar = new Controls.ToolBar();
            menuStrip1.SuspendLayout();
            MainToolBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbCanvas).BeginInit();
            pnlColorPalete.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { MenuFile, editToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(971, 28);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // MenuFile
            // 
            MenuFile.DropDownItems.AddRange(new ToolStripItem[] { menuNewFile, menuOpen, menuSave, menuSaveAs, menuRecent, toolStripSeparator1, menuClose });
            MenuFile.Name = "MenuFile";
            MenuFile.Size = new Size(77, 24);
            MenuFile.Text = "ფაილი";
            // 
            // menuNewFile
            // 
            menuNewFile.Image = Properties.Resources.newDoc_24x24;
            menuNewFile.Name = "menuNewFile";
            menuNewFile.ShortcutKeys = Keys.Control | Keys.N;
            menuNewFile.Size = new Size(308, 26);
            menuNewFile.Text = "ახალი ფაილი";
            menuNewFile.Click += menuNewFile_Click;
            // 
            // menuOpen
            // 
            menuOpen.Image = Properties.Resources.openFolder_24x24;
            menuOpen.Name = "menuOpen";
            menuOpen.ShortcutKeys = Keys.Control | Keys.O;
            menuOpen.Size = new Size(308, 26);
            menuOpen.Text = "გახსნა";
            menuOpen.Click += menuOpen_Click;
            // 
            // menuSave
            // 
            menuSave.Image = Properties.Resources.saveFile_24x24;
            menuSave.Name = "menuSave";
            menuSave.ShortcutKeys = Keys.Control | Keys.S;
            menuSave.Size = new Size(308, 26);
            menuSave.Text = "შენახვა";
            menuSave.Click += menuSave_Click;
            // 
            // menuSaveAs
            // 
            menuSaveAs.Name = "menuSaveAs";
            menuSaveAs.ShortcutKeys = Keys.Control | Keys.Shift | Keys.S;
            menuSaveAs.Size = new Size(308, 26);
            menuSaveAs.Text = "ფაილში შენახვა";
            menuSaveAs.Click += menuSaveAs_Click;
            // 
            // menuRecent
            // 
            menuRecent.Name = "menuRecent";
            menuRecent.Size = new Size(308, 26);
            menuRecent.Text = "ბოლო ფაილები";
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(305, 6);
            // 
            // menuClose
            // 
            menuClose.Name = "menuClose";
            menuClose.Size = new Size(308, 26);
            menuClose.Text = "დახურვა";
            menuClose.Click += menuClose_Click;
            // 
            // editToolStripMenuItem
            // 
            editToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { menuResize });
            editToolStripMenuItem.Name = "editToolStripMenuItem";
            editToolStripMenuItem.Size = new Size(78, 24);
            editToolStripMenuItem.Text = "მართვა";
            // 
            // menuResize
            // 
            menuResize.Name = "menuResize";
            menuResize.Size = new Size(203, 26);
            menuResize.Text = "სურათის ზომა";
            menuResize.Click += menuResize_Click;
            // 
            // MainToolBar
            // 
            MainToolBar.ImageScalingSize = new Size(20, 20);
            MainToolBar.Items.AddRange(new ToolStripItem[] { btnNew, btnOpen, btnSave });
            MainToolBar.Location = new Point(0, 28);
            MainToolBar.Name = "MainToolBar";
            MainToolBar.Size = new Size(971, 27);
            MainToolBar.TabIndex = 1;
            MainToolBar.Text = "toolStrip1";
            // 
            // btnNew
            // 
            btnNew.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnNew.Image = Properties.Resources.newDoc_24x24;
            btnNew.ImageTransparentColor = Color.Magenta;
            btnNew.Name = "btnNew";
            btnNew.Size = new Size(29, 24);
            btnNew.Text = "toolStripButton1";
            btnNew.Click += btnNew_Click;
            // 
            // btnOpen
            // 
            btnOpen.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnOpen.Image = Properties.Resources.openFolder_24x24;
            btnOpen.ImageTransparentColor = Color.Magenta;
            btnOpen.Name = "btnOpen";
            btnOpen.Size = new Size(29, 24);
            btnOpen.Text = "toolStripButton2";
            btnOpen.Click += btnOpen_Click;
            // 
            // btnSave
            // 
            btnSave.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnSave.Image = Properties.Resources.saveFile_24x24;
            btnSave.ImageTransparentColor = Color.Magenta;
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(29, 24);
            btnSave.Text = "toolStripButton3";
            btnSave.Click += btnSave_Click;
            // 
            // StatusBar
            // 
            StatusBar.ImageScalingSize = new Size(20, 20);
            StatusBar.Location = new Point(0, 483);
            StatusBar.Name = "StatusBar";
            StatusBar.Size = new Size(971, 22);
            StatusBar.TabIndex = 2;
            StatusBar.Text = "statusStrip1";
            // 
            // pbCanvas
            // 
            pbCanvas.BackColor = Color.FromArgb(203, 213, 228);
            pbCanvas.Dock = DockStyle.Fill;
            pbCanvas.Location = new Point(34, 55);
            pbCanvas.Name = "pbCanvas";
            pbCanvas.Size = new Size(849, 428);
            pbCanvas.TabIndex = 3;
            pbCanvas.TabStop = false;
            pbCanvas.Paint += pbCanvas_Paint;
            pbCanvas.MouseDown += pbCanvas_MouseDown;
            pbCanvas.MouseMove += pbCanvas_MouseMove;
            pbCanvas.MouseUp += pbCanvas_MouseUp;
            // 
            // pnlColorPalete
            // 
            pnlColorPalete.Controls.Add(UserPalete);
            pnlColorPalete.Dock = DockStyle.Right;
            pnlColorPalete.Location = new Point(883, 55);
            pnlColorPalete.Name = "pnlColorPalete";
            pnlColorPalete.Size = new Size(88, 428);
            pnlColorPalete.TabIndex = 4;
            // 
            // UserPalete
            // 
            UserPalete.ItemCount = 30;
            UserPalete.Location = new Point(6, 3);
            UserPalete.Name = "UserPalete";
            UserPalete.Size = new Size(76, 359);
            UserPalete.TabIndex = 0;
            // 
            // panel1
            // 
            panel1.Controls.Add(LeftToolBar);
            panel1.Dock = DockStyle.Left;
            panel1.Location = new Point(0, 55);
            panel1.Name = "panel1";
            panel1.Size = new Size(34, 428);
            panel1.TabIndex = 5;
            // 
            // LeftToolBar
            // 
            LeftToolBar.Dock = DockStyle.Fill;
            LeftToolBar.Location = new Point(0, 0);
            LeftToolBar.Name = "LeftToolBar";
            LeftToolBar.Size = new Size(34, 428);
            LeftToolBar.TabIndex = 0;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(971, 505);
            Controls.Add(pbCanvas);
            Controls.Add(panel1);
            Controls.Add(pnlColorPalete);
            Controls.Add(StatusBar);
            Controls.Add(MainToolBar);
            Controls.Add(menuStrip1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            KeyPreview = true;
            MainMenuStrip = menuStrip1;
            Name = "MainForm";
            Text = "Paint";
            WindowState = FormWindowState.Maximized;
            FormClosing += MainForm_FormClosing;
            KeyDown += MainForm_KeyDown;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            MainToolBar.ResumeLayout(false);
            MainToolBar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pbCanvas).EndInit();
            pnlColorPalete.ResumeLayout(false);
            panel1.ResumeLayout(false);
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
        private ToolStripMenuItem menuResize;
        private ToolStripMenuItem menuOpen;
        private ToolStripMenuItem menuSave;
        private ToolStripMenuItem menuRecent;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem menuClose;
        private ToolStripMenuItem menuSaveAs;
        private ToolStripButton btnNew;
        private ToolStripButton btnOpen;
        private ToolStripButton btnSave;
        private ToolStripMenuItem menuNewFile;
        private Panel panel1;
        private Controls.ToolBar LeftToolBar;
    }
}
