using UnityEngine;

public class SinkFillPlane : MonoBehaviour
{
    [Header("Local Water Height")]
    public float emptyLocalY = 0.02f;
    public float fullLocalY = 0.18f;

    [Header("Fill Settings")]
    public float fillSpeed = 0.15f;
    public bool filling = false;

    [Header("Floating Note")]
    public GameObject noteObject;
    public float noteRevealLocalY = 0.10f;
    public float noteOffsetAboveWater = 0.02f;

    private bool noteRevealed = false;

    private void Start()
    {
        Vector3 pos = transform.localPosition;
        pos.y = emptyLocalY;
        transform.localPosition = pos;

        if (noteObject != null)
        {
            noteObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (!filling)
            return;

        Vector3 pos = transform.localPosition;

        pos.y = Mathf.MoveTowards(
            pos.y,
            fullLocalY,
            fillSpeed * Time.deltaTime
        );

        transform.localPosition = pos;

        HandleNoteReveal();
    }

    private void HandleNoteReveal()
    {
        if (noteObject == null)
            return;

        // Reveal the note once the water reaches this height
        if (!noteRevealed && transform.localPosition.y >= noteRevealLocalY)
        {
            noteRevealed = true;
            noteObject.SetActive(true);
        }

        // If revealed, keep note floating above the water
        if (noteRevealed)
        {
            Vector3 notePos = noteObject.transform.position;
            notePos.y = transform.position.y + noteOffsetAboveWater;
            noteObject.transform.position = notePos;
        }
    }

    public void StartFilling()
    {
        filling = true;
    }

    public void StopFilling()
    {
        filling = false;
    }

    public void Drain()
    {
        filling = false;
        noteRevealed = false;

        Vector3 pos = transform.localPosition;
        pos.y = emptyLocalY;
        transform.localPosition = pos;

        if (noteObject != null)
        {
            noteObject.SetActive(false);
        }
    }
}