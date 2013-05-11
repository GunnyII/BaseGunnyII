using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Bussiness
{
    public class CheckCode
    {
        #region new 

        #region property

        public static ThreadSafeRandom rand = new ThreadSafeRandom();

        //定义颜色
        private static Color[] c = { Color.BlueViolet, Color.Red, Color.DarkBlue, Color.Green, Color.Orange, Color.Brown, Color.DarkCyan, Color.Purple };
        //定义字体
        private static string[] font = { "Verdana", "Terminal", "Comic Sans MS", "Arial", "Tekton Pro" };

         //以数组方式候选字符，可以更方便的剔除不要的字符，如数字 0 与字母 o
        private static char[] digitals = new char[] { '1', '2', '3', '4', '5', '6', '7', '8', '9' };

        private static char[] lowerLetters = new char[] {
                'a', 'b', 'c', 'd', 'e', 'f',  
                'h', 'k', 'm', 'n', 
                 'p', 'q', 'r', 's', 't', 
                'u', 'v', 'w', 'x', 'y', 'z' };

        private static char[] upperLetters = new char[] {
                'A', 'B', 'C', 'D', 'E', 'F', 'G', 
                'H', 'K', 'M', 'N', 
                'P', 'Q', 'R', 'S', 'T', 
                'U', 'V', 'W', 'X', 'Y', 'Z' };

        private static char[] letters = new char[]{
                'a', 'b', 'c', 'd', 'e', 'f', 'g', 
                'h', 'i', 'j', 'k', 'l', 'm', 'n', 
                 'p', 'q', 'r', 's', 't', 
                'u', 'v', 'w', 'x', 'y', 'z',
                'A', 'B', 'C', 'D', 'E', 'F', 'G', 
                'H', 'I', 'J', 'K', 'L', 'M', 'N', 
                 'P', 'Q', 'R', 'S', 'T', 
                'U', 'V', 'W', 'X', 'Y', 'Z' };

        private static char[] mix = new char[]{
                '2', '3', '4', '5', '6', '7', '8', '9',
                'a', 'b', 'c', 'd', 'e', 'f', 
                'h', 'k', 'm', 'n', 
                 'p', 'q', 'r', 's', 't', 
                'u', 'v', 'w', 'x', 'y', 'z',
                'A', 'B', 'C', 'D', 'E', 'F', 'G', 
                'H', 'K', 'M', 'N', 
                 'P', 'Q', 'R', 'S', 'T', 
                'U', 'V', 'W', 'X', 'Y', 'Z' };

        private enum RandomStringMode
        {
            /// <summary>
            /// 小写字母
            /// </summary>
            LowerLetter,
            /// <summary>
            /// 大写字母
            /// </summary>
            UpperLetter,
            /// <summary>
            /// 混合大小写字母
            /// </summary>
            Letter,
            /// <summary>
            /// 数字
            /// </summary>
            Digital,
            /// <summary>
            /// 混合数字与大小字母
            /// </summary>
            Mix
        }

        #endregion
        public static byte[] CreateVane(string wind, int id)
        {            
            int randAngle = 0; //随机转动角度
            int mapwidth = 0;
            int f_size = 18;
            if (id == 0)
            {
                mapwidth = 15;
                f_size = 36;
            }
            else
            {
                mapwidth = 24;
                f_size = 18;
            }
            Bitmap map = new Bitmap(mapwidth, 35);//创建图片背景

            Graphics graph = Graphics.FromImage(map);
            try
            {

                graph.Clear(Color.Transparent);//清除画面，填充背景
                
                int cindex = rand.Next(7);
                Brush b = new System.Drawing.SolidBrush(c[cindex]);                

                //验证码旋转，防止机器识别
                //char[] chars = wind.ToCharArray();//拆散字符串成单字符数组

                //文字距中
                StringFormat format = new StringFormat(StringFormatFlags.NoClip);
                format.Alignment = StringAlignment.Center;
                format.LineAlignment = StringAlignment.Center;

                //for (int i = 0; i < chars.Length; i++)
                //{
                    int findex = rand.Next(5);

                    Font f = new System.Drawing.Font(font[findex], f_size, FontStyle.Bold);//字体样式(参数2为字体大小)

                    Point dot = new Point(16, 16);
                    //graph.DrawString(dot.X.ToString(),fontstyle,new SolidBrush(Color.Black),10,150);//测试X坐标显示间距的
                    float angle = ThreadSafeRandom.NextStatic(-randAngle, randAngle);//转动的度数

                    graph.TranslateTransform(dot.X, dot.Y);//移动光标到指定位置
                    graph.RotateTransform(angle);
                    graph.DrawString(wind.ToString(), f, b, 1, 1, format);
                    graph.RotateTransform(-angle);//转回去
                    graph.TranslateTransform(2, -dot.Y);//移动光标到指定位置

                //}

                //生成图片
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                map.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                return ms.ToArray();
            }
            finally
            {
                graph.Dispose();
                map.Dispose();
            }
        }
         ///  <summary>
        ///  创建随机码图片
        ///  </summary>
        ///  <param  name="randomcode">随机码</param>
        public static byte[] CreateImage(string randomcode)
        {
            int randAngle = 30; //随机转动角度
            int mapwidth = (int)(randomcode.Length * 30);
            Bitmap map = new Bitmap(mapwidth, 36);//创建图片背景

            Graphics graph = Graphics.FromImage(map);
            try
            {

                graph.Clear(Color.White);//清除画面，填充背景


                int cindex = rand.Next(7);
                Brush b = new System.Drawing.SolidBrush(c[cindex]);

                for (int i = 0; i < 1; i++)
                {
                    int x1 = rand.Next(map.Width / 2);
                    int x2 = rand.Next(map.Width * 3 / 4, map.Width);
                    int y1 = rand.Next(map.Height);
                    int y2 = rand.Next(map.Height);

                    graph.DrawBezier(new Pen(c[cindex], 2), x1, y1, (x1 + x2) / 4, 0, (x1 + x2) * 3 / 4, map.Height, x2, y2);
                }


                //验证码旋转，防止机器识别
                char[] chars = randomcode.ToCharArray();//拆散字符串成单字符数组

                //文字距中
                StringFormat format = new StringFormat(StringFormatFlags.NoClip);
                format.Alignment = StringAlignment.Center;
                format.LineAlignment = StringAlignment.Center;

                for (int i = 0; i < chars.Length; i++)
                {
                    int findex = rand.Next(5);

                    Font f = new System.Drawing.Font(font[findex], 22, System.Drawing.FontStyle.Bold);//字体样式(参数2为字体大小)

                    Point dot = new Point(16, 16);
                    //graph.DrawString(dot.X.ToString(),fontstyle,new SolidBrush(Color.Black),10,150);//测试X坐标显示间距的
                    float angle = ThreadSafeRandom.NextStatic(-randAngle, randAngle);//转动的度数

                    graph.TranslateTransform(dot.X, dot.Y);//移动光标到指定位置
                    graph.RotateTransform(angle);
                    graph.DrawString(chars[i].ToString(), f, b, 1, 1, format);
                    graph.RotateTransform(-angle);//转回去
                    graph.TranslateTransform(2, -dot.Y);//移动光标到指定位置

                }

                //生成图片
                System.IO.MemoryStream ms = new System.IO.MemoryStream();                
                map.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                return ms.ToArray();
            }
            finally
            {
                graph.Dispose();
                map.Dispose();
            }
        }

        private static string GenerateRandomString(int length, RandomStringMode mode)
        {
            string rndStr = string.Empty;
            if (length == 0)
                return rndStr;         

            int[] range = new int[2] { 0, 0 };

            switch (mode)
            {
                case RandomStringMode.Digital:
                    for (int i = 0; i < length; ++i)
                        rndStr += digitals[rand.Next(0, digitals.Length)];
                    break;

                case RandomStringMode.LowerLetter:
                    for (int i = 0; i < length; ++i)
                        rndStr += lowerLetters[rand.Next(0, lowerLetters.Length)];
                    break;

                case RandomStringMode.UpperLetter:
                    for (int i = 0; i < length; ++i)
                        rndStr += upperLetters[rand.Next(0, upperLetters.Length)];
                    break;

                case RandomStringMode.Letter:
                    for (int i = 0; i < length; ++i)
                        rndStr += letters[rand.Next(0, letters.Length)];
                    break;

                default:
                    for (int i = 0; i < length; ++i)
                        rndStr += mix[rand.Next(0, mix.Length)];
                    break;
            }

            return rndStr;
        }

        public static string GenerateCheckCode()
        {
            return GenerateRandomString(4, RandomStringMode.Mix);
        }

        #endregion
       
    }
}
