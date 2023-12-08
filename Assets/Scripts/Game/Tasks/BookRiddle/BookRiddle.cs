using UnityEngine;

namespace Game.Tasks.BookRiddle
{
    
    public class BookRiddle : TimerTask
    {
        public GameObject taskPrefab;

        public BookRiddleSolution solution { get; set; }
        
        public BookRiddle() : base(120, "Book Riddle", "Solve this book riddle", 10)
        {
        }

        public override void Initialize()
        {
            // no implementation needed
        }

        protected override void BeforeStateCheck()
        {
            // not implemented yet
        }

        protected override TaskState CheckTaskState()
        {
            return TaskState.Ongoing;
        }

        protected override void AfterStateCheck()
        {
            
        }
    }
}