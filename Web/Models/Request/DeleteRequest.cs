using System.ComponentModel.DataAnnotations;

namespace Web.Models.Request
{
    public class DeleteRequest
    {
        [Required] public int Id { get; set; }
    }
}
