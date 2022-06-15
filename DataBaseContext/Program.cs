using System;
using System.Linq;
using Bogus;
using DataBaseContext.Entities;
namespace DataBaseContext
{
   internal class Program
   {
      static void Main(string[] args)
      {
         using (var db = new AppDbContext())
         {

            ////dodanie 30 pracowników
            //for (int i = 0; i < 30; i++)
            //    DataBase.AddStaffToDataBase(random.RandomStaff.Generate());

            ////dodanie 30 stałych klientów 
            //for (int i = 0; i < 30; i++)
            //    DataBase.AddCustomerToDataBase(random.RandomCustomer.Generate());

            //// dodanie 50 zamówień
            //for (int i = 0; i < 50; i++)
            //    DataBase.AddOrderToDataBase(random.RandomOrder.Generate());
            ////
            //for (int i = 0; i < 500; i++)
            //    DataBase.AddProduct_OrderToDataBase(random.RandomProduct_Order.Generate());

            var query = from p in db.Products
                        join po in db.Products_Orders
                         on p.Id equals po.Id_Product
                        join o in db.Orders
                         on po.Id_Order equals o.Id
                        join c in db.Customers
                         on o.Id_Customer equals c.Id
                        where c.Id == 15
                        select new { prod = p, orde = o, cust = c };

            Console.WriteLine("customer: " + query.First().cust.Email + " zamówił");
            decimal k = 0;
            foreach (var item in query)
            {
               k += item.prod.Price;
               Console.WriteLine("\t" + item.prod.Name);
            }
            Console.WriteLine($"\tza kwotę: {k:f2}");
         }
      }
   }
}
