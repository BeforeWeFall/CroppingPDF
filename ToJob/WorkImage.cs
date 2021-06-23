using System.Drawing;


namespace ToJob
{
    class WorkImage
    {
        //private int FindLine(ArrayRGB rgb, int search, int move) //Чекни
        //{
        //    int count = 0;
        //    int posX = 0;
        //    bool flag = false;

        //    for (int i = 2; i < move; i++)
        //    {
        //        for (int j = 2; j < search - 6; j++)
        //        {
        //            if (rgb.R[i, j + 5] < 200 || rgb.R[i, j + 4] < 200 || rgb.R[i, j + 3] < 200 || rgb.R[i, j + 2] < 200 || rgb.R[i, j + 1] < 200 || rgb.R[i, j] < 200)
        //            {
        //                count++;
        //                if (count >= search * 0.8)
        //                {
        //                    posX = i;
        //                    flag = true;
        //                }
        //            }
        //            else count = 0;
        //        }
        //        if (flag) break;
        //    }
        //    return posX;
        //}

        private int FindLineY(Bitmap image, ArrayRGB rgb) 
        {
            int count = 0;
            int posY = 0;
            int searchInterval = 4;
            bool flag = false;

            for (int i = 2; i < image.Height - 6; i++)
            {
                for (int j = 2; j < image.Width; j++)
                {
                    if (ColorYFind(rgb, searchInterval, j,i))//(rgb.R[j, i + 4] < maxColor || rgb.R[j, i + 3] < maxColor || rgb.R[j, i + 2] < maxColor || rgb.R[j, i + 1] < maxColor || rgb.R[j, i] < maxColor)
                    {
                        count++;
                        if (count >= image.Width * 0.8)
                        {
                            posY = i;
                            flag = true;
                        }
                    }
                    else count = 0;
                }
                if (flag) break;
            }
            return posY;
        }

        private int FindLineX(Bitmap image, ArrayRGB rgb) 
        {
            int count = 0;
            int posX = 0;
            int searchInterval = 4;
            bool flag = false;

            for (int i = 2; i < image.Width - 6; i++)
            {
                for (int j = 2; j < image.Height; j++)
                {
                    if (ColorXFind(rgb, searchInterval, i,j)) //(rgb.R[i + 4, j] < maxColor || rgb.R[i + 3, j] < maxColor || rgb.R[i + 2, j] < maxColor || rgb.R[i + 1, j] < maxColor || rgb.R[i, j] < maxColor)
                    {
                        count++;
                        if (count >= image.Height * 0.7)
                        {
                            posX = i;
                            flag = true;
                        }
                    }
                    else
                        count = 0;
                }
                if (flag) break;
            }
            return posX;
        }

        private bool ColorXFind(ArrayRGB rgb, int maxIter,int w, int h)
        {
            int maxColor = 200;
            for (int i = 0; i <= maxIter; i++)
            {
                if (rgb.R[w + i, h] < maxColor)
                    return true;
            }
            return false;
        }

        private bool ColorYFind(ArrayRGB rgb, int maxIter, int w, int h)
        {
            int maxColor = 200;
            for (int i = 0; i <= maxIter; i++)
            {
                if (rgb.R[w, h+i] < maxColor)
                    return true;
            }
            return false;
        }

        private (int start, int end) FindStartAndEnd(int y, int heightorweidth)
        {
            int start, finish = 0;
            if (y < heightorweidth * 0.4)
            {
                start = 0;
                finish = y;
            }
            else
            {
                start = y;
                finish = heightorweidth;
            }
            var tuple = (start, finish);
            return tuple;
        }

        private Bitmap CreateX45(int firstStart, int firstFinish, int secondStart, int secondFinsh, int heiF, int heiS, ArrayRGB rgb, ArrayRGB rgb2)
        {
            Bitmap bit2 = new Bitmap(3508, 2480);

            LockBitmap lockBitmap = new LockBitmap(bit2);
            lockBitmap.LockBits();

            for (int fx = firstStart; fx < firstFinish; fx++)
                for (int fy = 0; fy < heiF; fy++)
                {
                    lockBitmap.SetPixel(fx - firstStart, fy, Color.FromArgb(rgb.R[fx, fy], rgb.G[fx, fy], rgb.B[fx, fy]));
                }

            for (int fx = secondStart; fx < secondFinsh; fx++)
                for (int fy = 0; fy < heiS; fy++)
                {
                    lockBitmap.SetPixel(fx - secondStart + firstFinish - firstStart + 10, fy, Color.FromArgb(rgb2.R[fx, fy], rgb2.G[fx, fy], rgb2.B[fx, fy]));
                }

            lockBitmap.UnlockBits();

            return bit2;
        }

