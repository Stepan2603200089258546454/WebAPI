using System.ComponentModel.DataAnnotations;

namespace Web.Models.Request
{
    public class UpdateDrivingSchoolRequest
    {
        [Required] public int Id { get; set; }
        [Required] public string Name { get; set; }
        [Required] public string Adress { get; set; }
    }
}
