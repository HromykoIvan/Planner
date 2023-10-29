using Newtonsoft.Json;
using Planner.Data.Models;

namespace Planner.Data;

public class Storage : IStorage
{
    public string FileName { get; }
    public string FilePath { get; }

    public Storage(string fileName) 
    {
        FileName = fileName;
        FilePath = Path.Combine(Directory.GetCurrentDirectory(), fileName);
    }
    public void Save(IList<TaskModel> tasks)
    {
        var json = JsonConvert.SerializeObject(tasks, Formatting.Indented);
        File.WriteAllText("tasks.json", json);
    }

    public List<TaskModel> GetAllTasks()
    {
        if (File.Exists("tasks.json"))
        {
            string json = File.ReadAllText("tasks.json");
            var tasks = JsonConvert.DeserializeObject<List<TaskModel>>(json);
            return tasks ?? new List<TaskModel>();
        }
        return new List<TaskModel> ();
    }
}