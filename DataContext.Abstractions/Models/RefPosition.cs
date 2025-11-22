using DataContext.Abstractions.Interfaces;

namespace DataContext.Abstractions.Models
{
    /// <summary>
    /// Справочник должностей
    /// </summary>
    public class RefPosition : IDBEntity
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int StandardSalary { get; set; }
        public List<Position> Positions { get; set; } = [];
    }
}
