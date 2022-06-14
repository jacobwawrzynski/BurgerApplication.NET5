using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomDataToDataBase.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Decimal Price { get; set; }
        public string? Description { get; set; }
        public List<Product_Allergen> Products_Allergens { get; set; }
        public List<Product_Order> Products_Orders { get; set; }

    }
}
