using ToDoListAppConsole.Models;
using NUnit.Framework;
using System.Linq;

namespace ToDoListAppNunit.Tests
{
    public class ToDoListTests
    {
        private ToDoList toDoList;

        [SetUp]
        public void Setup()
        {
            toDoList = new ToDoList();
        }

        // Test sprawdzaj¹cy, czy zadanie zostaje poprawnie dodane do listy
        [Test]
        public void AddTask_ShouldAddTaskToList()
        {
            toDoList.AddTask("Zadanie 1");

            var tasks = toDoList.GetAllTasks();
            Assert.AreEqual(1, tasks.Count);
            Assert.AreEqual("Zadanie 1", tasks[0].Title);
            Assert.IsFalse(tasks[0].IsCompleted);
        }

        // Test sprawdzaj¹cy, czy zadanie zostaje oznaczone jako zakoñczone
        [Test]
        public void CompleteTask_ShouldMarkTaskAsCompleted()
        {
            toDoList.AddTask("Zadanie 2");
            var task = toDoList.GetAllTasks().First();

            toDoList.CompleteTask(task.Id);

            Assert.IsTrue(task.IsCompleted);
        }

        // Test sprawdzaj¹cy, czy zadanie zostaje poprawnie usuniête z listy
        [Test]
        public void RemoveTask_ShouldRemoveTaskFromList()
        {
            toDoList.AddTask("Zadanie 3");
            var task = toDoList.GetAllTasks().First();

            toDoList.RemoveTask(task.Id);

            Assert.AreEqual(0, toDoList.GetAllTasks().Count);
        }

        // Test sprawdzaj¹cy, czy próba usuniêcia nieistniej¹cego zadania nie powoduje wyj¹tku
        [Test]
        public void RemoveTask_InvalidId_ShouldNotThrow()
        {
            toDoList.AddTask("Zadanie 4");

            Assert.DoesNotThrow(() => toDoList.RemoveTask(999));
            Assert.AreEqual(1, toDoList.GetAllTasks().Count);
        }

        // Test sprawdzaj¹cy, czy oznaczenie nieistniej¹cego zadania jako zakoñczonego nie powoduje wyj¹tku
        [Test]
        public void CompleteTask_InvalidId_ShouldNotThrow()
        {
            toDoList.AddTask("Zadanie 5");

            Assert.DoesNotThrow(() => toDoList.CompleteTask(999));
            Assert.IsFalse(toDoList.GetAllTasks().First().IsCompleted);
        }

        // Test sprawdzaj¹cy, czy ka¿de zadanie dostaje unikalny identyfikator (ID)
        [Test]
        public void AddMultipleTasks_ShouldAssignUniqueIds()
        {
            toDoList.AddTask("Task A");
            toDoList.AddTask("Task B");
            toDoList.AddTask("Task C");

            var tasks = toDoList.GetAllTasks();
            var ids = tasks.Select(t => t.Id).ToList();

            CollectionAssert.AllItemsAreUnique(ids);
        }

        // Test sprawdzaj¹cy, czy metoda GetAllTasks zwraca wszystkie dodane zadania
        [Test]
        public void GetAllTasks_ShouldReturnAllTasks()
        {
            toDoList.AddTask("X");
            toDoList.AddTask("Y");

            var allTasks = toDoList.GetAllTasks();

            Assert.AreEqual(2, allTasks.Count);
        }

        // Test sprawdzaj¹cy, czy oznaczenie jednego zadania jako zakoñczonego nie wp³ywa na inne
        [Test]
        public void CompleteTask_ShouldOnlyAffectOneTask()
        {
            toDoList.AddTask("A");
            toDoList.AddTask("B");
            var tasks = toDoList.GetAllTasks();

            toDoList.CompleteTask(tasks[0].Id);

            Assert.IsTrue(tasks[0].IsCompleted);
            Assert.IsFalse(tasks[1].IsCompleted);
        }

        // Test sprawdzaj¹cy, czy po usuniêciu wszystkich zadañ lista jest pusta
        [Test]
        public void RemoveAllTasks_ShouldLeaveEmptyList()
        {
            toDoList.AddTask("1");
            toDoList.AddTask("2");

            foreach (var task in toDoList.GetAllTasks().ToList())
            {
                toDoList.RemoveTask(task.Id);
            }

            Assert.IsEmpty(toDoList.GetAllTasks());
        }

        // Test sprawdzaj¹cy, czy tytu³ zadania nie zmienia siê po oznaczeniu go jako zakoñczonego
        [Test]
        public void TaskTitle_ShouldNotChangeAfterCompletion()
        {
            toDoList.AddTask("Original Title");
            var task = toDoList.GetAllTasks().First();

            toDoList.CompleteTask(task.Id);

            Assert.AreEqual("Original Title", task.Title);
        }

        // Test sprawdzaj¹cy, czy dodanie zadania z pustym tytu³em dzia³a poprawnie
        [Test]
        public void AddTask_EmptyTitle_ShouldAddTask()
        {
            toDoList.AddTask("");

            var task = toDoList.GetAllTasks().First();
            Assert.AreEqual("", task.Title);
            Assert.IsFalse(task.IsCompleted);
        }

