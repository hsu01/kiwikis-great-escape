using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SojaExiles
{
    public class SinkToggle : MonoBehaviour
    {
        [Header("Animator")]
        public Animator sinkAnimator;

        [Header("Player")]
        public Transform Player;

        [Header("UI")]
        public GameObject interactPrompt;

        [Header("FX")]
        public GameObject sinkFX; // drag your water/splash FX object here

        [Header("Animation State Names")]
        public string sinkOnAnimation = "sink_on";
        public string sinkOffAnimation = "sink_off";

        private bool playerInRange = false;
        private bool sinkOn = false;
        private bool isAnimating = false;

        void Start()
        {
            sinkOn = false;

            if (interactPrompt != null)
            {
                interactPrompt.SetActive(false);
            }

            if (sinkFX != null)
            {
                sinkFX.SetActive(false);
            }

            if (sinkAnimator != null)
            {
                sinkAnimator.Play(sinkOffAnimation, 0, 0f);
            }
        }

        void Update()
        {
            if (!playerInRange)
            {
                return;
            }

            if (Keyboard.current != null &&
                Keyboard.current.eKey.wasPressedThisFrame &&
                !isAnimating)
            {
                ToggleSink();
            }
        }

        private void ToggleSink()
        {
            if (!sinkOn)
            {
                StartCoroutine(TurnSinkOn());
            }
            else
            {
                StartCoroutine(TurnSinkOff());
            }
        }

        private IEnumerator TurnSinkOn()
        {
            isAnimating = true;
            Debug.Log("Turning sink ON");

            sinkAnimator.Play(sinkOnAnimation, 0, 0f);
            sinkOn = true;

            // Optional: wait a little so FX starts after handle moves
            yield return new WaitForSeconds(0.25f);

            if (sinkFX != null)
            {
                sinkFX.SetActive(true);
            }

            yield return new WaitForSeconds(0.25f);
            isAnimating = false;
        }

        private IEnumerator TurnSinkOff()
        {
            isAnimating = true;
            Debug.Log("Turning sink OFF");

            sinkAnimator.Play(sinkOffAnimation, 0, 0f);
            sinkOn = false;

            if (sinkFX != null)
            {
                sinkFX.SetActive(false);
            }

            yield return new WaitForSeconds(0.5f);
            isAnimating = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!IsPlayer(other))
            {
                return;
            }

            playerInRange = true;

            if (interactPrompt != null)
            {
                interactPrompt.SetActive(true);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (!IsPlayer(other))
            {
                return;
            }

            playerInRange = false;

            if (interactPrompt != null)
            {
                interactPrompt.SetActive(false);
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
}