using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SojaExiles
{
    public class opencloseDoorBox : MonoBehaviour
    {
        public Animator openandclose;
        public bool open;
        public Transform Player;

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
            }
        }

        private bool IsPlayer(Collider other)
        {
            // Option 1: uses your assigned Player transform
            if (Player != null && other.transform.root == Player.root)
            {
                return true;
            }

            // Option 2: works if the player is tagged "Player"
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