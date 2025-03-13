using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Model
{
    public class User : IdentityUser
    {
        public required string FirstName { get; set; } // Use PascalCase for consistency
        public required string LastName { get; set; } // Use PascalCase for consistency
        //public DateOnly DateOfBirth { get; set; } // Use PascalCase for consistency
    }
}
