using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using System.IO;
using System.Diagnostics;
using DataBaseContext.Entities;
using PdfSharp.Pdf.IO;

namespace DataBaseContext.MyPdf
{
    public class DeliveryMenager
    {

        DateTime date = DateTime.Now;
        List<(string, int)> dostawa = new List<(string, int)>();
        public Restaurant restaurant { get; private set; }
        public DeliveryMenager(Restaurant restaurant, DateTime date)
        {
            this.date = date;
            this.restaurant = restaurant;
        }
        public DeliveryMenager(Restaurant restaurant)
        {
            this.restaurant = restaurant;
        }

        public void dodaj(string name, int value)
        {
            dostawa.Add((name, value));
        }
        public PdfDocument GeneratePdf()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            using (PdfDocument pdf = new PdfDocument())
            {
                int currentYposion_values = 0;
                int lineHorizontalStrat = 55;
                int lineHorizontalEnd = 215;
                int leftmargin = 100;
                PdfPage page;
                XGraphics gfx = null;
                XFont font = new XFont("Arial", 10);

                XFont boldFont = new XFont("Arial", 10, XFontStyle.Bold);

                XFont TitfleFont = new XFont("Arial", 18, XFontStyle.Bold);
                XPen pen = new XPen(XColor.FromArgb(0, 0, 0));

                page = pdf.AddPage();
                gfx = XGraphics.FromPdfPage(page);

                //obrazek
                XImage image = null;
                var res = DataBaseQuery.DownladImage("burger.png");
                using (MemoryStream stream = new MemoryStream(res.ImageData))
                {
                    image = XImage.FromStream(stream);
                }

                double width = image.PixelWidth * 72 / image.HorizontalResolution;
                double height = image.PixelHeight * 72 / image.HorizontalResolution;
                gfx.DrawImage(image, page.Width / 2 - 50, 20, 100, 100);
                //

                currentYposion_values = 150;
                gfx.DrawString($"Dostawa z dnia {date.ToString("D")} z restauracji nr{restaurant.Id}", TitfleFont, XBrushes.Black, new XPoint(70, currentYposion_values));
                currentYposion_values += 30;
                gfx.DrawLine(pen, new XPoint(lineHorizontalStrat + leftmargin, currentYposion_values - 10), new XPoint(lineHorizontalEnd + leftmargin, currentYposion_values - 10));

                gfx.DrawLine(pen, new XPoint(55 + leftmargin, currentYposion_values - 10), new XPoint(55 + leftmargin, currentYposion_values + 10));
                gfx.DrawLine(pen, new XPoint(85 + leftmargin, currentYposion_values - 10), new XPoint(85 + leftmargin, currentYposion_values + 10));
                gfx.DrawLine(pen, new XPoint(175 + leftmargin, currentYposion_values - 10), new XPoint(175 + leftmargin, currentYposion_values + 10));
                gfx.DrawLine(pen, new XPoint(215 + leftmargin, currentYposion_values - 10), new XPoint(215 + leftmargin, currentYposion_values + 10));
                gfx.DrawString("Lp", boldFont, XBrushes.Black, new XPoint(60 + leftmargin, currentYposion_values));

                gfx.DrawString("Nazwa", boldFont, XBrushes.Black, new XPoint(90 + leftmargin, currentYposion_values));

                gfx.DrawString("Ilość", boldFont, XBrushes.Black, new XPoint(180 + leftmargin, currentYposion_values));

                currentYposion_values += 20;
                gfx.DrawLine(pen, new XPoint(lineHorizontalStrat + leftmargin, currentYposion_values - 10), new XPoint(lineHorizontalEnd + leftmargin, currentYposion_values - 10));
                for (int j = 0; j < dostawa.Count; j++)
                {

                    gfx.DrawString((j + 1).ToString(), font, XBrushes.Black, new XPoint(60 + leftmargin, currentYposion_values));
                    gfx.DrawString(dostawa[j].Item1, font, XBrushes.Black, new XPoint(90 + leftmargin, currentYposion_values));
                    gfx.DrawString(dostawa[j].Item2.ToString(), font, XBrushes.Black, new XPoint(180 + leftmargin, currentYposion_values));

                    gfx.DrawLine(pen, new XPoint(55 + leftmargin, currentYposion_values - 10), new XPoint(55 + leftmargin, currentYposion_values + 8.5));
                    gfx.DrawLine(pen, new XPoint(85 + leftmargin, currentYposion_values - 10), new XPoint(85 + leftmargin, currentYposion_values + 8.5));
                    gfx.DrawLine(pen, new XPoint(175 + leftmargin, currentYposion_values - 10), new XPoint(175 + leftmargin, currentYposion_values + 8.5));
                    gfx.DrawLine(pen, new XPoint(215 + leftmargin, currentYposion_values - 10), new XPoint(215 + leftmargin, currentYposion_values + 8.5));
                    currentYposion_values += 18;


                    gfx.DrawLine(pen, new XPoint(lineHorizontalStrat + leftmargin, currentYposion_values - 10), new XPoint(lineHorizontalEnd + leftmargin, currentYposion_values - 10));
                }
                return pdf;
            }
        }
    }
}
