using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.UserDto
{
    public class UserDto
    {
      public required string Id { get; set; }
        
        public required string email { get; set; }
        
        public required string firstName { get; set; }
        
        public required string lastName { get; set; }
    
    }
}
