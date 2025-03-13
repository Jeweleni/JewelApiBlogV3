using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Model
{
    public class Post : BaseModel
    {
        public required string Title { get; set; }

        //setting the table to varchar in the database
        [Column(TypeName = "varchar(1000)")]
        public required string Content { get; set; }

        [ForeignKey("UserId")]
        public required User user { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public required string UserId { get; set; }
    }
}
