using DataContext.Abstractions.Interfaces;
using DataContext.Models;

namespace DataContext.Abstractions.Models
{
    /// <summary>
    /// Назначенные должности
    /// </summary>
    public class Position : IDBEntity
    {
        public int Id { get; set; }
        public int IdDrivingSchool { get; set; }
        public DrivingSchool DrivingSchool { get; set; }
        public int IdRefPosition { get; set; }
        public RefPosition RefPosition { get; set; }
        public string IdUser { get; set; }
        public ApplicationUser User { get; set; }
        public int Salary { get; set; }
        public List<Havings> Havings { get; set; }
    }
}
