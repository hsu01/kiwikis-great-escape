using System.Collections;
using UnityEngine;
using TMPro;

/// <summary>
/// Drives the opening tutorial: the cat must first turn on the lights, then open
/// the drawer. Steps are completed in a strict order and the current objective
/// is shown on an on-screen text panel.
///
/// Wiring (in the Inspector):
///   - LightSwitch.onLightsTurnedOn  -> TutorialManager.OnLightsTurnedOn
///   - Drawer_Pull_X.onOpened        -> TutorialManager.OnDrawerOpened
/// </summary>
public class TutorialManager : MonoBehaviour
{
    public static TutorialManager Instance { get; private set; }

    public enum Step
    {
        TurnOnLights,
        OpenDrawer,
        Complete
    }

    [Header("UI")]
    [Tooltip("On-screen text that shows the current objective.")]
    public TMP_Text instructionText;
    [Tooltip("Optional panel to hide once the tutorial is done. Defaults to the instruction text's object.")]
    public GameObject instructionPanel;
    [Tooltip("Seconds the message stays on screen after the drawer is opened, before hiding (<= 0 keeps it forever).")]
    public float completeMessageDuration = 3f;

    [Header("Step Messages")]
    [TextArea] public string turnOnLightsMessage = "It's dark in here... find the light switch and press E to turn on the lights.";
    [TextArea] public string openDrawerMessage = "Now find the drawer and press E to open it.";

    [Header("Highlights")]
    [Tooltip("Glows during the TurnOnLights step. Put a TutorialHighlight on the light switch.")]
    public TutorialHighlight lightSwitchHighlight;
    [Tooltip("Glows during the OpenDrawer step. Put a TutorialHighlight on the drawer.")]
    public TutorialHighlight drawerHighlight;

    [Header("State")]
    [SerializeField] private Step currentStep = Step.TurnOnLights;

    public Step CurrentStep => currentStep;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        currentStep = Step.TurnOnLights;
        UpdateInstruction();
    }

    /// <summary>Hook this up to LightSwitch.onLightsTurnedOn.</summary>
    public void OnLightsTurnedOn()
    {
        if (currentStep != Step.TurnOnLights)
        {
            return;
        }

        currentStep = Step.OpenDrawer;
        Debug.Log("[Tutorial] Lights on. Next: open the drawer.");
        UpdateInstruction();
    }

    /// <summary>Hook this up to Drawer_Pull_X.onOpened.</summary>
    public void OnDrawerOpened()
    {
        if (currentStep != Step.OpenDrawer)
        {
            return;
        }

        currentStep = Step.Complete;
        Debug.Log("[Tutorial] Drawer opened. Tutorial complete.");

        // Stop the drawer glowing now that it's open (independent of the message timing).
        UpdateHighlights();

        // Leave the "open the drawer" message on screen, then hide it after the
        // delay (do NOT swap the text).
        if (completeMessageDuration > 0f)
        {
            StartCoroutine(HideAfterDelay(completeMessageDuration));
        }
        else
        {
            UpdateInstruction();
        }
    }

    private IEnumerator HideAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        GameObject toHide = instructionPanel != null
            ? instructionPanel
            : (instructionText != null ? instructionText.gameObject : null);

        if (toHide != null)
        {
            toHide.SetActive(false);
        }
    }

    private void UpdateInstruction()
    {
        if (instructionText == null)
        {
            return;
        }

        switch (currentStep)
        {
            case Step.TurnOnLights:
                instructionText.text = turnOnLightsMessage;
                break;
            case Step.OpenDrawer:
                instructionText.text = openDrawerMessage;
                break;
            case Step.Complete:
                // Message stays as-is, then HideAfterDelay removes it.
                break;
        }

        UpdateHighlights();
    }

    /// <summary>Glow only the object relevant to the current step.</summary>
    private void UpdateHighlights()
    {
        if (lightSwitchHighlight != null)
        {
            lightSwitchHighlight.SetHighlighted(currentStep == Step.TurnOnLights);
        }
        if (drawerHighlight != null)
        {
            drawerHighlight.SetHighlighted(currentStep == Step.OpenDrawer);
        }
    }
}
