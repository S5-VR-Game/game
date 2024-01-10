using UnityEngine;

namespace Game.Tasks.BookRiddle
{
    /// <summary>
    /// This class represents the Book-Riddle-Logic
    /// </summary>
    public class BookRiddle : GameTask
    {
        /// <summary>
        /// Object that represents the Task, which
        /// needs to be Destroyed at the end.
        /// </summary>
        public GameObject taskPrefab;
        private TaskState _bookRiddleState = TaskState.Ongoing;
        public BookRiddleSolution solution { get; set; }
        public BookRiddleSolutionListener bookRiddleSolutionListener;
        
        public BookRiddle() : base("Book Riddle", "", GameTaskType.BookRiddle, 35)
        {
            taskDescription = "Einige Sicherheitsmaßnahmen müssen deaktiviert werden.\n" +
                              "Du musst diese Schalter bestimmt oft drücken.\n" +
                              "Die Bücher sehen aber seltsam aus...";
        }

        public override void Initialize()
        {
            bookRiddleSolutionListener.SetBookRiddleSolution(solution);
        }

        public void SetBookRiddleState(TaskState taskState)
        {
            _bookRiddleState = taskState;
        }

        protected override void BeforeStateCheck()
        {
            // no implementation needed.
        }

        protected override TaskState CheckTaskState()
        {
            return _bookRiddleState;
        }

        protected override void AfterStateCheck()
        {
            if (currentTaskState != TaskState.Ongoing)
            {
                DestroyTask();
            }
        }
    }
}