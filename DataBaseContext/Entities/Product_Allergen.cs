using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseContext.Entities
{
    public class Product_Allergen
    {
        public int Id { get; set; }
        public int Id_Product { get; set; }
        public Product Product { get; set; }
        public int Id_Allergen { get; set; }
        public Allergen Allergen { get; set; }
    }
}
