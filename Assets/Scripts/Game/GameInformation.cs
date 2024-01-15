using System;
using System.Security.Cryptography;
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
        private string m_GameID;
        public event Action<GameState> OnGameStateChanged;
        
        /// <summary>
        /// If the integrity is lower than or equal to this threshold, the game will count as lost
        /// </summary>
        private const int IntegrityLostThreshold = 0;

        private void Start()
        {
            // generate game id
            m_GameID = GenerateGameID();
            // invoke evaluation method, when time over or integrity change event occurs
            gameTimer.OnTimeOver += EvaluateGameState;
            integrity.OnIntegrityChanged += _ => EvaluateGameState();
            currentGameState = GameState.Ongoing;
        }

        /// <summary>
        /// Returns the generated game id for this game instance.
        /// For more information about the generation of the id, see <see cref="GenerateGameID"/> 
        /// </summary>
        /// <returns>generated id</returns>
        public string GetGameID() => m_GameID;
        
        /// <summary>
        /// Generates a id based on the hashed unix time.
        /// </summary>
        /// <returns>generated id</returns>
        private static string GenerateGameID()
        {
            // Get the current Unix timestamp
            long unixTimestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

            // Convert the Unix timestamp to a byte array
            byte[] unixBytes = BitConverter.GetBytes(unixTimestamp);

            // Hash the byte array using SHA256
            SHA256 sha256Hash = SHA256.Create();
            byte[] hashedBytes = sha256Hash.ComputeHash(unixBytes);

            // Convert the hashed bytes to a hexadecimal string
            string hashedId = BitConverter.ToString(hashedBytes).Replace("-", String.Empty);

            // Take only the first 5 characters of the hashed string
            return hashedId.Substring(0, 5);
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