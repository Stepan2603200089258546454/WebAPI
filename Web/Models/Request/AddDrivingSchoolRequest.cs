using System.ComponentModel.DataAnnotations;

namespace Web.Models.Request
{
    public class AddDrivingSchoolRequest
    {
        [Required] public string Name { get; set; }
        [Required] public string Adress { get; set; }
    }
}
