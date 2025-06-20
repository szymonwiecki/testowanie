using System;
using Xunit;
using ToDoListAppConsole.Models;

namespace ToDoListAppConsole.Tests
{
    public class ToDoListTests
    {
        // Testowanie dodania zadania do listy
        [Fact]
        public void AddTask_ShouldAddTaskToList()
        {
            // Arrange: Przygotowujemy nową instancję ToDoList
            var toDoList = new ToDoList();

            // Act: Dodajemy zadanie do listy
            toDoList.AddTask("Test Task");

            // Assert: Sprawdzamy, czy lista zawiera jedno zadanie i czy jego tytuł to "Test Task"
            var tasks = toDoList.GetAllTasks();
            Assert.Single(tasks);  // Sprawdzamy, czy lista zawiera 1 zadanie
            Assert.Equal("Test Task", tasks[0].Title);  // Sprawdzamy, czy tytuł zadania to "Test Task"
        }

        // Testowanie usuwania zadania z listy
        [Fact]
        public void RemoveTask_ShouldRemoveTaskFromList()
        {
            // Arrange: Przygotowujemy nową instancję ToDoList i dodajemy zadanie
            var toDoList = new ToDoList();
            toDoList.AddTask("Test Task");
            var taskId = toDoList.GetAllTasks()[0].Id;  // Pobieramy ID zadania

            // Act: Usuwamy zadanie o pobranym ID
            toDoList.RemoveTask(taskId);

            // Assert: Sprawdzamy, czy lista jest pusta po usunięciu zadania
            var tasks = toDoList.GetAllTasks();
            Assert.Empty(tasks);  // Sprawdzamy, czy lista jest pusta po usunięciu zadania
        }

        // Testowanie oznaczania zadania jako zakończone
        [Fact]
        public void CompleteTask_ShouldMarkTaskAsCompleted()
        {
            // Arrange: Przygotowujemy nową instancję ToDoList i dodajemy zadanie
            var toDoList = new ToDoList();
            toDoList.AddTask("Test Task");
            var taskId = toDoList.GetAllTasks()[0].Id;  // Pobieramy ID zadania

            // Act: Oznaczamy zadanie jako zakończone
            toDoList.CompleteTask(taskId);

            // Assert: Sprawdzamy, czy zadanie zostało oznaczone jako zakończone
            var task = toDoList.GetAllTasks()[0];
            Assert.True(task.IsCompleted);  // Sprawdzamy, czy zadanie zostało oznaczone jako zakończone
        }

        // Testowanie wyświetlania zadań po dodaniu zadania
        [Fact]
        public void ShowTasks_ShouldDisplayCorrectTaskInfo()
        {
            // Arrange: Przygotowujemy nową instancję ToDoList i dodajemy zadanie
            var toDoList = new ToDoList();
            toDoList.AddTask("Test Task");

            // Act: Wywołujemy metodę ShowTasks() do wyświetlenia zadań (sprawdzimy manualnie w konsoli)
            toDoList.ShowTasks();  // Tutaj po prostu wyświetlamy zadania, co sprawdzimy ręcznie w konsoli
        }

        // Testowanie obsługi próby oznaczenia zadania jako zakończonego, które nie istnieje
        [Fact]
        public void CompleteTask_ShouldHandleTaskNotFound()
        {
            // Arrange: Przygotowujemy nową instancję ToDoList i dodajemy zadanie
            var toDoList = new ToDoList();
            toDoList.AddTask("Test Task");

            // Act: Próbujemy oznaczyć zadanie o nieistniejącym ID
            toDoList.CompleteTask(999);  // Próbujemy oznaczyć zadanie, które nie istnieje

            // Assert: Sprawdzamy, że zadanie nie zostało oznaczone jako zakończone
            var tasks = toDoList.GetAllTasks();
            Assert.Single(tasks);  // Sprawdzamy, że lista zawiera jedno zadanie
            Assert.False(tasks[0].IsCompleted);  // Sprawdzamy, że zadanie nie zostało oznaczone jako zakończone
        }

