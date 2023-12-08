using System.Collections.Generic;
using TMPro;
using Unity.XR.CoreUtils;
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
        [SerializeField] private Material[] allBookMaterials;
        
        private const int MaxRandomDigit = 10;
        private const int MaxDigitsAndColors = 4;
        private static readonly Random Rand = new(new Random().Next());

        
        // ReSharper disable Unity.PerformanceAnalysis
        protected override GameTask CreateTask(TaskSpawnPoint spawnPoint)
        {
            var transform1 = spawnPoint.transform;
            var riddle = Instantiate(bookRiddlePrefab, transform1.position, transform1.rotation);
            var task = riddle.GetComponent<BookRiddle>();
            SpawnBooks(bookSpawnPoints, riddle.transform, task);
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
            for (var i = 0; i < spawnPoints.Length; i++)
            {
                var currentSpawnPoint = spawnPoints[i];
                var currentNewBook =
                    Instantiate(bookPrefab, currentSpawnPoint.position, currentSpawnPoint.rotation);
                currentNewBook.transform.parent = parent;
                if (i < MaxDigitsAndColors)
                {
                    currentNewBook.GetComponentInChildren<TextMeshPro>().text = "" + GetRandomDigit();
                }

                var bookHalves = new List<GameObject>();
                currentNewBook.GetChildGameObjects(bookHalves);

                foreach (var half in bookHalves)
                {
                    var bookRenderer = half.GetComponent<Renderer>();
                    var currentBookMaterials = bookRenderer.materials;
                    currentBookMaterials[0] = allBookMaterials[i % MaxDigitsAndColors];
                    bookRenderer.materials = currentBookMaterials;
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