using System;
using ToDoListAppConsole.Models;

class Program
{
    static void Main(string[] args)
    {
        var toDoList = new ToDoList();

        while (true)
        {
            Console.Clear();
            Console.WriteLine("===== To-Do List =====");
            Console.WriteLine("1. Dodaj zadanie");
            Console.WriteLine("2. Pokaż zadania");
            Console.WriteLine("3. Oznacz zadanie jako zakończone");
            Console.WriteLine("4. Usuń zadanie");
            Console.WriteLine("5. Wyjście");
            Console.Write("Wybierz opcję: ");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Wpisz tytuł zadania: ");
                    string title = Console.ReadLine();
                    toDoList.AddTask(title);
                    break;
                case "2":
                    toDoList.ShowTasks();
                    break;
                case "3":
                    Console.Write("Wpisz ID zadania do oznaczenia jako zakończone: ");
                    int completeId = int.Parse(Console.ReadLine());
                    toDoList.CompleteTask(completeId);
                    break;
                case "4":
                    Console.Write("Wpisz ID zadania do usunięcia: ");
                    int removeId = int.Parse(Console.ReadLine());
                    toDoList.RemoveTask(removeId);
                    break;
                case "5":
                    Console.WriteLine("Zamykam aplikację...");
                    return;
                default:
                    Console.WriteLine("Nieprawidłowy wybór.");
                    break;
            }

            Console.WriteLine("Naciśnij dowolny klawisz, aby kontynuować...");
            Console.ReadKey();
        }
    }
}
