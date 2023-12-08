using TMPro;
using UnityEngine;
using Random = System.Random;

namespace Game.Tasks.BookRiddle
{
    /// <summary>
    /// This class is used to spawn a certain Book Riddle
    /// </summary>
    public class BookRiddleFactory : GameTaskFactory<TaskSpawnPoint>
    {
        [SerializeField] private GameObject bookRiddlePrefab;
        [SerializeField] private Transform[] bookSpawnPoints;
        [SerializeField] private GameObject bookPrefab;
        
        private const int MaxRandomDigit = 10;
        private const int MaxDigits = 4;
        private static readonly Random Rand = new();

        
        // ReSharper disable Unity.PerformanceAnalysis
        protected override GameTask CreateTask(TaskSpawnPoint spawnPoint)
        {
            var transform1 = spawnPoint.transform;
            var riddle = Instantiate(bookRiddlePrefab, transform1.position, transform1.rotation);
            var task = riddle.GetComponent<BookRiddle>();
            // SpawnBooks(bookSpawnPoints, riddle.transform, task);
            return task;
        }
        
        /// <summary>
        /// Spawns all the books needed and provides their parent transform
        /// </summary>
        /// <param name="spawnPoints">All The spawn-points for the books</param>
        /// <param name="parent">the Parent transform of the riddle</param>
        /// <param name="materials"></param>
        /// <param name="bookPrefab"></param>
        /// <param name="riddle"></param>
        private void SpawnBooks(Transform [] spawnPoints, Transform parent, BookRiddle riddle)
        {
            spawnPoints.Shuffle();
            var bookRiddleSolution = new BookRiddleSolution();
            for (var i = 0; i < 8; i++)
            {
                var currentSpawnPoint = spawnPoints[i];
                var currentNewBook =
                    Instantiate(bookPrefab, currentSpawnPoint.position, currentSpawnPoint.rotation);
                currentNewBook.transform.parent = parent;
                if (i < MaxDigits)
                {
                    currentNewBook.GetComponentInChildren<TextMeshPro>().text = "" + GetRandomDigit();
                }
            }
            riddle.solution = bookRiddleSolution;
        }
        
        /// <summary>
        /// Returns a random digit
        /// </summary>
        /// <returns>Random digit as Integer</returns>
        private static int GetRandomDigit()
        {
            return Rand.Next(MaxRandomDigit);
        }
    }
}