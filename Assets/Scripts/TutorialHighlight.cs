using UnityEngine;

/// <summary>
/// Pulses an emissive glow on a Renderer so the player can see which object they
/// should interact with next. Enable/disable this component (or call
/// SetHighlighted) to turn the glow on and off; TutorialManager drives it per step.
///
/// Put this on the object you want to glow (e.g. the light switch), and assign its
/// Renderer. If you leave targetRenderer empty it grabs the Renderer on this object.
/// </summary>
[DisallowMultipleComponent]
public class TutorialHighlight : MonoBehaviour
{
    [Header("Target")]
    [Tooltip("Renderer to glow. Defaults to a Renderer on this GameObject.")]
    public Renderer targetRenderer;

    [Header("Glow")]
    public Color glowColor = new Color(1f, 0.9f, 0.3f);
    [Tooltip("Peak emission intensity at the top of the pulse.")]
    public float maxIntensity = 3f;
    [Tooltip("Pulses per second.")]
    public float pulseSpeed = 2f;

    [Header("State")]
    [Tooltip("Start glowing on Awake. TutorialManager usually controls this instead.")]
    public bool highlightOnStart = false;

    private Material material;          // instanced material we drive
    private Color originalEmission;
    private bool hadEmission;
    private bool isHighlighted;

    private void Awake()
    {
        if (targetRenderer == null)
        {
            targetRenderer = GetComponent<Renderer>();
        }

        if (targetRenderer != null)
        {
            // .material gives us a per-instance copy so we don't edit the shared asset.
            material = targetRenderer.material;
            hadEmission = material.IsKeywordEnabled("_EMISSION");
            originalEmission = material.HasProperty("_EmissionColor")
                ? material.GetColor("_EmissionColor")
                : Color.black;
        }

        SetHighlighted(highlightOnStart);
    }

    /// <summary>Turn the glow on or off.</summary>
    public void SetHighlighted(bool on)
    {
        isHighlighted = on;
        if (material == null)
        {
            return;
        }

        if (!on)
        {
            RestoreEmission();
        }
    }

    private void Update()
    {
        if (!isHighlighted || material == null)
        {
            return;
        }

        // 0..1 sine pulse.
        float t = (Mathf.Sin(Time.time * pulseSpeed * Mathf.PI * 2f) + 1f) * 0.5f;
        material.EnableKeyword("_EMISSION");
        material.SetColor("_EmissionColor", glowColor * (t * maxIntensity));
    }

    private void RestoreEmission()
    {
        if (hadEmission)
        {
            material.EnableKeyword("_EMISSION");
            material.SetColor("_EmissionColor", originalEmission);
        }
        else
        {
            material.SetColor("_EmissionColor", Color.black);
            material.DisableKeyword("_EMISSION");
        }
    }

    private void OnDisable()
    {
        // Make sure we don't leave the object glowing if the component is disabled.
        if (material != null)
        {
            RestoreEmission();
        }
    }
}
