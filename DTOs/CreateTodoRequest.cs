using System.ComponentModel.DataAnnotations;

namespace DorisTodo.DTOs
{
    public class CreateTodoRequest
    {
        [Required]
        public string Content { get; set; }
    }
}
