using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Model
{
    public class Like : BaseModel
    {
        [ForeignKey("PostId")]
        public required Post post { get; set; }
        public int PostId { get; set; }

        [ForeignKey("UserId")]
        public required User user { get; set; }
        public required string UserId { get; set; }
    }
}
