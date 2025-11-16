using System.ComponentModel.DataAnnotations;

namespace Web.Models.Request
{
    public class GetRequest
    {
        [Required] public int Page { get; set; }
        [Required] public int PageSize { get; set; }
    }
}
