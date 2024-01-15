namespace Game.Tasks.GameExit
{
    /// <summary>
    /// Exit game task, which navigates the player to the game exit location and helps the player to end the game.
    /// Should only be spawned manually once, when the game is over.
    /// </summary>
    public class GameExitTask : GameTask
    {
        public GameExitTask() : base("Spiel gewonnen", "", GameTaskType.GameExitTask, 0)
        {
        }

        public override void Initialize()
        {
            taskDescription = "Du hast die Raumstation erfolgreich instand gehalten und kannst sie nun verlassen.";
        }

        protected override void BeforeStateCheck()
        {
        }

        protected override TaskState CheckTaskState()
        {
            // should never be finished once the game is over and the task is spawned
            return TaskState.Ongoing;
        }

        protected override void AfterStateCheck()
        {
        }
    }
}