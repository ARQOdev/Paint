using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Paint.Dialogs
{
    public partial class ResizeForm : Form
    {
        private Size ImageSize { get; set; }
        private int MaxSize = 10000;
        private int MinSize = 1;

        public ResizeForm()
        {
            InitializeComponent();
            txtWidth.KeyPress += TextBox_KeyPress;
            txtHeight.KeyPress += TextBox_KeyPress;
        }

        private void TextBox_KeyPress(object? sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        public ResizeForm(Size image_size) : this()
        {
            ImageSize = image_size;
            txtWidth.Text = image_size.Width.ToString();
            txtHeight.Text = image_size.Height.ToString();
        }

        private void rbPixel_CheckedChanged(object sender, EventArgs e)
        {
            if (rbPixel.Checked == false)
                return;

            txtWidth.Text = ImageSize.Width.ToString();
            txtHeight.Text = ImageSize.Height.ToString();
        }

        private void rbPercent_CheckedChanged(object sender, EventArgs e)
        {
            if (rbPercent.Checked == false)
                return;

            txtWidth.Text = "100";
            txtHeight.Text = "100";
        }

        private void txtWidth_TextChanged(object sender, EventArgs e)
        {
            if (!chkRatio.Checked)
                return;

            if (!txtWidth.Focused)
                return;

            if (rbPercent.Checked)
            {
                txtHeight.Text = txtWidth.Text;
                return;
            }

            if (txtWidth.Text == "0")
            {
                txtHeight.Text = "0";
                return;
            }

            int w = 0;
            if (int.TryParse(txtWidth.Text, out w))
            {
                int h = w * ImageSize.Height / ImageSize.Width;
                txtHeight.Text = h.ToString();
            }
            else
            {
                txtHeight.Text = "0";
            }
        }

        private void txtHeight_TextChanged(object sender, EventArgs e)
        {
            if (!chkRatio.Checked)
                return;

            if (!txtHeight.Focused)
                return;

            if (rbPercent.Checked)
            {
                txtWidth.Text = txtHeight.Text;
                return;
            }

            if (txtHeight.Text == "0")
            {
                txtWidth.Text = "0";
                return;
            }

            int h = 0;
            if (int.TryParse(txtHeight.Text, out h))
            {
                int w = h * ImageSize.Width / ImageSize.Height;
                txtWidth.Text = w.ToString();
            }
            else
            {
                txtWidth.Text = "0";
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int w = 0, h = 0;
            int.TryParse(txtWidth.Text, out w);
            int.TryParse(txtHeight.Text, out h);

            if (w < MinSize || w > MaxSize || h < MinSize || h > MaxSize)
            {
                MessageBox.Show(string.Format("გთხოვთ მიუთითოთ რიცხვები {0}-დან {1}-მდე დიაპაზონში!", MinSize, MaxSize), "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        public Size GetNewSize()
        {
            int w = 0, h = 0;
            int.TryParse(txtWidth.Text, out w);
            int.TryParse(txtHeight.Text, out h);

            if (rbPercent.Checked)
            {
                w = ImageSize.Width * w / 100;
                h = ImageSize.Height * h / 100;
            }

            return new Size(w, h);
        }
    }
}
