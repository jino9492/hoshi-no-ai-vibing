using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;


namespace hoshi_no_ai_vibing
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x80;
                return cp;
            }
        }

        private void InitializeComponent()
        {
            string gifPath = Application.StartupPath + "/Resources/ai.gif";

            PictureBox pictureBox = new PictureBox();
            pictureBox.Location = new System.Drawing.Point(50, 50);
            pictureBox.Size = new System.Drawing.Size(280, 266);

            Image gifImage = Image.FromFile(gifPath);

            pictureBox.Image = gifImage;

            ImageAnimator.Animate(gifImage, (sender, e) =>
            {
                pictureBox.Invalidate();
            });

            pictureBox.MouseDown += (sender, e) => ImageMouseDown(sender, e);
            pictureBox.MouseUp += (sender, e) => ImageMouseUp(sender, e);
            pictureBox.MouseMove += (sender, e) => ImageMouseMove(sender, e);

            this.Controls.Add(pictureBox);

            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Text = "";

            this.BackColor = Color.Magenta;
            this.TransparencyKey = Color.Magenta;
            this.FormBorderStyle = FormBorderStyle.None;
            this.TopMost = true;
            this.ShowInTaskbar = false;
        }

        #region Drag Functions
        private bool isDragging = false;
        private Point lastLocation;
        private void ImageMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                lastLocation = e.Location;
            }
        }

        private void ImageMouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                this.Location = new Point(
                    (this.Location.X - lastLocation.X) + e.X,
                    (this.Location.Y - lastLocation.Y) + e.Y);

                this.Update();
            }
        }

        private void ImageMouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Application.Exit();
            }

            isDragging = false;
        }
        #endregion
    }
}

