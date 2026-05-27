using UnityEngine;
using UnityEngine.InputSystem;

public class LightSwitch : MonoBehaviour
{
    public Light[] lightsToToggle;
    public Renderer[] emissiveObjects;

    public Color emissionOnColor = new Color(1f, 0.75f, 0.45f);
    public float emissionIntensity = 2f;

    public bool lightsOn = true;

    private void Start()
    {
        ApplyLightState();
    }

    private void Update()
    {
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            Debug.Log("Mouse clicked");

            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Debug.Log("Hit object: " + hit.collider.gameObject.name);

                if (hit.collider.transform == transform || hit.collider.transform.IsChildOf(transform))
                {
                    ToggleLights();
                }
            }
            else
            {
                Debug.Log("Raycast hit nothing");
            }
        }
    }

    public void ToggleLights()
    {
        lightsOn = !lightsOn;
        Debug.Log("Light switch clicked. Lights on: " + lightsOn);
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