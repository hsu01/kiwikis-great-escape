using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SojaExiles
{
    public class Drawer_Pull_X_Box : MonoBehaviour
    {
        public Animator pull_01;
        public bool open;
        public Transform Player;

        // Drag your UI prompt here
        public GameObject interactPrompt;

        private bool playerInRange = false;
        private bool isAnimating = false;

        void Start()
        {
            open = false;

            if (interactPrompt != null)
            {
                interactPrompt.SetActive(false);
            }
        }

        void Update()
        {
            if (playerInRange &&
                Keyboard.current != null &&
                Keyboard.current.eKey.wasPressedThisFrame &&
                !isAnimating)
            {
                if (!open)
                {
                    StartCoroutine(opening());
                }
                else
                {
                    StartCoroutine(closing());
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("Entered drawer trigger: " + other.name);

            if (IsPlayer(other))
            {
                Debug.Log("Turning drawer prompt ON");

                playerInRange = true;

                if (interactPrompt != null)
                {
                    interactPrompt.SetActive(true);
                    Debug.Log("Prompt activeSelf: " + interactPrompt.activeSelf);
                    Debug.Log("Prompt activeInHierarchy: " + interactPrompt.activeInHierarchy);
                }
                else
                {
                    Debug.LogWarning("Interact Prompt is not assigned on drawer.");
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            Debug.Log("Exited drawer trigger: " + other.name);

            if (IsPlayer(other))
            {
                Debug.Log("Turning drawer prompt OFF");

                playerInRange = false;

                if (interactPrompt != null)
                {
                    interactPrompt.SetActive(false);
                }
            }
        }

        private bool IsPlayer(Collider other)
        {
            // Works if the player collider is on a child object
            if (Player != null && other.transform.root == Player.root)
            {
                return true;
            }

            // Backup check if your player is tagged Player
            return other.CompareTag("Player");
        }

        IEnumerator opening()
        {
            isAnimating = true;
            print("you are opening the drawer");

            pull_01.Play("openpull_01");
            open = true;

            yield return new WaitForSeconds(.5f);
            isAnimating = false;
        }

        IEnumerator closing()
        {
            isAnimating = true;
            print("you are closing the drawer");

            pull_01.Play("closepush_01");
            open = false;

            yield return new WaitForSeconds(.5f);
            isAnimating = false;
        }
    }
}