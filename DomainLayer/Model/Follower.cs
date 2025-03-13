using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Model
{
    public class Follower : BaseModel
    {
        [ForeignKey("UserId")]
        public required User user { get; set; }
        public required string UserId { get; set; }


    }
}