        // Testowanie dodania wielu zadań do listy
        [Fact]
        public void AddTask_ShouldAddMultipleTasks()
        {
            // Arrange: Przygotowujemy nową instancję ToDoList
            var toDoList = new ToDoList();

            // Act: Dodajemy trzy zadania
            toDoList.AddTask("Task 1");
            toDoList.AddTask("Task 2");
            toDoList.AddTask("Task 3");

            // Assert: Sprawdzamy, czy lista zawiera 3 zadania
            var tasks = toDoList.GetAllTasks();
            Assert.Equal(3, tasks.Count);  // Sprawdzamy, czy lista zawiera 3 zadania
        }

        // Testowanie usuwania zakończonego zadania
        [Fact]
        public void RemoveTask_ShouldRemoveCompletedTask()
        {
            // Arrange: Przygotowujemy nową instancję ToDoList, dodajemy zadanie i oznaczamy je jako zakończone
            var toDoList = new ToDoList();
            toDoList.AddTask("Task 1");
            var taskId = toDoList.GetAllTasks()[0].Id;  // Pobieramy ID zadania
            toDoList.CompleteTask(taskId);  // Oznaczamy zadanie jako zakończone

            // Act: Usuwamy zakończone zadanie
            toDoList.RemoveTask(taskId);

            // Assert: Sprawdzamy, czy lista jest pusta po usunięciu zadania
            var tasks = toDoList.GetAllTasks();
            Assert.Empty(tasks);  // Sprawdzamy, że lista jest pusta po usunięciu zakończonego zadania
        }
        // Testowanie usuwania jedynego zadania z listy
        [Fact]
        public void RemoveTask_ShouldEmptyListWhenOnlyOneTaskExists()
        {
            // Arrange: Przygotowujemy nową instancję ToDoList i dodajemy jedno zadanie
            var toDoList = new ToDoList();
            toDoList.AddTask("Test Task");

            // Act: Usuwamy jedyne zadanie
            toDoList.RemoveTask(1);  // Usuwamy jedyne zadanie

            // Assert: Sprawdzamy, czy lista jest pusta
            var tasks = toDoList.GetAllTasks();
            Assert.Empty(tasks);  // Sprawdzamy, czy lista jest pusta po usunięciu jedynego zadania
        }

        // Testowanie próby dodania zadania z pustym tytułem
        [Fact]
        public void AddTask_ShouldNotAddTaskWithEmptyTitle()
        {
            // Arrange: Przygotowujemy nową instancję ToDoList

            // Act: Próbujemy dodać zadanie z pustym tytułem
            var toDoList = new ToDoList();
            toDoList.AddTask("");  // Próbujemy dodać zadanie z pustym tytułem

            // Assert: Sprawdzamy, czy lista nie zawiera żadnych zadań
            var tasks = toDoList.GetAllTasks();
            Assert.Empty(tasks);  // Sprawdzamy, że lista nie zawiera żadnych zadań
        }

        // Testowanie, czy zadanie zostało usunięte z listy po wywołaniu RemoveTask
        [Fact]
        public void RemoveTask_ShouldNotContainRemovedTask()
        {
            // Arrange: Przygotowujemy nową instancję ToDoList, dodajemy zadanie i usuwamy je
            var toDoList = new ToDoList();
            toDoList.AddTask("Test Task");
            var taskId = toDoList.GetAllTasks()[0].Id;

            // Act: Usuwamy zadanie
            toDoList.RemoveTask(taskId);

            // Assert: Sprawdzamy, że lista nie zawiera usuniętego zadania
            var tasks = toDoList.GetAllTasks();
            Assert.DoesNotContain(tasks, t => t.Id == taskId);  // Sprawdzamy, że lista nie zawiera usuniętego zadania
        }

        // Testowanie dodania zadania z długim tytułem
        [Fact]
        public void AddTask_ShouldHandleLongTitles()
        {
            // Arrange: Przygotowujemy nową instancję ToDoList i długi tytuł
            var toDoList = new ToDoList();
            var longTitle = new string('A', 1000);  // Długi tytuł (1000 znaków)

            // Act: Dodajemy zadanie z długim tytułem
            toDoList.AddTask(longTitle);

            // Assert: Sprawdzamy, czy zadanie zostało dodane
            var tasks = toDoList.GetAllTasks();
            Assert.Single(tasks);  // Sprawdzamy, że zostało dodane jedno zadanie
            Assert.Equal(longTitle, tasks[0].Title);  // Sprawdzamy, czy tytuł zadania jest taki, jak powinien
        }


