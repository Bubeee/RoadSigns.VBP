using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurfaceHandling.Designs
{
    public class Signer
    {
        public static Dictionary<int, PixelClass> GetSigns(ImgInfo info)
        {
            var dict = info.Classes;
            var labels = info.Labels;
            for (int i = 0; i < labels.GetLength(0); i++)
                for (int j = 0; j < labels.GetLength(1); j++)
                {
                    if (labels[i, j] == 0) continue;
                    var pclass = dict[labels[i, j]];
                    if (pclass.Angles.Bottom.Y < j)
                        pclass.Angles.Bottom = new Point(i, j);
                    if (pclass.Angles.Top.Y > j)
                        pclass.Angles.Top = new Point(i, j);
                    if (pclass.Angles.Left.X > i)
                        pclass.Angles.Left = new Point(i, j);
                    if (pclass.Angles.Right.X < i)
                        pclass.Angles.Right = new Point(i, j);
                }

            foreach (var v in dict)
            {
                v.Value.Width = v.Value.Angles.Right.X - v.Value.Angles.Left.X;
                v.Value.Height = v.Value.Angles.Bottom.Y - v.Value.Angles.Top.Y;
                v.Value.Ratio = (float)v.Value.Weight / (v.Value.Height * v.Value.Width);
                v.Value.Center = new Point(v.Value.Angles.Left.X + v.Value.Width / 2, v.Value.Angles.Top.Y + v.Value.Height / 2);
            }
            return dict;
        }

        public static PixelClass GetFourWeight(PixelClass pclass, ImgInfo inf)
        {
            int[] foutWeight = { 0, 0, 0, 0 };
            Point[] fourPoints = new Point[4];
            for (int i = 0; i < 4; i++)
            {
                fourPoints[i] = new Point(pclass.Center.X, pclass.Center.Y);
            }
            for (int i = pclass.Angles.Left.X; i < pclass.Angles.Right.X; i++)
                for (int j = pclass.Angles.Top.Y; j < pclass.Angles.Bottom.Y; j++)
                {
                    int index = 0;
                    if (i > pclass.Center.X)
                        index = 1;
                    if (j > pclass.Center.Y)
                        index += 2;
                    if (inf.Labels[i, j] == pclass.Class)
                    {
                        var olppoint = Math.Pow(fourPoints[index].X - pclass.Center.X, 2) +
                        Math.Pow(fourPoints[index].Y - pclass.Center.Y, 2);

                        var newp = Math.Pow(i - pclass.Center.X, 2) +
                        Math.Pow(j - pclass.Center.Y, 2);

                        if (olppoint < newp)
                            fourPoints[index] = new Point(i, j);
                        foutWeight[index] += 1;
                    }
                }


            int maxDiff = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int j = i; j < 4; j++)
                {
                    if (maxDiff < Math.Abs(foutWeight[i] - foutWeight[j]))
                        maxDiff = Math.Abs(foutWeight[i] - foutWeight[j]);
                }
            }
            pclass.FourPoints = fourPoints;
            pclass.FourWeights = foutWeight;
            pclass.MaxDiff = maxDiff;
            return pclass;
        }
    }
}
