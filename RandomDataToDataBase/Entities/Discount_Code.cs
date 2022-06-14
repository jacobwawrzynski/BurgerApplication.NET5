using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomDataToDataBase.Entities
{
    public class Discount_Code
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public int Percent { get; set; }
        public int Minimum_Order_Amount { get; set; }
        public int? Quantity { get; set; }

    }
}
