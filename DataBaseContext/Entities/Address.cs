using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseContext.Entities
{
    public class Address
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string Zip_Code { get; set; }
        public string? Street { get; set; }
        public string House_Number { get; set; }
        public string? Apartment_Number { get; set; }
    }
}
