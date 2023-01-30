using System.ComponentModel.DataAnnotations;

namespace DorisTodo.DTOs
{
    public class UpdateTodoRequest
    {
        [Required]
        public string Content { get; set; }
    }
}
