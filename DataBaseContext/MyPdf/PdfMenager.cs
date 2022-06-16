using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseContext.MyPdf
{
    public static class PdfMenager
    {
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
        public static PdfDocument ByteArrayToPdf(byte[] arr)
        {
            MemoryStream stream = new MemoryStream(arr);
            PdfDocument pdf = PdfReader.Open(stream);
            return pdf;
        }
        public static void SavePdf(PdfDocument pdf, string path) //"folder\plik.pdf"
        {
            pdf.Save(path);
        }
        public static void SavePdf(PdfDocument pdf, string path,string filename) //"folder" , "plik.pdf"
        {
            pdf.Save(path+ @$"\{filename}.pdf");
        }
    }
}
