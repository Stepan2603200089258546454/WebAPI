using DataContext.Abstractions.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContext.Abstractions.Models
{
    public class ApplicationUser : IdentityUser
    {
        public int? IdPosition { get; set; }
        public Position? Position { get; set; }
    }
}
