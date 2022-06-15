using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseContext.Entities
{
    public class Report
    {
        public int Id { get; set; }
        public byte[] File { get; set; }
        public DateTime Date { get; set; }
    }
}
