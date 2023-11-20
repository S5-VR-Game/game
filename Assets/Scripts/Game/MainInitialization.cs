using MyPrefabs.Scripts.Logging;
using UnityEngine;

namespace MyPrefabs.Scripts.Game
{
    /// <summary>
    /// Initialization class that should not be instantiated or referenced anywhere.
    /// The method <see cref="OnBeforeSceneLoadRuntimeMethod"/> is called by unity without any reference to this class.
    /// </summary>
    public static class MainInitialization
    {
        private static readonly Logger LOG = new Logger(new LogHandler());
        private const string LOGTag = "MainInitialization";

        /// <summary>
        /// This method is called once at the game startup before any scene is loaded and could be used to initialize
        /// global objects e.g. Random
        /// </summary>
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void OnBeforeSceneLoadRuntimeMethod()
        {
            LOG.Log(LOGTag,"MainInitialization started");
            
            // for example: initialize the unity random object with a specific seed
            // Random.InitState(seed);
            
            LOG.Log(LOGTag, "MainInitialization finished");
        }
    }
}