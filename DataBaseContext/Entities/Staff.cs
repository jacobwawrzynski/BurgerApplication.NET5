using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseContext.Entities
{
    public class Staff
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string Name { get; set; }
        public string Last_Name { get; set; }
        public string Pesel { get; set; }
        public string Email { get; set; }

      [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
      public DateTime Creation_Date { get; set; }
        public DateTime? Deletion_Date { get; set; }
        public int Id_Address { get; set; }
        public int Id_Restaurant { get; set; }
    }
}
