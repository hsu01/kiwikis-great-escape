using UnityEngine;

public class SpawnNoteOnDestruct : MonoBehaviour
{
    [Header("Note")]
    public GameObject notePrefab;
    public Transform spawnPoint;

    [Header("Pop Out Force")]
    public float upwardForce = 2f;
    public float outwardForce = 1f;

    private bool spawned = false;

    public void SpawnNote()
    {
        Debug.Log("SpawnNote was called!");

        if (spawned)
        {
            Debug.Log("Note already spawned.");
            return;
        }

        spawned = true;

        if (notePrefab == null)
        {
            Debug.LogWarning("Note prefab is not assigned.");
            return;
        }

        Vector3 spawnPosition = transform.position;

        if (spawnPoint != null)
        {
            spawnPosition = spawnPoint.position;
        }

        GameObject note = Instantiate(notePrefab, spawnPosition, Quaternion.identity);
        note.SetActive(true);

        Debug.Log("Note spawned at: " + spawnPosition);

        Rigidbody rb = note.GetComponent<Rigidbody>();

        if (rb != null)
        {
            Vector3 randomOutward = new Vector3(
                Random.Range(-1f, 1f),
                0f,
                Random.Range(-1f, 1f)
            ).normalized;

            Vector3 force = Vector3.up * upwardForce + randomOutward * outwardForce;

            rb.AddForce(force, ForceMode.Impulse);
            rb.AddTorque(Random.insideUnitSphere * 2f, ForceMode.Impulse);
        }
    }
}