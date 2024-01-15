using System;
using PlayerController;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Tutorial
{
    /// <summary>
    /// Handles the tutorial procedure and the order of the tutorial sequences
    /// </summary>
    public class TutorialProcedure : MonoBehaviour
    {
        private TutorialState m_TutorialState = TutorialState.Initialize;

        [SerializeField] private TutorialDoorController[] doorControllers;
        [SerializeField] private TextMeshPro[] turnAroundInfoTexts;
        [SerializeField] private PlayerProfileService playerProfileService;
        [SerializeField] private GameObject game;
        [SerializeField] private TutorialGameTimer tutorialGameTimer;
        [SerializeField] private TutorialSoundService tutorialSoundService;
        
        [Header("Teleport Positions")]
        [SerializeField] private Transform basicsTeleportPosition;
        [SerializeField] private Transform interactionTeleportPosition;
        [SerializeField] private Transform hudTeleportPosition;
        [SerializeField] private Transform tasksTeleportPosition;
        [SerializeField] private Transform completedTeleportPosition;
        
        private const String FollowingSceneName = "MainMenuScene";
        private const String TurnAroundInfoText = "Bitte umdrehen";
        
        public event Action<TutorialState> OnTutorialStateChanged;

        [SerializeField] private GameObject compass;
        [SerializeField] private GameObject waypoints;

        [Header("Evaluation Settings")]
        [SerializeField] private bool playHUDSectionOnly;

        private void Start()
        {
            // disable components on start
            game.SetActive(false);
            playerProfileService.GetHUD().gameObject.SetActive(false);
            
            // enable compass or waypoints
            handleCompassWaypointVisualization();
            
            // initialize text of turn around info texts
            foreach (var turnAroundInfoText in turnAroundInfoTexts)
            {
                turnAroundInfoText.text = TurnAroundInfoText;
            }
            

            if (playHUDSectionOnly)
            {
                // deactivate sound requests to skip room introduction sound playback
                // when updating the state
                tutorialSoundService.SetAcceptRequests(false);
                // start with HUD state, if playHUDSectionOnly is enabled
                UpdateTutorialState(TutorialState.HUD);
                // reactivate sound requests
                tutorialSoundService.SetAcceptRequests(true);
            }
            else
            {
                // start tutorial procedure
                NextTutorialState();
            }
        }

        private void handleCompassWaypointVisualization()
        {
            Destroy(playerProfileService.IsAltMarkerActive() ?  compass : waypoints);
            playerProfileService.SetIsAltMarkerActive(playerProfileService.IsAltMarkerActive());
        }

        private void Update()
        {
            // debug input to change tutorial state
            if (Input.GetKeyDown(KeyCode.N))
            {
                NextTutorialState();
            }
            // debug input to skip current sound
            if (Input.GetKeyDown(KeyCode.M))
            {
                tutorialSoundService.StopCurrentPlayBack();
            }
        }

        /// <summary>
        /// Updates the state variable and executes task specific actions.
        /// </summary>
        /// <param name="newState">new state</param>
        private void UpdateTutorialState(TutorialState newState)
        {
            m_TutorialState = newState;
            
            // check if HUD section should be played only and exit tutorial, if the HUD scene if finished
            if (playHUDSectionOnly &&  m_TutorialState != TutorialState.HUD && m_TutorialState != TutorialState.Exit)
            {
                UpdateTutorialState(TutorialState.Exit);
                return;
            }
            // execute state specific actions
            switch (m_TutorialState)
            {
                case TutorialState.HUD:
                    // enable HUD and game for HUD tutorial sequence and spawn the HUD task
                    game.SetActive(true);
                    playerProfileService.GetHUD().gameObject.SetActive(true);
                    tutorialGameTimer.SpawnOpenHUDTask();
                    break;
                case TutorialState.Tasks:
                    // spawn the tutorial task
                    tutorialGameTimer.SpawnTutorialTask();
                    break;
                case TutorialState.Completed:
                    // open all doors on completion to allow the player move freely in the tutorial scene
                    foreach (var doorController in doorControllers)
                    {
                        doorController.OpenDoor();
                    }
                    // deactivate all turn around info texts
                    foreach (var turnAroundInfoText in turnAroundInfoTexts)
                    {
                        turnAroundInfoText.gameObject.SetActive(false);
                    }
                    // allow normal task spawning
                    tutorialGameTimer.SetTaskSpawningEnabled(true);
                    break;
                case TutorialState.Exit:
                    print("Tutorial completed!");
                    // change unity scene to main menu scene to exit tutorial scene
                    SceneManager.LoadScene(FollowingSceneName);
                    break;
            }
            
            TeleportPlayerAccordingToState();
            OnTutorialStateChanged?.Invoke(m_TutorialState);
        }

        /// <summary>
        /// Changes the current tutorial state to the following state and executes state specific actions.
        /// Teleports the player to the position according to the new tutorial state.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void NextTutorialState()
        {
            // determine next state
            var nextState = m_TutorialState switch
            {
                TutorialState.Initialize => TutorialState.Basics,
                TutorialState.Basics => TutorialState.Interaction,
                TutorialState.Interaction => TutorialState.HUD,
                TutorialState.HUD => TutorialState.Tasks,
                TutorialState.Tasks => TutorialState.Completed,
                TutorialState.Completed => TutorialState.Exit,
                _ => throw new ArgumentOutOfRangeException()
            };
            
            UpdateTutorialState(nextState);
        }
        
        /// <summary>
        /// Called when a tutorial task is successful completed. Continues the tutorial procedure, if procedure is
        /// currently in <see cref="TutorialState.HUD"/> or <see cref="TutorialState.Tasks"/> state. 
        /// </summary>
        public void OnTutorialTaskSuccessful()
        {
            if (m_TutorialState is TutorialState.Tasks or TutorialState.HUD)
            {
                NextTutorialState();
            }
        }

        /// <summary>
        /// Teleports the player to the position according to the current tutorial state.
        /// </summary>
        private void TeleportPlayerAccordingToState()
        {
            // teleport player to position according to current state
            Transform teleportationTransform = m_TutorialState switch
            {
                TutorialState.Basics => basicsTeleportPosition,
                TutorialState.Interaction => interactionTeleportPosition,
                TutorialState.HUD => hudTeleportPosition,
                TutorialState.Tasks => tasksTeleportPosition,
                TutorialState.Completed => completedTeleportPosition,
                _ => playerProfileService.GetPlayerGameObject().transform
            };
            
            // set player position and rotation
            playerProfileService.GetPlayerGameObject().transform.position = teleportationTransform.position;
            playerProfileService.GetPlayerGameObject().transform.rotation = teleportationTransform.rotation;
        }
    }
}