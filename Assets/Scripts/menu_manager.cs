using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    [Header("Scene To Load")]
    public string gameSceneName = "MainScene";

    public void StartGame()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    public void QuitGame()
    {
        Application.Quit();

        // This only helps you see it working inside the Unity Editor.
        Debug.Log("Quit game");
    }
}