        private Bitmap CreateX90(int firstStart, int firstFinish, int secondStart, int secondFinsh, int heiF, int heiS, ArrayRGB rgb, ArrayRGB rgb2)
        {
            Bitmap bit2 = new Bitmap(2480, 3508);

            LockBitmap lockBitmap = new LockBitmap(bit2);
            lockBitmap.LockBits();

            for (int fx = firstFinish; fx > firstStart; fx--)
                for (int fy = heiF - 1; fy > 0; fy--)
                {
                    lockBitmap.SetPixel(firstFinish - fx, heiF - fy - 1, Color.FromArgb(rgb.R[fx, fy], rgb.G[fx, fy], rgb.B[fx, fy]));
                }

            for (int fx = secondStart; fx < secondFinsh; fx++)
                for (int fy = heiS - 1; fy > 0; fy--)
                {
                    lockBitmap.SetPixel(heiS - 1 - fy, fx - secondStart + heiF - 1, Color.FromArgb(rgb2.R[fx, fy], rgb2.G[fx, fy], rgb2.B[fx, fy]));
                }

            //for (int fx = secondStart; fx < secondFinsh; fx++)   //без поворота
            //    for (int fy = 0; fy < heiS; fy++)
            //{
            //    lockBitmap.SetPixel(fx - secondStart + firstFinish - firstStart + 10, fy, Color.FromArgb(rgb2.R[fx, fy], rgb2.G[fx, fy], rgb2.B[fx, fy]));
            //}

            lockBitmap.UnlockBits();

            return bit2;
        }

        private Bitmap CreateY90(int firstStart, int firstFinish, int secondStart, int secondFinsh, int widthF, int widthS, ArrayRGB rgb, ArrayRGB rgb2)
        {
            Bitmap bit2 = new Bitmap(2480, 3508);

            LockBitmap lockBitmap = new LockBitmap(bit2);
            lockBitmap.LockBits();

            for (int fx = widthF - 1; fx >= 0; fx--)
                for (int fy = firstStart; fy < firstFinish; fy++)
                {
                    lockBitmap.SetPixel(fy - firstStart, widthF - 1 - fx, Color.FromArgb(rgb.R[fx, fy], rgb.G[fx, fy], rgb.B[fx, fy]));
                }

            for (int fx = 0; fx < widthS; fx++)
                for (int fy = secondStart; fy < secondFinsh; fy++)
                {
                    lockBitmap.SetPixel(fx, fy - secondStart + widthF + 10, Color.FromArgb(rgb2.R[fx, fy], rgb2.G[fx, fy], rgb2.B[fx, fy]));
                }

            lockBitmap.UnlockBits();

            return bit2;
        }

        private Bitmap CreateY45(int firstStart, int firstFinish, int secondStart, int secondFinsh, int widthF, int widthS, ArrayRGB rgb, ArrayRGB rgb2)
        {
            Bitmap bit2 = new Bitmap(2480, 3508);

            LockBitmap lockBitmap = new LockBitmap(bit2);
            lockBitmap.LockBits();

            for (int fx = 0; fx != widthF - 1; fx++)
                for (int fy = firstStart; fy < firstFinish; fy++)
                {
                    lockBitmap.SetPixel(fy - firstStart, widthF - 1 - fx, Color.FromArgb(rgb.R[fx, fy], rgb.G[fx, fy], rgb.B[fx, fy]));
                }

            for (int fx = 0; fx < widthS; fx++)
                for (int fy = secondStart; fy < secondFinsh; fy++)
                {
                    lockBitmap.SetPixel(fx, fy - secondStart + widthF + 10, Color.FromArgb(rgb2.R[fx, fy], rgb2.G[fx, fy], rgb2.B[fx, fy]));
                }

            lockBitmap.UnlockBits();

            return bit2;
        }

        public void CropAndCombine(Bitmap[] images, string fileNameToSave, int a)
        {
            Bord First = new Bord(images[0]);
            Bord Second = new Bord(images[1]);
            ArrayRGB rgb = new ArrayRGB(images[0]);
            ArrayRGB rgb2 = new ArrayRGB(images[1]);
            int firstStart, firstFinish, secondStart, secondFinsh = 0;

            bool fromx = false;

            (int start, int end) tuple;

            if (First.Width < First.Height)
            {
                tuple = FindStartAndEnd(FindLineY(images[0], rgb), First.Height);
                firstStart = tuple.start;
                firstFinish = tuple.end;
                tuple = FindStartAndEnd(FindLineY(images[1], rgb2), Second.Height);
            }
            else
            {

                tuple = FindStartAndEnd(FindLineX(images[0], rgb), First.Width);
                firstStart = tuple.start;
                firstFinish = tuple.end;
                tuple = FindStartAndEnd(FindLineX(images[1], rgb2), Second.Width);
                fromx = true;
            }

            secondStart = tuple.start;
            secondFinsh = tuple.end;

            images[1].Dispose();
            images[0].Dispose();

            string nameFile = $"{fileNameToSave}\\{a}.jpg";

            if (fromx)
            {
                if (firstStart > First.Width * 0.5)
                    CreateX45(firstStart, firstFinish, secondStart, secondFinsh, First.Height, Second.Height, rgb, rgb2).Save(nameFile);
                else
                    CreateX90(firstStart, firstFinish, secondStart, secondFinsh, First.Height, Second.Height, rgb, rgb2).Save(nameFile);
            }
            else
            {
                if (firstStart > First.Width * 0.5)
                    CreateY90(firstStart, firstFinish, secondStart, secondFinsh, First.Width, Second.Width, rgb, rgb).Save(nameFile);
                else
                    CreateY45(firstStart, firstFinish, secondStart, secondFinsh, First.Width, Second.Width, rgb, rgb).Save(nameFile);
            }
        }
    }
}
