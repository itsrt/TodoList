namespace TodoList.Api.Models
{
    /// <summary>
    /// Uniquely identifiable task to be completed
    /// </summary>
    public class TodoItem
    {
        /// <summary>
        /// Unique identifier for task
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Task Header
        /// </summary>
        public string Header { get; set; } = string.Empty;

        /// <summary>
        /// Detail description on task 
        /// </summary>
        public string? Detail { get; set; }

        /// <summary>
        /// Status of the Task
        /// </summary>
        //public TodoStatus Status { get; set; } = TodoStatus.Defined;

        /// <summary>
        /// Date Time when Task created
        /// </summary>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Date Time when Task needs to be finished
        /// </summary>
        public DateOnly? CompletedBefore { get; set; }

    }
}
