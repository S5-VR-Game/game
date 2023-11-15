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
        private static Logger log = new Logger(new LogHandler());
        private static string logTAG = "MainInitialization";
        
        /// <summary>
        /// This method is called once at the game startup before any scene is loaded and could be used to initialize
        /// global objects e.g. Random
        /// </summary>
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void OnBeforeSceneLoadRuntimeMethod()
        {
            log.Log(logTAG,"MainInitialization started");
            
            // for example: initialize the unity random object with a specific seed
            // Random.InitState(seed);
            
            log.Log(logTAG, "MainInitialization finished");
        }
    }
}