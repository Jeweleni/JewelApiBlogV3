﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.CommentDto
{
    public class UpdateCommentDto
    {
       
        public int Id { get; set; }
        public required string Content { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public required string UserId { get; set; }
    }
}
