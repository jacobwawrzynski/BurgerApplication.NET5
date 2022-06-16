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
    /// <summary>
    /// PdfMenager
    /// </summary>
    public static class PdfMenager
    {
        /// <summary>
        /// convert  byte array to image 
        /// </summary>
        /// <param name="arr">byte array</param>
        /// <returns>System.Drawing.Image</returns>
        public static Image ByteArrayToImage(byte[] arr)
        {
            using (var ms = new MemoryStream(arr))
            {
                return Image.FromStream(ms);
            }
        }
        /// <summary>
        /// convert byte array to pdf
        /// </summary>
        /// <param name="arr">byte array</param>
        /// <returns>PdfDocument</returns>
        public static PdfDocument ByteArrayToPdf(byte[] arr)
        {
            MemoryStream stream = new MemoryStream(arr);
            PdfDocument pdf = PdfReader.Open(stream);
            return pdf;
        }
        /// <summary>
        /// convert System.Drawing.Image to byte array
        /// </summary>
        /// <param name="image">System.Drawing.Image</param>
        /// <returns>byte array</returns>
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
        /// <summary>
        ///  convert PdfDocument to byte array
        /// </summary>
        /// <param name="pdf">PdfDocument</param>
        /// <returns>byte array</returns>
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
        /// <summary>
        /// saves pdf file 
        /// </summary>
        /// <param name="pdf">PdfDocument</param>
        /// <param name="path">path</param>
        public static void SavePdf(PdfDocument pdf, string path) //"folder\plik.pdf"
        {
            pdf.Save(path);
        }
        /// <summary>
        /// saves pdf file 
        /// </summary>
        /// <param name="arr">byte array</param>
        /// <param name="path">path</param>
        public static void SavePdf(byte[] arr, string path) //"folder\plik.pdf"
        {
            var pdf = ByteArrayToPdf(arr);
            pdf.Save(path);
        }
        /// <summary>
        /// saves pdf file 
        /// </summary>
        /// <param name="pdf">PdfDocument</param>
        /// <param name="path">path</param>
        /// <param name="filename">filename</param>
        public static void SavePdf(PdfDocument pdf, string path,string filename) //"folder" , "plik.pdf"
        {
            pdf.Save(path+ @$"\{filename}.pdf");
        }
        /// <summary>
        /// saves pdf file 
        /// </summary>
        /// <param name="arr">byte array</param>
        /// <param name="path">path</param>
        /// <param name="filename">filename</param>
        public static void SavePdf(byte[] arr, string path, string filename) //"folder" , "plik.pdf"
        {
            var pdf = ByteArrayToPdf(arr);
            pdf.Save(path + @$"\{filename}.pdf");
        }
        /// <summary>
        /// Try Save Pdf file
        /// </summary>
        /// <param name="arr">byte array</param>
        /// <param name="path">path</param>
        /// <param name="filename">filename</param>
        /// <returns>true if saves successfully</returns>
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
