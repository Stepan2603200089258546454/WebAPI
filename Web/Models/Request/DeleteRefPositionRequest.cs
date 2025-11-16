using System.ComponentModel.DataAnnotations;

namespace Web.Models.Request
{
    public class DeleteRefPositionRequest
    {
        [Required] public int Id { get; set; }
    }
}
