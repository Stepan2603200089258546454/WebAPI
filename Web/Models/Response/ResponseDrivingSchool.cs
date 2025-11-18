using DataContext.Abstractions.Interfaces;
using DataContext.Abstractions.Models;

namespace Web.Models.Response
{
    public class ResponseDrivingSchool
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Adress { get; set; }
    }
}
