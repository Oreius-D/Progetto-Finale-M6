using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// This class is responsible for managing the flow of the game, such as starting levels, handling game over, and transitioning between scenes.
public class GameFlowManager : Singleton<GameFlowManager>
{
    [Header("Scenes")]
    [SerializeField] private string mainMenuSceneName = "MainMenu"; // Name of the main menu scene

    [Header("Death Behaviour")]
    [SerializeField] private bool goToMenuOnDeath = true;
    [SerializeField] private bool reloadCurrentSceneOnDeath = false;

    //Awake is called when the script instance is being loaded
    protected override void Awake()
    {
        base.Awake();
    }

    // OnEnable is called when the object becomes enabled and active
    private void OnEnable()
    {
        GameEvents.PlayerDied += PlayerDeath; // Subscribe to the player's death event
    }

    // OnDisable is called when the behaviour becomes disabled or inactive
    private void OnDisable()
    {
        GameEvents.PlayerDied -= PlayerDeath;
    }

    // This method is called when the player dies. It handles the logic for what happens when the player dies, such as transitioning to the main menu or reloading the current scene.
    private void PlayerDeath()
    {
        if (goToMenuOnDeath && !string.IsNullOrEmpty(mainMenuSceneName))
        {
            SceneManager.LoadScene(mainMenuSceneName);
            return;
        }

        if (reloadCurrentSceneOnDeath)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    //Menu utility method to load the main menu scene
    public void LoadGameplay(string gameplaySceneName)
    {
        SceneManager.LoadScene(gameplaySceneName);
    }
}
