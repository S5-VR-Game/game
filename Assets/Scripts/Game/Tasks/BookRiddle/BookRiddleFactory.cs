using System.Collections.Generic;
using TMPro;
using Unity.XR.CoreUtils;
using UnityEngine;

namespace Game.Tasks.BookRiddle
{
    /// <summary>
    /// This class is there for book summoning.
    /// </summary>
    public class BookRiddleFactory : GameTaskFactory<TaskSpawnPoint>
    {
        [SerializeField] private GameObject bookRiddlePrefab;
        [SerializeField] private Transform[] bookSpawnPoints;
        [SerializeField] private GameObject bookPrefab;
        [SerializeField] private Material[] allBookMaterials;

        private const int MaxDigitsAndColors = 4;
        private const int MaxBooksEasy = 8;
        private const int MaxBooksMedium = 16;
        private const int MaxBooksHard = 32;

        // ReSharper disable Unity.PerformanceAnalysis
        protected override GameTask CreateTask(TaskSpawnPoint spawnPoint)
        {
            var transform1 = spawnPoint.transform;
            var riddle = Instantiate(bookRiddlePrefab, transform1.position, transform1.rotation);
            var bookRiddle = riddle.GetComponent<BookRiddle>();
            var bookSpawnPointsNeeded = CalculateAllNeededBookSpawns();
            SpawnBooks(bookSpawnPointsNeeded, riddle.transform, bookRiddle);
            return bookRiddle;
        }
        
        /// <summary>
        /// Calculates all necessary book spawn points and returns them.
        /// </summary>
        /// <returns>All the necessary book spawn points.</returns>
        private Transform[] CalculateAllNeededBookSpawns()
        {
            var amount = CalculateAmountOfBooksAccordingToDifficulty();
            var neededBookSpawnPoints = new Transform[amount];
            for (var i = 0; i < neededBookSpawnPoints.Length; i++)
            {
                neededBookSpawnPoints[i] = bookSpawnPoints[i];
            }
            return neededBookSpawnPoints;
        }

        /// <summary>
        /// Calculates the amount of books needed and returns it.
        /// </summary>
        /// <returns>The amount of books that will spawn.</returns>
        private int CalculateAmountOfBooksAccordingToDifficulty()
        {
            return mDifficulty.GetSeparatedDifficulty() switch
            {
                SeparatedDifficulty.Easy => MaxBooksEasy,
                SeparatedDifficulty.Medium => MaxBooksMedium,
                SeparatedDifficulty.Hard => MaxBooksHard,
                _ => MaxBooksEasy
            };
        }
        
        /// <summary>
        /// Spawns all necessary books.
        /// </summary>
        /// <param name="spawnPoints">Spawnpoints that get shuffled.</param>
        /// <param name="parent">the parent transform of the object.</param>
        /// <param name="bookRiddle">The Book riddle itself to provide the solution.</param>
        private void SpawnBooks(Transform[] spawnPoints, Transform parent, BookRiddle bookRiddle)
        {
            spawnPoints.Shuffle();
            var bookRiddleSolution = new BookRiddleSolution();

            for (var i = 0; i < spawnPoints.Length; i++)
            {
                var currentNewBook = SetUpCurrentBook(spawnPoints[i], parent, i, bookRiddleSolution);
                AddMaterialToCurrentBook(currentNewBook, i);
            }

            bookRiddle.solution = bookRiddleSolution;
        }

        /// <summary>
        /// Sets up the spawnPoint and parent of a certain book
        /// And Constructs the Solution-object.
        /// </summary>
        /// <param name="spawnPoint">The spawn-point of the book</param>
        /// <param name="parent">The parent Object</param>
        /// <param name="index">The current Spawn-point Index</param>
        /// <param name="bookRiddleSolution">The Solution that gets constructed.</param>
        /// <returns>An book instance.</returns>
        private GameObject SetUpCurrentBook(Transform spawnPoint, Transform parent, int index, BookRiddleSolution bookRiddleSolution)
        {
            var currentNewBook = Instantiate(bookPrefab, spawnPoint.position, spawnPoint.rotation, parent);

            if (index >= MaxDigitsAndColors)
            {
                return currentNewBook;
            }
            var digit = GetRandomDigit();
            currentNewBook.GetComponentInChildren<TextMeshPro>().text = digit.ToString();
            bookRiddleSolution.GetSolutionMap().Add(allBookMaterials[index].ToString(), digit);

            return currentNewBook;
        }

        /// <summary>
        /// Adds the missing material to a certain book
        /// </summary>
        /// <param name="currentNewBook">The book that needs material</param>
        /// <param name="index">The color Index</param>
        private void AddMaterialToCurrentBook(GameObject currentNewBook, int index)
        {
            var bookHalves = new List<GameObject>();
            currentNewBook.GetChildGameObjects(bookHalves);

            foreach (var half in bookHalves)
            {
                var bookRenderer = half.GetComponent<Renderer>();
                var currentBookMaterials = bookRenderer.materials;
                currentBookMaterials[0] = allBookMaterials[index % MaxDigitsAndColors];
                bookRenderer.materials = currentBookMaterials;
            }
        }
        
        private static int GetRandomDigit()
        {
            return Random.Range(0, 6);
        }
    }
}