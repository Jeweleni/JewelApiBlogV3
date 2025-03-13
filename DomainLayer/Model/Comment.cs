using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainLayer.Model
{
    public class Comment : BaseModel
    {
        // Setting the column type to VARCHAR(200) in the database
        [Column(TypeName = "varchar(200)")]
        //public required string Author { get; set; }

        public required string Content { get; set; }

        // Foreign key for Post
        public int PostId { get; set; }

        [ForeignKey("PostId")]
        public required Post Post { get; set; } // Updated property name to PascalCase

        // Foreign key for User (Identity)
        public required string UserId { get; set; }

        [ForeignKey("UserId")]
        public required User User { get; set; } // Updated property name to PascalCase
    }
}
