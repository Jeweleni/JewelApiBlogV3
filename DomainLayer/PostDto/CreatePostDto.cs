using DomainLayer.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.PostDto
{
    public class CreatePostDto
    {
        public required string Title { get; set; }
        public required string Content { get; set; }
        public required string UserId { get; set; }

    }
}
