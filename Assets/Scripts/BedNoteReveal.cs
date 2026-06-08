using UnityEngine;

public class BedNoteReveal : MonoBehaviour
{
    [Header("Note")]
    public GameObject noteObject;

    [Header("Player")]
    public Transform Player;

    [Header("Settings")]
    public bool revealOnlyOnce = true;

    private bool hasRevealed = false;

    private void Start()
    {
        if (noteObject != null)
        {
            noteObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (revealOnlyOnce && hasRevealed)
        {
            return;
        }

        if (IsPlayer(other))
        {
            RevealNote();
        }
    }

    private bool IsPlayer(Collider other)
    {
        if (Player != null && other.transform.root == Player.root)
        {
            return true;
        }

        return other.CompareTag("Player");
    }

    private void RevealNote()
    {
        hasRevealed = true;

        if (noteObject != null)
        {
            noteObject.SetActive(true);
        }

        Debug.Log("Bed note revealed!");
    }
}