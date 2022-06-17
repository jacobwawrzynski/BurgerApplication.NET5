using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseContext.Entities
{
    /// <summary>
    /// Customer Entity
    /// </summary>
    public class Customer
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Code { get; set; }
    }
}
