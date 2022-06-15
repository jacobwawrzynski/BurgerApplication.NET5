using System;
using System.Linq;
using Bogus;
using DataBaseContext.Entities;
using DataBaseContext.random;
namespace DataBaseContext
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //for (int i = 0; i < 100; i++)
            //{
            //    DataBaseQuery.AddProduct_OrderToDataBase(random.RandomProduct_Order.Generate());
            //}

            Order order;
            using (var db = new AppDbContext())
            {
                order = (from o in db.Orders
                         where o.Id == 1
                         select o).FirstOrDefault();
            }
            MyPdf.InvoiceMenager invoice = new MyPdf.InvoiceMenager(order);

            var iv = DataBaseQuery.DownloadInvoice().Last();
            var pdf3 = MyPdf.InvoiceMenager.ByteArrayToPdf(iv.File);
            MyPdf.InvoiceMenager.SavePdf(pdf3,@$"C:\Users\jasie\Desktop\testplz\{iv.Id}-{iv.Date.Day}-{iv.Date.Month}-{iv.Date.Year}.pdf");

        }
    }
}