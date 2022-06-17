using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseContext.Entities
{
    /// <summary>
    /// Discount_Code Entity
    /// </summary>
    public class Discount_Code
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public int Percent { get; set; }
        public int Minimum_Order_Amount { get; set; }
        public int? Quantity { get; set; }

    }
}
