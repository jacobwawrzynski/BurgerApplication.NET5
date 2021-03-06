using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseContext.Entities
{
    /// <summary>
    /// Product_Order Entity
    /// </summary>
    public class Product_Order
    {
        public int Id { get; set; }
        public int Id_Product { get; set; }
        public Product Product { get; set; }
        public int Id_Order { get; set; }
        public Order Order { get; set; }
    }
}
