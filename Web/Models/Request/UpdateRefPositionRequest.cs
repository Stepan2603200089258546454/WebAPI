using System.ComponentModel.DataAnnotations;

namespace Web.Models.Request
{
    public class UpdateRefPositionRequest
    {
        [Required] public int Id { get; set; }
        [Required] public string Name { get; set; }
        [Required] public int Salary { get; set; }
    }
}
