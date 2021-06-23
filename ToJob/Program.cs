using PdfSharp.Pdf;
using PdfSharp.Pdf.Advanced;
using PdfSharp.Pdf.IO;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;


namespace ToJob
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Text.Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            const string PathToPDF = @"D:\doc.pdf";
            string PathToJpg = "D:\\пдф";//without .jpg
            string PathToCutJpg=@"D:\пдф\cut";
            string fileNamePDF = @"D:\пдф\ПДФ";
            PDF pf = new PDF();

            WorkImage clear = new WorkImage();
            PdfDocument document = PdfReader.Open(PathToPDF);
            
            int imageCount = 0;

            #region считывание пдф
            //foreach (PdfPage page in document.Pages)
            //{
            //    // Get resources dictionary
            //    PdfDictionary resources = page.Elements.GetDictionary("/Resources");
            //    if (resources != null)
            //    {
            //        // Get external objects dictionary
            //        PdfDictionary xObjects = resources.Elements.GetDictionary("/XObject");
            //        if (xObjects != null)
            //        {
            //            var items = xObjects.Elements.Values;
            //            // Iterate references to external objects
            //            foreach (PdfItem item in items)
            //            {
            //                PdfReference reference = item as PdfReference;
            //                if (reference != null)
            //                {
            //                    PdfDictionary xObject = reference.Value as PdfDictionary;
            //                    // Is external object an image?
            //                    if (xObject != null && xObject.Elements.GetString("/Subtype") == "/Image")
            //                    {
            //                        //var bitjpg =  pf.ExportImageToBitmap(xObject, ref imageCount);
            //                        //  clear.Clear(bitjpg, ref imageCount);

            //                        //bitmaps.Add(pf.ExportImageToBitmap(xObject, ref imageCount));
            //                        pf.ExportImageToFile(xObject, ref imageCount, PathToJpg);
            //                        ////dictBit.Add(bitjpg, clear.Findborders(bitjpg));
            //                    }
            //                }
            //            }
            //        }
            //    }
            //}
            #endregion

            int count = 0;
            int counjpg = 0;
            while (count < Directory.GetFiles(PathToJpg).Length - 1)
            {
                clear.CropAndCombine(new[] { new Bitmap(PathToJpg+"\\"+count.ToString()+".jpg"), new Bitmap(PathToJpg + "\\" + (count +1).ToString()+".jpg") }, PathToCutJpg, counjpg);
                count += 2;
                counjpg++;
            }

            #region создание пдф
            int countp = 0;
            while (countp < Directory.GetFiles(PathToCutJpg).Length)
            {
                var s_document = new PdfDocument();
                pf.DrawPageFromFiles(s_document.AddPage(), countp, PathToCutJpg);
                countp++;
                s_document.Save(fileNamePDF + countp.ToString() + ".pdf");
            }
            #endregion
        }
    }
}
