using System;
using System.Linq;
using Bogus;
using DataBaseContext.Entities;
using DataBaseContext.random;
using DataBaseContext.MyPdf;

namespace DataBaseContext
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //for (int i = 0; i < 20; i++)
            //{
            //    DataBaseQuery.AddStaffToDataBase(random.RandomStaff.Generate());
            //}
            //for (int i = 0; i < 50; i++)
            //{
            //    DataBaseQuery.AddCustomerToDataBase(random.RandomCustomer.Generate());
            //}
            //for (int i = 0; i < 8000; i++)
            //{
            //    DataBaseQuery.AddOrderToDataBase(random.RandomOrder.Generate());
            //}
            //for (int i = 0; i < 20000; i++)
            //{
            //    DataBaseQuery.AddProduct_OrderToDataBase(random.RandomProduct_Order.Generate());
            //}

            //Staff staff = random.RandomStaff.Generate();
            //staff.Login = "Admin";
            //staff.Password = Security.Encryption.ComputeHash("Admin", "SHA512", null);
            //staff.Role = "Manager";

            //DataBaseQuery.AddStaffToDataBase(staff);

            #region invoice
            //Order order;
            //using (var db = new AppDbContext())
            //{
            //    order = (from o in db.Orders
            //             where o.Id == 69
            //             select o).FirstOrDefault();
            //}

            //InvoiceMenager invoiceMenager = new InvoiceMenager(order);

            //Invoice invoice = new Invoice() { File = PdfMenager.PdfToByteArray(invoiceMenager.GeneratePdf()), Id_Restaurant = invoiceMenager.restaurant.Id };

            //DataBaseQuery.AddInvoiceToDataBase(invoice);

            //var iv = DataBaseQuery.DownloadInvoices().Last();

            //PdfMenager.SavePdf(iv.File, @$"C:\Users\jasie\Desktop\testplz\{iv.Id}-{iv.Date.Day}-{iv.Date.Month}-{iv.Date.Year}.pdf");
            #endregion

            #region report
            //Restaurant restaurant;
            //using (var db = new AppDbContext())
            //{
            //    restaurant = (from r in db.Restaurants
            //                  where r.Id == 1
            //                  select r).FirstOrDefault();
            //}
            //ReportMenager reportMenager = new ReportMenager(new DateTime(2022, 03, 14), restaurant);
            //var rap = reportMenager.GeneratePdf();
            //Report report = new Report() { File = PdfMenager.PdfToByteArray(rap), Id_Restaurant = reportMenager.restaurant.Id };
            //DataBaseQuery.AddReportsToDataBase(report);

            //var repfromdb = PdfMenager.ByteArrayToPdf(DataBaseQuery.DownloadReports().Last().File);

            //PdfMenager.SavePdf(repfromdb, @$"C:\Users\jasie\Desktop\testplz", "raporcikzbazy");
            ////PdfMenager.SavePdf(rap, @$"C:\Users\jasie\Desktop\testplz", "raporcik");
            #endregion

            #region Delivery
            //Restaurant restaurant2;
            //using (var db = new AppDbContext())
            //{
            //    restaurant2 = (from r in db.Restaurants
            //                  where r.Id == 2
            //                  select r).FirstOrDefault();
            //}
            //DeliveryMenager delivaryMenager = new DeliveryMenager(restaurant2);
            //delivaryMenager.Add("bułki", 100);
            //delivaryMenager.Add("Ser", 1);
            //delivaryMenager.Add("Nachosy", 2);
            //delivaryMenager.Add("Warzywa", 3);

            //var pdf = delivaryMenager.GeneratePdf();

            //Delivery delivery = new Delivery() { File = PdfMenager.PdfToByteArray(pdf), Id_Restaurant = delivaryMenager.restaurant.Id };
            //DataBaseQuery.AddDeliveryToDataBase(delivery);

            var pdfzbazy = DataBaseQuery.DownloadDelivery().Last().File;
            PdfMenager.SavePdf(pdfzbazy, @$"C:\Users\jasie\Desktop\testplz", "dostawaZaplikacji");
            #endregion

            #region image

            //System.Drawing.Image image = System.Drawing.Image.FromFile(@"C:\Users\jasie\Desktop\brgmain.png");

            //DataBaseQuery.AddImageToDataBase(new Entities.Image() { ImageData = PdfMenager.ImageToByteArray(image),Alt_Text="burger.png" });
            #endregion
        }
    }

}