using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoListAppConsole.Models
{ 

    public class ToDoList
    {
        private List<Task> tasks = new List<Task>();
        private int nextId = 1;

        public void AddTask(string title)
        {
            tasks.Add(new Task(nextId++, title));
            Console.WriteLine("Dodano zadanie.");
        }

        public void RemoveTask(int id)
        {
            var task = tasks.Find(t => t.Id == id);
            if (task != null)
            {
                tasks.Remove(task);
                Console.WriteLine("Usunięto zadanie.");
            }
            else
            {
                Console.WriteLine("Zadanie o tym ID nie istnieje.");
            }
        }

        public void ShowTasks()
        {
            if (tasks.Count == 0)
            {
                Console.WriteLine("Brak zadań.");
            }
            else
            {
                foreach (var task in tasks)
                {
                    string status = task.IsCompleted ? "Zakończone" : "Nieukończone";
                    Console.WriteLine($"{task.Id}. {task.Title} - {status}");
                }
            }
        }

        public void CompleteTask(int id)
        {
            var task = tasks.Find(t => t.Id == id);
            if (task != null)
            {
                task.IsCompleted = true;
                Console.WriteLine("Zadanie oznaczone jako zakończone.");
            }
            else
            {
                Console.WriteLine("Zadanie o tym ID nie istnieje.");
            }
        }

        public List<Task> GetAllTasks()
        {
            return tasks;
        }
    }

}
