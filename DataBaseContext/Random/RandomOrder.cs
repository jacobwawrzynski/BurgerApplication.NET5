using Bogus;
using DataBaseContext.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseContext.random
{
    public static class RandomOrder
    {
        public static Entities.Order Generate()
        {
            var faker = new Faker("pl");
            var order = new Entities.Order();

            using (var db = new AppDbContext())
            {
                var customers = (from c in db.Customers
                                 orderby c.Id
                                 select c.Id).ToList();
                var staff = (from s in db.Staff
                             orderby s.Id
                             select s.Id).ToList();
                var discount_codes = (from dc in db.Discount_Codes
                                      orderby dc.Id
                                      select dc).ToList();
                int idStaff = faker.Random.Int(0, staff.Count - 1);
                int idCustomer = faker.Random.Int(0, customers.Count - 1);
                int idDiscount_code = faker.Random.Int(0, discount_codes.Count - 1);
                order.Id_Staff = staff[idStaff];
                if (customers.Count != 0)
                    order.Id_Customer = customers[idCustomer].OrNull(faker, .7f);
                if (discount_codes.Count != 0 && (discount_codes[idDiscount_code].Quantity == null || discount_codes[idDiscount_code].Quantity > 0))
                order.Id_Discount_Code = discount_codes[idDiscount_code].Id.OrNull(faker, .7f);
            }
            order.Date = faker.Date.Past(1, DateTime.Now);

            return order;
        }
    }
}
