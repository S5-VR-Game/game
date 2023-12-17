using System;
using System.Collections.Generic;
using TMPro;
using Unity.XR.CoreUtils;
using UnityEngine;
using Random = UnityEngine.Random;

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
            SpawnBooks(GetAmountOfSpawnPoints(bookSpawnPoints), riddle.transform, bookRiddle);
            return bookRiddle;
        }

        /// <summary>
        /// Calculates according to the difficulty all spawn-points needed in order to spawn books
        /// depending on the difficulty of the task.
        /// </summary>
        /// <param name="spawnPoints">all spawn points that exist.</param>
        /// <returns>a list with the first n spawn points needed.</returns>
        private Transform[] GetAmountOfSpawnPoints(IReadOnlyList<Transform> spawnPoints)
        {
            var amountOfSpawnPoints = GetAmountDependingOnDifficulty();
            var newAmountOfSpawnPoints = new Transform[amountOfSpawnPoints];
            for (var i = 0; i < amountOfSpawnPoints; i++)
            {
                newAmountOfSpawnPoints[i] = spawnPoints[i];
            }
            return newAmountOfSpawnPoints;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private int GetAmountDependingOnDifficulty()
        {
            return mDifficulty.GetSeparatedDifficulty() switch
            {
                SeparatedDifficulty.Easy => MaxBooksEasy,
                SeparatedDifficulty.Hard => MaxBooksHard,
                SeparatedDifficulty.Medium => MaxBooksMedium,
                _ => MaxBooksEasy
            };
        }

        /// <summary>
        /// Spawns all the books necessary and also gives them their color.
        /// </summary>
        /// <param name="spawnPoints">All Spawn Points of the books that can be allocated.</param>
        /// <param name="parent">The parent transform so that everything can be cleaned up later on.</param>
        /// <param name="bookRiddle">The Book riddle where the solution is submitted.</param>
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
        /// <returns></returns>
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
        
        /// <summary>
        /// Retrieves a random digit in the range from 1 - 10
        /// </summary>
        /// <returns>Just the digit.</returns>
        private static int GetRandomDigit()
        {
            return Random.Range(0, 10);
        }
    }
}