using DataContext.Abstractions.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContext.Abstractions.Models
{
    /// <summary>
    /// Автошкола
    /// </summary>
    public class DrivingSchool : IDBEntity
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Adress { get; set; }
        public List<Position> Positions { get; set; } = [];
    }
}
