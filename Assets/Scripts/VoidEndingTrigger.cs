using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class VoidEndingTrigger : MonoBehaviour
{
    [Header("Ending UI")]
    public GameObject larryEndingPanel;

    [Header("Player")]
    public Transform player;

    [Header("Timing")]
    public float delayBeforeEnding = 1.5f;

    private bool endingStarted = false;
    private bool endingShown = false;

    private void Start()
    {
        if (larryEndingPanel != null)
        {
            larryEndingPanel.SetActive(false);
        }
    }

    private void Update()
    {
        if (!endingShown)
        {
            return;
        }

        if (Keyboard.current != null && Keyboard.current.rKey.wasPressedThisFrame)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            Application.Quit();

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (endingStarted)
        {
            return;
        }

        if (IsPlayer(other))
        {
            StartCoroutine(ShowEndingAfterDelay());
        }
    }

    private bool IsPlayer(Collider other)
    {
        if (player != null && other.transform.root == player.root)
        {
            return true;
        }

        return other.CompareTag("Player");
    }

    private IEnumerator ShowEndingAfterDelay()
    {
        endingStarted = true;

        yield return new WaitForSeconds(delayBeforeEnding);

        if (larryEndingPanel != null)
        {
            larryEndingPanel.SetActive(true);
        }

        endingShown = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Time.timeScale = 0f;
    }
}