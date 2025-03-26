namespace TodoListAPI.Models
{
    public class TodoItem
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime DueDate { get; set; }
        public string Status { get; set; } = "Not Started";

        // Optional fields
        public int Priority { get; set; } //Priority 1-5, 1=Lowest, 5 Highest
        public string? Category { get; set; }
    }
}
