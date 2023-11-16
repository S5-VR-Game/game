namespace MyPrefabs.Scripts.Game.Tasks
{
    public abstract class NotTimerTask : GameTask
    {
        private void Update()
        {
            TaskState currentCheckedTaskState = CheckTaskState();
            if (currentCheckedTaskState != TaskState.Ongoing)
            {
                UpdateTaskState(currentCheckedTaskState);
                DestroyGameObject();
            }
        }
    }
}