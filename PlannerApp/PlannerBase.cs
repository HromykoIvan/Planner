using Microsoft.Extensions.Logging;
using Planner.BusinessLogic.Interfaces;
using Planner.Data.Models;
using Planner.Data.Models.Enums;

namespace PlannerApp;

public class PlannerBase
{
    private readonly ITaskService _taskService;
    private readonly ILogger<PlannerBase> _logger;

    public PlannerBase(ITaskService taskService, ILogger<PlannerBase> logger)
    {
        _taskService = taskService;
        _logger = logger;
    }
    public void Start()
    {
        while (true)
        {
            var actionNumber = ShowMenu();
            switch (actionNumber)
            {
                case "1":
                    Console.Clear();
                    ShowTasks();
                    break;
                case "2":
                    Console.Clear();
                    ShowTasks();
                    break;
                case "3":
                    Console.Clear();
                    TaskCreation();
                    break;
                case "4":
                    return;
                case "5":
                    break;
                default:

                    continue;
            }
        }
    }
    
    private string? ShowMenu()
    {
        _logger.LogInformation("Available commands:");
        _logger.LogInformation("1. Show all tasks");
        _logger.LogInformation("2. Show task today");
        _logger.LogInformation("3. Add new task ");
        _logger.LogInformation("4. Close program ");
        return Console.ReadLine();
    }
    
    private void ShowTasks()
    {
        _logger.LogInformation("Доступные команды");
        _logger.LogInformation("1. Для сортировки напишите sort deadline/Name/Prior");
        _logger.LogInformation("2. Поиск по имени/приоритету/дедлайну: filtr Priority High/Medium/Low)");
        _logger.LogInformation("3. Изменение задачи: edit 1 ");
        _logger.LogInformation("4. Close program \n");
        var tasks = _taskService.GetAllTasks();
        _logger.LogInformation("{0,5} {1,-20} {2,-30} {3,-10} {4,-15}", "ID", "Name", "Deadline", "Priority", "Status");
        _logger.LogInformation(new string('-', 80));

        for (int i = 0; i < tasks.Count; i++)
        {
            var task = tasks[i];
            _logger.LogInformation("{0,5} {1,-20} {2,-30} {3,-10} {4,-15}", i + 1, task.Name, task.Deadline.ToString("dd/MM/yyyy HH:mm"), task.Priority, task.Status);
        }
        BackToMenu();
    }
    
    private void TaskCreation()
        {
            while (true)
            {
                _logger.LogInformation("Введите заголовок задачи");
                var name = Console.ReadLine() ?? string.Empty;
                _logger.LogInformation("Опишите задачу");
                var description = Console.ReadLine() ?? string.Empty;
                int days;

                while (true)
                {
                    _logger.LogInformation("Дней на задачу");
                    if (!int.TryParse(Console.ReadLine(), out days))
                    {
                        _logger.LogInformation("Введено некорректное количество дней, попробуйте снова.");
                    }
                    else
                    {
                        break;
                    }
                }

                var deadline = DateTime.Now.AddDays(days == default ? 1 : days);
                var priority = Priority.Normal;

                while (true)
                {
                    _logger.LogInformation("Выберите нужный приортитет:\n" +
                        "1 - Low\n" +
                        "2 - Normal\n" +
                        "3 - High");
                    
                    var userPriority = Console.ReadLine();
                    switch (userPriority)
                    {
                        case "1":
                            priority = Priority.Low;
                            break;

                        case "2":
                            priority = Priority.Normal;
                            break;

                        case "3":
                            priority = Priority.High;
                            break;

                        default:
                            _logger.LogInformation("Вы ввели некорректно, попробуйте снова");
                            break;
                    }
                    break;
                }

                var newTask = new TaskModel
                {
                    Name = name,
                    Description = description,
                    Deadline = deadline,
                    Priority = priority,
                    Status = Status.Todo
                };
                _taskService.AddTask(newTask);
                _logger.LogInformation("Вы создали задачу:");
                ShowTaskDetails(newTask);
                BackToMenu();
                return;
            }
        }
    
    private void BackToMenu()
    {
        _logger.LogInformation("Нажмите Enter, чтобы вернуться в меню...");
        Console.ReadLine();
        Console.Clear();
    }
    
    public void ShowTaskDetails(TaskModel task) // отображение последней задачи в AddTask
    {
        _logger.LogInformation($"Id: {task.Id}");
        _logger.LogInformation($"Name: {task.Name}");
        _logger.LogInformation($"Description: {task.Description}");
        _logger.LogInformation($"Deadline: {task.Deadline}");
        _logger.LogInformation($"Priority: {task.Priority}");
        _logger.LogInformation($"Status: {task.Status}");
    }
}