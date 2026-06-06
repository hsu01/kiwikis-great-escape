using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SojaExiles
{
    public class openclosedoorlock : MonoBehaviour
    {
        public Animator openandclose;
        public bool open;
        public Transform Player;

        [Header("Lock Settings")]
        public bool isLocked = true;

        [Header("UI")]
        public GameObject interactPrompt;
        public GameObject lockedPrompt; // optional: "Door is locked"

        private bool playerInRange = false;
        private bool isAnimating = false;

        void Start()
        {
            open = false;

            if (interactPrompt != null)
            {
                interactPrompt.SetActive(false);
            }

            if (lockedPrompt != null)
            {
                lockedPrompt.SetActive(false);
            }
        }

        void Update()
        {
            if (playerInRange &&
                Keyboard.current != null &&
                Keyboard.current.eKey.wasPressedThisFrame &&
                !isAnimating)
            {
                if (isLocked)
                {
                    Debug.Log("Door is locked.");

                    if (lockedPrompt != null)
                    {
                        StartCoroutine(ShowLockedPrompt());
                    }

                    return;
                }

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

        // This is what the keypad will call
        public void UnlockDoor()
        {
            isLocked = false;
            Debug.Log("Door unlocked!");
        }

        // Optional, in case you ever want to lock it again
        public void LockDoor()
        {
            isLocked = true;
            Debug.Log("Door locked!");
        }

        private IEnumerator ShowLockedPrompt()
        {
            lockedPrompt.SetActive(true);
            yield return new WaitForSeconds(1.5f);
            lockedPrompt.SetActive(false);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (IsPlayer(other))
            {
                playerInRange = true;

                if (interactPrompt != null)
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

                if (lockedPrompt != null)
                {
                    lockedPrompt.SetActive(false);
                }
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

        IEnumerator opening()
        {
            isAnimating = true;
            print("you are opening the door");

            openandclose.Play("Opening");
            open = true;

            yield return new WaitForSeconds(.5f);
            isAnimating = false;
        }

        IEnumerator closing()
        {
            isAnimating = true;
            print("you are closing the door");

            openandclose.Play("Closing");
            open = false;

            yield return new WaitForSeconds(.5f);
            isAnimating = false;
        }
    }
}