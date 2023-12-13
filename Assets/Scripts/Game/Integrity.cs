using System;
using Logging;
using UnityEngine;

namespace Game
{
    /// <summary>
    /// Class that represents the Integrity-Value which can be changed anytime.
    /// </summary>
    public class Integrity : MonoBehaviour
    {
        private readonly Logger m_LOG = new Logger(new LogHandler());
        private const string LOGTag = "Integrity";
        public event Action<int> OnIntegrityChanged;

        private int m_InitialIntegrityValue;

        [SerializeField] private int integrityValue = 100;

        private void Start()
        {
            m_InitialIntegrityValue = integrityValue;
        }

        /// <summary>
        /// Delivers the current Integrity-value so that it can be
        /// displayed on the UI later on.
        /// </summary>
        /// <returns>The integrity-value that represents the game score</returns>
        public int GetCurrentIntegrity()
        {
            return this.integrityValue;
        }

        /// <summary>
        /// Increments the integrityValue of the game.
        /// </summary>
        /// <param name="value">The operand for the addition</param>
        public void IncrementIntegrity(int value)
        {
            integrityValue += value;
            NotifyScoreChanged();
        }

        /// <summary>
        /// Decrements the integrityValue of the game.
        /// </summary>
        /// <param name="value">The operand for the subtraction</param>
        public void DecrementIntegrity(int value)
        {
            integrityValue -= value;
            NotifyScoreChanged();
        }

        // ReSharper disable Unity.PerformanceAnalysis
        /// <summary>
        /// This method is invoked if something changed on the integrityValue.
        /// </summary>
        private void NotifyScoreChanged()
        {
            m_LOG.Log(LOGTag, "new integrity value: " + integrityValue);
            OnIntegrityChanged?.Invoke(integrityValue);
        }

        /// <summary>
        /// Returns the current integrity value as a percentage of the initial integrity value.
        /// </summary>
        /// <returns>percentage value in interval [0;1]</returns>
        public float GetCurrentIntegrityPercentage()
        {
            return (float) integrityValue / m_InitialIntegrityValue;
        }
    }
}