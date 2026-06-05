using UnityEngine;

public class SinkFillPlane : MonoBehaviour
{
    [Header("Local Water Height")]
    public float emptyLocalY = 0.02f;
    public float fullLocalY = 0.18f;

    [Header("Fill Settings")]
    public float fillSpeed = 0.15f;
    public bool filling = false;

    private void Start()
    {
        Vector3 pos = transform.localPosition;
        pos.y = emptyLocalY;
        transform.localPosition = pos;
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

        Vector3 pos = transform.localPosition;
        pos.y = emptyLocalY;
        transform.localPosition = pos;
    }
}