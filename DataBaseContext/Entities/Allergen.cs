using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseContext.Entities
{
    /// <summary>
    /// Allergen Entity
    /// </summary>
    public class Allergen
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Product_Allergen> Products_Allergens { get; set; }
    }
}
