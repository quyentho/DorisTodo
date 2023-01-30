namespace DorisTodo.Models
{
    public class TodoItem
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public bool IsDone { get; set; } = false;
        public DateTime CreatedAt { get; set; }
        public DateTime? DoneAt { get; set; }
    }
}
