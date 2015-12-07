using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Windows.Forms.VisualStyles;
using SurfaceHandling.Designs;
using SurfaceHandling.Info;
using SurfaceHandling.Draw;

namespace SurfaceHandling
{
    public class SurfaceLabeler
    {
        private readonly SameList _sameList = new SameList();

        public readonly int PixColor;
        public SurfaceLabeler(int pixColor = 0)
        {
            PixColor = pixColor;
            //choose = new Func<bool>(a => a == 1); 
        }
        public ImgInfo Labeling(int[,] img, Bitmap bitmap)
        {
            int[,] labels = new int[img.GetLength(0), img.GetLength(1)];
            int l = 1;
            for (int i = 0; i < img.GetLength(1); i++)
                for (int j = 0; j < img.GetLength(0); j++)
                {
                    Fill(img, labels, i, j, ref l);
                }
            var allClasses = _sameList.AllClasses();
            var classes = new Dictionary<int, PixelClass>();
            for (int i = 0; i < img.GetLength(1); i++) // weight
                for (int j = 0; j < img.GetLength(0); j++)
                {
                    if (allClasses.Contains(labels[j, i]))
                        labels[j, i] = _sameList.SerchClass(labels[j, i]);
                    if (classes.ContainsKey(labels[j, i]))
                        classes[labels[j, i]].Weight++;
                    else
                        classes.Add(labels[j, i], new PixelClass() { Class = labels[j, i], Weight = 1, Angles = new Angles() });
                }

            var inf = new ImgInfo()
            {
                Labels = labels,
                OldPicture = bitmap
            };

            inf.Classes = classes;


            Signer.GetSigns(inf);
            foreach (var c in inf.Classes.Values)
            {
                Signer.GetFourWeight(c, inf);
            }

            var designer = DesigerConfiguration.GetConfiguration(inf);

            RemoveByDesigener(inf, designer);

            LinkedLetters(inf);

            RemoveByDesigener(inf, DesigerConfiguration.GetSecondConfiguration(inf));


            var toRemove = (from v in inf.Classes.Values where v.Angles.Right.X > bitmap.Width || v.Angles.Top.Y > bitmap.Height select v.Class).ToList();

            foreach (var v in toRemove)
            {
                inf.Classes.Remove(v);
            }

            foreach (var v in inf.Classes.Values)
            {
                float bright = 0;
                int count = 0;
                for (int i = v.Angles.Top.Y; i < v.Angles.Bottom.Y; i++)
                {
                    for (int j = v.Angles.Left.X; j < v.Angles.Right.X; j++)
                    {
                        if (labels[j, i] == 0)
                        {
                            bright += bitmap.GetPixel(j, i).GetBrightness();
                            count++;
                        }
                    }

                }
                v.WBrtight = count != 0 ? bright / count : 0;
            }

            if (inf.Classes.Values.Any())
            {
                inf.Brightnest = (int)(inf.Classes.Values.ToList().Min(x => x.WBrtight) * 255);

                int iter = 0;
                int clcount = inf.Classes.Values.Count;
                for (int i = 1; i <= clcount; i++)
                {
                    inf.Classes.Add(-i, new PixelClass() { Class = -i, Weight = 0 });
                }
                var blist = inf.Classes.Values.OrderBy(x => x.WBrtight).ToList();
                foreach (var v in blist)
                {
                    if (v.Angles != null)
                    {
                        iter--;

                        var a = v.Angles.Top;


                        RecursiveFill(a.X, a.Y - 3, inf, iter, (int)(v.WBrtight * 255));
                        a = v.Angles.Bottom;
                        RecursiveFill(a.X, a.Y + 3, inf, iter, (int)(v.WBrtight * 255));
                        a = v.Angles.Left;
                        RecursiveFill(a.X - 3, a.Y, inf, iter, (int)(v.WBrtight * 255));
                        a = v.Angles.Right;
                        RecursiveFill(a.X + 3, a.Y, inf, iter, (int)(v.WBrtight * 255));
                    }
                }

                var zero = inf.Classes.Values.Where(x => x.Weight == 0).ToList();

                foreach (var z in zero)
                {
                    inf.Classes.Remove(z.Class);
                }



                //Signer.GetSigns(inf);
                inf.NewPicture = Draw.Draw.DrawClasses(inf, new Designer());
                Draw.Draw.Rectangles(inf.NewPicture, inf);
            }
            else
            {
                inf.NewPicture = inf.OldPicture;
            }
            return inf;
        }


