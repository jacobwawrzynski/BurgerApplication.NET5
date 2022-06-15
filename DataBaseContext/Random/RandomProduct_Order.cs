using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using DataBaseContext.Entities;

namespace DataBaseContext.random
{
    public static class RandomProduct_Order
    {
        static public Entities.Product_Order Generate()
        {
            var faker = new Faker("pl");
            var product_Order = new Entities.Product_Order();

            using (var db = new AppDbContext())
            {
                var orders = (from o in db.Orders
                                 orderby o.Id
                                 select o).ToList();
                var order = orders[faker.Random.Int(0,orders.Count-1)];

                var products = (from p in db.Products
                              orderby p.Id
                              select p).ToList();
                var product = products[faker.Random.Int(0, products.Count - 1)];

                //product_Order.Order = order;
                product_Order.Id_Order = order.Id;
                //product_Order.Product = product;
                product_Order.Id_Product = product.Id;
            }

            return product_Order;
        }

    }
}
