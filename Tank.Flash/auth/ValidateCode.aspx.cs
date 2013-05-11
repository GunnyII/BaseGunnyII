using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Bussiness;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace Tank.Flash.auth
{
    public partial class ValidateCode : System.Web.UI.Page
    {
        public static Color[] colors = new Color[] { Color.Blue, Color.DarkRed, Color.Green, Color.Gold };
        
        private void CreateCheckCodeImage(string checkCode)
        {
            if ((checkCode != null) && !(checkCode.Trim() == string.Empty))
            {
                Bitmap image = new Bitmap((int)Math.Ceiling((double)(checkCode.Length * 40.5)), 44);
                image.MakeTransparent();
                Graphics g = Graphics.FromImage(image);
                try
                {
                    Random random = new Random();
                    Color color = colors[random.Next(colors.Length)];
                    g.Clear(Color.Transparent);
                    for (int i = 0; i < 2; i++)
                    {
                        int x1 = random.Next(image.Width);
                        int x2 = random.Next(image.Width);
                        int y1 = random.Next(image.Height);
                        int y2 = random.Next(image.Height);
                        g.DrawArc(new Pen(color, 2f), -x1, -y1, image.Width * 2, image.Height, 45, 100);
                    }
                    Font font = new Font("Arial", 24f, FontStyle.Italic | FontStyle.Bold);
                    LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, image.Width, image.Height), color, color, 1.2f, true);
                    g.DrawString(checkCode, font, brush, (float)2f, (float)2f);
                    int angle = 40;
                    double sin = Math.Sin((3.1415926535897931 * angle) / 180.0);
                    double cos = Math.Cos((3.1415926535897931 * angle) / 180.0);
                    double tan = Math.Atan((3.1415926535897931 * angle) / 180.0);
                    int px = 0;
                    int py = 0;
                    if (angle > 0)
                    {
                        px = (int)(sin * 20.0);
                        py = (int)(-sin * image.Width);
                    }
                    else
                    {
                        py = (int)(-sin * 22.0);
                    }
                    new TextureBrush(image).RotateTransform(30f);
                    
                    image.Save(@"c:\1.png", ImageFormat.Png);
                    MemoryStream ms = new MemoryStream();
                    image.Save(ms, ImageFormat.Png);
                    Response.ClearContent();
                    Response.ContentType = "image/Png";
                    Response.BinaryWrite(ms.ToArray());
                }
                finally
                {
                    g.Dispose();
                    image.Dispose();
                }
            }
        }

        private string GenerateCheckCode()
        {
            string checkCode = string.Empty;
            Random random = new Random();
            for (int i = 0; i < 4; i++)
            {
                int number = random.Next();
                checkCode = checkCode + ((char)(65 + ((ushort)(number % 26)))).ToString();
            }
            return checkCode;
        }

        public static Bitmap KiRotate(Bitmap bmp, float angle, Color bkColor)
        {
            PixelFormat pf;
            int w = bmp.Width + 2;
            int h = bmp.Height + 2;
            if (bkColor == Color.Transparent)
            {
                pf = PixelFormat.Format32bppArgb;
            }
            else
            {
                pf = bmp.PixelFormat;
            }
            Bitmap tmp = new Bitmap(w, h, pf);
            Graphics g = Graphics.FromImage(tmp);
            g.Clear(bkColor);
            g.DrawImageUnscaled(bmp, 1, 1);
            g.Dispose();
            GraphicsPath path = new GraphicsPath();
            path.AddRectangle(new RectangleF(0f, 0f, (float)w, (float)h));
            Matrix mtrx = new Matrix();
            mtrx.Rotate(angle);
            RectangleF rct = path.GetBounds(mtrx);
            Bitmap dst = new Bitmap((int)rct.Width, (int)rct.Height, pf);
            g = Graphics.FromImage(dst);
            g.Clear(bkColor);
            g.TranslateTransform(-rct.X, -rct.Y);
            g.RotateTransform(angle);
            g.InterpolationMode = InterpolationMode.HighQualityBilinear;
            g.DrawImageUnscaled(tmp, 0, 0);
            g.Dispose();
            tmp.Dispose();
            return dst;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string code = CheckCode.GenerateCheckCode();
            byte[] bytes = CheckCode.CreateImage(code);
            //byte[] bytes = CheckCode.CreateVane("2.7");
            Session["CheckCode"] = code;
            Response.ClearContent();
            Response.ContentType = "image/Gif";
            Response.BinaryWrite(bytes);
        }
    }
}