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
            //for (int i = 0; i < 5000; i++)
            //{
            //    DataBaseQuery.AddOrderToDataBase(random.RandomOrder.Generate());
            //}
            //for (int i = 0; i < 13000; i++)
            //{
            //    DataBaseQuery.AddProduct_OrderToDataBase(random.RandomProduct_Order.Generate());
            //}

            #region invoice
            //Order order;
            //using (var db = new AppDbContext())
            //{
            //    order = (from o in db.Orders
            //             where o.Id == 1
            //             select o).FirstOrDefault();
            //}
            //MyPdf.InvoiceMenager invoice = new MyPdf.InvoiceMenager(order);

            //var iv = DataBaseQuery.DownloadInvoice().Last();
            //var pdf3 = MyPdf.InvoiceMenager.ByteArrayToPdf(iv.File);
            //MyPdf.InvoiceMenager.SavePdf(pdf3, @$"C:\Users\jasie\Desktop\testplz\{iv.Id}-{iv.Date.Day}-{iv.Date.Month}-{iv.Date.Year}.pdf");

            //using (var db = new AppDbContext())
            //{
            //    order = (from o in db.Orders
            //             where o.Id == 1
            //             select o).FirstOrDefault();
            //}
            #endregion

            #region report
            //Restaurant restaurant;
            //using (var db = new AppDbContext())
            //{
            //    restaurant = (from r in db.Restaurants
            //                  where r.Id == 2
            //                  select r).FirstOrDefault();
            //}
            //ReportMenager reportMenager = new ReportMenager(new DateTime(2022,03,14),restaurant);
            //var rap = reportMenager.GeneratePdf();
            //Report report = new Report() { File = PdfMenager.PdfToByteArray(rap) };
            //DataBaseQuery.AddReportsToDataBase(report);

            //var repfromdb = PdfMenager.ByteArrayToPdf(DataBaseQuery.DownloadReportsAt(2).File);

            //PdfMenager.SavePdf(repfromdb, @$"C:\Users\jasie\Desktop\testplz", "raporcikzbazy");
            //PdfMenager.SavePdf(rap, @$"C:\Users\jasie\Desktop\testplz","raporcik");
            #endregion

            #region Delivery
            Restaurant restaurant;
            using (var db = new AppDbContext())
            {
                restaurant = (from r in db.Restaurants
                              where r.Id == 2
                              select r).FirstOrDefault();
            }
            DeliveryMenager delivery = new DeliveryMenager(restaurant);
            delivery.dodaj("bułki", 100);
            delivery.dodaj("Ser", 1);
            delivery.dodaj("Nachosy", 2);
            delivery.dodaj("Warzywa", 3);

            var pdf = delivery.GeneratePdf();

            PdfMenager.SavePdf(pdf, @$"C:\Users\jasie\Desktop\testplz", "dostawa");
            #endregion
        }
    }

}