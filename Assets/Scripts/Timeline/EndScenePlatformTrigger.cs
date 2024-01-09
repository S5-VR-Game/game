using Game;
using UnityEngine;

namespace Timeline
{
    public class EndScenePlatformCollision : MonoBehaviour
    {
        [SerializeField] private TimelineManager timelineManager;

        [SerializeField] private GameInformation gameInformation;

        private void OnTriggerEnter(Collider collision)
        {
            if (collision.gameObject.CompareTag("Player") && gameInformation.currentGameState == GameState.GameWon)
            {
                timelineManager.PlayEndSceneWin();
            }
        }
    }
}
