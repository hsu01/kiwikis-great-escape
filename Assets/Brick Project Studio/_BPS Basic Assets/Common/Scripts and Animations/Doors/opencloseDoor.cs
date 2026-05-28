using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SojaExiles
{
    public class opencloseDoor : MonoBehaviour
    {
        public Animator openandclose;
        public bool open;
        public Transform Player;

        public float interactDistance = 3f;
        public GameObject interactMessage; // drag UI text object here

        private bool isAnimating = false;

        void Start()
        {
            open = false;

            if (interactMessage != null)
            {
                interactMessage.SetActive(false);
            }
        }

        void Update()
        {
            if (Player == null)
                return;

            float dist = Vector3.Distance(Player.position, transform.position);
            bool playerNearby = dist <= interactDistance;

            // Show/hide UI message based on distance
            if (interactMessage != null)
            {
                interactMessage.SetActive(playerNearby);
            }

            if (playerNearby &&
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

        IEnumerator opening()
        {
            isAnimating = true;
            Debug.Log("you are opening the door");

            openandclose.Play("Opening");
            open = true;

            yield return new WaitForSeconds(0.5f);
            isAnimating = false;
        }

        IEnumerator closing()
        {
            isAnimating = true;
            Debug.Log("you are closing the door");

            openandclose.Play("Closing");
            open = false;

            yield return new WaitForSeconds(0.5f);
            isAnimating = false;
        }
    }
}