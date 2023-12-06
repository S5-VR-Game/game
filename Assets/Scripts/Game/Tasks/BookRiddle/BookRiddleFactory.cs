using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Tasks.BookRiddle
{
    /// <summary>
    /// This class is used to spawn a certain Book Riddle
    /// </summary>
    public class BookRiddleFactory : GameTaskFactory<TaskSpawnPoint>
    {
        [SerializeField] private BookRiddle bookRiddle;
        [SerializeField] private Difficulty difficulty;
        protected override GameTask CreateTask(TaskSpawnPoint spawnPoint)
        {
            var transform1 = spawnPoint.transform;
            var riddle = Instantiate(bookRiddle.gameObject, transform1.position, transform1.rotation);
            var task = riddle.GetComponent<BookRiddle>();
            return task;
        }
    }
}