using DataContext.Abstractions.Interfaces;

namespace DataContext.Abstractions.Models
{
    /// <summary>
    /// Имущество
    /// </summary>
    public class Havings : IDBEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int IdPosition { get; set; }
        public Position Position { get; set; }
    }
}
