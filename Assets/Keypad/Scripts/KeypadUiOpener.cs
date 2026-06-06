using UnityEngine;
using UnityEngine.InputSystem;

namespace NavKeypad
{
    public class KeypadUIOpener : MonoBehaviour
    {
        [Header("UI")]
        public GameObject keypadUIPanel;
        public GameObject interactPrompt;

        [Header("Player")]
        public Transform Player;

        private bool playerInRange = false;
        private bool keypadOpen = false;

        void Start()
        {
            if (keypadUIPanel != null)
            {
                keypadUIPanel.SetActive(false);
            }

            if (interactPrompt != null)
            {
                interactPrompt.SetActive(false);
            }

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        void Update()
        {
            if (!playerInRange)
            {
                return;
            }

            if (Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
            {
                ToggleKeypadUI();
            }

            if (keypadOpen && Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
            {
                CloseKeypadUI();
            }
        }

        private void ToggleKeypadUI()
        {
            if (keypadOpen)
            {
                CloseKeypadUI();
            }
            else
            {
                OpenKeypadUI();
            }
        }

        private void OpenKeypadUI()
        {
            keypadOpen = true;

            if (keypadUIPanel != null)
            {
                keypadUIPanel.SetActive(true);
            }

            if (interactPrompt != null)
            {
                interactPrompt.SetActive(false);
            }

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        private void CloseKeypadUI()
        {
            keypadOpen = false;

            if (keypadUIPanel != null)
            {
                keypadUIPanel.SetActive(false);
            }

            if (playerInRange && interactPrompt != null)
            {
                interactPrompt.SetActive(true);
            }

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (IsPlayer(other))
            {
                playerInRange = true;

                if (!keypadOpen && interactPrompt != null)
                {
                    interactPrompt.SetActive(true);
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (IsPlayer(other))
            {
                playerInRange = false;

                if (interactPrompt != null)
                {
                    interactPrompt.SetActive(false);
                }

                CloseKeypadUI();
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