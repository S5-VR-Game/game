using System;
using UnityEngine;

namespace Game
{
    
    /// <summary>
    /// Provides access to game related information and values such as the game difficulty, or the current
    /// <see cref="GameState"/>
    /// </summary>
    public class GameInformation : MonoBehaviour
    {
        [SerializeField] private GameTimer gameTimer;
        [SerializeField] private Integrity integrity;
        [SerializeField] private Difficulty difficulty;

        public GameState currentGameState;
        public event Action<GameState> OnGameStateChanged;

        /// <summary>
        /// If the integrity is lower than or equal to this threshold, the game will count as lost
        /// </summary>
        private const int IntegrityLostThreshold = 0;

        private void Start()
        {
            // invoke evaluation method, when time over or integrity change event occurs
            gameTimer.OnTimeOver += EvaluateGameState;
            integrity.OnIntegrityChanged += _ => EvaluateGameState();
            currentGameState = GameState.Ongoing;
        }

        /// <summary>
        /// Evaluates the current game state and updates <see cref="currentGameState"/> with the new evaluated value.
        /// In addition the <see cref="OnGameStateChanged"/> event will be invoked to notify all subscribed listeners.
        /// </summary>
        /// <returns>evaluated game state</returns>
        private void EvaluateGameState()
        {
            if (integrity.GetCurrentIntegrity() <= IntegrityLostThreshold)
            {
                // if integrity lost threshold reached, evaluate to game lost
                currentGameState = GameState.GameLost;
            }
            else if (gameTimer.timeOver)
            {
                // if time over, evaluate depending on integrity value
                currentGameState = integrity.GetCurrentIntegrity() > IntegrityLostThreshold ? GameState.GameWon : GameState.GameLost;
            }
            else
            { 
                // time is not over, evaluate to ongoing
                currentGameState = GameState.Ongoing;
            }
            
            // invoke game state changed event in order to notify subscribed listeners
            OnGameStateChanged?.Invoke(currentGameState);
        }

        /// <summary>
        /// Returns the difficulty object that is set for the game currently
        /// </summary>
        /// <returns>current game difficulty</returns>
        public Difficulty GetCurrentDifficulty()
        {
            return difficulty;
        }
    }
}