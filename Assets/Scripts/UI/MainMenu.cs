using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 1f;
    }

    // function to load level scene
    public void PlayGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level");
    }

    // function to quit the game
    public void QuitGame()
    {
        Application.Quit();
    }

    // function to stop game from running in editor
#if UNITY_EDITOR
    public void StopGame()
    {
        UnityEditor.EditorApplication.isPlaying = false;
    }
#endif
}
