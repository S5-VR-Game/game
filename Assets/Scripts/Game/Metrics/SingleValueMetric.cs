namespace Game.Metrics
{
    /// <summary>
    /// Represents a metric which can only have one value.
    /// </summary>
    public enum SingleValueMetric
    {
        GameID, 
        GameWon, // whether the game was won or lost
        FinalIntegrity, // final integrity of the ship, when the game is over
        IntegrityIncreaseSum, // sum of all additions to the integrity of the ship
        IntegrityDecreaseSum, // sum of all subtractions from the integrity of the ship
        WalkedDistance, // sum of the walked distance of the player
        Difficulty, // difficulty value of the game
        HudTaskDescriptionOpenCount, // how often the task description was opened on hud
        FailedTasksCount, // how many tasks were failed
        SuccessfulTasksCount, // how many tasks were successful completed
    }
}