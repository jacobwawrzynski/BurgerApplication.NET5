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
    /// <summary>
    /// Invoice Menager
    /// </summary>
    public class InvoiceMenager
    {
        public string name = "";
        Address address;
        private decimal discount;
        Order order;
        Staff owner;
        Dictionary<Product, int> products = new Dictionary<Product, int>();
        Staff staff;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="order">order</param>
        public InvoiceMenager(Order order)
        {
            this.order = order;
            Initialize();
        }

        public Restaurant restaurant { get; private set; }
        /// <summary>
        /// create invoice pdf file
        /// </summary>
        /// <returns>pdf file</returns>
        /// <exception cref="Exception"></exception>
        public PdfDocument GeneratePdf()
        {
            if (products.Count == 0) throw new Exception("there is no products in order");
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            using (PdfDocument pdf = new PdfDocument())
            {

                int pages = products.Count / 20 + 1;
                int count = products.Count;
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

                    gfx.DrawString($"{address.Street} {address.House_Number} {address.Apartment_Number}" +
                    $",{address.Zip_Code} {address.City}", boldFont, XBrushes.Black, new XPoint(50, 30));

                    gfx.DrawString($"{owner.Name} {owner.Last_Name}", boldFont, XBrushes.Black, new XPoint(50, 45));

                    gfx.DrawString($"Data wystawnienia", boldFont, XBrushes.Black, new XPoint(495, 30));
                    gfx.DrawString($"{order.Date.ToString("d")}", boldFont, XBrushes.Black, new XPoint(495, 45));
                    currentYposion_values = 150;
                    gfx.DrawString($"Faktura nr {this.name}", TitfleFont, XBrushes.Black, new XPoint(200, currentYposion_values));
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
                    var keys = products.Keys.ToList();
                    for (int j = 0; j < c; j++)
                    {
                        decimal brutto = (decimal)keys[i * 20 + j].Price * (decimal)products[keys[i * 20 + j]] * (1 - (discount / 100));
                        decimal netto = (brutto * (decimal)(0.92));
                        totalBrutto += brutto;
                        totalNetto += netto;
                        totalVat += (brutto - netto);
                        gfx.DrawString((i * 20 + j + 1).ToString(), font, XBrushes.Black, new XPoint(20, currentYposion_values));
                        gfx.DrawString(keys[i * 20 + j].Name, font, XBrushes.Black, new XPoint(50, currentYposion_values));
                        gfx.DrawString(products[keys[i * 20 + j]].ToString(), font, XBrushes.Black, new XPoint(145, currentYposion_values));
                        gfx.DrawString(discount.ToString(), font, XBrushes.Black, new XPoint(185, currentYposion_values));
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

                gfx.DrawString(($"Wystawił:").ToString(), font, XBrushes.Black, new XPoint(50, currentYposion_values + 70));
                gfx.DrawString(($"{staff.Name} {staff.Last_Name}").ToString(), font, XBrushes.Black, new XPoint(50, currentYposion_values + 90));
                gfx.DrawString(($"Odebrał:").ToString(), font, XBrushes.Black, new XPoint(500, currentYposion_values + 70));
                return pdf;
            }

        }

        private void Initialize()
        {
            List<Product> p = DataBaseQuery.DownloadProductsFromOrder(order);
            foreach (var item in p)
            {
                if (products.TryAdd(item, 1))
                    continue;
                else
                    products[item]++;
            }
            int numer;
            using (var db = new AppDbContext())
            {
                discount = (from dc in db.Discount_Codes
                            where dc.Id == order.Id_Discount_Code
                            select dc.Percent).FirstOrDefault();
                numer = (from i in db.Invoices
                         where i.Date == order.Date
                         select i.Id).Count();
                staff = (from s in db.Staff
                         where s.Id == order.Id_Staff
                         select s).FirstOrDefault();
                owner = (from s in db.Staff
                         where (s.Id_Restaurant == staff.Id_Restaurant && s.Role == "Owner")
                         select s).FirstOrDefault();
                restaurant = (from r in db.Restaurants
                              where r.Id == order.Id_Staff
                              select r).FirstOrDefault();
                address = (from a in db.Addresses
                           where a.Id == order.Id_Staff
                           select a).FirstOrDefault();
                numer = (from i in db.Invoices
                         select i).Count();
            }
            name = $"{numer}/{order.Date.Day}/{order.Date.Month}/{order.Date.Year}";

            //var pdf = GeneratePdf();
            //Entities.Invoice invoice = new Entities.Invoice();
            //invoice.File = PdfMenager.PdfToByteArray(pdf);
            //DataBaseQuery.AddInvoiceToDataBase(invoice);

        }
    }
}
