using Game.Tasks;
using MyPrefabs.Scripts.Game;
using UnityEngine;

namespace Game.Observer
{
    /// <summary>
    /// Observer for integrity updates. 
    /// </summary>
    public class IntegrityObserver : AbstractObserver
    {
        [SerializeField] public Integrity integrity;
        
        protected override void OnTaskSuccessful(GameTask task)
        {
            integrity.IncrementIntegrity(task.integrityValue);
        }
        
        protected override void OnTaskFailed(GameTask task)
        {
            integrity.DecrementIntegrity(task.integrityValue);
        }
    }
}