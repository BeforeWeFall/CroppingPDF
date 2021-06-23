using System.Drawing;

namespace ToJob
{
        struct Bord
        {
            private int width;
            private int height;

            public int Width { get => width;}
            public int Height { get => height;}

            public Bord(Bitmap x)
            {
                this.width = x.Width;
                this.height = x.Height;
            }
        }

}
