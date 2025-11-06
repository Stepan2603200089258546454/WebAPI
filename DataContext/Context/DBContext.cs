using DataContext.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContext.Context
{
    public class DBContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }
    }
}
