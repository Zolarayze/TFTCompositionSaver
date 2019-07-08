using System.Drawing;
using System.Windows.Forms;

namespace TFT_CompositionSaver.Views.Forms
{
    public partial class Loading : Form
    {
        public Loading()
        {
            InitializeComponent();

            StringFormat sf = new StringFormat();
            
            sf.Alignment = StringAlignment.Center;

            Bitmap bmp = new Bitmap(351, 168);
            Graphics g = Graphics.FromImage(bmp);
            g.FillRectangle(new SolidBrush(Color.FromArgb(2, 15, 23)), 4, 4, bmp.Width-8, bmp.Height-8);
            g.DrawRectangle(new Pen(Brushes.White, 2), 4, 4, bmp.Width-8, bmp.Height-8);
            g.DrawString("Teamfight Tactics\nComposition saver", new Font("Arial", 20), Brushes.White, new RectangleF(4, 20, bmp.Width-8, bmp.Height-20), sf);
            sf.LineAlignment = StringAlignment.Far;
            sf.Alignment = StringAlignment.Far;
            g.DrawString("V1.0", new Font("Arial", 8), Brushes.White, new RectangleF(0, 0, bmp.Width - 10, bmp.Height - 10), sf);

            sf.Alignment = StringAlignment.Center;
            g.DrawString("Loading...", new Font("Arial", 16), Brushes.White, new RectangleF(4, 4, bmp.Width-8, bmp.Height-32), sf);

            this.pictureBox1.Image = bmp;
        }
    }
}
