using System;
using PlayerController;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Tutorial
{
    /// <summary>
    /// Handles the tutorial procedure and the order of the tutorial sequences
    /// </summary>
    public class TutorialProcedure : MonoBehaviour
    {
        private TutorialState m_TutorialState = TutorialState.Basics;

        [SerializeField] private TutorialDoorController[] doorControllers;
        [SerializeField] private PlayerProfileService playerProfileService;
        [SerializeField] private GameObject game;
        
        [Header("Teleport Positions")]
        [SerializeField] private Transform basicsTeleportPosition;
        [SerializeField] private Transform interactionTeleportPosition;
        [SerializeField] private Transform hudTeleportPosition;
        [SerializeField] private Transform tasksTeleportPosition;
        [SerializeField] private Transform completedTeleportPosition;
        
        private const String FollowingSceneName = "MainMenuScene";

        private void Start()
        {
            TeleportPlayerAccordingToState();
            // disable components on start
            game.SetActive(false);
            playerProfileService.GetHUD().gameObject.SetActive(false);
        }
        
        private void Update()
        {
            // debug input to change tutorial state
            if (Input.GetKeyDown(KeyCode.N))
            {
                NextTutorialState();
            }
        }

        /// <summary>
        /// Changes the current tutorial state to the following state and executes state specific actions.
        /// Teleports the player to the position according to the new tutorial state.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void NextTutorialState()
        {
            // change to next state
            m_TutorialState = m_TutorialState switch
            {
                TutorialState.Basics => TutorialState.Interaction,
                TutorialState.Interaction => TutorialState.HUD,
                TutorialState.HUD => TutorialState.Tasks,
                TutorialState.Tasks => TutorialState.Completed,
                TutorialState.Completed => TutorialState.Exit,
                _ => throw new ArgumentOutOfRangeException()
            };

            // execute state specific actions
            switch (m_TutorialState)
            {
                case TutorialState.HUD:
                    // enable HUD and game for HUD tutorial sequence
                    game.SetActive(true);
                    playerProfileService.GetHUD().gameObject.SetActive(true);
                    break;
                case TutorialState.Completed:
                    // open all doors on completion to allow the player move freely in the tutorial scene
                    foreach (var doorController in doorControllers)
                    {
                        doorController.OpenDoor();
                    }
                    break;
                case TutorialState.Exit:
                    print("Tutorial completed!");
                    // change unity scene to main menu scene to exit tutorial scene
                    SceneManager.LoadScene(FollowingSceneName);
                    break;
            }
            
            TeleportPlayerAccordingToState();
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