using Game.Observer;
using PlayerController;

namespace Game
{
    /// <summary>
    /// Record class that contains all necessary data for factory initialization and combines game data references
    /// </summary>
    /// <param name="difficulty">difficulty reference</param>
    /// <param name="playerProfileService">player profile service reference</param>
    /// <param name="gameTaskObserver">game task observer reference</param>
    /// <param name="integrityObserver">integrity observer reference</param>
    public record FactoryInitializationData(Difficulty difficulty, PlayerProfileService playerProfileService, 
        GameTaskObserver gameTaskObserver, IntegrityObserver integrityObserver, float taskSpawnPointTimeout,
        AltMarker markerPrefab)
    {
        public Difficulty difficulty { get; } = difficulty;
        public PlayerProfileService playerProfileService { get; } = playerProfileService;
        public GameTaskObserver gameTaskObserver { get; } = gameTaskObserver;
        public IntegrityObserver integrityObserver { get; } = integrityObserver;
        public AltMarker markerPrefab { get; } = markerPrefab;
        public float taskSpawnPointTimeout { get; } = taskSpawnPointTimeout;
    }
}