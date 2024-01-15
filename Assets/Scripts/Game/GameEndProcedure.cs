using Evaluation;
using Game.Metrics;
using Game.Observer;
using Game.Tasks.GameExit;
using PlayerController;
using UnityEngine;

namespace Game
{
    /// <summary>
    /// Listens to game state changes and initiates the game exit procedure, when the game is won.
    /// Additionally this class removes all currently active tasks and pauses the game timer, when the game is over.
    /// </summary>
    public class GameEndProcedure : MonoBehaviour
    {
        [SerializeField] private GameInformation gameInformation;
        [SerializeField] private GameTaskObserver gameTaskObserver;
        [SerializeField] private GameExitTaskFactory gameExitTaskFactory;
        [SerializeField] private Difficulty difficulty;
        [SerializeField] private IntegrityObserver integrityObserver;
        [SerializeField] private GameTimer gameTimer;
        [SerializeField] private EvaluationDataWrapper evaluationDataWrapper;
        [SerializeField] private PlayerProfileService playerProfileService;
        [SerializeField] private MetricCollector metricCollector;
        [SerializeField] private AltMarker markerPrefab;

        private const string GameWonText = "Du hast die Raumstation erfolgreich instand gehalten!\n" +
                                           "Begib dich jezt zum Ausgangspunkt, um vom Raumschiff abgeholt zu werden " +
                                           "und das Spiel zu beenden.\nDer Ausgangspunkt ist als {0} markiert.";

        private const string NormalMarker = "Marker auf der Navigationsleiste oben";
        private const string AltMarker = "grüner Punkt im Sichtfeld";

        private void Start()
        {
            gameInformation.OnGameStateChanged += OnGameStateChanged;
            gameExitTaskFactory.Initialize(new FactoryInitializationData(difficulty, playerProfileService,
                gameTaskObserver, integrityObserver, 0, metricCollector, markerPrefab));
        }

        private void OnGameStateChanged(GameState gameState)
        {
            // remove all currently ongoing tasks and stop timer, when the game is over
            if (gameState != GameState.Ongoing)
            {
                gameTaskObserver.GetActiveTasks().ForEach(task => task.DestroyTask());
                gameTimer.PauseTimer();
            }

            // spawn a marker at the game exit position, when the game is won
            if (gameState == GameState.GameWon)
            {
                string infoText;
                // format string according to the currently active marker type
                if (playerProfileService.IsAltMarkerActive())
                {
                    infoText = string.Format(GameWonText, AltMarker);
                }
                else
                {
                    infoText = string.Format(GameWonText, NormalMarker);
                }

                // spawn game exit task
                gameExitTaskFactory.TrySpawnTask(evaluationDataWrapper);
                // show game won info text on hud
                playerProfileService.GetHUD().uiTextBox.DismissText();
                playerProfileService.GetHUD().uiTextBox.DisplayText(infoText);
                playerProfileService.GetHUD().uiTextBox.ToggleShow();
            }
        }
    }
}