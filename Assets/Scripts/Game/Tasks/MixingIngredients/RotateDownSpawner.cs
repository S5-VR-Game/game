using System;
using Unity.VisualScripting;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Game.Tasks.MixingIngredients
{
    /// <summary>
    /// Spawns a prefab when the game object is rotated downwards. The prefab is spawned at a given spawn point.
    /// </summary>
    /// <typeparam name="T">Type of the GameObject, that will be spawned</typeparam>
    public abstract class RotateDownSpawner<T> : MonoBehaviour where T : Object
    {
        /// <summary>
        /// Threshold that determines, how far the object needs to be rotated upwards/downwards to trigger spawning
        /// </summary>
        [SerializeField] [Range(0,1)] private float rotationResetThreshold = 0.5f;
        [SerializeField] private T prefab;
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private Transform rotationTransform;
        private bool m_FacingDown;
        protected bool spawningEnabled = true;
        
        /// <summary>
        /// Event that is invoked when a game object is spawned. If the type parameter <see cref="T"/> is neither
        /// a game object nor a component of a game object, this event will not be invoked.  
        /// </summary>
        public event Action<GameObject> OnGameObjectSpawnedEvent; 
        
        protected virtual void Update()
        {
            if (!spawningEnabled) return;
            
            // check if game object is facing down
            var scalarProduct = Vector3.Dot(rotationTransform.up, Vector3.down);
            if (!m_FacingDown && scalarProduct > rotationResetThreshold)
            {
                // spawn a prefab and lock spawning to prevent spawning multiple objects
                m_FacingDown = true;
                T newObject = Instantiate(prefab, spawnPoint.position, Quaternion.identity);
                
                OnGameObjectSpawnedEvent?.Invoke(newObject.GameObject());
                OnSpawned(newObject);
            }
            else if (scalarProduct < -rotationResetThreshold)
            {
                // reset lock again, to allow spawning another prefab
                m_FacingDown = false;
            }
        }

        protected virtual void OnSpawned(T newObject)
        {
        }
    }
}
