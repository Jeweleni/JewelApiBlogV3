using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Model
{
    public class Comment : BaseModel
    {
        //setting the table to varchar in the database
        [Column(TypeName = "varchar(200)")]

        public required string Author { get; set; }

        public required string Content { get; set; }

        [ForeignKey("PostId")]
        public required Post post { get; set; }
        public int PostId { get; set; }

        [ForeignKey("UserId")]
        public required User user { get; set; }
        public required string UserId { get; set; }
    }
}