        // Test sprawdzaj¹cy, czy dodanie zadania z bardzo d³ugim tytu³em dzia³a poprawnie
        [Test]
        public void AddTask_LongTitle_ShouldAddTaskCorrectly()
        {
            string longTitle = new string('A', 1000);
            toDoList.AddTask(longTitle);

            var task = toDoList.GetAllTasks().First();
            Assert.AreEqual(longTitle, task.Title);
        }

        // Test sprawdzaj¹cy, czy dwa ró¿ne zadania mog¹ mieæ taki sam tytu³
        [Test]
        public void AddTasks_WithDuplicateTitles_ShouldBeAddedSeparately()
        {
            toDoList.AddTask("Powtarzaj¹cy siê tytu³");
            toDoList.AddTask("Powtarzaj¹cy siê tytu³");

            var tasks = toDoList.GetAllTasks();
            Assert.AreEqual(2, tasks.Count);
            Assert.AreNotEqual(tasks[0].Id, tasks[1].Id);
        }

        // Test sprawdzaj¹cy, czy oznaczenie zakoñczenia dzia³a tylko raz
        [Test]
        public void CompleteTask_Twice_ShouldRemainCompleted()
        {
            toDoList.AddTask("Do wykonania");
            var task = toDoList.GetAllTasks().First();

            toDoList.CompleteTask(task.Id);
            toDoList.CompleteTask(task.Id);

            Assert.IsTrue(task.IsCompleted);
        }

        // Test sprawdzaj¹cy, czy lista zadañ zawiera dok³adnie te tytu³y, które zosta³y dodane
        [Test]
        public void GetAllTasks_ShouldContainCorrectTitles()
        {
            toDoList.AddTask("Zadanie A");
            toDoList.AddTask("Zadanie B");

            var titles = toDoList.GetAllTasks().Select(t => t.Title).ToList();
            CollectionAssert.Contains(titles, "Zadanie A");
            CollectionAssert.Contains(titles, "Zadanie B");
        }

        // Test sprawdzaj¹cy, czy ka¿de zadanie ma wartoœæ IsCompleted ustawion¹ na false po dodaniu
        [Test]
        public void NewTasks_ShouldBeNotCompletedByDefault()
        {
            toDoList.AddTask("Nowe 1");
            toDoList.AddTask("Nowe 2");

            Assert.IsTrue(toDoList.GetAllTasks().All(t => !t.IsCompleted));
        }

        // Test sprawdzaj¹cy usuniêcie zadania, a nastêpnie dodanie nowego i sprawdzenie ID
        [Test]
        public void RemoveThenAddTask_ShouldIncrementId()
        {
            toDoList.AddTask("A");
            toDoList.RemoveTask(1);
            toDoList.AddTask("B");

            var task = toDoList.GetAllTasks().First();
            Assert.AreEqual(2, task.Id); // Nie resetujemy ID, wiêc kolejne powinno byæ 2
        }

        // Test sprawdzaj¹cy, czy próba usuniêcia wszystkich zadañ nie generuje wyj¹tków
        [Test]
        public void RemoveAllTasks_Loop_ShouldNotThrow()
        {
            for (int i = 0; i < 5; i++)
                toDoList.AddTask($"Zadanie {i + 1}");

            foreach (var task in toDoList.GetAllTasks().ToList())
                Assert.DoesNotThrow(() => toDoList.RemoveTask(task.Id));

            Assert.IsEmpty(toDoList.GetAllTasks());
        }

        // Test sprawdzaj¹cy, czy lista dzia³a poprawnie przy bardzo du¿ej liczbie zadañ
        [Test]
        public void AddManyTasks_ShouldHandleLargeNumber()
        {
            for (int i = 1; i <= 1000; i++)
                toDoList.AddTask($"Zadanie {i}");

            Assert.AreEqual(1000, toDoList.GetAllTasks().Count);
        }

        // Test sprawdzaj¹cy, czy po dodaniu zadania mo¿na je odczytaæ po ID
        [Test]
        public void FindTaskById_ShouldReturnCorrectTask()
        {
            toDoList.AddTask("Unikalne zadanie");
            var addedTask = toDoList.GetAllTasks().First();

            var found = toDoList.GetAllTasks().Find(t => t.Id == addedTask.Id);
            Assert.AreEqual("Unikalne zadanie", found.Title);
        }

        // Test sprawdzaj¹cy, czy po dodaniu i oznaczeniu kilku zadañ tylko oznaczone maj¹ IsCompleted = true
        [Test]
        public void CompleteMultipleTasks_ShouldSetProperStates()
        {
            toDoList.AddTask("A");
            toDoList.AddTask("B");
            toDoList.AddTask("C");

            toDoList.CompleteTask(2); // tylko B

            var tasks = toDoList.GetAllTasks();
            Assert.IsFalse(tasks[0].IsCompleted);
            Assert.IsTrue(tasks[1].IsCompleted);
            Assert.IsFalse(tasks[2].IsCompleted);
        }

        // Test sprawdzaj¹cy, czy po dodaniu zadania, jego ID jest wiêksze od 0
        [Test]
        public void AddedTask_ShouldHavePositiveId()
        {
            toDoList.AddTask("ID test");
            var task = toDoList.GetAllTasks().First();

            Assert.Greater(task.Id, 0);
        }

    }
}
