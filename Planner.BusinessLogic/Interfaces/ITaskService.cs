using Planner.Data.Models;

namespace Planner.BusinessLogic.Interfaces;

public interface ITaskService
{
    public IList<TaskModel> GetAllTasks();
    public void AddTask(TaskModel task);

    public TaskModel EditTask(TaskModel task);
    
    public void DeleteTask(TaskModel task);
}