        // Testowanie dodania dwóch zadań o tej samej nazwie, ale z unikalnymi ID
        // Sprawdzamy, czy zadania o tej samej nazwie otrzymują różne unikalne ID.
        [Fact]
        public void AddTask_ShouldAssignUniqueIdsToTasksWithSameTitle()
        {
            // Arrange: Przygotowujemy nową instancję ToDoList
            var toDoList = new ToDoList();

            // Act: Dodajemy dwa zadania o tej samej nazwie
            toDoList.AddTask("Test Task");
            toDoList.AddTask("Test Task");

            // Assert: Sprawdzamy, że zadania mają różne ID
            var tasks = toDoList.GetAllTasks();
            Assert.Equal(2, tasks.Count);  // Sprawdzamy, że lista zawiera 2 zadania
            Assert.NotEqual(tasks[0].Id, tasks[1].Id);  // Sprawdzamy, że zadania mają różne ID
        }

        // Testowanie poprawności działania aplikacji po kilku operacjach (dodawanie, usuwanie, oznaczanie)
        // Sprawdzamy, czy aplikacja poprawnie działa, gdy wykonamy różne operacje w różnych kolejnościach.
        [Fact]
        public void MultipleOperations_ShouldWorkTogetherCorrectly()
        {
            // Arrange: Przygotowujemy nową instancję ToDoList i dodajemy 3 zadania
            var toDoList = new ToDoList();
            toDoList.AddTask("Task 1");
            toDoList.AddTask("Task 2");
            toDoList.AddTask("Task 3");

            // Act: Usuwamy pierwsze zadanie i oznaczamy drugie jako zakończone
            var taskIdToRemove = toDoList.GetAllTasks()[0].Id;
            toDoList.RemoveTask(taskIdToRemove);  // Usuwamy pierwsze zadanie
            var taskIdToComplete = toDoList.GetAllTasks()[0].Id;
            toDoList.CompleteTask(taskIdToComplete);  // Oznaczamy drugie zadanie jako zakończone

            // Assert: Sprawdzamy, czy lista zawiera 2 zadania i czy drugie zadanie zostało oznaczone jako zakończone
            var tasks = toDoList.GetAllTasks();
            Assert.Equal(2, tasks.Count);  // Powinna zostać tylko 2 zadania
            Assert.True(tasks[0].IsCompleted);  // Sprawdzamy, czy drugie zadanie zostało oznaczone jako zakończone
        }

        // Testowanie dodawania dużej liczby zadań
        // Sprawdzamy, jak aplikacja radzi sobie z dodaniem dużej liczby zadań.
        [Fact]
        public void AddTask_ShouldHandleLargeNumberOfTasks()
        {
            // Arrange: Przygotowujemy nową instancję ToDoList i określamy liczbę zadań
            var toDoList = new ToDoList();
            int taskCount = 10000;

            // Act: Dodajemy 10000 zadań
            for (int i = 0; i < taskCount; i++)
            {
                toDoList.AddTask($"Task {i + 1}");
            }

            // Assert: Sprawdzamy, czy lista zawiera 10000 zadań
            var tasks = toDoList.GetAllTasks();
            Assert.Equal(taskCount, tasks.Count);  // Sprawdzamy, czy lista zawiera 10000 zadań
        }

        // Testowanie, czy komunikat "Brak zadań" jest wyświetlany, gdy lista jest pusta
        // Sprawdzamy, czy program wyświetla odpowiedni komunikat, gdy lista jest pusta.
        [Fact]
        public void ShowTasks_ShouldDisplayNoTasksMessage_WhenNoTasksExist()
        {
            // Arrange: Przygotowujemy nową instancję ToDoList (bez zadań)

            // Act: Wywołujemy metodę ShowTasks() i przechwytujemy jej wyjście
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);  // Przechwytujemy wyjście konsoli
                var toDoList = new ToDoList();
                toDoList.ShowTasks();
                var result = sw.ToString().Trim();

                // Assert: Sprawdzamy, czy komunikat o braku zadań jest poprawny
                Assert.Equal("Brak zadań.", result);  // Sprawdzamy, czy komunikat jest prawidłowy
            }
        }


    }
}
