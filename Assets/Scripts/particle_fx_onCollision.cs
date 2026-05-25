using UnityEngine;

public class ParticleFxOnCollision : MonoBehaviour
{
    [SerializeField] private GameObject smokeFxPrefab;
    [SerializeField] private float minImpactSpeed = 2.0f;

    private bool hasBroken = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (hasBroken)
        {
            return;
        }

        // Only trigger if the object hits hard enough
        if (collision.relativeVelocity.magnitude < minImpactSpeed)
        {
            return;
        }

        hasBroken = true;

        Vector3 spawnPosition = collision.GetContact(0).point;

        GameObject fx = Instantiate(smokeFxPrefab, spawnPosition, Quaternion.identity);

        ParticleSystem[] particleSystems = fx.GetComponentsInChildren<ParticleSystem>(); // add particle systems to the smoke prefab and assign them in the inspector
        foreach (ParticleSystem ps in particleSystems)
        {
            ps.Play();
        }

        Destroy(fx, 3f);

        // Later replace this with a broken version of the object if needed
        Destroy(gameObject);
    }
}