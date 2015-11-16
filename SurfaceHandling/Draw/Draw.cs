using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SurfaceHandling.Designs;
using SurfaceHandling.Info;

namespace SurfaceHandling.Draw
{
    public class Draw
    {
        public static void Noise(Bitmap bitmap)
        {
            Random rnd = new Random();

            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = 0; j < bitmap.Height; j++)
                {
                    Color color = bitmap.GetPixel(i, j);
                    byte r = (byte)(rnd.Next(0, 10) != 1 ? color.R : 255);
                    byte b = (byte)(rnd.Next(0, 10) != 1 ? color.B : 255);
                    byte g = (byte)(rnd.Next(0, 10) != 1 ? color.G : 255);

                    bitmap.SetPixel(i, j, Color.FromArgb(255, r, g, b));
                }
            }
        }

        public static void Rectangles(Bitmap bitmap, ImgInfo info)
        {
            foreach (var c in info.Classes)
            {
                if (c.Value.FourPoints != null)
                {
                    foreach (var v in c.Value.FourPoints)
                    {
                        Rect(bitmap, v, Color.Green);
                    }
                    //Rect(bitmap, imgInfo.Value.Angles.Right);
                    //Rect(bitmap, imgInfo.Value.Angles.Left);
                    //Rect(bitmap, imgInfo.Value.Angles.Top);
                    //Rect(bitmap, imgInfo.Value.Angles.Bottom);
                    Rect(bitmap, c.Value.Center, Color.Red);
                }
                                    
            }

        }
        public static void Rect(Bitmap bitmap, Point a, Color cl)
        {
            int rx = 2;
            for (int i = a.X - rx; i < a.X + rx; i++)
            {
                for (int j = a.Y - rx; j < a.Y + rx; j++)
                {
                    if (i > 0 && j > 0 && i < bitmap.Width && j < bitmap.Height)
                        bitmap.SetPixel(i, j, cl);
                }
            }
        }

        public static Line GetLineFunc(Point p1, Point p2)
        {
            var b1 = (p2.X - p1.X) != 0 ? -(p1.X) * (p2.Y - p1.Y) / (p2.X - p1.X) + p1.Y : p1.Y;
            var k1 = (p2.X - p1.X) != 0 ? (float)(p2.Y - p1.Y) / (p2.X - p1.X) : 1;
            return new Line()
            {
                k =  k1,
                b = b1,

                Func = x => (p2.X - p1.X) == 0 ? p1.Y : (int)(((float)x - p1.X) * (p2.Y - p1.Y)) / (p2.X - p1.X) + p1.Y,
                Right = Math.Max(p1.X, p2.X),
                Left = Math.Min(p1.X, p2.X)
            };

        }


        public static Bitmap DrawClasses(ImgInfo imgInfo, Designer designer)
        {
            var labels = imgInfo.Labels;
            List<int> classesToRemove = new List<int>();
            Bitmap result = new Bitmap(imgInfo.OldPicture.Width, imgInfo.OldPicture.Height);
            for (int i = 0; i < result.Height; i++)
                for (int j = 0; j < result.Width; j++)
                    if (labels[j, i] != 0)
                    {
                        //var cl = imgInfo.Classes[labels[j, i]];
                        result.SetPixel(j, i, labels[j, i] < 0 ? Color.FromArgb(Math.Abs((labels[j, i]*15)%255), 
                                              Math.Abs(labels[j, i] * 12 + labels[j, i] * labels[j, i]) % 255,
                                              Math.Abs((labels[j, i] * 5 + labels[j, i]) % 255 * labels[j, i]) % 255)
                            : Color.Brown);
                        // Color.FromArgb(Math.Abs((labels[j, i]*15)%255), Color.AliceBlue));
                        //Math.Abs(labels[j, i] * 12 + labels[j, i] * labels[j, i]) % 255,
                        //Math.Abs((labels[j, i] * 5 + labels[j, i]) % 255 * labels[j, i]) % 255));
                        //else
                        //{
                        //   classesToRemove.Add(labels[j, i]);
                        //    labels[j, i] = 0;
                        //}
                    }

            foreach (var r in classesToRemove)
            {
                imgInfo.Classes.Remove(r);
            }
            return result;
        }
    }
}
