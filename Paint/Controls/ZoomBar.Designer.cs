namespace MyPaint.Controls
{
    partial class ZoomBar
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblValue = new Label();
            pnlZoomOut = new Panel();
            pnlZoomIn = new Panel();
            pnlSlider = new Panel();
            SuspendLayout();
            // 
            // lblValue
            // 
            lblValue.Dock = DockStyle.Left;
            lblValue.Location = new Point(0, 0);
            lblValue.Name = "lblValue";
            lblValue.Size = new Size(50, 31);
            lblValue.TabIndex = 0;
            lblValue.Text = "100%";
            lblValue.TextAlign = ContentAlignment.MiddleRight;
            // 
            // pnlZoomOut
            // 
            pnlZoomOut.Dock = DockStyle.Left;
            pnlZoomOut.Location = new Point(50, 0);
            pnlZoomOut.Name = "pnlZoomOut";
            pnlZoomOut.Size = new Size(31, 31);
            pnlZoomOut.TabIndex = 1;
            // 
            // pnlZoomIn
            // 
            pnlZoomIn.Dock = DockStyle.Right;
            pnlZoomIn.Location = new Point(246, 0);
            pnlZoomIn.Name = "pnlZoomIn";
            pnlZoomIn.Size = new Size(31, 31);
            pnlZoomIn.TabIndex = 2;
            // 
            // pnlSlider
            // 
            pnlSlider.Dock = DockStyle.Fill;
            pnlSlider.Location = new Point(81, 0);
            pnlSlider.Name = "pnlSlider";
            pnlSlider.Size = new Size(165, 31);
            pnlSlider.TabIndex = 3;
            pnlSlider.Paint += pnlSlider_Paint;
            pnlSlider.MouseDown += pnlSlider_MouseDown;
            pnlSlider.MouseMove += pnlSlider_MouseMove;
            pnlSlider.MouseUp += pnlSlider_MouseUp;
            pnlSlider.Resize += pnlSlider_Resize;
            // 
            // ZoomBar
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(pnlSlider);
            Controls.Add(pnlZoomIn);
            Controls.Add(pnlZoomOut);
            Controls.Add(lblValue);
            Name = "ZoomBar";
            Size = new Size(277, 31);
            ResumeLayout(false);
        }

        #endregion

        private Label lblValue;
        private Panel pnlZoomOut;
        private Panel pnlZoomIn;
        private Panel pnlSlider;
    }
}
