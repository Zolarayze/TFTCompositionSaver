using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace TFT_CompositionSaver.Helper
{
    public class ApiDataHelper
    {
        public static Image CreateTextImage(string name)
        {
            Image bmp = new Bitmap (64, 64, PixelFormat.Format32bppArgb);
            
            Graphics g = Graphics.FromImage(bmp);
            g.Clear(Color.Black);
            g.DrawString(name, new Font("Arial", 10, FontStyle.Regular), Brushes.Aqua, new RectangleF(2, 2, 62, 62));

            return bmp;
        }

        public static void CreateFolder(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
    }
}
