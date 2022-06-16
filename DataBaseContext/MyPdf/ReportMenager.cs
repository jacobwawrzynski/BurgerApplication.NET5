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
    public class ReportMenager
    {
        DateTime date;
        Restaurant restaurant;
        Dictionary<(Entities.Product, decimal), int> ProdDis;
        public ReportMenager(DateTime date, Restaurant restaurant)
        {
            this.date = date;
            this.restaurant = restaurant;
            Initialize();
        }

        private void Initialize()
        {
            ProdDis = new Dictionary<(Product, decimal), int>();
            var orders = new List<Order>();
            using (var db = new AppDbContext())
            {
                orders = (from o in db.Orders
                          join s in db.Staff on o.Id_Staff equals s.Id
                          where (o.Date.Year == date.Year && o.Date.Month == date.Month && o.Date.Day == date.Day && s.Id_Restaurant == restaurant.Id)
                          select o).ToList();

            }
            foreach (var o in orders)
            {
                using (var db = new AppDbContext())
                {
                    var prod = (from p in db.Products
                                join po in db.Products_Orders on p.Id equals po.Id_Product
                                where po.Id_Order == o.Id
                                select p).ToList();
                    var orderDis = (from d in db.Discount_Codes
                                    where d.Id == o.Id_Discount_Code
                                    select d.Percent).FirstOrDefault();
                    foreach (var pr in prod)
                    {
                        if (ProdDis.TryAdd((pr, orderDis), 1))
                            continue;
                        else
                            ProdDis[(pr, orderDis)]++;
                    }
                }
            }


        }


        public PdfDocument GeneratePdf()
        {
            if (ProdDis == null) throw new NullReferenceException();
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            using (PdfDocument pdf = new PdfDocument())
            {

                int pages = ProdDis.Count / 20 + 1;
                int count = ProdDis.Count;
                decimal totalBrutto = 0;
                decimal totalVat = 0;
                decimal totalNetto = 0;
                int currentYposion_values = 0;
                int lineHorizontalStrat = 15;
                int lineHorizontalEnd = 570;
                PdfPage page;
                XGraphics gfx = null;
                XFont font = new XFont("Arial", 10);

                XFont boldFont = new XFont("Arial", 10, XFontStyle.Bold);

                XFont TitfleFont = new XFont("Arial", 18, XFontStyle.Bold);
                XPen pen = new XPen(XColor.FromArgb(0, 0, 0));
                for (int i = 0; i < pages; i++)
                {
                    page = pdf.AddPage();
                    gfx = XGraphics.FromPdfPage(page);

                    currentYposion_values = 150;
                    gfx.DrawString($"Raport z dnia {date.ToString("D")} z restauracji nr{restaurant.Id}", TitfleFont, XBrushes.Black, new XPoint(70, currentYposion_values));
                    currentYposion_values += 30;
                    gfx.DrawLine(pen, new XPoint(lineHorizontalStrat, currentYposion_values - 10), new XPoint(lineHorizontalEnd, currentYposion_values - 10));

                    gfx.DrawLine(pen, new XPoint(15, currentYposion_values - 10), new XPoint(15, currentYposion_values + 10));
                    gfx.DrawLine(pen, new XPoint(45, currentYposion_values - 10), new XPoint(45, currentYposion_values + 10));
                    gfx.DrawLine(pen, new XPoint(135, currentYposion_values - 10), new XPoint(135, currentYposion_values + 10));
                    gfx.DrawLine(pen, new XPoint(175, currentYposion_values - 10), new XPoint(175, currentYposion_values + 10));
                    gfx.DrawLine(pen, new XPoint(245, currentYposion_values - 10), new XPoint(245, currentYposion_values + 10));
                    gfx.DrawLine(pen, new XPoint(295, currentYposion_values - 10), new XPoint(295, currentYposion_values + 10));
                    gfx.DrawLine(pen, new XPoint(395, currentYposion_values - 10), new XPoint(395, currentYposion_values + 10));
                    gfx.DrawLine(pen, new XPoint(495, currentYposion_values - 10), new XPoint(495, currentYposion_values + 10));
                    gfx.DrawLine(pen, new XPoint(570, currentYposion_values - 10), new XPoint(570, currentYposion_values + 10));
                    gfx.DrawString("Lp", boldFont, XBrushes.Black, new XPoint(20, currentYposion_values));

                    gfx.DrawString("Nazwa", boldFont, XBrushes.Black, new XPoint(50, currentYposion_values));

                    gfx.DrawString("Ilość", boldFont, XBrushes.Black, new XPoint(140, currentYposion_values));

                    gfx.DrawString("Rabat[%]", boldFont, XBrushes.Black, new XPoint(180, currentYposion_values));

                    gfx.DrawString("Vat[%]", boldFont, XBrushes.Black, new XPoint(250, currentYposion_values));

                    gfx.DrawString("Wartość netto", boldFont, XBrushes.Black, new XPoint(300, currentYposion_values));

                    gfx.DrawString("Wartość vat", boldFont, XBrushes.Black, new XPoint(400, currentYposion_values));

                    gfx.DrawString("Wartość brutto", boldFont, XBrushes.Black, new XPoint(500, currentYposion_values));

                    currentYposion_values += 20;
                    gfx.DrawLine(pen, new XPoint(lineHorizontalStrat, currentYposion_values - 10), new XPoint(lineHorizontalEnd, currentYposion_values - 10));
                    int c = count > 20 ? 20 : count;
                    var keys = ProdDis.Keys.ToList();
                    for (int j = 0; j < c; j++)
                    {
                        decimal brutto = (decimal)keys[i * 20 + j].Item1.Price * (decimal)ProdDis[keys[i * 20 + j]] * (1 - ((decimal)keys[i * 20 + j].Item2 / 100));
                        decimal netto = (brutto * (decimal)(0.92));
                        totalBrutto += brutto;
                        totalNetto += netto;
                        totalVat += (brutto - netto);

                        gfx.DrawString((i * 20 + j + 1).ToString(), font, XBrushes.Black, new XPoint(20, currentYposion_values));
                        gfx.DrawString(keys[i * 20 + j].Item1.Name, font, XBrushes.Black, new XPoint(50, currentYposion_values));
                        gfx.DrawString(ProdDis[keys[i * 20 + j]].ToString(), font, XBrushes.Black, new XPoint(145, currentYposion_values));
                        gfx.DrawString(keys[i * 20 + j].Item2.ToString(), font, XBrushes.Black, new XPoint(185, currentYposion_values));
                        gfx.DrawString("8", font, XBrushes.Black, new XPoint(255, currentYposion_values));
                        gfx.DrawString($"{netto:f2}", font, XBrushes.Black, new XPoint(300, currentYposion_values));
                        gfx.DrawString(($"{(brutto - netto):f2}").ToString(), font, XBrushes.Black, new XPoint(400, currentYposion_values));
                        gfx.DrawString(($"{brutto:f2}").ToString(), font, XBrushes.Black, new XPoint(500, currentYposion_values));
                        count--;
                        gfx.DrawLine(pen, new XPoint(15, currentYposion_values - 10), new XPoint(15, currentYposion_values + 8.5));
                        gfx.DrawLine(pen, new XPoint(45, currentYposion_values - 10), new XPoint(45, currentYposion_values + 8.5));
                        gfx.DrawLine(pen, new XPoint(135, currentYposion_values - 10), new XPoint(135, currentYposion_values + 8.5));
                        gfx.DrawLine(pen, new XPoint(175, currentYposion_values - 10), new XPoint(175, currentYposion_values + 8.5));
                        gfx.DrawLine(pen, new XPoint(245, currentYposion_values - 10), new XPoint(245, currentYposion_values + 8.5));
                        gfx.DrawLine(pen, new XPoint(295, currentYposion_values - 10), new XPoint(295, currentYposion_values + 8.5));
                        gfx.DrawLine(pen, new XPoint(395, currentYposion_values - 10), new XPoint(395, currentYposion_values + 8.5));
                        gfx.DrawLine(pen, new XPoint(495, currentYposion_values - 10), new XPoint(495, currentYposion_values + 8.5));
                        gfx.DrawLine(pen, new XPoint(570, currentYposion_values - 10), new XPoint(570, currentYposion_values + 8.5));
                        currentYposion_values += 18;


                        gfx.DrawLine(pen, new XPoint(lineHorizontalStrat, currentYposion_values - 10), new XPoint(lineHorizontalEnd, currentYposion_values - 10));
                    }


                }
                gfx.DrawString("Suma:", font, XBrushes.Black, new XPoint(250, currentYposion_values));
                gfx.DrawString($"{totalNetto:f2}", font, XBrushes.Black, new XPoint(300, currentYposion_values));
                gfx.DrawString(($"{(totalVat):f2}").ToString(), font, XBrushes.Black, new XPoint(400, currentYposion_values));
                gfx.DrawString(($"{totalBrutto:f2}").ToString(), font, XBrushes.Black, new XPoint(500, currentYposion_values));
                gfx.DrawLine(pen, new XPoint(245, currentYposion_values - 10), new XPoint(245, currentYposion_values + 8.5));
                gfx.DrawLine(pen, new XPoint(295, currentYposion_values - 10), new XPoint(295, currentYposion_values + 8.5));
                gfx.DrawLine(pen, new XPoint(395, currentYposion_values - 10), new XPoint(395, currentYposion_values + 8.5));
                gfx.DrawLine(pen, new XPoint(495, currentYposion_values - 10), new XPoint(495, currentYposion_values + 8.5));
                gfx.DrawLine(pen, new XPoint(570, currentYposion_values - 10), new XPoint(570, currentYposion_values + 8.5));
                gfx.DrawLine(pen, new XPoint(245, currentYposion_values + 8), new XPoint(lineHorizontalEnd, currentYposion_values + 8));
                return pdf;
            }
        }
    }
}
