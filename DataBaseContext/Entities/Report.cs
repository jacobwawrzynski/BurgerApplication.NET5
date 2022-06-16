using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseContext.Entities
{
    public class Report
    {
        public int Id { get; set; }
        public byte[] File { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime Date { get; set; }
        public int Id_Restaurant { get; set; }
    }
}
