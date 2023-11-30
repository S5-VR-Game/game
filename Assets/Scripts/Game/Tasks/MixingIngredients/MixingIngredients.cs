namespace Game.Tasks.MixingIngredients
{
    public class MixingIngredients : GameTask
    {
        private const string Name = "Mixing ingredients";
        private const string Description = "Task decsription";
        
        public MixingIngredients() : base(Name, Description)
        {
        }

        public override void Initialize()
        {
        }

        protected override void BeforeStateCheck()
        {
        }

        protected override TaskState CheckTaskState()
        {
            return TaskState.Ongoing;
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