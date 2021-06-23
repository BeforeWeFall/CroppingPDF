using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System.Drawing;
using System.IO;


namespace CroppingPDF
{
    class PDF
    {
        public Bitmap ExportImageToBitmap(PdfDictionary image, ref int count)
        {
            string filter = image.Elements.GetName("/Filter");
            switch (filter)
            {
                case "/DCTDecode":
                    return ExportJpegImageToBimap(image, ref count);       
            }
            return null;
        }

        public void ExportImageToFile(PdfDictionary image, ref int count, string namejpg)
        {
            string filter = image.Elements.GetName("/Filter");
            switch (filter)
            {
                case "/DCTDecode":
                    ExportJpegImageToFile(image, ref count, namejpg);
                    break;
            }
        }

        private Bitmap ExportJpegImageToBimap(PdfDictionary image, ref int count)
        {
            byte[] stream = image.Stream.Value;

            Image bmp2;

            using (var ms = new MemoryStream(stream))
            {
                bmp2 = Image.FromStream(ms);
            }

            count++;
            return new Bitmap(bmp2);
        }

        private void ExportJpegImageToFile(PdfDictionary image, ref int count, string namejpg)
        {
            byte[] stream = image.Stream.Value;
            FileStream fs = new FileStream($"{namejpg}\\{count}.jpg", FileMode.Create, FileAccess.Write);

            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(stream);
            bw.Close();

            count++;
        }

        private void DrawImage(XGraphics gfx, int count, string from)
        {
            XImage image = XImage.FromFile(Directory.GetFiles(from)[count]);
            if (image.PixelHeight < image.PixelWidth)
            {
                gfx.DrawImage(image, 0, 0, 800, 600);
            }
            else
                gfx.DrawImage(image, 0, 0, 600, 800);
        }

        public void DrawPageFromFiles(PdfPage page, int count, string from)
        {
            XGraphics gfx = XGraphics.FromPdfPage(page);
            //  DrawTitle(page, gfx, "Images",s_document);
            DrawImage(gfx, count, from);
        }
    }
}
