using System.Drawing;


namespace ToJob
{
    class ArrayRGB
    {
        public int[,] R;
        public int[,] G;
        public int[,] B;

        public ArrayRGB(Bitmap image2)
        {

            R = new int[image2.Width, image2.Height];
            G = new int[image2.Width, image2.Height];
            B = new int[image2.Width, image2.Height];

            LockBitmap lockBitmap = new LockBitmap(image2);
            lockBitmap.LockBits();

            for (int i = 0; i < image2.Width; i++)
            {
                for (int j = 0; j < image2.Height; j++)
                {
                    R[i, j] = lockBitmap.GetPixel(i, j).R;
                    G[i, j] = lockBitmap.GetPixel(i, j).G;
                    B[i, j] = lockBitmap.GetPixel(i, j).B;
                }
            }

            lockBitmap.UnlockBits();
        }
    }
}
