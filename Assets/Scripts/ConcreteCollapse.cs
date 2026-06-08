using System.Collections;
using UnityEngine;

public class ConcreteCollapse : MonoBehaviour
{
    [Header("Concrete To Remove")]
    public GameObject concreteObject;

    [Header("Player")]
    public Transform Player;

    [Header("Timing")]
    public float delayBeforeCollapse = 0.2f;

    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (triggered)
        {
            return;
        }

        if (IsPlayer(other))
        {
            StartCoroutine(CollapseConcrete());
        }
    }

    private IEnumerator CollapseConcrete()
    {
        triggered = true;

        yield return new WaitForSeconds(delayBeforeCollapse);

        if (concreteObject != null)
        {
            concreteObject.SetActive(false);
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
}