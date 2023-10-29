using Planner.Data.Models.Enums;

namespace Planner.Data.Models;

public class TaskModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime Deadline { get; set; }
    public Priority Priority { get; set; }
    public Status Status { get; set; } = Status.InProgress;
}