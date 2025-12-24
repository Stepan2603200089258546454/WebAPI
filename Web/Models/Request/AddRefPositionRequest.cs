using System.ComponentModel.DataAnnotations;

namespace Web.Models.Request
{
    public class AddRefPositionRequest
    {
        [Required] public string Name { get; set; }
        [Required] public int Salary { get; set; }
    }
}
