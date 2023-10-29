using Microsoft.Extensions.Logging;
using Planner.BusinessLogic.Interfaces;
using Planner.Data;
using Planner.Data.Models;

namespace Planner.BusinessLogic;

public class TaskService : ITaskService
{
    private static IList<TaskModel> _taskList;
    private readonly ILogger<TaskService> _logger;
    private readonly IStorage _storage;

    public TaskService(ILogger<TaskService> logger, IStorage storage)
    {
        _logger = logger;
        _storage = storage;
        _taskList = storage.GetAllTasks();
    }

    public IList<TaskModel> GetAllTasks()
    {
        return _taskList;
    }

    public void AddTask(TaskModel task)
    {
        int newId;
        if (_taskList.Count == 0)
        {
            newId = 1;
        }
        else
        {
            newId = _taskList.Max(t => t.Id) + 1;
        }

        task.Id = newId;
        _taskList.Add(task);
        _storage.Save(_taskList);
        _logger.LogInformation("Task '{Name}' added.", task.Name);
    }

    public TaskModel EditTask(TaskModel task)
    {
        var existingTask = _taskList.FirstOrDefault(t => t.Id == task.Id);
        if (existingTask == null) throw new InvalidOperationException("Задача с указанным Id не найдена.");
        
        existingTask.Name = task.Name;
        existingTask.Description = task.Description;
        existingTask.Deadline = task.Deadline;
        existingTask.Priority = task.Priority;
        existingTask.Status = task.Status;
        return existingTask;

    }

    public void DeleteTask(TaskModel task)
    {
        var taskToDelete = _taskList.FirstOrDefault(t => t.Id == task.Id);
        if (taskToDelete == null)throw new InvalidOperationException("Задача с указанным Id не найдена и не может быть удалена.");
        _taskList.Remove(taskToDelete);
    }
}