using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    [Header("Scene To Load")]
    public string gameSceneName = "MainScene";

    [Header("UI Panels")]
    public GameObject instructionsPanel;

    public void StartGame()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    public void OpenInstructions()
    {
        instructionsPanel.SetActive(true);
    }

    public void CloseInstructions()
    {
        instructionsPanel.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();

        // This only helps you see it working inside the Unity Editor.
        Debug.Log("Quit game");
    }
}