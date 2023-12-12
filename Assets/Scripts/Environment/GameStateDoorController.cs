using Game;
using UnityEngine;

namespace Environment
{
    public class GameStateDoorController : DoorController
    {

        [SerializeField] private GameInformation gameInformation;
        /// <summary>
        /// Determines the game state, for which the door can be opened
        /// </summary>
        [SerializeField] private GameState allowOpenOnState;
        
        /// <summary>
        /// Checks if the current game state equals the state set in <see cref="allowOpenOnState"/>.
        /// If this state is active, the base function will be invoked, to open the door only, if the player is nearby.
        /// </summary>
        protected override void Update ()
        {
            if (gameInformation.currentGameState == allowOpenOnState)
            {
                base.Update();
            }
        }
    }
}