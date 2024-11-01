namespace MyPaint.Dialogs
{
    partial class ResizeForm
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
            groupBox = new GroupBox();
            chkRatio = new CheckBox();
            rbPixel = new RadioButton();
            rbPercent = new RadioButton();
            txtHeight = new TextBox();
            txtWidth = new TextBox();
            lblHeight = new Label();
            lblWidth = new Label();
            btnSave = new Button();
            btnCancel = new Button();
            groupBox.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox
            // 
            groupBox.Controls.Add(chkRatio);
            groupBox.Controls.Add(rbPixel);
            groupBox.Controls.Add(rbPercent);
            groupBox.Controls.Add(txtHeight);
            groupBox.Controls.Add(txtWidth);
            groupBox.Controls.Add(lblHeight);
            groupBox.Controls.Add(lblWidth);
            groupBox.Location = new Point(12, 12);
            groupBox.Name = "groupBox";
            groupBox.Size = new Size(326, 181);
            groupBox.TabIndex = 0;
            groupBox.TabStop = false;
            groupBox.Text = "სურათის ზომა";
            // 
            // chkRatio
            // 
            chkRatio.AutoSize = true;
            chkRatio.Checked = true;
            chkRatio.CheckState = CheckState.Checked;
            chkRatio.Location = new Point(15, 144);
            chkRatio.Name = "chkRatio";
            chkRatio.Size = new Size(245, 24);
            chkRatio.TabIndex = 6;
            chkRatio.Text = "პროპორციების შენარჩუნება";
            chkRatio.UseVisualStyleBackColor = true;
            // 
            // rbPixel
            // 
            rbPixel.AutoSize = true;
            rbPixel.Checked = true;
            rbPixel.Location = new Point(181, 26);
            rbPixel.Name = "rbPixel";
            rbPixel.Size = new Size(126, 24);
            rbPixel.TabIndex = 5;
            rbPixel.TabStop = true;
            rbPixel.Text = "პიქსელებით";
            rbPixel.UseVisualStyleBackColor = true;
            rbPixel.CheckedChanged += rbPixel_CheckedChanged;
            // 
            // rbPercent
            // 
            rbPercent.AutoSize = true;
            rbPercent.Location = new Point(15, 26);
            rbPercent.Name = "rbPercent";
            rbPercent.Size = new Size(139, 24);
            rbPercent.TabIndex = 4;
            rbPercent.TabStop = true;
            rbPercent.Text = "პროცენტობით";
            rbPercent.UseVisualStyleBackColor = true;
            rbPercent.CheckedChanged += rbPercent_CheckedChanged;
            // 
            // txtHeight
            // 
            txtHeight.Location = new Point(116, 98);
            txtHeight.Name = "txtHeight";
            txtHeight.Size = new Size(191, 27);
            txtHeight.TabIndex = 3;
            txtHeight.TextAlign = HorizontalAlignment.Center;
            txtHeight.TextChanged += txtHeight_TextChanged;
            // 
            // txtWidth
            // 
            txtWidth.Location = new Point(116, 65);
            txtWidth.Name = "txtWidth";
            txtWidth.Size = new Size(191, 27);
            txtWidth.TabIndex = 2;
            txtWidth.TextAlign = HorizontalAlignment.Center;
            txtWidth.TextChanged += txtWidth_TextChanged;
            // 
            // lblHeight
            // 
            lblHeight.AutoSize = true;
            lblHeight.Location = new Point(15, 101);
            lblHeight.Name = "lblHeight";
            lblHeight.Size = new Size(81, 20);
            lblHeight.TabIndex = 1;
            lblHeight.Text = "სიმაღლე:";
            // 
            // lblWidth
            // 
            lblWidth.AutoSize = true;
            lblWidth.Location = new Point(15, 68);
            lblWidth.Name = "lblWidth";
            lblWidth.Size = new Size(62, 20);
            lblWidth.TabIndex = 0;
            lblWidth.Text = "სიგანე:";
            // 
            // btnSave
            // 
            btnSave.Location = new Point(145, 199);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(94, 29);
            btnSave.TabIndex = 1;
            btnSave.Text = "შენახვა";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(245, 199);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(94, 29);
            btnCancel.TabIndex = 2;
            btnCancel.Text = "გაუქმება";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // ResizeForm
            // 
            AcceptButton = btnSave;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancel;
            ClientSize = new Size(351, 240);
            Controls.Add(btnCancel);
            Controls.Add(btnSave);
            Controls.Add(groupBox);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ResizeForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "ზომის შეცვლა";
            groupBox.ResumeLayout(false);
            groupBox.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox;
        private RadioButton rbPixel;
        private RadioButton rbPercent;
        private TextBox txtHeight;
        private TextBox txtWidth;
        private Label lblHeight;
        private Label lblWidth;
        private CheckBox chkRatio;
        private Button btnSave;
        private Button btnCancel;
    }
}