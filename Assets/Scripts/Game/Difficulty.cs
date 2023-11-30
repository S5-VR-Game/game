using System;
using UnityEngine;

namespace Game
{
    
    /// <summary>
    /// Separates the continuous difficulty value into three intervals of the same size.
    /// Easy &lt; Medium &lt; Hard
    /// </summary>
    public enum SeparatedDifficulty
    {
        Easy, Medium, Hard
    }
    
    /// <summary>
    /// Stores the current game difficulty value as well as the minimum and maximum value.
    /// Enables to obtain the <see cref="SeparatedDifficulty"/> value out of this data and allows to change the
    /// difficulty value within the valid range.
    /// </summary>
    public class Difficulty : MonoBehaviour
    {
        private const float MinValue = 0f;
        private const float MaxValue = 1f;
        private float m_DifficultyValue;

        [Range(MinValue, MaxValue)]
        public float initialDifficultyValue = 0.5f;

        private void Start()
        {
            m_DifficultyValue = initialDifficultyValue;
        }

        public float GetMaxValue() => MaxValue;
        
        public float GetMinValue() => MinValue;
        
        public float GetValue() => m_DifficultyValue;

        /// <summary>
        /// Checks if the given new value is within the <see cref="MinValue"/> and <see cref="MaxValue"/> range and
        /// updates the current difficulty if the value is valid.
        /// </summary>
        /// <param name="newDifficulty">value to which the difficulty should be changed</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// will be thrown if the new difficulty is not valid according to the minimum and maximum value
        /// </exception>
        public void SetValue(float newDifficulty)
        {
            if (newDifficulty is < MinValue or > MaxValue)
            {
                throw new ArgumentOutOfRangeException(nameof(newDifficulty));
            }
            m_DifficultyValue = newDifficulty;
        }

        /// <summary>
        /// Determines the separated difficulty
        /// </summary>
        /// <returns>the current separated difficulty value</returns>
        public SeparatedDifficulty GetSeparatedDifficulty()
        {
            var interval = (MaxValue - MinValue) / 3;
            if (m_DifficultyValue <= MinValue + interval)
            {
                return SeparatedDifficulty.Easy;
            }
            if (m_DifficultyValue <= MinValue + 2*interval)
            {
                return SeparatedDifficulty.Medium;
            }

            return SeparatedDifficulty.Hard;
        }
    }
}