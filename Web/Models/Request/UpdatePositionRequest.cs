using System.ComponentModel.DataAnnotations;

namespace Web.Models.Request
{
    public class UpdatePositionRequest
    {
        [Required] public int Id { get; set; }
        [Required] public int IdDrivingSchool { get; set; }
        [Required] public int IdRefPosition { get; set; }
        [Required] public string IdUser { get; set; }
        [Required] public int Salary { get; set; }
    }
}
