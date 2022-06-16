using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseContext.MyPdf
{
    public static class PdfMenager
    {
        public static Image ByteArrayToImage(byte[] arr)
        {
            using (var ms = new MemoryStream(arr))
            {
                return Image.FromStream(ms);
            }
        }

        public static PdfDocument ByteArrayToPdf(byte[] arr)
        {
            MemoryStream stream = new MemoryStream(arr);
            PdfDocument pdf = PdfReader.Open(stream);
            return pdf;
        }

        public static byte[] ImageToByteArray(Image image)
        {
            byte[] fileContents = null;
            using (MemoryStream stream = new MemoryStream())
            {
                image.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                fileContents = stream.ToArray();
            }
            return fileContents;

        }

        public static byte[] PdfToByteArray(PdfDocument pdf)
        {
            byte[] fileContents = null;
            using (MemoryStream stream = new MemoryStream())
            {
                pdf.Save(stream, true);
                fileContents = stream.ToArray();
            }
            return fileContents;
        }
        public static void SavePdf(PdfDocument pdf, string path) //"folder\plik.pdf"
        {
            pdf.Save(path);
        }
        public static void SavePdf(byte[] arr, string path) //"folder\plik.pdf"
        {
            var pdf = ByteArrayToPdf(arr);
            pdf.Save(path);
        }
        public static void SavePdf(PdfDocument pdf, string path,string filename) //"folder" , "plik.pdf"
        {
            pdf.Save(path+ @$"\{filename}.pdf");
        }
        public static void SavePdf(byte[] arr, string path, string filename) //"folder" , "plik.pdf"
        {
            var pdf = ByteArrayToPdf(arr);
            pdf.Save(path + @$"\{filename}.pdf");
        }
        public static bool TrySavePdf(byte[] arr, string path, string filename) //"folder" , "plik.pdf"
        {
            try
            {
                var pdf = ByteArrayToPdf(arr);
                pdf.Save(path + @$"\{filename}.pdf");
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}
