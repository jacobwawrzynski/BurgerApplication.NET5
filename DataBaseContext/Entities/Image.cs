using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseContext.Entities
{
    /// <summary>
    /// Image Entity
    /// </summary>
    public class Image
    {

        public int Id { get; set; }
        [Column(TypeName = "image")]
        public byte[] ImageData { get; set; }
        public string Alt_Text { get; set; }
    }
}