        public void RemoveByDesigener(ImgInfo inf, Designer designer)
        {
            var labels = inf.Labels;
            List<int> classesToRemove = new List<int>();
            for (int i = 0; i < inf.OldPicture.Height; i++)
                for (int j = 0; j < inf.OldPicture.Width; j++)
                    if (labels[j, i] > 0)
                    {
                        var cl = inf.Classes[labels[j, i]];
                        if (!designer.Decide(cl))
                        {
                            classesToRemove.Add(labels[j, i]);
                            labels[j, i] = 0;
                        }
                        else
                        {
                            int a = 3;
                        }



                    }

            foreach (var r in classesToRemove)
            {
                inf.Classes.Remove(r);
            }
        }

        public void RecursiveFill(int x, int y, ImgInfo info, int cl, int brightnes)
        {

            if (x >= 0 && x < info.OldPicture.Width && y >= 0 && y < info.OldPicture.Height &&
                brightnes - 10 < info.OldPicture.GetPixel(x, y).GetBrightness() * 255
                && info.Labels[x, y] == 0)
            {
                info.Classes[cl].Weight++;

                info.Labels[x, y] = cl;
                RecursiveFill(x - 1, y, info, cl, brightnes);
                RecursiveFill(x + 1, y, info, cl, brightnes);
                RecursiveFill(x, y + 1, info, cl, brightnes);
                RecursiveFill(x, y - 1, info, cl, brightnes);
            }
        }


        private float rangeLeft = 0.34f;
        private float rangeRight = 3f;
        public bool WH(PixelClass cl)
        {
            var ratio = (float)cl.Height / cl.Width;
            return ratio > rangeLeft && ratio < rangeRight;
        }

        public void LinkedLetters(ImgInfo inf)
        {
            inf.LincedLaters = new List<int>();
            inf.Lines = new List<Line>();
            foreach (var v in inf.Classes.Values)
            {
                foreach (var v2 in inf.Classes.Values)
                {
                    if (v != v2 && v.Weight > 40
                        && WH(v) && WH(v2)
                        && ((float)v.Weight) / v2.Weight > 0.5 && ((float)v.Weight) / v2.Weight < 2
                        )
                    {
                        var s = Math.Sqrt((v.Center.X - v2.Center.X) * (v.Center.X - v2.Center.X) +
                        (v.Center.Y - v2.Center.Y) * (v.Center.Y - v2.Center.Y));

                        if (s < Math.Max(v.Width, v.Height) * 1.75)
                        {
                            inf.LincedLaters.Add(v.Class);
                            inf.LincedLaters.Add(v2.Class);
                            v.LikedClass.Add(v2);
                            v2.LikedClass.Add(v);
                            inf.Lines.Add(Draw.Draw.GetLineFunc(new Point(v.Center.X, v.Center.Y), new Point(v2.Center.X, v2.Center.Y)));
                        }

                    }
                }
            }
        }


        #region Fill
        private void Fill(int[,] img, int[,] label, int x, int y, ref int l)
        {
            if (img[y, x] == PixColor)
            {
                return;
            }
            if (!(IsBLabeled(label, y, x) || IsCLabeled(label, y, x)))
            {
                l++;
                label[y, x] = l;
                return;
            }

            if (IsBLabeled(label, y, x) && IsCLabeled(label, y, x))
            {
                label[y, x] = label[y - 1, x];
                if (label[y - 1, x] != label[y, x - 1])
                {
                    _sameList.Add(label[y, x - 1], label[y - 1, x]);
                }
                return;
            }
            if (IsBLabeled(label, y, x))
            {
                label[y, x] = label[y, x - 1];
                return;
            }
            if (IsCLabeled(label, y, x))
            {
                label[y, x] = label[y - 1, x];
            }
        }

        public bool IsBLabeled(int[,] label, int y, int x)
        {
            return !(x - 1 < 0) && label[y, x - 1] > 0;
        }

        public bool IsCLabeled(int[,] label, int y, int x)
        {
            return !(y - 1 < 0) && label[y - 1, x] > 0;
        }
        #endregion


    }
}
