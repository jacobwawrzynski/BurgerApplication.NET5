using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseContext.Entities
{
    /// <summary>
    /// Order Entity
    /// </summary>
    public class Order
    {
        public int Id { get; set; }
        public int Id_Staff { get; set; }
        public int? Id_Customer { get; set; }
        public int? Id_Discount_Code { get; set; }
      [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
      public DateTime Date { get; set; }

        public List<Product_Order> Products_Orders { get; set; }
    }
}
