using Planner.Data.Models;

namespace Planner.Data;

public interface IStorage
{
    public void Save(IList<TaskModel> tasks);
    public List<TaskModel> GetAllTasks();
}