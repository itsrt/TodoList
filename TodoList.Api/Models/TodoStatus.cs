namespace TodoList.Api.Models
{
    /// <summary>
    /// Status of the Task
    /// </summary>
    public sealed class TodoStatus
    {
        public string Code { get; }

        public string ShortDescription { get; }

        public string LongDescription { get; }

        private TodoStatus(string code,
                           string shortDescription,
                           string longDescription)
        {
            Code = code;
            ShortDescription = shortDescription;
            LongDescription = longDescription;
        }

        public static readonly TodoStatus Defined = new(
            "DEF", "DEFINED", "Identified Task to be performed");

        public static readonly TodoStatus InProgress = new(
            "INP", "IN PROGRESS", "Task is in progress");

        public static readonly TodoStatus OnHold = new(
            "HLD", "ON HOLD", "Task is on hold");

        public static readonly TodoStatus Completed = new(
            "COM", "COMPLETED", "Task is completed");

        public static readonly TodoStatus ReOpened = new(
            "REO", "RE OPEN", "Task is reopen");


    }
}
