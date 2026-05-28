using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class LightSwitch : MonoBehaviour
{
    [Header("Lights")]
    public Light[] lightsToToggle;
    public Renderer[] emissiveObjects;

    [Header("Emission")]
    public Color emissionOnColor = new Color(1f, 0.75f, 0.45f);
    public float emissionIntensity = 2f;

    [Header("UI")]
    public GameObject interactMessage; // drag "Press E" text object here

    [Header("State")]
    public bool lightsOn = true;

    private bool playerNearby = false;

    private void Start()
    {
        ApplyLightState();

        if (interactMessage != null)
        {
            interactMessage.SetActive(false);
        }
    }

    private void Update()
    {
        if (playerNearby && Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
        {
            ToggleLights();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;

            if (interactMessage != null)
            {
                interactMessage.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;

            if (interactMessage != null)
            {
                interactMessage.SetActive(false);
            }
        }
    }

    public void ToggleLights()
    {
        lightsOn = !lightsOn;
        Debug.Log("Light switch toggled. Lights on: " + lightsOn);
        ApplyLightState();
    }

    private void ApplyLightState()
    {
        foreach (Light lightObj in lightsToToggle)
        {
            if (lightObj != null)
            {
                lightObj.enabled = lightsOn;
            }
        }

        foreach (Renderer rend in emissiveObjects)
        {
            if (rend != null)
            {
                Material mat = rend.material;

                if (lightsOn)
                {
                    mat.EnableKeyword("_EMISSION");
                    mat.SetColor("_EmissionColor", emissionOnColor * emissionIntensity);
                }
                else
                {
                    mat.SetColor("_EmissionColor", Color.black);
                }
            }
        }
    }
}